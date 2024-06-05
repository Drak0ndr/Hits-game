public class Mountain : Biome
{

    public Mountain()
    {
        BaseHeight = 70;
        Octaves = new NoiseOctaveSettings[] { new NoiseOctaveSettings(FastNoiseLite.NoiseType.OpenSimplex2, 0.01f, 100f, 1), new NoiseOctaveSettings(FastNoiseLite.NoiseType.Perlin, 0.01f, 100f, 1) };
        DomainWarp = new NoiseOctaveSettings(FastNoiseLite.NoiseType.OpenSimplex2, 0, 0, 1);
    }
    public override float GetHeight(float x, float z)
    {
        warpNoise.DomainWarp(ref x, ref z);
        float result = 0;
        float k = 1;
        var mountainNoise = new FastNoiseLite();
        mountainNoise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
        mountainNoise.SetFrequency(0.01f);


        for (int i = 0; i < Octaves.Length; i++)
        {
            float noise = octaveNoises[i].GetNoise(x, z);
            result += noise * Octaves[i].Amplitude / 2;
        }

        if (mountainNoise.GetNoise(x, z) > -1.9 && result > 0)
        {
            k = 1;
        }
        else
        {
            k = 0;
        }
        return result * k;
    }
}