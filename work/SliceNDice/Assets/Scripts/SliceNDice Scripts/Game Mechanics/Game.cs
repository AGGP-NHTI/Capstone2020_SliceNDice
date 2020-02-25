using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Characters")]
    public List<GameObject> players;

    [Header("Game Controllers")]
    public GameObject OutOfBounds;


    public void Awake()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    public void Update()
    {
        if (players[0] != null)
        {
            if (players[0].GetComponent<Character>().playerHealth <= 0)
            {
                players.RemoveAt(0);
            }
        }
    }
}
