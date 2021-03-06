﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuScript : MonoBehaviour
{
    public int selectedOptionIndex;
    private Color desiredColor;

    public GameObject Canvas;
    public GameObject MainMenu;
    public GameObject StageSelectionMenu;
    public GameObject SelectionMenus;
    public GameObject OptionMenuPanel;
    public GameObject ControlsPanel;

    public InputDevice p1Device;
    public InputDevice p2Device;
    public Gamepad p1DevicePad;
    public Gamepad p2DevicePad;

    private bool HasSelectedOption = false;

    [Header("List of Options")]
    [SerializeField] protected List<OptionSelectionObject> OptionList = new List<OptionSelectionObject>();

    [Header("UI References")]
    [SerializeField] protected TextMeshProUGUI OptionName;
    [SerializeField] protected Image OptionSplash;
    [SerializeField] protected Image backgroundColor;


    [Header("Options")]
    [SerializeField] protected float backgroundColorTransitionSpeed = 5f;



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
        UpdateLevelSelectionUI();
    }




    public void Update()
    {

        if (this.gameObject.tag == "MainMenuPanel")
        {
            backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
        }
        if (p1DevicePad.leftStick.left.wasPressedThisFrame)
        {
            LeftArrow();
        }
        if (p1DevicePad.leftStick.right.wasPressedThisFrame)
        {
            RightArrow();
        }
        if (p1DevicePad.buttonSouth.wasPressedThisFrame)
        {
            ConfirmSelection();
        }

        if (p2DevicePad.leftStick.left.wasPressedThisFrame)
        {
            LeftArrow();
        }
        if (p2DevicePad.leftStick.right.wasPressedThisFrame)
        {
            RightArrow();
        }
        if (p2DevicePad.buttonSouth.wasPressedThisFrame)
        {
            ConfirmSelection();
        }

    }



    public void ConfirmSelection()
    {
        if (!HasSelectedOption)
        {
            if (OptionList[selectedOptionIndex].OptionName == "Fight")
            {
                Debug.LogError("Going To Character Selection Menu");
                SelectionMenus.SetActive(true);
                StageSelectionMenu.SetActive(true);
                MainMenu.SetActive(false);
            }

            if (OptionList[selectedOptionIndex].OptionName == "Controls")
            {
                Debug.LogError("Going To Controls Panel");
                ControlsPanel.SetActive(true);
                gameObject.SetActive(false);
            }

            if (OptionList[selectedOptionIndex].OptionName == "Options")
            {
                Debug.LogError("Going To Options Menu");
                OptionMenuPanel.SetActive(true);
                gameObject.SetActive(false);
            }

            if (OptionList[selectedOptionIndex].OptionName == "Quit")
            {
                Debug.Log("Quit Applicaiton");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#endif

                Application.Quit();
                Debug.LogError("Game is exiting");
            }

        }


    }
    public void LeftArrow()
    {
        if (HasSelectedOption)
        {
            return;
        }
        selectedOptionIndex--;
        if (selectedOptionIndex < 0)
        {
            selectedOptionIndex = OptionList.Count - 1;
        }
        UpdateLevelSelectionUI();
        Debug.Log("Game is moving left");

    }
    public void RightArrow()
    {
        if (HasSelectedOption)
        {
            return;
        }
        selectedOptionIndex++;
        if (selectedOptionIndex == OptionList.Count)
        {
            selectedOptionIndex = 0;
        }
        UpdateLevelSelectionUI();
    }
    private void UpdateLevelSelectionUI()
    {
        OptionSplash.sprite = OptionList[selectedOptionIndex].splash;
        OptionName.text = OptionList[selectedOptionIndex].OptionName;
        desiredColor = OptionList[selectedOptionIndex].OptionColor;
    }

    [System.Serializable]
    public class OptionSelectionObject
    {
        public Sprite splash;
        public string OptionName;
        public Color OptionColor;
    }
}
