using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;
using DestroyIt;

public class SimpleMove : MonoBehaviour
{
    [Header("Standard")]
    public Rigidbody rb;
    public float speed;
    bool setDead;
    public int Build;

    [Range(0, 200)]
    public int playerHealth;

    [Range(0, 100)]
    public int playerGuard;

    [Header("Hit Effects")]
    public GameObject guardHit;
    public GameObject healthHit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);

        playerHealth = 50 + (Build * 25);

        playerGuard = 100;

        speed = 2;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (setDead)
        {
            speed = 0;
        }

        if (playerHealth <= 0)
        {
            setDead = true;

            if (GetComponent<KnifeSliceableAsync>() != null)
            {
                Debug.LogWarning("KnifeSliceableAsync is disabled despite the character being dead. If it's a slash weapon, it's been commented out. Be sure to check the code if this is unintended.");

                // GetComponent<KnifeSliceableAsync>().enabled = true;
            }
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

            rb.freezeRotation = false;
        }
    }

    private void LateUpdate()
    {
        ErrorChecker();
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
                    Instantiate(guardHit, gameObject.transform);
                    playerGuard -= w.weaponDamage;
                }

                if (playerGuard <= 0)
                {
                    Instantiate(healthHit, gameObject.transform);
                    playerHealth -= w.weaponDamage;
                }

                Debug.Log("Slash to Guard/Health: " + w.weaponDamage);
            }


            if (w.weaponType == BzKnife.WeaponType.Bludgeon)    // Damage with Bludgeoning Weapon
            {
                rb.AddForce(w.BladeDirection * 1.25f, ForceMode.Impulse);

                speed -= 0.01f;

                if (playerGuard > 0)
                {
                    Instantiate(guardHit, gameObject.transform);
                    playerGuard -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                }

                if (playerGuard <= 0)
                {
                    Instantiate(healthHit, gameObject.transform);
                    playerHealth -= Mathf.CeilToInt(w.weaponDamage * 2f);
                    gameObject.GetComponent<Destructible>().currentHitPoints -= Mathf.CeilToInt(w.weaponDamage * 2f);
                }
            }


            if (w.weaponType == BzKnife.WeaponType.Pierce)    // Damage with Piercing Weapon
            {
                rb.AddForceAtPosition(w.BladeDirection * 4f, gameObject.transform.position, ForceMode.Impulse);

                if (playerGuard > 0)
                {
                    Instantiate(guardHit, gameObject.transform);
                    playerGuard -= Mathf.CeilToInt(w.weaponDamage * 2f);
                }

                if (playerGuard <= 0)
                {
                    Instantiate(healthHit, gameObject.transform);
                    playerHealth -= Mathf.CeilToInt(w.weaponDamage * 0.5f);
                }

                Debug.Log("Pierce to Guard: " + Mathf.CeilToInt(w.weaponDamage * 2f));
                Debug.Log("Pierce to Health: " + Mathf.CeilToInt(w.weaponDamage * 0.5f));
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