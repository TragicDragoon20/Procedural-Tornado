using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCircle : ProceduralBase
{
    [SerializeField]
    private float radius = 0.5f;
    [SerializeField]
    private int radialSegmentCount = 10;

    public override Mesh BuildMesh()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        BuildCap(meshBuilder, Vector3.zero, false);

        return meshBuilder.CreateMesh();
    }

    protected void BuildCap(MeshBuilder meshBuilder, Vector3 centre, bool reverseDir)
    {
        Vector3 normal = reverseDir ? Vector3.down : Vector3.up;

        meshBuilder.Vertices.Add(centre);
        meshBuilder.Normals.Add(normal);
        meshBuilder.Uvs.Add(new Vector2(0.5f, 0.5f));

        int centreVertexIndex = meshBuilder.Vertices.Count - 1;

        float angleInc = (Mathf.PI * 2.0f) / radialSegmentCount;

        for (int i = 0; i <= radialSegmentCount; i++)
        {
            float angle = angleInc * i;

            Vector3 unitPos = Vector3.zero;
            unitPos.x = Mathf.Cos(angle);
            unitPos.z = Mathf.Sin(angle);
            if (reverseDir)
            {
                meshBuilder.Vertices.Add(centre + unitPos * (radius));
            }
            else if (!reverseDir)
            {
                meshBuilder.Vertices.Add(centre + unitPos * radius );
            }

            meshBuilder.Normals.Add(normal);

            Vector2 uv = new Vector2(unitPos.x + 1.0f, unitPos.z + 1.0f * 0.5f);
            meshBuilder.Uvs.Add(uv);

            if (i > 0)
            {
                int baseIndex = meshBuilder.Vertices.Count - 1;

                if (reverseDir)
                {
                    meshBuilder.AddTriangle(centreVertexIndex, baseIndex - 1, baseIndex);
                }
                else
                {
                    meshBuilder.AddTriangle(centreVertexIndex, baseIndex, baseIndex - 1);
                }
            }
        }
    }
}
