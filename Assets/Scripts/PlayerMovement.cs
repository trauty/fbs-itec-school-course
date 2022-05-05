using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;
    public Transform orientation;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpHeight = 10.0f;

    public float sensitivity = 20.0f;
    public float movementSpeed = 10.0f;

    float keyX, keyY;

    float mouseX, mouseY;

    float xRotation, yRotation;

    Vector3 moveDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        keyX = Input.GetAxisRaw("Horizontal");
        keyY = Input.GetAxisRaw("Vertical");
        moveDirection = keyX * orientation.right + keyY * orientation.forward;

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivity;
        xRotation -= mouseY * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        cam.transform.localRotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 0.0f));
        orientation.localRotation = Quaternion.Euler(new Vector3(0.0f, yRotation, 0.0f));

        Jump();
    }
 
    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * movementSpeed);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
        }
    }
}
