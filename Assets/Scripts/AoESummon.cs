using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoESummon : MonoBehaviour
{
    [SerializeField]
    private GameObject particles = null;
    [SerializeField]
    private GameObject cylinderMesh = null;
    [SerializeField]
    private GameObject quadMesh = null;

    [SerializeField]
    private float maxTime = 0.0f;
    [SerializeField]
    private float explodeSpeed = 0.0f;

    private bool delayStart;
    private float currentTimer = 0.0f;
    [SerializeField]
    private float delayTime = 0.0f;
    private Vector3 spawnPos;

    [SerializeField]
    private float range = 10.0f;

    [SerializeField]
    private LayerMask layer = 0;
    [SerializeField]
    private Camera cam = null;

    void Update()
    {
        AoECheck();
    }

    #region AoEVFX


    /// <summary>
    /// Checks when the key has been pressed to summon the AoE attack
    /// and makes sure that it can't be summoned whilst the effect is already happening
    /// </summary>
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
        // Waits until the delayTime has passed before summoning the second part of the AoE attack
        if (delayStart)
        {
            if (currentTimer <= 0.0f)
            {
                EndVfx();
            }
            currentTimer -= Time.deltaTime;
        }
    }
    /// <summary>
    /// Summons the first part of the AoE attack
    /// and destroys them after their effect has been completed
    /// </summary>
    private void StartVfx()
    {
        GameObject particle = Instantiate(particles, spawnPos, Quaternion.identity);
        GameObject cylinder = Instantiate(cylinderMesh, spawnPos, Quaternion.identity);
        GameObject quad = Instantiate(quadMesh, spawnPos, Quaternion.identity);

        cylinder.GetComponent<Renderer>().material.SetFloat("Vector1_659CD848", Time.time);
        cylinder.GetComponent<Renderer>().material.SetFloat("CompTime", maxTime);

        quad.GetComponent<Renderer>().material.SetFloat("StartTime", Time.time);
        quad.GetComponent<Renderer>().material.SetFloat("CompTime", maxTime);

        Destroy(cylinder, maxTime);
        Destroy(quad, maxTime);
        Destroy(particle, maxTime * 4);
    }

    /// <summary>
    /// Summons the ending part of the AoE attack
    /// and destroys them after the entire effect has been completed
    /// </summary>
    private void EndVfx()
    {
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
        delayStart = false;
    }

    #endregion

}
