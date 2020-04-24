using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicerSamples;
using DestroyIt;
using System.Reflection;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Sliceable))]
[RequireComponent(typeof(Destructible))]
[RequireComponent(typeof(AdderSliceableAsync))]
public class Character : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    Rigidbody rb;                           // Character's Rigidbody.

    [SerializeField]
    CharacterControl cc;                    // Paul and Nick's Character script

    public CameraControl control;           // Paul and Nick's Camera script

    [Range(100f, 1000f)]
    public int jumpForce;                   // Character's jumping force.

    public bool isRunning;                  // Bool to determine if they're running or not.

    /**************************/

    [Header("Character Statistics")]
    public int Build;                       // Determines character Health. Based upon their physical size.

    [Range(0, 200)]
    public float playerHealth;

    public int playerMaxHealth;

    [Range(0, 100)]
    public float playerGuard;
    public float playerMaxGuard;


    /**************************/

    [Header("External Objects")]
    [SerializeField]
    bool isTwoHanded;                       // Determines which animations will be used (one-handed or two-handed).
    public Animator anim;
    public GameObject platform;
    public Sprite characterWinPanel;
    public Sprite characterDeathPanel;

    /**************************/

    [Header("Hit Effects")]
    public GameObject guardHit;             // Player effect when their Guard is hit.
    public GameObject healthHit;            // Player effect when their Health is hit.
    GameObject bleedParticles;

    void Awake()
    {
        // Stored Statistic Variables
        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);     // How heavy/big the character is.
        playerMaxHealth = 50 + (Build * 25);                                    // Calculate Health based upon Build.
        playerHealth = playerMaxHealth;                                         // Player Health = Max Health at start.
        playerMaxGuard = 100;
        playerGuard = 100;                                                      // Guard is always set to 100. No more, no less.

        // Stored Movement Variables
        rb = GetComponent<Rigidbody>();                                         // DO. NOT. DELETE.

        bleedParticles = GetComponent<Sliceable>()._bloodPrefub;
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterControl>();

        // Set up Destructible
        gameObject.GetComponent<Destructible>().totalHitPoints = playerMaxHealth;
        gameObject.GetComponent<Destructible>().currentHitPoints = playerMaxHealth;
    }

    void Start()
    {
        if (cc.P1)
        {
            gameObject.AddComponent<Player1Object>();
            platform.AddComponent<Player1Object>();
            cc.Wep.AddComponent<Player1Object>();

            if (cc.WepOff)
            {
                cc.WepOff.AddComponent<Player1Object>();
            }
        }
       
        if (!cc.P1)
        {
            gameObject.AddComponent<Player2Object>();
            platform.AddComponent<Player2Object>();
            cc.Wep.AddComponent<Player2Object>();

            if (cc.WepOff)
            {
                cc.WepOff.AddComponent<Player2Object>();
            }
        }
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (playerHealth <= 0)
        {
            IsDead();
        }

        playerGuard += 0.3f;

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
        playerHealth = 0;
        playerGuard = 0;

        rb.freezeRotation = false;      // Allows them to fall over at any angle.
        GetComponent<AdderSliceableAsync>().enabled = true;
        cc.movementSpeed = 0;
        cc.Weapon.transform.parent = null;
        control.isCameraFollowing = false;

        GameObject.Find("DeathPanel").SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            Weapon w = other.GetComponent<Weapon>();

            if (cc.P1)
            {
                if (!w.gameObject.transform.parent.gameObject.GetComponent<Player1Object>())
                {
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
                        if (playerGuard > 0)
                        {
                            Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerGuard -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                        }

                        if (playerGuard <= 0)
                        {
                            Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerHealth -= Mathf.CeilToInt(w.weaponDamage * 2f);
                            gameObject.GetComponent<Destructible>().currentHitPoints -= w.weaponDamage;
                        }
                    }

                    if (w.weaponType == BzKnife.WeaponType.Pierce)    // Damage with Piercing Weapon
                    {
                        if (playerGuard > 0)
                        {
                            Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerGuard -= Mathf.CeilToInt(w.weaponDamage * 2f);
                        }

                        if (playerGuard <= 0)
                        {
                            Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerHealth -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                        }
                    }
                }

            }

            if (!cc.P1)
            {
                if (!w.gameObject.transform.parent.gameObject.GetComponent<Player2Object>())
                {
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
                        if (playerGuard > 0)
                        {
                            Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerGuard -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                        }

                        if (playerGuard <= 0)
                        {
                            Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerHealth -= Mathf.CeilToInt(w.weaponDamage * 2f);
                            gameObject.GetComponent<Destructible>().currentHitPoints -= w.weaponDamage;
                        }
                    }

                    if (w.weaponType == BzKnife.WeaponType.Pierce)    // Damage with Piercing Weapon
                    {
                        if (playerGuard > 0)
                        {
                            Instantiate(guardHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerGuard -= Mathf.CeilToInt(w.weaponDamage * 2f);
                        }

                        if (playerGuard <= 0)
                        {
                            Instantiate(healthHit, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                            playerHealth -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                        }
                    }
                }
            }

        }
    }

    IEnumerator SwingSword()    // This will be altered to not move the weapon after animations are implemented.
    {
        playerGuard -= 10;      // Characters attacking reduce Guard gradually.

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
