using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SND.BaseControls;

public class Trap : MonoBehaviour
{
    public float trapDamage;

    CharacterControls c;

    public void Update()
    {
        c = GameObject.Find("Player").GetComponent<CharacterControls>();
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
