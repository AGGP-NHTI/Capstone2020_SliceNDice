using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoB : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("P1");
        player2 = GameObject.FindGameObjectWithTag("P2");
        player3 = GameObject.FindGameObjectWithTag("P3");
        player4 = GameObject.FindGameObjectWithTag("P4");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1.transform.position = new Vector3(0, 1, 0);
        }

        if (other.gameObject == player2)
        {
            player2.transform.position = new Vector3(0, 1, 0);
        }

        if (other.gameObject == player3)
        {
            player3.transform.position = new Vector3(0, 1, 0);
        }

        if (other.gameObject == player4)
        {
            player4.transform.position = new Vector3(0, 1, 0);
        }
    }


}