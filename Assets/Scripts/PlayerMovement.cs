using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    float keyX, keyY;

    // Update is called once per frame
    void Update()
    {
        keyX = Input.GetAxisRaw("Horizontal");
        keyY = Input.GetAxisRaw("Vertical");
    }
 
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(keyX * 50.0f, 0.0f, keyY * 50.0f));
    }
}
