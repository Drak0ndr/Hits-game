using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Profiling;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    public const int ChunkWidth = 30;
    public const int ChunkHeight = 128;
    public const float BlockScale = 0.25f;
    public ChunkData ChunkData;
    public GameWorld ParentWorld;

    private Mesh chunkMesh;

    private ChunkData leftChunk;
    private ChunkData rightChunk;
    private ChunkData fwdChunk;
    private ChunkData backChunk;
    private static ProfilerMarker MeshingMarker = new ProfilerMarker(ProfilerCategory.Loading, "Meshing");
    private List<Vector3> vecticies = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<int> triangles = new List<int>();
    void Start()
    {

        ParentWorld.ChunkDatas.TryGetValue(ChunkData.ChunkPositoin + Vector2Int.left, out leftChunk);
        ParentWorld.ChunkDatas.TryGetValue(ChunkData.ChunkPositoin + Vector2Int.right, out rightChunk);
        ParentWorld.ChunkDatas.TryGetValue(ChunkData.ChunkPositoin + Vector2Int.up, out fwdChunk);
        ParentWorld.ChunkDatas.TryGetValue(ChunkData.ChunkPositoin + Vector2Int.down, out backChunk);
        chunkMesh = new Mesh();

        RegenerateMesh();

        GetComponent<MeshFilter>().mesh = chunkMesh;

    }

    private void RegenerateMesh()
    {
        MeshingMarker.Begin();
        vecticies.Clear();
        triangles.Clear();
        uvs.Clear();

        for (int x = 0; x < ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkWidth; z++)
            {
                // float blockHeight00 = ChunkData.heightMap[x,z];
                float blockHeight00 = 0;

                for (int y = 0; y < ChunkHeight; y++)
                {
                    if (false && y == (int)blockHeight00) {
                        float blockHeight01 = ChunkData.heightMap[x,z+1];
                        float blockHeight10 = ChunkData.heightMap[x+1,z];
                        float blockHeight11 = ChunkData.heightMap[x+1,z+1];
                        float bh00 = blockHeight00 - y + 1;
                        float bh01 = blockHeight01 - y + 1;
                        float bh10 = blockHeight10 - y + 1;
                        float bh11 = blockHeight11 - y + 1;
                        if (bh00 >= 1.5) bh00 = 2;
                        if (bh00 < 0.5) bh00 = 0;
                        if (bh01 >= 1.5) bh01 = 2;
                        if (bh01 < 0.5) bh01 = 0;
                        if (bh10 >= 1.5) bh10 = 2;
                        if (bh10 < 0.5) bh10 = 0;
                        if (bh11 >= 1.5) bh11 = 2;
                        if (bh11 < 0.5) bh11 = 0;
                        if (bh00 >= 1 && bh00 < 1.5) bh00 = 1;
                        if (bh01 >= 1 && bh01 < 1.5) bh01 = 1;
                        if (bh10 >= 1 && bh10 < 1.5) bh10 = 1;
                        if (bh11 >= 1 && bh11 < 1.5) bh11 = 1;
                        if (bh00 >=0.5 && bh00 <=1) bh00 = 1;
                        if (bh01 >=0.5 && bh01 <=1) bh01 = 1;
                        if (bh10 >=0.5 && bh10 <=1) bh10 = 1;
                        if (bh11 >=0.5 && bh11 <=1) bh11 = 1;
                        GenerateBlock(x, y, z);
                    } else {
                        GenerateBlock(x, y, z);
                    }
                
                }
            }
        }



        chunkMesh.triangles = Array.Empty<int>();
        chunkMesh.vertices = vecticies.ToArray();
        chunkMesh.uv = uvs.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.Optimize();

        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        GetComponent<MeshCollider>().sharedMesh = chunkMesh;
        MeshingMarker.End();
    }

    public void SpawnBlock(Vector3Int blockPosition)
    {
        
        print(blockPosition);
        ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z] = BlockType.Stone;
        RegenerateMesh();
    }
    public void DestroyBlock(Vector3Int blockPosition)
    {
        ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z] = BlockType.Air;
        RegenerateMesh();
    }
    private void GenerateBlock(int x, int y, int z, float blockHeight00 = 1, float blockHeight01 = 1, float blockHeight10 = 1, float blockHeight11 = 1)
    {
        var blockPosition = new Vector3Int(x, y, z);
        if (GetBlockAtPosition(blockPosition) == 0) return;


        if (GetBlockAtPosition(blockPosition + Vector3Int.right) == 0)
        {
            GenerateRightSide(blockPosition, blockHeight10, blockHeight11);
            AddUvs(GetBlockAtPosition(blockPosition));
        }
        if (GetBlockAtPosition(blockPosition + Vector3Int.left) == 0)
        {
            GenerateLeftSide(blockPosition, blockHeight00, blockHeight01);
            AddUvs(GetBlockAtPosition(blockPosition));
        }
        if (GetBlockAtPosition(blockPosition + Vector3Int.forward) == 0)
        {
            GenerateFrontSide(blockPosition, blockHeight01, blockHeight11);
            AddUvs(GetBlockAtPosition(blockPosition));
        }
        if (GetBlockAtPosition(blockPosition + Vector3Int.back) == 0)
        {
            GenerateBackSide(blockPosition, blockHeight00, blockHeight10);
            AddUvs(GetBlockAtPosition(blockPosition));
        }
        if (GetBlockAtPosition(blockPosition + Vector3Int.up) == 0)
        {
            GenerateTopSide(blockPosition, blockHeight00, blockHeight01, blockHeight10, blockHeight11);
            AddUvs(GetBlockAtPosition(blockPosition));
        }
        if (blockPosition.y > 0 && GetBlockAtPosition(blockPosition + Vector3Int.down) == 0)
        {
            GenerateBottomSide(blockPosition);
            AddUvs(GetBlockAtPosition(blockPosition));
        }
    }

    private BlockType GetBlockAtPosition(Vector3Int blockPosition)
    {
        if (blockPosition.x >= 0 && blockPosition.x < ChunkWidth &&
        blockPosition.y >= 0 && blockPosition.y < ChunkHeight &&
        blockPosition.z >= 0 && blockPosition.z < ChunkWidth)
        {
            return ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
        }
        else
        {
            if (blockPosition.y < 0 || blockPosition.y >= ChunkHeight) return BlockType.Air;

            if (blockPosition.x < 0)
            {
                if (leftChunk == null) {
                    return BlockType.Air;
                }
                blockPosition.x += ChunkWidth;
                return leftChunk.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }
            else if (blockPosition.x >= ChunkWidth)
            {
                if (rightChunk == null) {
                    return BlockType.Air;
                }
                blockPosition.x -= ChunkWidth;
                return rightChunk.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }

            if (blockPosition.z < 0)
            {
                if (backChunk == null) {
                    return BlockType.Air;
                }
                blockPosition.z += ChunkWidth;
                return backChunk.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }
            else if (blockPosition.z >= ChunkWidth)
            {
                if (fwdChunk == null) {
                    return BlockType.Air;
                }
                blockPosition.z -= ChunkWidth;
                return fwdChunk.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }

            return BlockType.Air;

        }
    }
    private void GenerateRightSide(Vector3Int blockPosition, float blockHeight10, float blockHeight11)
    {
        vecticies.Add((new Vector3(1, 0, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, blockHeight10, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, 0, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, blockHeight11, 1) + blockPosition) * BlockScale);

        AddLastVerticiesSquare();

    }

    private void GenerateLeftSide(Vector3Int blockPosition, float blockHeight00, float blockHeight01)
    {
        vecticies.Add((new Vector3(0, 0, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, 0, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, blockHeight00, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, blockHeight01, 1) + blockPosition) * BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateFrontSide(Vector3Int blockPosition, float blockHeight01, float blockHeight11)
    {
        vecticies.Add((new Vector3(0, 0, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, 0, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, blockHeight01, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, blockHeight11, 1) + blockPosition) * BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateBackSide(Vector3Int blockPosition, float blockHeight00, float blockHeight10)
    {
        vecticies.Add((new Vector3(0, 0, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, blockHeight00, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, 0, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, blockHeight10, 0) + blockPosition) * BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateTopSide(Vector3Int blockPosition, float blockHeight00, float blockHeight01, float blockHeight10, float blockHeight11)
    {
        vecticies.Add((new Vector3(0, blockHeight00, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, blockHeight01, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, blockHeight10, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, blockHeight11, 1) + blockPosition) * BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateBottomSide(Vector3Int blockPosition)
    {
        vecticies.Add((new Vector3(0, 0, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, 0, 0) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(0, 0, 1) + blockPosition) * BlockScale);
        vecticies.Add((new Vector3(1, 0, 1) + blockPosition) * BlockScale);

        AddLastVerticiesSquare();
    }
    private void AddLastVerticiesSquare()
    {

        triangles.Add(vecticies.Count - 4);
        triangles.Add(vecticies.Count - 3);
        triangles.Add(vecticies.Count - 2);

        triangles.Add(vecticies.Count - 3);
        triangles.Add(vecticies.Count - 1);
        triangles.Add(vecticies.Count - 2);
    }

    private void AddUvs(BlockType blockType)
    {
        if (blockType == BlockType.Grass)
        {
            uvs.Add(new Vector2(0.1f, 0.1f));
            uvs.Add(new Vector2(0.1f, 0.9f));
            uvs.Add(new Vector2(0.4f, 0.1f));
            uvs.Add(new Vector2(0.4f, 0.9f));
        }
        if (blockType == BlockType.Stone)
        {
            uvs.Add(new Vector2(0.6f, 0.1f));
            uvs.Add(new Vector2(0.6f, 0.9f));
            uvs.Add(new Vector2(0.9f, 0.1f));
            uvs.Add(new Vector2(0.9f, 0.9f));
        }

    }
}
