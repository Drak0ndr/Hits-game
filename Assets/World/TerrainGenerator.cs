using System;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public float BaseHeight = 60;
    public NoiseOctaveSettings[] Octaves;
    public NoiseOctaveSettings DomainWarp;
    [Serializable]
    public class NoiseOctaveSettings
    {
        public FastNoiseLite.NoiseType NoiseType;
        public float Frequency = 0.2f;
        public float Amplitude = 1f;

    }
    private FastNoiseLite[] octaveNoises;
    private FastNoiseLite warpNoise;
    public void Awake()
    {
        Init();
    }

    public void Init() {
        octaveNoises = new FastNoiseLite[Octaves.Length];
        for (int i = 0; i < Octaves.Length; i++)
        {
            octaveNoises[i] = new FastNoiseLite();
            octaveNoises[i].SetNoiseType(Octaves[i].NoiseType);
            octaveNoises[i].SetFrequency(Octaves[i].Frequency);
        }

        warpNoise = new FastNoiseLite();
        warpNoise.SetNoiseType(DomainWarp.NoiseType);
        warpNoise.SetFrequency(DomainWarp.Frequency);
        warpNoise.SetDomainWarpAmp(DomainWarp.Amplitude);
    }
    public BlockType[,,] GenerateTerrain(float xOffset, float zOffset)
    {
        var result = new BlockType[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];
        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
            {
                // float height = Mathf.PerlinNoise((x * ChunkRenderer.BlockScale + xOffset) * .2f, (z * ChunkRenderer.BlockScale + zOffset) * .2f) * 25 + 70;
                float height = GetHeight((x * ChunkRenderer.BlockScale + xOffset), (z * ChunkRenderer.BlockScale + zOffset));
                float grassLayerHeight = 1;
                if (height >= ChunkRenderer.ChunkHeight)
                {
                    height = ChunkRenderer.ChunkHeight - 1;
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

        return result;
    }

    private float GetHeight(float x, float z)
    {
        warpNoise.DomainWarp(ref x, ref z);
        float result = BaseHeight;

        for (int i = 0; i < Octaves.Length; i++)
        {
            float noise = octaveNoises[i].GetNoise(x, z);
            result += noise * Octaves[i].Amplitude / 2;
        }

        return result;
    }
}