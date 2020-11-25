using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particles;
    [SerializeField]
    private GameObject cylinderMesh;
    [SerializeField]
    private GameObject quadMesh;

    [SerializeField]
    private float maxTime;
    [SerializeField]
    private float explodeSpeed;

    private bool delayStart;
    private float currentTimer = 0.0f;
    [SerializeField]
    private float delayTime;
    private Vector3 spawnPos;

    [SerializeField]
    private float range = 10.0f;

    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private Camera cam;

    void Update()
    {
        AoECheck();
    }

    #region AoEVFX

    private void AoECheck()
    {
        if (Input.GetKeyDown(KeyCode.E) && !delayStart)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, range, layer))
            {
                spawnPos = hit.point;
                StartVfx();
                currentTimer = delayTime;
                delayStart = true;
            }
        }

        if (delayStart)
        {
            if (currentTimer <= 0.0f)
            {
                EndVfx();
            }
            currentTimer -= Time.deltaTime;
        }
    }

    private void StartVfx()
    {
        GameObject particle = Instantiate(particles[0], spawnPos, Quaternion.identity);
        GameObject cylinder = Instantiate(cylinderMesh, spawnPos, Quaternion.identity);
        GameObject quad = Instantiate(quadMesh, spawnPos, Quaternion.identity);

        cylinder.GetComponent<Renderer>().material.SetFloat("Vector1_659CD848", Time.time);
        cylinder.GetComponent<Renderer>().material.SetFloat("CompTime", maxTime);
        quad.GetComponent<Renderer>().material.SetFloat("StartTime", Time.time);
        quad.GetComponent<Renderer>().material.SetFloat("CompTime", maxTime);

        Destroy(cylinder, maxTime);
        Destroy(quad, maxTime);
        Destroy(particle, maxTime);
    }

    private void EndVfx()
    {
        GameObject particle = Instantiate(particles[1], spawnPos, Quaternion.identity);
        GameObject cylinder = Instantiate(cylinderMesh, spawnPos, Quaternion.identity);
        GameObject quad = Instantiate(quadMesh, spawnPos, Quaternion.identity);

        cylinder.GetComponent<Renderer>().material.SetFloat("Vector1_659CD848", Time.time);
        cylinder.GetComponent<Renderer>().material.SetFloat("CompTime", 0.0f);
        cylinder.GetComponent<Renderer>().material.SetFloat("Speed", explodeSpeed);
        quad.GetComponent<Renderer>().material.SetFloat("StartTime", Time.time);
        quad.GetComponent<Renderer>().material.SetFloat("CompTime", 0.0f);
        quad.GetComponent<Renderer>().material.SetFloat("Speed", explodeSpeed);

        Destroy(cylinder, maxTime);
        Destroy(quad, maxTime);
        Destroy(particle, maxTime);
        delayStart = false;
    }

    #endregion

}
