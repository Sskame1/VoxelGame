using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRender : MonoBehaviour
{
    public const int ChunkWidth = 10;
    public const int ChunkHeight = 128;
    public const float BlockScale = .5f;

    public BlockType[,,] Blocks = new BlockType[ChunkWidth, ChunkHeight, ChunkWidth];

    private List<Vector3> verticies = new List<Vector3>();
    private List<int> triangles = new List<int>();
    
    void Start()
    {
        Mesh chunkMesh = new Mesh();

        Blocks = TerrainGenerator.GenerateTerrain((int)transform.position.x, (int)transform.position.z );

        for (int y = 0; y < ChunkHeight; y++)
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkWidth; z++) 
                {
                    GenerateBlock(x,y,z);
                }
            }
        }

        

        chunkMesh.vertices = verticies.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.Optimize();

        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = chunkMesh;
    }

    private void GenerateBlock(int x, int y, int z)
    {
        var blockPosition = new Vector3Int(x, y, z);

        if (GetBlockAtPosition(blockPosition) == 0) return;


        if(GetBlockAtPosition(blockPosition + Vector3Int.right) ==0) GenerateRightSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.left) ==0) GenerateLeftSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.forward) ==0) GenerateFrontSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.back) ==0) GenerateBackSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.up) ==0) GenerateTopSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.down) == 0) GenerateBottomSide(blockPosition);
    }

    private BlockType GetBlockAtPosition(Vector3Int blockPostition)
    {
        if(blockPostition.x >= 0 && blockPostition.x < ChunkWidth &&
            blockPostition.y >= 0 && blockPostition.y < ChunkHeight &&
            blockPostition.z >= 0 && blockPostition.z < ChunkWidth)
        {
            return Blocks[blockPostition.x, blockPostition.y, blockPostition.z];
        }
        else return BlockType.Air;
    }

    private void GenerateRightSide(Vector3Int blockPostition)
    {
        verticies.Add((new Vector3(1, 0, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 0, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 1) + blockPostition) * BlockScale);

        AddLastVerticalSquare();
    }

    private void GenerateLeftSide(Vector3Int blockPostition)
    {
        verticies.Add((new Vector3(0, 0, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 0, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 1, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 1, 1) + blockPostition) * BlockScale);

        AddLastVerticalSquare();
    }

    private void GenerateFrontSide(Vector3Int blockPostition)
    {
        verticies.Add((new Vector3(0, 0, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 0, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 1, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 1) + blockPostition) * BlockScale);

        AddLastVerticalSquare();
    }

    private void GenerateBackSide(Vector3Int blockPostition)
    {
        verticies.Add((new Vector3(0, 0, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 1, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 0, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 0) + blockPostition) * BlockScale);

        AddLastVerticalSquare();
    }

    private void GenerateTopSide(Vector3Int blockPostition)
    {
        verticies.Add((new Vector3(0, 1, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 1, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 1) + blockPostition) * BlockScale);

        AddLastVerticalSquare();
    }

    private void GenerateBottomSide(Vector3Int blockPostition)
    {
        verticies.Add((new Vector3(0, 0, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 0, 0) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(0, 0, 1) + blockPostition) * BlockScale);
        verticies.Add((new Vector3(1, 0, 1) + blockPostition) * BlockScale);

        AddLastVerticalSquare();
    }

    private void AddLastVerticalSquare()
    {
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }
}
