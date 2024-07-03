using System.Collections.Generic;
using UnityEngine;

public class ChunkData 
{
    public Vector2Int ChunkPositoin;
    public ChunkRenderer Renderer;
    public BlockType[,,] Blocks;
    public float[,] heightMap;
    public List<MeshRenderer> vegetation = new List<MeshRenderer>();
}