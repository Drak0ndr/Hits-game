public class FlatLands : Biome
{

    public FlatLands()
    {
        BaseHeight = 60;
        Octaves = new NoiseOctaveSettings[] { new NoiseOctaveSettings(FastNoiseLite.NoiseType.Perlin, 0.05f, 10f, 1) };
        DomainWarp = new NoiseOctaveSettings(FastNoiseLite.NoiseType.OpenSimplex2, 0.01f, 1, 1);
    }
    public override float GetHeight(float x, float z)
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