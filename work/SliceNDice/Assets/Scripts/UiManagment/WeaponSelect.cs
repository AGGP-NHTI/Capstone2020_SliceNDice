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
    private static bool HasSelectedWeapon1 = false;
    private static bool HasSelectedWeapon2 = false;
    public SelectionScreen Character;
    public LevelSelect LS;
  
    TPSpawn spawnman;
    CharacterControl Control;
    Animator Anim;
    

    [Header("List of Weapons Per Character")]
    public List<AvacadoWeaponSelectObject> AvacadoWeaponList = new List<AvacadoWeaponSelectObject>();

    [Header("List of Animator controllers per character")]
    public List<RuntimeAnimatorController> AvacadoAnims = new List<RuntimeAnimatorController>();

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
        if (!HasSelectedWeapon1)
        {
            if (P1 &&(Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado"))
            {
                Control = spawnman.P1.GetComponent<CharacterControl>();
                Control.Weapon = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP1;
                Control.WeaponOffhand = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP1OffHand;
                Anim = spawnman.P1.GetComponent<Animator>();
                Player1.playerCharacter1 = "Avacado";
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                    Player1.PlayerCharWeapon1 = "Knife";
                }
                if (WeaponName.text == "Cleaver")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                    Player1.PlayerCharWeapon1 = "Cleaver";
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                    Player1.PlayerCharWeapon1 = "Rolling Pin";
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[2];
                    Player1.PlayerCharWeapon1 = "Tenderizer";
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[4];
                    Player1.PlayerCharWeapon1 = "Spoon";
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[6];
                    Player1.PlayerCharWeapon1 = "ToothPicks";
                }
                HasSelectedWeapon1 = true;
            }
           
        }
        if (!HasSelectedWeapon2)
        {
            if (P2 && (Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado"))
            {
                Control = spawnman.P2.GetComponent<CharacterControl>();
                Control.Weapon = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP2;
                Control.WeaponOffhand = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP2OffHand;
                Anim = spawnman.P2.GetComponent<Animator>();
                Player2.playerCharacter2 = "Avacado";
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                    Player2.PlayerCharWeapon2 = "Knife";
                }
                if (WeaponName.text == "Cleaver")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                    Player2.PlayerCharWeapon2 = "Cleaver";
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                    Player2.PlayerCharWeapon2 = "Rolling Pin";
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[3];
                    Player2.PlayerCharWeapon2 = "Tenderizer";
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[5];
                    Player2.PlayerCharWeapon2 = "Spoon";
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[7];
                    Player2.PlayerCharWeapon2 = "ToothPicks";
                }
                HasSelectedWeapon2 = true;
            }
        }
        if (HasSelectedWeapon1 && HasSelectedWeapon2)
        {
            LS.managerob.SetActive(true);
            LS.DynamicCamera.SetActive(true);
            LS.WeaponSelect1.SetActive(false);
            LS.WeaponSelect2.SetActive(false);
            LS.Canvas.SetActive(false);
            Camera.main.gameObject.SetActive(false);
        }
       

    }
    public void LeftArrow()
    {
        
        if (P1 && (Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado"))
        {
            if (HasSelectedWeapon1)
            {
                return;
            }
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0)
            {
                selectedWeaponIndex = AvacadoWeaponList.Count - 1;
            }

            UpdateWeaponSelectionUI();
        }
        if (P2 && (Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado"))
        {
            if (HasSelectedWeapon2)
            {
                return;
            }
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0)
            {
                selectedWeaponIndex = AvacadoWeaponList.Count - 1;
            }

            UpdateWeaponSelectionUI();
        }
    }
    public void RightArrow()
    {
        if (P1 && (Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado"))
        {
            if (HasSelectedWeapon1)
            {
                return;
            }
            selectedWeaponIndex++;
            if (selectedWeaponIndex == AvacadoWeaponList.Count)
            {
                selectedWeaponIndex = 0;
            }
            UpdateWeaponSelectionUI();
        }
        if (P2 && (Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado"))
        {
            if (HasSelectedWeapon2)
            {
                return;
            }
            selectedWeaponIndex++;
            if (selectedWeaponIndex == AvacadoWeaponList.Count)
            {
                selectedWeaponIndex = 0;
            }
            UpdateWeaponSelectionUI();
        }
    }
    private void UpdateWeaponSelectionUI()
    {
        if(Character.characterName.text == "Avacado" || Character.characterName.text == "NEGA Avacado")
        {
            WeaponSplash.sprite = AvacadoWeaponList[selectedWeaponIndex].splash;
            WeaponName.text = AvacadoWeaponList[selectedWeaponIndex].WeaponName;
            desiredColor = AvacadoWeaponList[selectedWeaponIndex].WeaponColor;
        }
    }


    [System.Serializable]
    public class TofuWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP1OffHand;
        public GameObject selectedWeaponP2;
        public GameObject selectedWeaponP2OffHand;
    }

    [System.Serializable]
    public class BlueBerryWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP1OffHand;
        public GameObject selectedWeaponP2;
        public GameObject selectedWeaponP2OffHand;
    }

    [System.Serializable]
    public class SeaweedWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP1OffHand;
        public GameObject selectedWeaponP2;
        public GameObject selectedWeaponP2OffHand;
    }

    [System.Serializable]
    public class ChiliPepperWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP1OffHand;
        public GameObject selectedWeaponP2;
        public GameObject selectedWeaponP2OffHand;
    }

    [System.Serializable]
    public class BroccoliWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP1OffHand;
        public GameObject selectedWeaponP2;
        public GameObject selectedWeaponP2OffHand;
    }

    [System.Serializable]
    public class AvacadoWeaponSelectObject
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
