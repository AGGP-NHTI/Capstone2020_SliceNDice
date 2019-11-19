using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody playerBody;
    public float walkspeed = 3f;
    public float zSpeed = 1.5f;

    public float rotationspeed;
    public float rotatey;


    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        DetectMovement();
    }

    void DetectMovement()
    {
        playerBody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (walkspeed),
            playerBody.velocity.y, Input.GetAxisRaw("Vertical") * (zSpeed));
    }
}
