using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTornado : ProceduralBase
{
    [Header("Base Dimensions")]
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float radius = 1.0f;

    [Header("Offset Values")]
    [Space(10)]
    [SerializeField]
    private float radiusBottomOffset = 0.5f;
    [SerializeField]
    private float radiusTopOffset = 0.5f;
    [SerializeField]
    [Range(0.0f, 0.4f)]
    private float maxCentreOffset;

    [Header("Segments")]
    [Space(10)]
    [SerializeField]
    private int heightSegmentCount = 10;
    [SerializeField]
    private int radialSegmentCount = 10;

    public override Mesh BuildMesh()
    {
        MeshBuilder meshBuilder = new MeshBuilder();
        // Gets the distance between each height segments 
        float heightInc = height / heightSegmentCount;

        for (int i = 0; i <= heightSegmentCount; i++)
        {
            // Sets the centre poss to be at the correct height
            Vector3 centrepos = Vector3.up * heightInc * i; 

            float v = (float)i / heightSegmentCount;

            // Generates the bottom ring of the tornado based on the bottom radius offset
            if (i == 0)
            {
                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius + radiusBottomOffset, v, i > 0);
            }

            // Generates the the rings between the top and bottom rings with a random off set to change the radius of the rings
            if (i > 0 && i != heightSegmentCount)
            {
                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius + Random.Range(0.0f, maxCentreOffset), v, i > 0);
            }

            // Generates the top ring of the tornado based on the top radius offset
            if (i == heightSegmentCount)
            {
                BuildRing(meshBuilder, radialSegmentCount, centrepos, radius + radiusTopOffset, v, i > 0);
            }
        }

        return meshBuilder.CreateMesh();
    }
}
