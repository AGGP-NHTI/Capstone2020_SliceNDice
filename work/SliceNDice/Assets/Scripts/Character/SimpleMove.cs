using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;

public class SimpleMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    bool setDead;

    public int Build;

    [Range(0, 200)]
    public int playerHealth;

    [Range(0, 100)]
    public int playerGuard;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);

        playerHealth = 50 + (Build * 25);

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            if (playerGuard > 0)
            {
                playerGuard -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }

            if (playerGuard <= 0)
            {
                playerHealth -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            }
        }
    }
}
