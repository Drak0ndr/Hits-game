using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    private const int ViewRadius = 9;
    public Dictionary<Vector2Int, ChunkData> ChunkDatas = new Dictionary<Vector2Int, ChunkData>();
    public ChunkRenderer chunkPrefab;
    public MeshRenderer taigaFullTree;
    public MeshRenderer taigaSmallTree;
    public MeshRenderer polemonium;
    public TerrainGenerator Generator;
    private FastNoiseLite precipitation = new FastNoiseLite();
    private FastNoiseLite temperature = new FastNoiseLite();
    private Camera mainCamera;
    private Vector2Int currentPlayerChunk = new Vector2Int(-18,-35);
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        precipitation.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        precipitation.SetFrequency(0.005f);
        precipitation.SetFractalType(FastNoiseLite.FractalType.FBm);
        precipitation.SetFractalOctaves(10);
        precipitation.SetSeed(2);
        precipitation.SetFractalGain(0.6f);
        temperature.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        temperature.SetFrequency(0.001f);
        temperature.SetFractalType(FastNoiseLite.FractalType.FBm);
        temperature.SetFractalOctaves(10);
        temperature.SetSeed(2);
        temperature.SetFractalGain(0.6f);
        StartCoroutine(Generate(false));
    }

    private IEnumerator Generate(bool wait)
    {
        int loadRadius = ViewRadius+1;

        for (int x = currentPlayerChunk.x - loadRadius; x <= currentPlayerChunk.x + loadRadius; x++)
        {
            for (int z = currentPlayerChunk.y - loadRadius; z <= currentPlayerChunk.y + loadRadius; z++)
            {
                var chunkPosition = new Vector2Int(x, z);
                if (ChunkDatas.ContainsKey(chunkPosition)) continue;
                LoadChunkAt(chunkPosition);

                if (wait) yield return null;
            }
        }

        for (int x = currentPlayerChunk.x - ViewRadius; x <= currentPlayerChunk.x + ViewRadius; x++)
        {
            for (int z = currentPlayerChunk.y - ViewRadius; z <= currentPlayerChunk.y + ViewRadius; z++)
            {
                var chunkPosition = new Vector2Int(x, z);
                ChunkData chunkData = ChunkDatas[chunkPosition];

                if (chunkData.Renderer != null) continue;

                SpawnChunkRenderer(chunkData);

                if (wait) yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }

    [ContextMenu("Regenerate World")]
    public void Regenerate()
    {
        Generator.Init();
        foreach (var chunkData in ChunkDatas)
        {
            Destroy(chunkData.Value.Renderer.gameObject);
        }

        ChunkDatas.Clear();

        StartCoroutine(Generate(false));
    }

    private void LoadChunkAt(Vector2Int chunkPosition)
    {

        int x = chunkPosition.x;
        int z = chunkPosition.y;
        float xPos = x * ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale;
        float zPos = z * ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale;
        float chunkCenter = ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale / 2f;
        ChunkData chunkData = new ChunkData();
        chunkData.Blocks = Generator.GenerateTerrain(xPos, zPos);
        chunkData.ChunkPositoin = new Vector2Int(x, z);
        chunkData.heightMap = Generator.GenerateHightMap(xPos,zPos);
        if (xPos == -144 && zPos == -288) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -144 && zPos == -296) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -136 && zPos == -296) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -128 && zPos == -296) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -120 && zPos == -304) {
            for (int cy = 61; cy < 61+33; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -128 && zPos == -304) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -136 && zPos == -304) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        if (xPos == -144 && zPos == -304) {
            for (int cy = 61; cy < 78; cy++) {
                for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                    for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }

        if (xPos >= 56-56 && xPos<= 56+56 && zPos >= 472 - 56 && zPos <= 472 + 56) {
            
            for (int cx = 0; cx < ChunkRenderer.ChunkWidth; cx++) {
                for (int cz = 0; cz < ChunkRenderer.ChunkWidth; cz++) {
                    int r = (int)(Math.Pow(Math.Pow(xPos+cx*ChunkRenderer.BlockScale - 56,2) + Math.Pow(zPos+cz*ChunkRenderer.BlockScale - 472,2), 0.5) * 1.35);
                    for (int cy = 1 + r; cy < 150; cy++) {
                        chunkData.Blocks[cx,cy,cz] = BlockType.Air;
                    }
                }
            }
        }
        ChunkDatas.Add(new Vector2Int(x, z), chunkData);
        
    }

    private void SpawnChunkRenderer(ChunkData chunkData) {
        float xPos = chunkData.ChunkPositoin.x * ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale;
        float zPos = chunkData.ChunkPositoin.y * ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale;

        var chunk = Instantiate(chunkPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);
        if (xPos >= 56-48 && xPos<= 56+48 && zPos >= 472 - 48 && zPos <= 472 + 48) {

        } else {
            var treeHeight = Generator.GetHeight(xPos, zPos) * ChunkRenderer.BlockScale;
            var precipitationLevel = (precipitation.GetNoise(xPos, zPos) + 1) * 200;
            var temperatureLevel = temperature.GetNoise(xPos, zPos) * 30;
            var temp = temperatureLevel + Mathf.Min(0, 16 - treeHeight) * 0.25;
            if (temp >= -5 && temp <= 5 && precipitationLevel >= 50 && precipitationLevel <= 300)
            {
                float bestConditions = (precipitation.GetNoise(xPos, zPos) + 1) * 200 + temperature.GetNoise(xPos, zPos) * 30;
                float bestTreePosX = xPos;
                float bestTreePosZ = zPos;
                float bestFlowerCond = Math.Abs(100 - (precipitation.GetNoise(xPos, zPos) + 1) * 200);
                float bestFlPosX = xPos;
                float bestFlPosZ = zPos;
                float FlHeight = Generator.GetHeight(xPos, zPos) * ChunkRenderer.BlockScale;
                for (float i = xPos - ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale / 3f; i < xPos + ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale / 3f; i += ChunkRenderer.BlockScale)
                {
                    for (float j = zPos - ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale / 3f; j < zPos + ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale / 3f; j += ChunkRenderer.BlockScale)
                    {
                        var tempCond = (precipitation.GetNoise(i, j) + 1) * 200 + temperature.GetNoise(i, j) * 30;
                        if ( tempCond > bestConditions)
                        {
                            bestConditions = tempCond;
                            bestTreePosX = i;
                            bestTreePosZ = j;
                            treeHeight = Generator.GetHeight(i, j) * ChunkRenderer.BlockScale;
                        }
                        var tempFlCond = Math.Abs(100 - (precipitation.GetNoise(i, j) + 1) * 200);
                        if (tempFlCond < bestFlowerCond) {
                            bestFlowerCond = tempFlCond;
                            bestFlPosX = i;
                            bestFlPosZ = j;
                            FlHeight = Generator.GetHeight(i, j) * ChunkRenderer.BlockScale;
                        }
                    }
                }
                
                    
                if (bestConditions < 150) {
                    Instantiate(taigaSmallTree, new Vector3(bestTreePosX, treeHeight, bestTreePosZ), Quaternion.identity, transform);
                } else {
                    Instantiate(taigaFullTree, new Vector3(bestTreePosX, treeHeight, bestTreePosZ), Quaternion.identity, transform);
                }
                if (bestFlowerCond <= 50) {
                    Instantiate(polemonium, new Vector3(bestFlPosX, FlHeight, bestFlPosZ), Quaternion.identity, transform);
                }

                
                
                
            }
        }
        chunk.ChunkData = chunkData;
        chunk.ParentWorld = this;

        chunkData.Renderer = chunk;
    }
    void Update()
    {
        Vector3Int playerWorldPos = Vector3Int.FloorToInt(mainCamera.transform.position / ChunkRenderer.BlockScale);
        Vector2Int playerChunk = GetChunkContainingBlock(playerWorldPos);

        if (playerChunk != currentPlayerChunk)
        {
            currentPlayerChunk = playerChunk;
            StartCoroutine(Generate(true));
        }

        // CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            bool isDestoying = Input.GetMouseButtonDown(0);
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out var hitInfo))
            {
                Vector3 blockCenter;
                if (isDestoying)
                {
                    blockCenter = hitInfo.point - hitInfo.normal * ChunkRenderer.BlockScale / 2;
                }
                else
                {
                    blockCenter = hitInfo.point + hitInfo.normal * ChunkRenderer.BlockScale / 2;
                }

                Vector3Int blockWorldPos = Vector3Int.FloorToInt(blockCenter / ChunkRenderer.BlockScale);
                Vector2Int chunkPos = GetChunkContainingBlock(blockWorldPos);
                if (blockWorldPos.z - chunkPos.y * ChunkRenderer.ChunkWidth < 0) chunkPos.y--;
                if (blockWorldPos.x - chunkPos.x * ChunkRenderer.ChunkWidth >= ChunkRenderer.ChunkWidth) chunkPos.x++;
                if (ChunkDatas.TryGetValue(chunkPos, out ChunkData chunkData))
                {
                    Vector3Int chunkOrigin = new Vector3Int(chunkPos.x, 0, chunkPos.y) * ChunkRenderer.ChunkWidth;
                    if (isDestoying)
                    {
                        chunkData.Renderer.DestroyBlock(blockWorldPos - chunkOrigin);
                    }
                    else
                    {

                        print(chunkOrigin);
                        chunkData.Renderer.SpawnBlock(blockWorldPos - chunkOrigin);
                    }

                }
            }
        }
    }

    public Vector2Int GetChunkContainingBlock(Vector3Int blockWorldPos)
    {
        Vector2Int chunkPosition = new Vector2Int(blockWorldPos.x / ChunkRenderer.ChunkWidth, blockWorldPos.z / ChunkRenderer.ChunkWidth);

        if (blockWorldPos.x < 0) chunkPosition.x--;
        if (blockWorldPos.y < 0) chunkPosition.y--;

        // print(chunkPosition);
        return chunkPosition;
    }
}
