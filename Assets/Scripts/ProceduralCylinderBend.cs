using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCylinderBend : ProceduralBase
{
    [SerializeField]
    private float radius = 0.5f;
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private int radialSegmentCount = 10;
    [SerializeField]
    private int heightSegmentCount = 5;

    [SerializeField]
    private float bendAngle = 90.0f;

    public override Mesh BuildMesh()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        if (bendAngle == 0.0f)
        {
            float heightinc = height / heightSegmentCount;

            for (int i = 0; i <= heightSegmentCount; i++)
            {
                Vector3 centrepos = Vector3.up * heightinc * i;
                float v = (float)i / heightSegmentCount;

                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius, v, i > 0);
            }
        }
        else
        {
            // Gets the angle in radians
            float bendAngleRadians = bendAngle * Mathf.Deg2Rad;
            // Gets the radius of the bend
            float bendRadius = height / bendAngleRadians;
            // The angle increment per height segment
            float angleInc = bendAngleRadians / heightSegmentCount;

            // Gets an offset so that the start of the bend doesn't start at the centre but starts at the radius
            Vector3 startOffset = new Vector3(bendRadius, 0.0f, 0.0f);

            for (int i = 0; i <= heightSegmentCount; i++)
            {
                Vector3 centrePos = Vector3.zero;
                centrePos.x = Mathf.Cos(angleInc * i);
                centrePos.y = Mathf.Sin(angleInc * i);

                float zAngle = angleInc * i * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, zAngle);

                centrePos *= bendRadius;
                centrePos -= startOffset;

                float v = (float) i / heightSegmentCount;

                BuildBendRing(meshBuilder, radialSegmentCount, centrePos, radius, v, i > 0, rotation);
            }
        }

        
        return meshBuilder.CreateMesh();
    }
}
