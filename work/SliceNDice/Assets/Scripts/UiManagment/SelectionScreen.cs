using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SelectionScreen : MonoBehaviour
{
    protected int selectedCharacterIndex;
    private Color desiredColor;

    private static bool HasSelectedCharacter1 = false;
    private static bool HasSelectedCharacter2 = false;
    public bool P1;
    public bool P2;
    public bool P1Checked = false;
    public bool P2Checked = false;

    public GameObject canvas;
    public LevelSelect LS;
    public GameObject LevelSelectionObject;
 
    TPSpawn spawnman;

    public InputDevice p1Device;
    public InputDevice p2Device;
    public Gamepad p1DevicePad;
    public Gamepad p2DevicePad;

    [Header("List of Characters")]
    [SerializeField] public List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] public TextMeshProUGUI characterName;
    [SerializeField] public Image characterSplash;
    [SerializeField] public Image backgroundColor;
   

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
        UpdateCharacterSelectionUI();
    }




    public void Update()
    {
        if (this.gameObject.tag == "CharacterSelection")
        {
            backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
        }

        if (p1DevicePad.buttonEast.wasPressedThisFrame)
        {
            LS.CharacterSelect1.SetActive(false);
            LS.CharacterSelect2.SetActive(false);
            LS.gameObject.SetActive(true);
        }
        if (p2DevicePad.buttonEast.wasPressedThisFrame)
        {
            LS.CharacterSelect1.SetActive(false);
            LS.CharacterSelect2.SetActive(false);
            LS.gameObject.SetActive(true);
        }

        if (P1)
        {
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

        }
        if (P2)
        {
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

    }

  

    public void ConfirmSelection()
    {
        spawnman = LS.managerob.GetComponent<TPSpawn>();
            if (P1)
            {
                spawnman.P1 = characterList[selectedCharacterIndex].selectedchar;
                P1Checked = true;
                HasSelectedCharacter1 = true;
                
            }

            if (P2)
            {
                spawnman.P2 = characterList[selectedCharacterIndex].selectedchar;
                P2Checked = true;
                HasSelectedCharacter2 = true;
            }

        if (HasSelectedCharacter1 && HasSelectedCharacter2)
        {
            LS.CharacterSelect1.SetActive(false);
            LS.CharacterSelect2.SetActive(false);
            LS.WeaponSelect1.SetActive(true);
            LS.WeaponSelect2.SetActive(true);
        }
        

    }
    public void LeftArrow()
    {
        if (P1)
        {
            selectedCharacterIndex--;
            if (selectedCharacterIndex < 0)
            {
                selectedCharacterIndex = characterList.Count - 1;
            }

            UpdateCharacterSelectionUI();
        }
        if (P2)
        {
            selectedCharacterIndex--;
            if (selectedCharacterIndex < 0)
            {
                selectedCharacterIndex = characterList.Count - 1;
            }

            UpdateCharacterSelectionUI();
        }

    }
    public void RightArrow()
    {
        if (P1)
        {
            selectedCharacterIndex++;
            if (selectedCharacterIndex == characterList.Count)
            {
                selectedCharacterIndex = 0;
            }
            UpdateCharacterSelectionUI();
        }
        if (P2)
        {
            selectedCharacterIndex++;
            if (selectedCharacterIndex == characterList.Count)
            {
                selectedCharacterIndex = 0;
            }
            UpdateCharacterSelectionUI();
        }

    }
    private void UpdateCharacterSelectionUI()
    {
        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
        public GameObject selectedchar;
      
    }
}
