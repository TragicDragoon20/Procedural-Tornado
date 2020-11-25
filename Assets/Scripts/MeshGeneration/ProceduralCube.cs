using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCube : ProceduralBase
{
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float width = 1.0f;
    [SerializeField]
    private float length = 1.0f;

    public override Mesh BuildMesh()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        Vector3 upDir = Vector3.up * height;
        Vector3 rightDir = Vector3.right * width;
        Vector3 forwardDir = Vector3.forward * length;

        Vector3 farCorner = (upDir + rightDir + forwardDir) / 2;
        Vector3 nearCorner = -farCorner;


        BuildQuad(meshBuilder, nearCorner, forwardDir, rightDir);
        BuildQuad(meshBuilder, nearCorner, rightDir, upDir);
        BuildQuad(meshBuilder, nearCorner, upDir, forwardDir);

        BuildQuad(meshBuilder, farCorner, - rightDir, -forwardDir);
        BuildQuad(meshBuilder, farCorner, -upDir, -rightDir);
        BuildQuad(meshBuilder, farCorner, -forwardDir, -upDir);

        Mesh mesh = meshBuilder.CreateMesh();
        return mesh;
    }
}
