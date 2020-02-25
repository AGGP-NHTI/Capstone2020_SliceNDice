using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    Game g;
    Image PHealth;

    public bool PlayerNumber;

    void Start()
    {
        g = GameObject.Find("Game Manager").GetComponent<Game>();

        PHealth = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float HPAmount;

        if (PlayerNumber)   // Player 1
        {
            HPAmount = g.players[0].GetComponent<Character>().playerHealth / g.players[0].GetComponent<Character>().playerMaxHealth;

            PHealth.fillAmount = HPAmount;
        }

        if (!PlayerNumber)  // Player 2
        {
            HPAmount = g.players[1].GetComponent<Character>().playerHealth / g.players[1].GetComponent<Character>().playerMaxHealth;

            PHealth.fillAmount = HPAmount;
        }
    }
}
