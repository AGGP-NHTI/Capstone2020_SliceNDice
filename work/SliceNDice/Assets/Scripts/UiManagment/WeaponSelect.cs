using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [Header("UI References")]
    [SerializeField] protected TextMeshProUGUI WeaponName;
    [SerializeField] protected Image WeaponSplash;
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
        if (!HasSelectedWeapon1)
        {
            if (P1 && Character.characterName.text == "Avacado")
            {
                Control = spawnman.P1.GetComponent<CharacterControl>();
                Control.Weapon = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP1;
                Anim = spawnman.P1.GetComponent<Animator>();
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                }
                if (WeaponName.text == "Cleaver")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[2];
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[4];
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[6];
                }
                HasSelectedWeapon1 = true;
            }
            if (P1 && Character.characterName.text == "NEGA Avacado")
            {
                Control = spawnman.P1.GetComponent<CharacterControl>();
                Control.Weapon = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP1;
                Anim = spawnman.P1.GetComponent<Animator>();
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                }
                if (WeaponName.text == "Cleaver")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[0];
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[2];
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[4];
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[6];
                }
                HasSelectedWeapon1 = true;
            }
        }
        if (!HasSelectedWeapon2)
        {
            if (P2 && Character.characterName.text == "Avacado")
            {
                Control = spawnman.P2.GetComponent<CharacterControl>();
                Control.Weapon = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP2;
                Anim = spawnman.P2.GetComponent<Animator>();
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                }
                if (WeaponName.text == "Cleaver")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[3];
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[5];
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[7];
                }
                HasSelectedWeapon2 = true;
            }

            if (P2 && Character.characterName.text == "NEGA Avacado")
            {
                Control = spawnman.P2.GetComponent<CharacterControl>();
                Control.Weapon = AvacadoWeaponList[selectedWeaponIndex].selectedWeaponP2;
                Anim = spawnman.P2.GetComponent<Animator>();
                if (WeaponName.text == "Knife")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                }
                if (WeaponName.text == "Cleaver")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                }
                if (WeaponName.text == "Rolling Pin")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[1];
                }
                if (WeaponName.text == "Tenderizer")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[3];
                }
                if (WeaponName.text == "Spoon")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[5];
                }
                if (WeaponName.text == "ToothPicks")
                {
                    Anim.runtimeAnimatorController = AvacadoAnims[7];
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
        
        if (P1 && Character.characterName.text == "Avacado")
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
        if (P2 && Character.characterName.text == "Avacado")
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
        if (P1 && Character.characterName.text == "NEGA Avacado")
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
        if (P2 && Character.characterName.text == "NEGA Avacado")
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
    public void RightArrow()
    {
        if (P1 && Character.characterName.text == "Avacado")
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
        if (P2 && Character.characterName.text == "Avacado")
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
        if (P1 && Character.characterName.text == "NEGA Avacado")
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
        if (P2 && Character.characterName.text == "NEGA Avacado")
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
        if(Character.characterName.text == "Avacado")
        {
            WeaponSplash.sprite = AvacadoWeaponList[selectedWeaponIndex].splash;
            WeaponName.text = AvacadoWeaponList[selectedWeaponIndex].WeaponName;
            desiredColor = AvacadoWeaponList[selectedWeaponIndex].WeaponColor;
        }
        if (Character.characterName.text == "NEGA Avacado")
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
        public GameObject selectedWeaponP2;
    }

    [System.Serializable]
    public class BlueBerryWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP2;
    }

    [System.Serializable]
    public class SeaweedWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP2;
    }

    [System.Serializable]
    public class ChiliPepperWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP2;
    }

    [System.Serializable]
    public class BroccoliWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP2;
    }

    [System.Serializable]
    public class AvacadoWeaponSelectObject
    {
        public Sprite splash;
        public string WeaponName;
        public Color WeaponColor;
        public GameObject selectedWeaponP1;
        public GameObject selectedWeaponP2;
    }
}
