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

    static bool HasConfirmedp1 = false;
    static bool HasConfirmedp2 = false;

    public MainMenuScript main;
    public LevelSelect LS;
    public WeaponSelect WS;

    public GameObject Wpanel;
    public GameObject Dpanel;
    public SelectionScreen Character1;
    public SelectionScreen Character2;
    public GameObject SelectedChar1;
    public GameObject SelectedChar2;

    GameObject P1;
    GameObject P2;

    Color original;
    Color transparency;

    public void Start()
    {
        Wpanel.gameObject.SetActive(false);
        Dpanel.gameObject.SetActive(false);
        //original = winPanel.color;
        //transparency = new Color(300, 300, 300, 0);

        //winPanel.color = transparency;
        //deathPanel.color = transparency;

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
        if (LS.Canvas.activeSelf == false && main.Canvas.activeSelf == false)
        {
            P1 = GameObject.FindGameObjectWithTag("P1");
            P2 = GameObject.FindGameObjectWithTag("P2");

            if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth > 0)
            {
                matchP2Won = true;
                winPanel.sprite = P2.GetComponent<Character>().characterWinPanel;
                //winPanel.color = original;
                Wpanel.gameObject.SetActive(true);
                deathPanel.sprite = P1.GetComponent<Character>().characterDeathPanel;
                //deathPanel.color = original;
                Dpanel.gameObject.SetActive(true);
            }

            if (P1.GetComponent<Character>().playerHealth > 0 && P2.GetComponent<Character>().playerHealth <= 0)
            {
                matchP1Won = true;
                winPanel.sprite = P1.GetComponent<Character>().characterWinPanel;
                //winPanel.color = original;
                Wpanel.gameObject.SetActive(true);
                deathPanel.sprite = P2.GetComponent<Character>().characterDeathPanel;
                //deathPanel.color = original;
                Dpanel.gameObject.SetActive(true);
            }

            if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth <= 0)
            {
                matchDraw = true;
                winPanel.sprite = P1.GetComponent<Character>().characterDeathPanel;
                //winPanel.color = original;
                Wpanel.gameObject.SetActive(true);
                deathPanel.sprite = P2.GetComponent<Character>().characterDeathPanel;
                //deathPanel.color = original;
                Dpanel.gameObject.SetActive(true);
            }
            if (Wpanel.activeSelf == true && Dpanel.activeSelf == true)
            {
                if (p1DevicePad.buttonSouth.wasPressedThisFrame)
                {
                    HasConfirmedp1 = true;
                }
                if (p2DevicePad.buttonSouth.wasPressedThisFrame)
                {
                    HasConfirmedp2 = true;
                }
                if (HasConfirmedp1 && HasConfirmedp2)
                {
                    ConfirmSelection();
                }
            }

        }

    }

    public void ConfirmSelection()
    {
        matchDraw = false;
        matchP1Won = false;
        matchP2Won = false;
        SelectedChar1 = GameObject.Find(Character1.SelectedCharacter.name + "(Clone)");
        SelectedChar2 = GameObject.Find(Character2.SelectedCharacter.name + "(Clone)");
        //winPanel.color = transparency;
        Wpanel.gameObject.SetActive(false);
        //deathPanel.color = transparency;
        Dpanel.gameObject.SetActive(false);
        WS.Camera.gameObject.SetActive(true);
        LS.Canvas.gameObject.SetActive(true);
        LS.gameObject.SetActive(true);
        Destroy(LS.LevelSpawned);
        Destroy(SelectedChar1);
        Destroy(SelectedChar2);
    }
}