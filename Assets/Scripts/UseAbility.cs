using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particle;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !particle[0].isPlaying)
        {
            StartVfx();
            currentTimer = delayTime;
            delayStart = true;
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
        particle[0].Play();
        GameObject cylinder = Instantiate(cylinderMesh, this.transform.position, Quaternion.identity);
        GameObject quad = Instantiate(quadMesh, this.transform.position, Quaternion.identity);
        cylinder.GetComponent<Renderer>().material.SetFloat("Vector1_659CD848", Time.time);
        cylinder.GetComponent<Renderer>().material.SetFloat("CompTime", maxTime);
        quad.GetComponent<Renderer>().material.SetFloat("StartTime", Time.time);
        quad.GetComponent<Renderer>().material.SetFloat("CompTime", maxTime);
        Destroy(cylinder, maxTime);
        Destroy(quad, maxTime);
    }

    private void EndVfx()
    {
        delayStart = false;
        particle[1].Play();
        GameObject cylinder = Instantiate(cylinderMesh, this.transform.position, Quaternion.identity);
        GameObject quad = Instantiate(quadMesh, this.transform.position, Quaternion.identity);
        cylinder.GetComponent<Renderer>().material.SetFloat("Vector1_659CD848", Time.time);
        cylinder.GetComponent<Renderer>().material.SetFloat("CompTime", 0.0f);
        cylinder.GetComponent<Renderer>().material.SetFloat("Speed", explodeSpeed);
        quad.GetComponent<Renderer>().material.SetFloat("StartTime", Time.time);
        quad.GetComponent<Renderer>().material.SetFloat("CompTime", 0.0f);
        quad.GetComponent<Renderer>().material.SetFloat("Speed", explodeSpeed);
        Destroy(cylinder, maxTime);
        Destroy(quad, maxTime);
    }
}
