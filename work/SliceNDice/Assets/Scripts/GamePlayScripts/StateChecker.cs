using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StateChecker : MonoBehaviour
{
    /****   Game Pad   ****/
    public InputDevice p1Device;
    public InputDevice p2Device;
    public Gamepad p1DevicePad;
    public Gamepad p2DevicePad;


    /****   Game Objects    ****/
    public Image winPanel;
    public Image deathPanel;
    public bool matchP1Won = false;
    public bool matchP2Won = false;
    public bool matchDraw = false;

    GameObject P1;
    GameObject P2;

    public void Start()
    {
        winPanel.gameObject.SetActive(false);
        deathPanel.gameObject.SetActive(false);

        Gamepad[] pads = Gamepad.all.ToArray();

        if (pads.Length < 2)
        {
            Debug.LogError("Connect More Controllers, Sucka!!!!!!!!");
            return;
        }

        p1Device = pads[0].device;
        p2Device = pads[1].device;
        p1DevicePad = pads[0];
        p2DevicePad = pads[1];
    }


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

        if (p1DevicePad.buttonSouth.wasPressedThisFrame)
        {
            ConfirmSelection();
        }
        if (p2DevicePad.buttonSouth.wasPressedThisFrame)
        {
            ConfirmSelection();
        }
    }

    public void ConfirmSelection()
    {
        winPanel.gameObject.SetActive(false);
        deathPanel.gameObject.SetActive(false);
    }
}