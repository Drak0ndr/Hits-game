    using System;
using Unity.Profiling;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    
    public Biome[] Biomes = {new FlatLands(), new Mountain()};

    private static ProfilerMarker GeneratingMarker = new ProfilerMarker(ProfilerCategory.Loading, "Generating");
    public void Awake()
    {
        Init();
    }

    public void Init() {
        for (int i = 0; i < Biomes.Length; i++)
        {
            Biomes[i].Init();
        }
    }
    public BlockType[,,] GenerateTerrain(float xOffset, float zOffset)
    {
        GeneratingMarker.Begin();
        var result = new BlockType[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];
        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
            {
                // float height = Mathf.PerlinNoise((x * ChunkRenderer.BlockScale + xOffset) * .2f, (z * ChunkRenderer.BlockScale + zOffset) * .2f) * 25 + 70;
                float height = GetHeight((x * ChunkRenderer.BlockScale + xOffset), (z * ChunkRenderer.BlockScale + zOffset));
                float grassLayerHeight = 1;
                if (height > ChunkRenderer.ChunkHeight)
                {
                    height = ChunkRenderer.ChunkHeight;
                }
                for (int y = 0; y < height; y++)
                {
                    if (height - y < grassLayerHeight) {
                        result[x, y, z] = BlockType.Grass;
                    } else {
                        result[x, y, z] = BlockType.Stone;
                    }
                    
                }
            }
        }
        GeneratingMarker.End();
        return result;
    }

    public float[,] GenerateHightMap(float xOffset, float zOffset) {
        var result = new float[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkWidth];
        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
            {
                float height = GetHeight((x * ChunkRenderer.BlockScale + xOffset), (z * ChunkRenderer.BlockScale + zOffset));
                result[x,z] = height;
            }
        }

        return result;
    }

    public float GetHeight(float x, float z)
    {
        float result = 0;
        for (int i = 0; i < Biomes.Length; i++)
        {
            result += Biomes[i].GetHeight(x,z);
        }

        return result;
    }
}