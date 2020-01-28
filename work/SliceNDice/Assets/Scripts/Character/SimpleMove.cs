using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            Weapon w = other.GetComponent<Weapon>();

            if (w.weaponType == BzKnife.WeaponType.Bludgeon)
            {
                rb.AddForce(gameObject.transform.position * 1.5f, ForceMode.Impulse);
            }

            if (playerGuard > 0)
            {
                Instantiate(guardHit, gameObject.transform);
                playerGuard -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }

            if (playerGuard <= 0)
            {
                Instantiate(healthHit, gameObject.transform);
                playerHealth -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }
        }
    }
}
