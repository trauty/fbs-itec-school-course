using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public Transform orientation;

    public float wallDistance = 0.6f;
    public float wallMinJumpHeight = 1.5f;
    public float wallRunJumpForce = 15.0f;
    public float wallRunGravity = 2.0f;
    public LayerMask wallLayer;

    [Header("Camera Effects")]
    public Camera cam;
    public float camTilt = 15.0f;
    public float camTiltTime = 25.0f;
    public float tilt = 0.0f;

    public Rigidbody rb;

    public bool isWallRunning = false;

    public bool isWallLeft, isWallRight;

    RaycastHit wallLeftHit, wallRightHit;
    
    void Update()
    {
        CheckWall();

        if (ReadyToWallRun() && (isWallLeft || isWallRight))
        {
            StartWallRun();
        }
        else
        {
            StopWallRun();
        }
    }

    private void CheckWall()
    {
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, out wallLeftHit, wallDistance, wallLayer);
        isWallRight = Physics.Raycast(transform.position, orientation.right, out wallRightHit, wallDistance, wallLayer);
    }

    private bool ReadyToWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, wallMinJumpHeight);
    }

    private void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        if (isWallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if (isWallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isWallLeft)
            {
                Vector3 wallJumpDirection = transform.up + wallLeftHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                rb.AddForce(wallJumpDirection * wallRunJumpForce, ForceMode.VelocityChange);
            }
            else if (isWallRight)
            {
                Vector3 wallJumpDirection = transform.up + wallRightHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                rb.AddForce(wallJumpDirection * wallRunJumpForce, ForceMode.VelocityChange);
            }
        }

        isWallRunning = true;
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;

        tilt = Mathf.Lerp(tilt, 0.0f, camTiltTime * Time.deltaTime);
    }
}
