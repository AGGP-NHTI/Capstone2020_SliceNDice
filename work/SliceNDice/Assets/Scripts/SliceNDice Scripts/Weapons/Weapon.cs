using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;

public class Weapon : BzKnife
{
    [Header("Weapon Controls")]
    public int weaponDamage;
    public float weaponWeight;

    public bool isTwoHanded(bool is2h)
    {
        return is2h;
    }
}
