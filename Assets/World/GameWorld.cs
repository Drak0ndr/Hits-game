using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public Dictionary<Vector2Int, ChunkData> ChunkDatas = new Dictionary<Vector2Int, ChunkData>();
    public ChunkRenderer chunkPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                float xPos = x * ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale;
                float zPos = z * ChunkRenderer.ChunkWidth * ChunkRenderer.BlockScale;
                ChunkData chunkData = new ChunkData();
                chunkData.Blocks = TerrainGenerator.GenerateTerrain(xPos, zPos);
                chunkData.ChunkPositoin = new Vector2Int(x,z);
                ChunkDatas.Add(new Vector2Int(x, z), chunkData);

                var chunk = Instantiate(chunkPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);
                chunk.ChunkData = chunkData;
                chunk.ParentWorld = this;
            }
        }
    }
}
