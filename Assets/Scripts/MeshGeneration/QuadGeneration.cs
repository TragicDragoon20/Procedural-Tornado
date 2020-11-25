using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class QuadGeneration : MonoBehaviour
{
    [SerializeField]
    private float length = 1.0f;

    [SerializeField]
    private float width = 1.0f;

    private void Start()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        // Set up the vertices and triangles
        meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, 0.0f));
        meshBuilder.Uvs.Add(new Vector2(0.0f, 0.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, length));
        meshBuilder.Uvs.Add(new Vector2(0.0f, 1.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(width, 0.0f, length));
        meshBuilder.Uvs.Add(new Vector2(0.0f, 1.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(width, 0.0f, 0.0f));
        meshBuilder.Uvs.Add(new Vector2(1.0f, 0.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.AddTriangle(0, 1, 2);
        meshBuilder.AddTriangle(0, 2, 3);

        // Create the mesh
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        if (meshFilter)
        {
            meshFilter.sharedMesh = meshBuilder.CreateMesh();
        }
    }
}
