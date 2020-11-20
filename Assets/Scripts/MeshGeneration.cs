using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGeneration : MonoBehaviour
{
    private Mesh mesh;
    private CubeMeshData cubeMesh;

    private List<Vector3> vertices;
    private List<int> triangles;

    private void Awake()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        cubeMesh = new CubeMeshData();
    }

    private void Start()
    {
        MakeCube();
        CreateMesh();
    }

    private void MakeCube()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            MakeFace(i);
        }
    }

    private void MakeFace(int dir)
    {
        vertices.AddRange(cubeMesh.faceVertices(dir));

        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    private void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
