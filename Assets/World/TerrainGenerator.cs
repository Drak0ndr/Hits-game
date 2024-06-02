using System;
using UnityEngine;

public static class TerrainGenerator
{
    public static BlockType[,,] GenerateTerrain(float xOffset, float zOffset)
    {
        var result = new BlockType[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];
        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
            {
                float height = Mathf.PerlinNoise((x * ChunkRenderer.BlockScale + xOffset) * .2f, (z * ChunkRenderer.BlockScale + zOffset) * .2f) * 25 + 10;
                if (height >= ChunkRenderer.ChunkHeight)
                {
                    height = ChunkRenderer.ChunkHeight - 1;
                }
                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = BlockType.Grass;
                }
            }
        }

        return result;
    }
}