using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public Transform orientation;

    public float wallDistance = 0.6f;
    public float wallMinJumpHeight = 1.5f;
    public float wallRunForce = 15.0f;
    public float wallRunGravity = 2.0f;
    public LayerMask wallLayer;

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
        isWallLeft = Physics.Raycast(transform.position, -transform.right, out wallLeftHit, wallDistance, wallLayer);
        isWallRight = Physics.Raycast(transform.position, transform.right, out wallRightHit, wallDistance, wallLayer);
    }

    private bool ReadyToWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, wallMinJumpHeight);
    }

    private void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
    }
}
