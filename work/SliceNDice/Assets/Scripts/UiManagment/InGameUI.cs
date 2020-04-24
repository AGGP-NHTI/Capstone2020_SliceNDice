using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Slider p1healthsilder;
    public Slider p1shieldslider;
    public Slider p2healthsilder;
    public Slider p2shieldslider;

    Character TheCharacter;

    // Start is called before the first frame update
    void Start()
    {
        p1healthsilder.maxValue = TheCharacter.playerMaxHealth;
        p2healthsilder.maxValue = TheCharacter.playerMaxHealth;
        p1shieldslider.maxValue = TheCharacter.playerMaxHealth;
        p2shieldslider.maxValue = TheCharacter.playerMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
