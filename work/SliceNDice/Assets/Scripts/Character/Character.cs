using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicerSamples;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    Rigidbody rb;
    public bool isGrounded;
    public bool isRunning;
    public int JumpForce;
    private float originalMovementSpeed;    // Stored while running.

    [Header("Character Statistics")]
    public int Build;

    [Range(0, 200)]
    public int playerHealth;

    [Range(0, 100)]
    public int playerGuard;

    [Range(0f, 20f)]
    public float moveSpeed;

    [Range(100f, 1000f)]
    public int jumpForce;

    [Header("External Objects")]
    public GameObject knife;
    [SerializeField]
    private GameObject _blade;

    [Header("Hit Effects")]
    public GameObject guardHit;
    public GameObject healthHit;

    void Awake()
    {
        // Stored Movement Variables
        rb = GetComponent<Rigidbody>();
        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);

        moveSpeed = (5 - Build) * 1.5f;

        originalMovementSpeed = moveSpeed;
        JumpForce = jumpForce;

        // Stored Statistic Variables
        playerHealth = 50 + (Build * 25);
        playerGuard = 100;
        knife = GameObject.Find("Knife");
    }

    void Update()
    {
        MovementControls();

        if (Input.GetMouseButtonDown(0))
        {
            var knife = _blade.GetComponentInChildren<BzKnife>();
            knife.BeginNewSlice();

            StartCoroutine(SwingSword());
        }

        if (playerHealth <= 0)
        {
            IsDead();
            GetComponent<KnifeSliceableAsync>().enabled = true;
        }

        playerGuard += Mathf.CeilToInt(Time.deltaTime);

        if (playerGuard >= 100)
        {
            playerGuard = 100;
        }

        if (playerGuard <= 0)
        {
            playerGuard = 0;
        }

        if (playerHealth <= 0)
        {
            playerGuard = 0;
        }
    }

    public void MovementControls()
    {
        // Movement Control
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, 0);

        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        // Jump Control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(JumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed * 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = originalMovementSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.Rotate(0, 2 * moveSpeed, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.Rotate(0, -2 * moveSpeed, 0);
        }

        if (Input.GetKey(KeyCode.C))
        {
            gameObject.GetComponent<CapsuleCollider>().height = 1;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            gameObject.GetComponent<CapsuleCollider>().height = 2;
        }
    }

    void Jump(int jumpForce)
    {
        if (isGrounded)
        {
            rb.AddForce(0, jumpForce, 0);
            playerGuard = 0;
            isGrounded = false;
        }
    }

    void IsDead()
    {
        moveSpeed = 0;
        originalMovementSpeed = 0;

        rb.freezeRotation = false;


        Destroy(_blade);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            if (playerGuard > 0)
            {
                Instantiate(guardHit);
                playerGuard -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }

            if (playerGuard <= 0)
            {
                Instantiate(healthHit);
                playerHealth -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }
        }
    }



    IEnumerator SwingSword()
    {
        var transformB = _blade.transform;
        transformB.position = gameObject.transform.position;
        transformB.rotation = gameObject.transform.rotation;

        float seconds = (4 - (moveSpeed / Build)) * 0.125f;

        if (seconds <= 0)
        {
            seconds = 0.05f;
        }

        playerGuard -= 25;

        for (float f = 0f; f < seconds; f += Time.deltaTime)
        {
            float aY = (f / seconds) * 180 - 90;
            float aX = (f / seconds) * 60 - 30;
            //float aX = 0;

            var r = Quaternion.Euler(aX, -aY, 0);

            transformB.rotation = gameObject.transform.rotation * r;
            yield return null;
        }
    }
}
