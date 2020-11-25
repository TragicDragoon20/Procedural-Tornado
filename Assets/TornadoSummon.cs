using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSummon : MonoBehaviour
{
    [SerializeField]
    private float range = 10.0f;
    private Vector3 spawnPos;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private GameObject tornado;

    private GameObject tornadoInst;

    [SerializeField]
    private float duration = 5.0f;

    private bool canSpawn = true;

    void Update()
    {
        TornadoCheck();
    }

    private void TornadoCheck()
    {
        if (tornadoInst == null)
        {
            canSpawn = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && canSpawn)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, range, layer))
            {
                spawnPos = hit.point;
                canSpawn = false;
                SpawnTornado();
            }
        }
    }

    private void SpawnTornado()
    {
        tornadoInst = Instantiate(tornado, spawnPos, Quaternion.identity);
        Destroy(tornadoInst, duration);
        
    }
}
