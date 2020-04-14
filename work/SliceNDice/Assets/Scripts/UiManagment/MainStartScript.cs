using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainStartScript : MonoBehaviour
{
    private bool HasStartedGame = false;


    public InputDevice p1Device;
    public InputDevice p2Device;
    public Gamepad p1DevicePad;
    public Gamepad p2DevicePad;

    public GameObject Canvas;
    public GameObject SelectionMenus;
    public GameObject MainMenuPanel;

    public void Awake()
    {
        SelectionMenus.SetActive(false);
        MainMenuPanel.SetActive(false);
    }
    // Start is called before the first frame update
    public void Start()
    {
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

    // Update is called once per frame
    public void Update()
    {
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
        if (!HasStartedGame)
        {
            gameObject.SetActive(false);
            MainMenuPanel.SetActive(true);
        }


    }
}
