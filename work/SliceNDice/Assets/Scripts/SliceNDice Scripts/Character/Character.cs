using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicerSamples;
using DestroyIt;

public class Character : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    private Rigidbody rb;                   // Character's Rigidbody.

    public bool isRunning;                  // Bool to determine if they're running or not.

    [Range(0f, 20f)]
    public float moveSpeed;                 // Character's movement speed.

    [Range(100f, 1000f)]
    public int jumpForce;                   // Character's jumping force.

    bool canJump = true;

    private float originalMovementSpeed;    // Stored while running.

    /**************************/

    [Header("Character Statistics")]
    public int Build;                       // Determines character Health. Based upon their physical size.

    [Range(0, 200)]
    public float playerHealth;

    public int playerMaxHealth;

    [Range(0, 100)]
    public int playerGuard;

    /**************************/

    [Header("External Objects")]
    public GameObject weaponPrefab;         // Prefab of weapon chosen.
    public GameObject weaponChosen;         // Weapon character has chosen via character selection.
    public Weapon Weapon;                   // Character's weapon.
    public GameObject spawnLoc;             // Where the weapon is spawned. Put this in the character's hand.

    [SerializeField]
    List<GameObject> children;              // This is only used to detach children from the GameObject upon death.

    bool isTwoHanded;                       // Determines which animations will be used (one-handed or two-handed).

    /**************************/

    [Header("Hit Effects")]
    public GameObject guardHit;             // Player effect when their Guard is hit.
    public GameObject healthHit;            // Player effect when their Health is hit.

    void Awake()
    {
        // Stored Movement Variables

        rb = GetComponent<Rigidbody>();                                         // DO. NOT. DELETE.

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);     // How heavy/big the character is.

        Instantiate(weaponPrefab, spawnLoc.transform);                          // Instantiate the weapon into the world.

        weaponChosen = GetComponentInChildren<Weapon>().gameObject;             // Gets the weapon game object so it can be edited.

        Weapon = weaponChosen.GetComponentInChildren<Weapon>();                 // Gets the weapon's "Weapon" script.

        // If weapon isn't null, alter move speed based on weapon weight vs. character's build. Also determines two-handed.
        if (Weapon != null)
        {
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

            if (Weapon.weaponWeight > Build)
            {
                isTwoHanded = Weapon.isTwoHanded(true);     // Use two-handed animations.
            }

            if (Weapon.weaponWeight < Build)
            {
                isTwoHanded = Weapon.isTwoHanded(false);    // Use one-handed animations.
            }
        }

        originalMovementSpeed = moveSpeed;                                      // Important for running.

        // Stored Statistic Variables
        playerMaxHealth = 50 + (Build * 25);                                    // Calculate Health based upon Build.

        playerHealth = playerMaxHealth;

        playerGuard = 100;                                                      // Guard is always set to 100. No more, no less.
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
            playerHealth = 0;
            playerGuard = 0;

            Debug.LogWarning("KnifeSliceableAsync is disabled despite the character being dead. If it's a slash weapon, it's been commented out. Be sure to check the code if this is unintended.");

            // GetComponent<KnifeSliceableAsync>().enabled = true;
        }

        playerGuard += Mathf.CeilToInt(Time.deltaTime);

        if (playerGuard >= 100 && playerHealth > 0)
        {
            playerGuard = 100;
        }

        if (playerGuard <= 0)
        {
            playerGuard = 0;
        }
    }

    private void LateUpdate()
    {
        ErrorChecker();     // Error checker makes sure that nothing is missing or going wrong. Displays errors.
    }

    public void MovementControls()
    {
        // Movement Control (FOR TESTING ONLY, THIS WILL BE REPLACED)
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
    }

    void IsDead()
    {
        moveSpeed = 0;                  // Ensures they can't move anymore.
        originalMovementSpeed = 0;      // Ensures they can't move anymore.

        rb.freezeRotation = false;      // Allows them to fall over at any angle.


        // The following below is to detach child objects, so weapons and the like fall to the ground.
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);     // Adds the game objects that are a child to the character.
        }

        Debug.Log(children.Count);

        for (int i = 0; i < children.Count; i++)
        {
            children[i].AddComponent<Rigidbody>();  // Adds a rigidbody so the objects won't fall through the ground.
        }

        gameObject.transform.DetachChildren();      // Finally, detaches all children from the character.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            if (playerGuard > 0)
            {
                // Guard was hit, reduce guard.
                Instantiate(guardHit);
                playerGuard -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }

            if (playerGuard <= 0)
            {
                // Health was hit, reduce health.
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

    IEnumerator SwingSword()    // This will be altered to not move the weapon after animations are implemented.
    {
        var transformB = Weapon.transform.parent;
        transformB.position = gameObject.transform.position;
        transformB.rotation = gameObject.transform.rotation;

        float seconds = (4 - (moveSpeed / Build)) * 0.125f;     // Attack speed. Should be altered with animations.

        if (seconds <= 0)
        {
            seconds = 0.05f;
        }

        playerGuard -= 25;      // Characters attacking reduce Guard gradually.

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

        if (!Weapon)
        {
            Debug.LogWarning("Weapon is not assigned to the character. Attacking will not work.");
        }

        if (moveSpeed == 0)
        {
            Debug.LogWarning("Move speed has been set to 0. Unless the character is dead, this is unintended.");
        }
    }
}
