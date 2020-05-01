using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedDespawner : MonoBehaviour
{
    public float lifetime = 1.5f;

    public void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
