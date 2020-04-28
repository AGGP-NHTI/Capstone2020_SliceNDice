using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public Slider p1healthsilder;
    public Slider p1shieldslider;
    public Slider p2healthsilder;
    public Slider p2shieldslider;

    public TextMeshProUGUI Char1Name;
    public TextMeshProUGUI Char2Name;

    public GameObject Char1Nameob;
    public GameObject Char2Nameob;

    Character TheCharacter;
    CharacterControl cc;

    // Start is called before the first frame update
    void Start()
    {
        //p1healthsilder.maxValue = TheCharacter.playerMaxHealth;
        //p2healthsilder.maxValue = TheCharacter.playerMaxHealth;
        //p1shieldslider.maxValue = TheCharacter.playerMaxHealth;
        //p2shieldslider.maxValue = TheCharacter.playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if (cc.P1)
        //{
        //    p1healthsilder.value = TheCharacter.playerHealth;
        //    p1shieldslider.value = playerGuard;
        //}

        //if (!cc.P1)
        //{
        //    p2healthsilder.value = playerHealth;
        //    p2shieldslider.value = playerGuard;
        //}
    }


}
