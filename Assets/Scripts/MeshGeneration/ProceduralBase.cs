using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public abstract class ProceduralBase : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract Mesh BuildMesh();

    public void Start()
    {
        Mesh mesh = BuildMesh();

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        if (meshFilter)
        {
            meshFilter.sharedMesh = mesh;
        }
    }

    #region BuildCube
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

    #region Circle

    protected void BuildCircle(MeshBuilder meshBuilder, int segmentCount, Vector3 centre, float radius, float v,
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

    #endregion
}
