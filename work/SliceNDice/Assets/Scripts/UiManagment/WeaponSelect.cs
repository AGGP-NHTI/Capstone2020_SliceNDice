using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponSelect : MonoBehaviour
{
    protected int selectedWeaponIndex;
    private Color desiredColor;

    private bool HasSelectedWeapon = false;

    [Header("List of Weapons")]
    [SerializeField] protected List<WeaponSelectObject> WeaponList = new List<WeaponSelectObject>();

    [Header("UI References")]
    [SerializeField] protected TextMeshProUGUI WeaponName;
    [SerializeField] protected Image WeaponSplash;
    [SerializeField] protected Image backgroundColor;


    [Header("Options")]
    [SerializeField] protected float backgroundColorTransitionSpeed = 5f;



    public void Start()
    {
        UpdateWeaponSelectionUI();
    }




    public void Update()
    {
        if (this.gameObject.tag == "WeaponSelection")
        {
            backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
        }


    }



    public void ConfirmSelection()
    {



    }
    public void LeftArrow()
    {
        if (HasSelectedWeapon)
        {
            return;
        }
        selectedWeaponIndex--;
        if (selectedWeaponIndex < 0)
        {
            selectedWeaponIndex = WeaponList.Count - 1;
        }

        UpdateWeaponSelectionUI();
    }
    public void RightArrow()
    {
        if (HasSelectedWeapon)
        {
            return;
        }
        selectedWeaponIndex++;
        if (selectedWeaponIndex == WeaponList.Count)
        {
            selectedWeaponIndex = 0;
        }
        UpdateWeaponSelectionUI();
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
        public GameObject selectedWeapon;
        public GameObject selectedWeaponUI;
    }
}
