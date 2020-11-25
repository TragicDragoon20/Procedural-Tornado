using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTornado : ProceduralBase
{
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float radius = 1.0f;
    [SerializeField]
    private float radiusBottom = 0.5f;
    [SerializeField]
    private float radiusTop = 0.5f;

    [Space(10)]
    [SerializeField]
    private int heightSegmentCount = 10;
    [SerializeField]
    private int radialSegmentCount = 10;


    public override Mesh BuildMesh()
    {
        MeshBuilder meshBuilder = new MeshBuilder();
        float heightInc = height / heightSegmentCount;

        for (int i = 0; i <= heightSegmentCount; i++)
        {
            Vector3 centrepos = Vector3.up * heightInc * i; 
            float v = (float)i / heightSegmentCount;

            if (i == 0)
            {
                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius + radiusBottom, v, i > 0);
            }

            if (i > 0 && i != heightSegmentCount)
            {
                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius + Random.Range(0.0f, 0.3f), v, i > 0);
            }

            if (i == heightSegmentCount)
            {
                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius + radiusTop, v, i > 0);
            }
        }

        return meshBuilder.CreateMesh();
    }
}
