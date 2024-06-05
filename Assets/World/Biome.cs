using System;
using UnityEngine;
public class Biome {
     public float BaseHeight = 60;
    public NoiseOctaveSettings[] Octaves;
    public NoiseOctaveSettings DomainWarp;
    [Serializable]
    public class NoiseOctaveSettings
    {
        public FastNoiseLite.NoiseType NoiseType;
        public float Frequency = 0.2f;
        public float Amplitude = 1f;
        public NoiseOctaveSettings(FastNoiseLite.NoiseType type, float freq, float amp) 
        {
            NoiseType = type;
            Frequency = freq;
            Amplitude = amp;
        }


    }
    public FastNoiseLite[] octaveNoises;
    public FastNoiseLite warpNoise;

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

    public virtual float GetHeight(float x, float z)
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