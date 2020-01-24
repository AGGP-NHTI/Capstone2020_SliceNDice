using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SND.BaseControls;

public class Character : MonoBehaviour
{
    public CharacterControls c;

    [Header("Character Statistics")]
    public int Build;

    [Range(0, 200)]
    public int playerHealth;

    [Range(0, 100)]
    public int playerGuard;

    [Range(0f, 20f)]
    public float moveSpeed;

    [Range(100f, 1000f)]
    public int jumpForce;

    void Awake()
    {
        gameObject.AddComponent<CharacterControls>();

        Build = Mathf.CeilToInt(gameObject.transform.localScale.magnitude);

        playerHealth = 50 + (Build * 25);

        c = GetComponent<CharacterControls>();
    }

    void Start()
    {
        c.movementSpeed = moveSpeed;
        c.JumpForce = jumpForce;
    }

    void Update()
    {
        c.MovementControls();
        c.AttackControls();

        c.c.transform.position = c.camPos.transform.position;
    }
}
