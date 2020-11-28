using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoPull : MonoBehaviour
{
    [SerializeField]
    private Transform pullCentre = null;
    [SerializeField]
    private float pullRadius;
    [SerializeField]
    private float pullForce;

    [SerializeField]
    private LayerMask layer;

    private float refreshRate = 4f;

    private void Update()
    {
        // Gets pull able objects within the given radius
        Collider[] colliders = Physics.OverlapSphere(pullCentre.position, pullRadius, layer);
        for (int i = 0; i < colliders.Length; i++)
        {
            StartCoroutine(pull(colliders[i]));
        }
    }

    /// <summary>
    /// Adds the force to the objects within the radius
    /// </summary>
    /// <param name="co">The object to add force to</param>
    /// <returns></returns>
    IEnumerator pull(Collider co)
    {
        Vector3 forceDir = pullCentre.position - co.transform.position;
        co.GetComponent<Rigidbody>().AddForce(forceDir.normalized * pullForce * Time.deltaTime, ForceMode.Acceleration);
        yield return refreshRate;
        StartCoroutine(pull(co));
    }
}
