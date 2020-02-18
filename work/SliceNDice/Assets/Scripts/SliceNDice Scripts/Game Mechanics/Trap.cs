using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SND.BaseControls;

public class Trap : MonoBehaviour
{
    public float trapDamage;

    Character c;

    public void Update()
    {
        c = GameObject.Find("Player").GetComponent<Character>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Inside a trap!");

            //HitPoints -= Mathf.Round(trapDamage);
        }
    }
}
