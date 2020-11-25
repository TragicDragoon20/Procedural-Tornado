using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    [SerializeField]
    private float movementRadius = 30.0f;
    [SerializeField]
    private float movementSpeed = 0.5f;

    private Vector3 newPos;
    private Vector3 originalPos;
    private bool getNewPos;

    private void Start()
    {
        originalPos = this.transform.position;
        getNewPos = true;
    }


    private void Update()
    {
        if (getNewPos)
        {
            newPos = new Vector3(originalPos.x + Random.Range(-movementRadius, movementRadius), originalPos.y,
                originalPos.z + Random.Range(-movementRadius, movementRadius));
            Debug.Log(newPos);
            getNewPos = false;
        }
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, movementSpeed);

        if (Vector3.Distance(this.transform.position, newPos) <= 1.5)
        {
            getNewPos = true;
        }

    }
}
