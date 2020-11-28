using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float movementSpeed;
    private Vector3 moveDir;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float mH = Input.GetAxisRaw("Horizontal");
        float mV = Input.GetAxisRaw("Vertical");

        moveDir = (mH * this.transform.right + mV * this.transform.forward);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(this.transform.position + moveDir.normalized * movementSpeed * Time.deltaTime);
    }
}
