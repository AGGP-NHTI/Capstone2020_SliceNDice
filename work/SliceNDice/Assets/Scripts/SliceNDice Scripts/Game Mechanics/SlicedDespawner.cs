using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedDespawner : MonoBehaviour
{
    public float lifetime = 4f;

    public void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
