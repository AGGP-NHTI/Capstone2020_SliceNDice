using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoB : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;

            player.GetComponent<Character>().playerGuard = 0;
            player.GetComponent<Character>().playerHealth = 0;

            player.transform.position = new Vector3(0, 1, 0);
        }
    }
}