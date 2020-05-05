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

    public bool HasConfirmedp1 = false;
    public bool HasConfirmedp2 = false;

    public GameObject background;
    public GameObject Wpanel;
    public GameObject Dpanel;
    public GameObject Check1;
    public GameObject Check2;

    public GameObject SelectedChar1;
    public GameObject SelectedChar2;
    public GameObject SelectedChar1Neg;
    public GameObject SelectedChar2Neg;
    public GameObject SelectedChar1Pos;
    public GameObject SelectedChar2Pos;
    public GameObject SelectedWep1;
    public GameObject SelectedWep2;


    GameObject P1;
    GameObject P2;

    public float Timeleft = 8.0f;


    public void Start()
    {
        background.SetActive(false);
        Check1.SetActive(false);
        Check2.SetActive(false);
        Wpanel.SetActive(false);
        Dpanel.SetActive(false);

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
        

        if (LevelSelect.canvas.activeSelf == false)
        {
            P1 = GameObject.FindGameObjectWithTag("P1");
            P2 = GameObject.FindGameObjectWithTag("P2");

            if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth > 0)
            {
                matchP2Won = true;
                winPanel.sprite = P1.GetComponent<Character>().characterDeathPanel;  
                deathPanel.sprite = P2.GetComponent<Character>().characterWinPanel;
                Timeleft -= Time.deltaTime;
                if (Timeleft < 0)
                {
                    background.SetActive(true);
                    Check1.SetActive(true);
                    Check2.SetActive(true);
                    Wpanel.SetActive(true);
                    Dpanel.SetActive(true);
                }
            }

            if (P1.GetComponent<Character>().playerHealth > 0 && P2.GetComponent<Character>().playerHealth <= 0)
            {
                matchP1Won = true;
                winPanel.sprite = P1.GetComponent<Character>().characterWinPanel;
                deathPanel.sprite = P2.GetComponent<Character>().characterDeathPanel;
                Timeleft -= Time.deltaTime;
                if (Timeleft < 0)
                {
                    background.SetActive(true);
                    Check1.SetActive(true);
                    Check2.SetActive(true);
                    Wpanel.SetActive(true);
                    Dpanel.SetActive(true);
                }
            }

            if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth <= 0)
            {
                matchDraw = true;
                winPanel.sprite = P1.GetComponent<Character>().characterDeathPanel;
                deathPanel.sprite = P2.GetComponent<Character>().characterDeathPanel;   
                Timeleft -= Time.deltaTime;
                if(Timeleft < 0)
                {
                    background.SetActive(true);
                    Check1.SetActive(true);
                    Check2.SetActive(true);
                    Wpanel.SetActive(true);
                    Dpanel.SetActive(true);
                }
                
            }
            if (Check1.activeSelf == true && Check2.activeSelf == true)
            {
                if (p1DevicePad.buttonSouth.wasPressedThisFrame)
                {
                    Wpanel.SetActive(false);
                    Timeleft = 100f;
                    HasConfirmedp1 = true;
                   
                }
                if (p2DevicePad.buttonSouth.wasPressedThisFrame)
                {
                    Dpanel.SetActive(false);
                    Timeleft = 100f;
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
        SelectedChar1 = GameObject.Find(SelectionScreen.SelectedCharacter1.name + "(Clone)");
        SelectedChar2 = GameObject.Find(SelectionScreen.SelectedCharacter2.name + "(Clone)");
        SelectedChar1Neg = GameObject.Find(SelectionScreen.SelectedCharacter1.name + "(Clone)_neg");
        SelectedChar2Neg = GameObject.Find(SelectionScreen.SelectedCharacter2.name + "(Clone)_neg");
        SelectedChar1Pos = GameObject.Find(SelectionScreen.SelectedCharacter1.name + "(Clone)_pos");
        SelectedChar2Pos = GameObject.Find(SelectionScreen.SelectedCharacter2.name + "(Clone)_pos");
        SelectedWep1 = GameObject.Find(WeaponSelect.SelectedWeapon1.name + "(Clone)");
        SelectedWep2 = GameObject.Find(WeaponSelect.SelectedWeapon2.name + "(Clone)");
        Destroy(SelectedChar1);
        Destroy(SelectedChar2);
        Destroy(SelectedChar1Neg);
        Destroy(SelectedChar2Neg);
        Destroy(SelectedChar1Pos);
        Destroy(SelectedChar2Pos);
        Destroy(SelectedWep1);
        Destroy(SelectedWep2);

        background.SetActive(false);
        Check1.SetActive(false);
        Check2.SetActive(false);
        LevelSelect.Camera.SetActive(true);
        LevelSelect.canvas.SetActive(true);
        LevelSelect.CharacterSelect1.SetActive(false);
        LevelSelect.CharacterSelect2.SetActive(false);
        LevelSelect.WeaponSelect1.SetActive(false);
        LevelSelect.WeaponSelect2.SetActive(false);
        LevelSelect.This.SetActive(true);
        HasConfirmedp1 = false;
        HasConfirmedp2 = false;
        Destroy(LevelSelect.LevelSpawned);
        

    }
}