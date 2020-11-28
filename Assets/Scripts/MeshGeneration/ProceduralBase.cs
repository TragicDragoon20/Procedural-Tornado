using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public abstract class ProceduralBase : MonoBehaviour
{
    /// <summary>
    /// Method for building a mesh with in the classes that inherit from this class
    /// </summary>
    /// <returns>The completed mesh</returns>
    public abstract Mesh BuildMesh();

    /// <summary>
    /// Builds the mesh and assigns 
    /// </summary>
    public void Start()
    {
        Mesh mesh = BuildMesh();

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        if (meshFilter)
        {
            meshFilter.sharedMesh = mesh;
        }
    }

    #region BuildQuad
    /// <summary>
    /// Builds a quad on a position offset and width and length vectors
    /// </summary>
    /// <param name="meshBuilder">The mesh builder that's storing all the current mesh values</param>
    /// <param name="offset">A position offset for the quad</param>
    /// <param name="widthDir">Width vector of the quad</param>
    /// <param name="lengthDir"> length vector of the quad</param>
    protected void BuildQuad(MeshBuilder meshBuilder, Vector3 offset, Vector3 widthDir, Vector3 lengthDir)
    {
        Vector3 normal = Vector3.Cross(lengthDir, widthDir).normalized;

        meshBuilder.Vertices.Add(offset);
        meshBuilder.Uvs.Add(new Vector2(0.0f, 0.0f));
        meshBuilder.Normals.Add(normal);

        meshBuilder.Vertices.Add(offset + lengthDir);
        meshBuilder.Uvs.Add(new Vector2(0.0f, 1.0f));
        meshBuilder.Normals.Add(normal);

        meshBuilder.Vertices.Add(offset + lengthDir + widthDir);
        meshBuilder.Uvs.Add(new Vector2(1.0f, 1.0f));
        meshBuilder.Normals.Add(normal);

        meshBuilder.Vertices.Add(offset + widthDir);
        meshBuilder.Uvs.Add(new Vector2(1.0f, 0.0f));
        meshBuilder.Normals.Add(normal);

        int baseIndex = meshBuilder.Vertices.Count - 4;

        meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
        meshBuilder.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
    }

    #endregion


    #region Cylinder
    /// <summary>
    /// Builds a ring for part of a cylinder
    /// </summary>
    /// <param name="meshBuilder">The mesh builder that's storing all the current mesh values</param>
    /// <param name="segmentCount">The number of segments in the ring</param>
    /// <param name="centre">The position of the rings centre</param>
    /// <param name="radius">The radius of the ring</param>
    /// <param name="v"></param>
    /// <param name="buildTriangles">Whether or not the triangles should be false for the ring. This should be false for the first ring</param>
    protected void BuildRing(MeshBuilder meshBuilder, int segmentCount, Vector3 centre, float radius, float v,
        bool buildTriangles)
    {
        float angleInc = (Mathf.PI * 2.0f) / segmentCount;

        for (int i = 0; i <= segmentCount; i++)
        {
            float angle = angleInc * i;

            Vector3 unitPos = Vector3.zero;
            unitPos.x = Mathf.Cos(angle);
            unitPos.z = Mathf.Sin(angle);

            meshBuilder.Vertices.Add(centre + unitPos * radius);
            meshBuilder.Normals.Add(unitPos);
            meshBuilder.Uvs.Add(new Vector2((float)i / segmentCount, v));

            if (i > 0 && buildTriangles)
            {
                int baseIndex = meshBuilder.Vertices.Count - 1;
                int vertsPerRov = segmentCount + 1;

                int index0 = baseIndex;
                int index1 = baseIndex - 1;
                int index2 = baseIndex - vertsPerRov;
                int index3 = baseIndex - vertsPerRov - 1;

                meshBuilder.AddTriangle(index0, index2, index1);
                meshBuilder.AddTriangle(index2, index3, index1);
            }
        }
    }

    /// <summary>
    /// Builds a ring for part of a cylinder that has a bend
    /// </summary>
    /// <param name="meshBuilder">The mesh builder that's storing all the current mesh values</param>
    /// <param name="segmentCount">The number of segments in the ring</param>
    /// <param name="centre">The position of the rings centre</param>
    /// <param name="radius">The radius of the ring</param>
    /// <param name="v"></param>
    /// <param name="buildTriangles">Whether or not the triangles should be false for the ring. This should be false for the first ring</param>
    /// <param name="rotation">A rotation value to be applied the the ring</param>
    protected void BuildBendRing(MeshBuilder meshBuilder, int segmentCount, Vector3 centre, float radius, float v,
        bool buildTriangles, Quaternion rotation)
    {
        float angleInc = (Mathf.PI * 2.0f) / segmentCount;

        for (int i = 0; i <= segmentCount; i++)
        {
            float angle = angleInc * i;

            Vector3 unitPos = Vector3.zero;
            unitPos.x = Mathf.Cos(angle);
            unitPos.z = Mathf.Sin(angle);
            unitPos = rotation * unitPos;

            meshBuilder.Vertices.Add(centre + unitPos *radius);
            meshBuilder.Normals.Add(unitPos);
            meshBuilder.Uvs.Add(new Vector2((float)i / segmentCount, v));

            if (i > 0 && buildTriangles)
            {
                int baseIndex = meshBuilder.Vertices.Count - 1;
                int vertsPerRov = segmentCount + 1;

                int index0 = baseIndex;
                int index1 = baseIndex - 1;
                int index2 = baseIndex - vertsPerRov;
                int index3 = baseIndex - vertsPerRov - 1;

                meshBuilder.AddTriangle(index0, index2, index1);
                meshBuilder.AddTriangle(index2, index3, index1);
            }
        }
    }

    #endregion
}
