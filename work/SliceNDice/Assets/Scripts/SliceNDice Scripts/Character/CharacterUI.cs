using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    Game g;
    Image PHealth;

    void Start()
    {
        g = GameObject.Find("Game Manager").GetComponent<Game>();

        PHealth = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float HPAmount = g.players[0].GetComponent<Character>().playerHealth / g.players[0].GetComponent<Character>().playerMaxHealth;

        PHealth.fillAmount = HPAmount;
    }
}
