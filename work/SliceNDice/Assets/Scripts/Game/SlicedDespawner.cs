using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedDespawner : MonoBehaviour
{
    public float lifetime = 0;

    private void Awake()
    {
        lifetime = gameObject.GetComponent<Rigidbody>().mass * 50f;
    }

    private void Start()
    {

    }

    public void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
