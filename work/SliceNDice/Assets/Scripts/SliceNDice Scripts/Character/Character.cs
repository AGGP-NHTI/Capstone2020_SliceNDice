using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicerSamples;
using DestroyIt;

public class Character : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    private Rigidbody rb;   // Character's Rigidbody.

    public bool isRunning;

    [Range(0f, 20f)]
    public float moveSpeed;     // Character's movement speed.

    [Range(100f, 1000f)]
    public int jumpForce;       // Character's jumping force.

    bool canJump = true;

    private float originalMovementSpeed;    // Stored while running.

    /**************************/

    [Header("Character Statistics")]
    public int Build;

    [Range(0, 200)]
    public int playerHealth;

    [Range(0, 100)]
    public int playerGuard;

    /**************************/

    [Header("External Objects")]
    public Weapon Weapon;
    [SerializeField]
    List<GameObject> children;      // This is only used to detach children from the GameObject upon death.

    /**************************/

    [Header("Hit Effects")]
    public GameObject guardHit;
    public GameObject healthHit;

    void Awake()
    {
        // Stored Movement Variables

        rb = GetComponent<Rigidbody>();

        Weapon = gameObject.GetComponentInChildren<Weapon>();

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);

        if (Weapon.weaponWeight > Build)
        {
            moveSpeed = (5 - (Weapon.weaponWeight - Build) * 1.5f);

            if (moveSpeed <= 0)
            {
                moveSpeed = 0.5f;
            }
        }
        else
        {
            moveSpeed = (5 - Build) * 1.5f;
        }

        originalMovementSpeed = moveSpeed;

        // Stored Statistic Variables
        playerHealth = 50 + (Build * 25);
        playerGuard = 100;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        MovementControls();

        if (Input.GetMouseButtonDown(0))
        {
            var knifeSlice = Weapon.GetComponentInChildren<BzKnife>();
            knifeSlice.BeginNewSlice();

            StartCoroutine(SwingSword());
        }

        if (playerHealth <= 0)
        {
            IsDead();

            Debug.LogWarning("KnifeSliceableAsync is disabled despite the character being dead. If it's a slash weapon, it's been commented out. Be sure to check the code if this is unintended.");

            // GetComponent<KnifeSliceableAsync>().enabled = true;
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

    private void LateUpdate()
    {
        ErrorChecker();
    }

    public void MovementControls()
    {
        // Movement Control
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, 0);

        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        // Jump Control
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(0, jumpForce, 0);

            playerGuard = 0;    // Characters cannot guard while jumping.

            StartCoroutine(JumpCoroutine());
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

    void IsDead()
    {
        moveSpeed = 0;
        originalMovementSpeed = 0;

        rb.freezeRotation = false;

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        Debug.Log(children.Count);

        for (int i = 0; i < children.Count; i++)
        {
            children[i].AddComponent<Rigidbody>();
        }

        gameObject.transform.DetachChildren();
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

    IEnumerator JumpCoroutine()
    {
        // Serves as the "cool down" for Jump.

        canJump = false;
        yield return new WaitForSeconds(1);
        canJump = true;
    }

    IEnumerator SwingSword()
    {
        var transformB = Weapon.transform.parent;
        transformB.position = gameObject.transform.position;
        transformB.rotation = gameObject.transform.rotation;

        float seconds = (4 - (moveSpeed / Build)) * 0.125f;

        if (seconds <= 0)
        {
            seconds = 0.05f;
        }

        playerGuard -= 25;

        if (Weapon.weaponType == BzKnife.WeaponType.Slash)
        {
            for (float f = 0f; f < seconds; f += Time.deltaTime)
            {
                float aY = (f / seconds) * 180 - 90;
                float aX = (f / seconds) * 60 - 30;

                var r = Quaternion.Euler(aX, -aY, 0);

                transformB.rotation = gameObject.transform.rotation * r;
                yield return null;
            }
        }

        if (Weapon.weaponType == BzKnife.WeaponType.Bludgeon)
        {
            for (float f = 0f; f < seconds; f += Time.deltaTime)
            {
                float aX = (f / seconds) * 60 - 55;

                var r = Quaternion.Euler(aX + 1f, 0, 0);

                transformB.rotation = gameObject.transform.rotation * r;
                yield return null;
            }
        }

        if (Weapon.weaponType == BzKnife.WeaponType.Pierce)
        {
            for (float f = 0f; f < seconds; f += Time.deltaTime)
            {
                var r = Quaternion.Euler(15, 0, 0);

                transformB.Translate(0, 0.025f, 0.03f);

                transformB.rotation = gameObject.transform.rotation * r;

                Debug.Log("Pierce Position Result: " + transformB.position);
                Debug.Log("Pierce Rotation: " + transformB.position);

                yield return null;
            }
        }
    }

    void ErrorChecker()
    {
        if (!GetComponent<KnifeSliceableAsync>())
        {
            Debug.LogWarning("KnifeSliceableAsync is missing! (This is what allows things to be cut.)");
        }

        if (!rb)
        {
            Debug.LogWarning("This needs a Rigidbody to work.");
        }
    }

}
