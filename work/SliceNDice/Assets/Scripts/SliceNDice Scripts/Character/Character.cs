using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicerSamples;
using DestroyIt;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Sliceable))]
[RequireComponent(typeof(Destructible))]
[RequireComponent(typeof(AdderSliceableAsync))]
public class Character : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    Rigidbody rb;                           // Character's Rigidbody.

    public bool isRunning;                  // Bool to determine if they're running or not.

    [Range(100f, 1000f)]
    public int jumpForce;                   // Character's jumping force.

    bool canJump = true;

    float originalMovementSpeed;            // Stored while running.

    public bool P2;                         // If P2, no movement.

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
    //public GameObject weaponPrefab;         // Prefab of weapon chosen.
    //public GameObject weaponChosen;         // Weapon character has chosen via character selection.
    //public Weapon Weapon;                   // Character's weapon.
    public GameObject spawnLoc;             // Where the weapon is spawned. Put this in the character's hand.

    [SerializeField]
    List<GameObject> children;              // This is only used to detach children from the GameObject upon death.

    bool isTwoHanded;                       // Determines which animations will be used (one-handed or two-handed).

    /**************************/

    [Header("Hit Effects")]
    public GameObject guardHit;             // Player effect when their Guard is hit.
    public GameObject healthHit;            // Player effect when their Health is hit.
    GameObject bleedParticles;

    void Awake()
    {
        // Stored Movement Variables

        rb = GetComponent<Rigidbody>();                                         // DO. NOT. DELETE.

        bleedParticles = GetComponent<Sliceable>()._bloodPrefub;

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);     // How heavy/big the character is.

        // Stored Statistic Variables
        playerMaxHealth = 50 + (Build * 25);                                    // Calculate Health based upon Build.

        playerHealth = playerMaxHealth;

        playerGuard = 100;                                                      // Guard is always set to 100. No more, no less.

        if (P2)
        {
            // moveSpeed = 0;
            originalMovementSpeed = 0;
        }
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (playerHealth <= 0)
        {
            IsDead();
            playerHealth = 0;
            playerGuard = 0;

            GetComponent<AdderSliceableAsync>().enabled = true;
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

    void IsDead()
    {
        rb.freezeRotation = false;      // Allows them to fall over at any angle.


        // The following below is to detach child objects, so weapons and the like fall to the ground.
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);     // Adds the game objects that are a child to the character.
        }

        Debug.Log(children.Count);

        /*for (int i = 0; i < children.Count; i++)
        {
            children[i].AddComponent<Rigidbody>();  // Adds a rigidbody so the objects won't fall through the ground.
        }*/

        // gameObject.transform.DetachChildren();      // Finally, detaches all children from the character.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            Weapon w = other.GetComponent<Weapon>();

            if (w.weaponType == BzKnife.WeaponType.Slash)       // Damage with Slashing Weapon
            {
                if (playerGuard > 0)
                {
                    Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    playerGuard -= w.weaponDamage;
                }

                if (playerGuard <= 0)
                {
                    Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    playerHealth -= w.weaponDamage;

                }

                Debug.Log("Slash to Guard/Health: " + w.weaponDamage);
            }

            if (w.weaponType == BzKnife.WeaponType.Bludgeon)    // Damage with Bludgeoning Weapon
            {
                rb.AddForce(w.BladeDirection * 1.25f, ForceMode.Impulse);

                // moveSpeed -= 0.04f;

                if (playerGuard > 0)
                {
                    Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    playerGuard -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                }

                if (playerGuard <= 0)
                {
                    Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    playerHealth -= Mathf.CeilToInt(w.weaponDamage * 2f);
                    gameObject.GetComponent<Destructible>().currentHitPoints -= Mathf.CeilToInt(w.weaponDamage * 2f);

                }
            }

            if (w.weaponType == BzKnife.WeaponType.Pierce)    // Damage with Piercing Weapon
            {
                rb.AddForceAtPosition(w.BladeDirection * 4f, gameObject.transform.position, ForceMode.Impulse);

                if (playerGuard > 0)
                {
                    Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    playerGuard -= Mathf.CeilToInt(w.weaponDamage * 2f);
                }

                if (playerGuard <= 0)
                {
                    Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    playerHealth -= Mathf.CeilToInt(w.weaponDamage * 0.5f);

                    Vector3 randomHitLoc = new Vector3(Random.Range(0, .1f), Random.Range(0, .1f), Random.Range(0, .1f));

                    Instantiate(bleedParticles, randomHitLoc, Quaternion.identity, gameObject.transform);
                }

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
        // float seconds = (4 - (moveSpeed / Build)) * 0.125f;     // Attack speed. Should be altered with animations. Modify later!

        playerGuard -= 25;      // Characters attacking reduce Guard gradually.

        yield return null;
    }

    void ErrorChecker()
    {
        if (!GetComponent<AdderSliceableAsync>())
        {
            Debug.LogWarning("AdderSliceableAsync is missing! (This is what allows things to be cut.)");
        }

        if (!rb)
        {
            Debug.LogWarning("This needs a Rigidbody to work.");
        }
    }
}
