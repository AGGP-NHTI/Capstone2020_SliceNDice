using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponSelect : MonoBehaviour
{
    protected int selectedWeaponIndex;
    private Color desiredColor;

    public bool P1;
    public bool P2;
    public static bool HasSelectedWeapon1 = false;
    public static bool HasSelectedWeapon2 = false;
    public SelectionScreen Character;
    public LevelSelect LS;

    public static GameObject SelectedWeapon1;
    public static GameObject SelectedWeapon2;
    public static GameObject SelectedWeapon1Off;
    public static GameObject SelectedWeapon2Off;

    TPSpawn spawnman;
    CharacterControl Control;
    Animator Anim;

    

    [Header("List of Weapons Per Character")]
    public List<WeaponSelectObject> WeaponList = new List<WeaponSelectObject>();
 

    [Header("List of Animator controllers")]
    public List<RuntimeAnimatorController> Anims = new List<RuntimeAnimatorController>();

    public InputDevice p1Device;
    public InputDevice p2Device;
    public Gamepad p1DevicePad;
    public Gamepad p2DevicePad;

    Player1Data Player1;
    Player2Data Player2;

    [Header("UI References")]
    [SerializeField] public TextMeshProUGUI WeaponName;
    [SerializeField] public Image WeaponSplash;
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
        UpdateWeaponSelectionUI();
    }
    public void Update()
    {
        if (this.gameObject.tag == "WeaponSelection")
        {
            backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
        }

        if (p1DevicePad.buttonEast.wasPressedThisFrame)
        {
            HasSelectedWeapon1 = false;
            HasSelectedWeapon2 = false;
            SelectionScreen.HasSelectedCharacter1 = false;
            SelectionScreen.HasSelectedCharacter2 = false;
            LevelSelect.WeaponSelect1.SetActive(false);
            LevelSelect.WeaponSelect2.SetActive(false);
            LevelSelect.CharacterSelect1.SetActive(true);
            LevelSelect.CharacterSelect2.SetActive(true);
        }
        if (p2DevicePad.buttonEast.wasPressedThisFrame)
        {
            SelectionScreen.HasSelectedCharacter1 = false;
            SelectionScreen.HasSelectedCharacter2 = false;
            HasSelectedWeapon1 = false;
            HasSelectedWeapon2 = false;
            LevelSelect.WeaponSelect1.SetActive(false);
            LevelSelect.WeaponSelect2.SetActive(false);
            LevelSelect.CharacterSelect1.SetActive(true);
            LevelSelect.CharacterSelect2.SetActive(true);
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
        Player1 = LS.managerob.GetComponent<Player1Data>();
        Player2 = LS.managerob.GetComponent<Player2Data>();

            if (P1)
            {
                Control = spawnman.P1.GetComponent<CharacterControl>();
                Control.Weapon = WeaponList[selectedWeaponIndex].selectedWeaponP1;
                Control.WeaponOffhand = WeaponList[selectedWeaponIndex].selectedWeaponP1OffHand;
                SelectedWeapon1 = WeaponList[selectedWeaponIndex].selectedWeaponP1;
                SelectedWeapon1Off = WeaponList[selectedWeaponIndex].selectedWeaponP1OffHand;
                Anim = spawnman.P1.GetComponent<Animator>();
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = Anims[0];
                    Player1.PlayerCharWeapon1 = "Knife";
                }
                if (WeaponName.text == "Cleaver")
                {
                    if(Character.characterName.text == "Tofu" || Character.characterName.text == "NEGA Tofu")
                    {
                        Anim.runtimeAnimatorController = Anims[6]; 
                    }
                    else
                    {
                        Anim.runtimeAnimatorController = Anims[0];
                    }
                    Player1.PlayerCharWeapon1 = "Cleaver";
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = Anims[0];
                    Player1.PlayerCharWeapon1 = "Rolling Pin";
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = Anims[2];
                    Player1.PlayerCharWeapon1 = "Tenderizer";
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = Anims[4];
                    Player1.PlayerCharWeapon1 = "Spoon";
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = Anims[6];
                    Player1.PlayerCharWeapon1 = "ToothPicks";
                }
                if(Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado")
                {
                    Player1.playerCharacter1 = "Avacado";
                }
                if(Character.characterName.text == "Tofu" || Character.characterName.text == "NEGA Tofu")
                {
                    Player1.playerCharacter1 = "Tofu";
                }
                if(Character.characterName.text == "BlueBerry" || Character.characterName.text == "NEGA BlueBerry")
                {
                    Player1.playerCharacter1 = "BlueBerry";
                }
                if(Character.characterName.text == "Broccoli" || Character.characterName.text == "NEGA Broccoli")
                {
                    Player1.playerCharacter1 = "Broccoli";
                }
                if (Character.characterName.text == "Seaweed" || Character.characterName.text == "NEGA Seaweed")
                {
                    Player1.playerCharacter1 = "Seaweed";
                }
                if (Character.characterName.text == "ChilliPepper" || Character.characterName.text == "NEGA ChilliPepper")
                {
                    Player1.playerCharacter1 = "ChilliPepper";
                }
                HasSelectedWeapon1 = true;
        }

            if (P2)
            {
                Control = spawnman.P2.GetComponent<CharacterControl>();
                Control.Weapon = WeaponList[selectedWeaponIndex].selectedWeaponP2;
                Control.WeaponOffhand = WeaponList[selectedWeaponIndex].selectedWeaponP2OffHand;
                SelectedWeapon2 = WeaponList[selectedWeaponIndex].selectedWeaponP2;
                SelectedWeapon2Off = WeaponList[selectedWeaponIndex].selectedWeaponP2OffHand;
                Anim = spawnman.P2.GetComponent<Animator>();
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = Anims[1];
                    Player2.PlayerCharWeapon2 = "Knife";
                }
                if (WeaponName.text == "Cleaver")
                {
                    if (Character.characterName.text == "Tofu" || Character.characterName.text == "NEGA Tofu")
                    {
                        Anim.runtimeAnimatorController = Anims[7];
                    }
                    else
                    {
                        Anim.runtimeAnimatorController = Anims[1];
                    }
                    Player2.PlayerCharWeapon2 = "Cleaver";
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = Anims[1];
                    Player2.PlayerCharWeapon2 = "Rolling Pin";
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = Anims[3];
                    Player2.PlayerCharWeapon2 = "Tenderizer";
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = Anims[5];
                    Player2.PlayerCharWeapon2 = "Spoon";
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = Anims[7];
                    Player2.PlayerCharWeapon2 = "ToothPicks";
                }
                if (Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado")
                {
                    Player2.playerCharacter2 = "Avacado";
                }
                if (Character.characterName.text == "Tofu" || Character.characterName.text == "NEGA Tofu")
                {
                    Player2.playerCharacter2 = "Tofu";
                }
                if (Character.characterName.text == "BlueBerry" || Character.characterName.text == "NEGA BlueBerry")
                {
                    Player2.playerCharacter2 = "BlueBerry";
                }
                if (Character.characterName.text == "Broccoli" || Character.characterName.text == "NEGA Broccoli")
                {
                    Player2.playerCharacter2 = "Broccoli";
                }
                if (Character.characterName.text == "Seaweed" || Character.characterName.text == "NEGA Seaweed")
                {
                    Player2.playerCharacter2 = "Seaweed";
                }
                if (Character.characterName.text == "ChilliPepper" || Character.characterName.text == "NEGA ChilliPepper")
                {
                    Player2.playerCharacter2 = "ChilliPepper";
                }
                HasSelectedWeapon2 = true;
            }
        
        if (HasSelectedWeapon1 && HasSelectedWeapon2)
        {
            LS.managerob.SetActive(true);
            LS.DynamicCamera.SetActive(true);
            LevelSelect.WeaponSelect1.gameObject.SetActive(false);
            LevelSelect.WeaponSelect2.gameObject.SetActive(false);
            LevelSelect.canvas.SetActive(false);
            LevelSelect.Camera.SetActive(false);
            HasSelectedWeapon1 = false;
            HasSelectedWeapon2 = false;
        }
    }
    public void LeftArrow()
    {        
        if (P1)
        {
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0)
            {
                selectedWeaponIndex = WeaponList.Count - 1;
            }
            UpdateWeaponSelectionUI();
        }
        if (P2)
        {
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0)
            {
                selectedWeaponIndex = WeaponList.Count - 1;
            }
            UpdateWeaponSelectionUI();
        }
    }
    public void RightArrow()
    {
        if (P1)
        {
            selectedWeaponIndex++;
            if (selectedWeaponIndex == WeaponList.Count)
            {
                selectedWeaponIndex = 0;
            }
            UpdateWeaponSelectionUI();
        }
        if (P2)
        {
            selectedWeaponIndex++;
            if (selectedWeaponIndex == WeaponList.Count)
            {
                selectedWeaponIndex = 0;
            }
            UpdateWeaponSelectionUI();
        }
    }
    private void UpdateWeaponSelectionUI()
    {
        WeaponSplash.sprite = WeaponList[selectedWeaponIndex].splash;
        WeaponName.text = WeaponList[selectedWeaponIndex].WeaponName;
        desiredColor = WeaponList[selectedWeaponIndex].WeaponColor;
    }

    [System.Serializable]
    public class WeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP1OffHand;
        public GameObject selectedWeaponP2;
        public GameObject selectedWeaponP2OffHand;
    }
}
