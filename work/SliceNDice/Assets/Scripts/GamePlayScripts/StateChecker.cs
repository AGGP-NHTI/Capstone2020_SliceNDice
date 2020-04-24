using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateChecker : MonoBehaviour
{
    /*******/
    public Image winPanel;
    public Image deathPanel;
    public bool matchP1Won = false;
    public bool matchP2Won = false;
    public bool matchDraw = false;

    GameObject P1;
    GameObject P2;

    void Update()
    {
        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");

        if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth > 0)
        {
            matchP2Won = true;
            winPanel.sprite = P2.GetComponent<Character>().characterWinPanel;
            deathPanel.sprite = P1.GetComponent<Character>().characterDeathPanel;
        }

        if (P1.GetComponent<Character>().playerHealth > 0 && P2.GetComponent<Character>().playerHealth <= 0)
        {
            matchP1Won = true;
            winPanel.sprite = P1.GetComponent<Character>().characterWinPanel;
            deathPanel.sprite = P2.GetComponent<Character>().characterDeathPanel;
        }

        if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth <= 0)
        {
            matchDraw = true;
            winPanel.sprite = P1.GetComponent<Character>().characterDeathPanel;
            deathPanel.sprite = P2.GetComponent<Character>().characterDeathPanel;
        }
    }
}