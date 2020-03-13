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

    CharacterControl cc;

    [Range(100f, 1000f)]
    public int jumpForce;                   // Character's jumping force.

    public bool P2;                         // If P2, no movement.

    public CameraControl control;     //Paul and Nick Camera script

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
    bool isTwoHanded;                       // Determines which animations will be used (one-handed or two-handed).
    public Animator anim;
    public GameObject platform;

    /**************************/

    [Header("Hit Effects")]
    public GameObject guardHit;             // Player effect when their Guard is hit.
    public GameObject healthHit;            // Player effect when their Health is hit.
    GameObject bleedParticles;

    void Awake()
    {
        // Stored Movement Variables

        control = GameObject.Find("CameraManager").GetComponent<CameraControl>();

        rb = GetComponent<Rigidbody>();                                         // DO. NOT. DELETE.

        bleedParticles = GetComponent<Sliceable>()._bloodPrefub;

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);     // How heavy/big the character is.

        anim = GetComponent<Animator>();

        cc = GetComponent<CharacterControl>();

        // Stored Statistic Variables
        playerMaxHealth = 50 + (Build * 25);                                    // Calculate Health based upon Build.

        playerHealth = playerMaxHealth;

        playerGuard = 100;                                                      // Guard is always set to 100. No more, no less.
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
        // anim.enabled = false;
        cc.movementSpeed = 0;
        //Destroy(platform);
        control.isCameraFollowing = false;
        
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

        yield return new WaitForSeconds(1);
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
