using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpScript : MonoBehaviour
{
    public bool isGrounded = false;
    public float jumpForce = 1500.0f;
    public Vector3 jumpValue = new Vector3(0.0f, 2.0f, 0.0f);
    Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(jumpValue * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
