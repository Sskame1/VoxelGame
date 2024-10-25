using UnityEngine;

public static class TerrainGenerator
{
    public static BlockType[,,] GenerateTerrain(int xOffset, int yOffset)
    {
        var result = new BlockType[ChunkRender.ChunkWidth, ChunkRender.ChunkHeight, ChunkRender.ChunkWidth];

        for (int x = 0; x < ChunkRender.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRender.ChunkWidth; z++)
            {
                float height = Mathf.PerlinNoise((x + xOffset) * .2f, (z + yOffset) * .2f) * 5 + 10;

                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = BlockType.Grass;
                }
            }
            
        }

        return result;
    }
}
