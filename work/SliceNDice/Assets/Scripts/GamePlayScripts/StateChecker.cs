using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChecker : MonoBehaviour
{
    /*******/
    public GameObject deathPanel;
    private GameObject drawPanel;
    public bool matchWon = false;
    public bool matchP1Won = false;
    public bool matchP2Won = false;
    public bool matchDraw = false;

    GameObject P1 = GameObject.FindGameObjectWithTag("P1");
    GameObject P2 = GameObject.FindGameObjectWithTag("P2");


    void Update()
    {
        while (matchWon)
        {
            if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth > 0)
            {
                matchP2Won = true;
                deathPanel = P1.GetComponent<Character>().characterDeathPanel;
            }

            if (P1.GetComponent<Character>().playerHealth > 0 && P2.GetComponent<Character>().playerHealth <= 0)
            {
                matchP1Won = true;
                deathPanel = P2.GetComponent<Character>().characterDeathPanel;
            }

            if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth <= 0)
            {
                matchDraw = true;
                deathPanel = drawPanel;
            }
        }
    }
}
