using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;
using DestroyIt;

public class BreakableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
        {
            Weapon w = other.GetComponent<Weapon>();

            if (w.weaponType == BzKnife.WeaponType.Slash)       // Damage with Slashing Weapon
            {
                gameObject.GetComponent<Destructible>().currentHitPoints -= Mathf.CeilToInt(w.weaponDamage / 1.25f);
            }


            if (w.weaponType == BzKnife.WeaponType.Bludgeon)    // Damage with Bludgeoning Weapon
            {
                gameObject.GetComponent<Destructible>().currentHitPoints -= Mathf.CeilToInt(w.weaponDamage * 2f);
            }


            if (w.weaponType == BzKnife.WeaponType.Pierce)    // Damage with Piercing Weapon
            {
                gameObject.GetComponent<Destructible>().currentHitPoints -= Mathf.CeilToInt(w.weaponDamage / 4f);
            }
        }
    }
}
