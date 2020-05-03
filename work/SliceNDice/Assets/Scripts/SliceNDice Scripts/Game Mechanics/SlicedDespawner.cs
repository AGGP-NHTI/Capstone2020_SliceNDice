using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedDespawner : MonoBehaviour
{
    public float lifetime = 5;

    public void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
