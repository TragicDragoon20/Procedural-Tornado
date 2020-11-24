using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private GameObject mesh;

    [SerializeField]
    private float maxTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            particle.Play();
            GameObject cylinder = Instantiate(mesh, this.transform.position, Quaternion.identity);
            cylinder.GetComponent<Renderer>().material.SetFloat("Vector1_659CD848", Time.time);
            Debug.Log(cylinder.GetComponent<Renderer>().material.GetFloat("Vector1_659CD848"));
            Destroy(cylinder, maxTime);
        }

    }
}
