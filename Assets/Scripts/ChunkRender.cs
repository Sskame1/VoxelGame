using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRender : MonoBehaviour
{
    private const int ChunkWidth = 10;
    private const int ChunkHeight = 128;

    public int[,,] Blocks = new int[ChunkWidth, ChunkWidth, ChunkHeight];

    private List<Vector3> verticies = new List<Vector3>();
    private List<int> triangles = new List<int>();
    
    void Start()
    {
        Mesh chunkMesh = new Mesh();

        verticies.Add(new Vector3(0, 0, 0));
        verticies.Add(new Vector3(0, 1, 0));
        verticies.Add(new Vector3(0, 0, 1));

        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        chunkMesh.vertices = verticies.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        GetComponent<MeshFilter>().mesh = chunkMesh;
    }
}
