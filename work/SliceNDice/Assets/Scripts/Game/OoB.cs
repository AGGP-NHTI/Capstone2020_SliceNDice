using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoB : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;


    public void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("P1");
        player2 = GameObject.FindGameObjectWithTag("P2");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1.GetComponent<Character>().playerGuard = 0;
            player1.GetComponent<Character>().playerHealth = 0;

            player1.transform.position = new Vector3(0, 1, 0);
        }

        if (other.gameObject == player2)
        {
            if (player2.GetComponent<Character>())
            {
                player2.GetComponent<Character>().playerGuard = 0;
                player2.GetComponent<Character>().playerHealth = 0;
            }

            if (!player2.GetComponent<Character>())
            {
                player2.GetComponent<SimpleMove>().playerGuard = 0;
                player2.GetComponent<SimpleMove>().playerHealth = 0;
            }

            player2.transform.position = new Vector3(0, 1, 0);
        }
    }
}