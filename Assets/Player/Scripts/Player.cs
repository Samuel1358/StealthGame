using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float turnSpeed = 20;
    [SerializeField] private float smoothMoveTime = 0.05f;

    private float angle;
    private float smoothInput;
    private float smoothMoveVelocity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    private void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        float inputMagnetude = inputDirection.magnitude;
        smoothInput = Mathf.SmoothDamp(smoothInput, inputMagnetude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, turnSpeed * inputMagnetude * Time.deltaTime);
        //transform.eulerAngles = Vector3.up * angle;
        //
        //transform.Translate(transform.forward * moveSpeed * smoothInput * Time.deltaTime, Space.World);
        rb.velocity = transform.forward * moveSpeed * smoothInput;
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rb.MovePosition(rb.position + rb.velocity * Time.deltaTime);
    }
}
