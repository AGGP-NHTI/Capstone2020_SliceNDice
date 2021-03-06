﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public float backSpeed = 5f;
    Vector2 Moveet;
    Animator anim;
    private PlayerControls controls = null;
    public Gamepad pDevice;
    public bool P1;
    public bool P2;
    public GameObject Weaponpoint1;
    public GameObject Weaponpoint2;
    GameObject managerob;
    public CameraControl control;

    public Player1Data Play1;
    public Player2Data Play2;

    Vector3 moveDirection;
    Vector3 movement;

    public GameObject Weapon;
    public GameObject WeaponOffhand;
    GameObject WepLoc;
    GameObject WepLocOff;
    public GameObject Wep;
    public GameObject WepOff;
    TPSpawn spawnman;

    Quaternion offset = new Quaternion(-40, 180, 0, 0);
    private void Awake()
    {
        managerob = GameObject.Find("PlayerManager");
        Play1 = GameObject.Find("PlayerManager").GetComponent<Player1Data>();
        Play2 = GameObject.Find("PlayerManager").GetComponent<Player2Data>();
        controls = new PlayerControls();
        if (P1 )
        {
            if (Play1.PlayerCharWeapon1 == "ToothPicks")
            {
                Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
                WepOff = Instantiate(WeaponOffhand, Weaponpoint2.transform.position, Weaponpoint2.transform.rotation);
                WepLoc = GameObject.Find(Weapon.name + "(Clone)");
                WepLocOff = GameObject.Find(WeaponOffhand.name + "(Clone)");
            }
            if (Play1.playerCharacter1 == "Tofu" && Play1.PlayerCharWeapon1 == "Cleaver")
            {
                Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
                WepOff = Instantiate(WeaponOffhand, Weaponpoint2.transform.position, Weaponpoint2.transform.rotation);
                WepLoc = GameObject.Find(Weapon.name + "(Clone)");
                WepLocOff = GameObject.Find(WeaponOffhand.name + "(Clone)");
            }
            else
            {
                Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
                WepLoc = GameObject.Find(Weapon.name + "(Clone)");
            }
        }
        
        if (P2)
        {
            if (Play2.PlayerCharWeapon2 == "ToothPicks")
            {
                Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
                WepOff = Instantiate(WeaponOffhand, Weaponpoint2.transform.position, Weaponpoint2.transform.rotation);
                WepLoc = GameObject.Find(Weapon.name + "(Clone)");
                WepLocOff = GameObject.Find(WeaponOffhand.name + "(Clone)");
            }
            if (Play2.playerCharacter2 == "Tofu" && Play2.PlayerCharWeapon2 == "Cleaver")
            {
                Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
                WepOff = Instantiate(WeaponOffhand, Weaponpoint2.transform.position, Weaponpoint2.transform.rotation);
                WepLoc = GameObject.Find(Weapon.name + "(Clone)");
                WepLocOff = GameObject.Find(WeaponOffhand.name + "(Clone)");
            }
            else
            {
                Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
                WepLoc = GameObject.Find(Weapon.name + "(Clone)");
            }
        }
        
    }

    private void Start()
    {
        control = GameObject.Find("CameraManager").GetComponent<CameraControl>();

        Play1 = GameObject.Find("PlayerManager").GetComponent<Player1Data>();
        Play2 = GameObject.Find("PlayerManager").GetComponent<Player2Data>();

        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;

        spawnman = managerob.GetComponent<TPSpawn>();

        if (P1)
        {
            if(Play1.playerCharacter1 == "Avocado")
            {
                if(Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);
                    
                }
                if (Play1.PlayerCharWeapon1 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-70, 180, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play1.PlayerCharWeapon1 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.75f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play1.PlayerCharWeapon1 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(0.03f, .34f, -0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, 0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play1.playerCharacter1 == "Tofu")
            {
                if (Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.05f,0,0.05f);
                    WepLoc.transform.Rotate(-87, 150, 30);
                    WepLoc.transform.localScale += new Vector3(.015f,.25f,.025f);
                }
                if (Play1.PlayerCharWeapon1 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.05f,-.09f,.05f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-.05f, -.09f, -.05f);
                    WepLoc.transform.Rotate(-87, 140, 40);
                    WepLocOff.transform.Rotate(-87,160,25);
                    WepLoc.transform.localScale += new Vector3(.0005f, .0009f, .0005f);
                    WepLocOff.transform.localScale += new Vector3(.0005f, .0009f, .0005f);
                }
                if (Play1.PlayerCharWeapon1 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.03f, .59f, .05f);
                    WepLoc.transform.Rotate(0, 190, 40);
                    WepLoc.transform.localScale += new Vector3(0, 0, 0.1f);
                }
                if (Play1.PlayerCharWeapon1 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.03f,0,0);
                    WepLoc.transform.Rotate(-85, 180, 10);
                    WepLoc.transform.localScale += new Vector3(0, 0.08f, 0f);
                }
                if (Play1.PlayerCharWeapon1 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.75f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLoc.transform.localScale += new Vector3(0, .0009f, 0);
                }
                if (Play1.PlayerCharWeapon1 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.05f, .34f, 0.05f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, -0.05f);
                    WepLoc.transform.Rotate(-87, 140, 40);
                    WepLocOff.transform.Rotate(-87, 160, 25);
                    WepLoc.transform.localScale += new Vector3(0, 0, 0);
                    WepLocOff.transform.localScale += new Vector3(0, 0, 0);
                }
            }
           
            if (Play1.playerCharacter1 == "Broccoli")
            {
                if (Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, -7);

                }
                if (Play1.PlayerCharWeapon1 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play1.PlayerCharWeapon1 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.55f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play1.PlayerCharWeapon1 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(0.03f, .34f, -0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, 0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play1.playerCharacter1 == "Seaweed")
            {
                if (Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 30);

                }
                if (Play1.PlayerCharWeapon1 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play1.PlayerCharWeapon1 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.75f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play1.PlayerCharWeapon1 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(0.03f, .34f, -0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, 0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play1.playerCharacter1 == "ChilliPepper")
            {
                if (Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);

                }
                if (Play1.PlayerCharWeapon1 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-70, 180, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play1.PlayerCharWeapon1 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.75f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play1.PlayerCharWeapon1 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(0.03f, .34f, -0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, 0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play1.playerCharacter1 == "BlueBerry")
            {
                if (Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);

                }
                if (Play1.PlayerCharWeapon1 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-70, 180, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play1.PlayerCharWeapon1 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play1.PlayerCharWeapon1 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.75f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play1.PlayerCharWeapon1 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(0.03f, .34f, -0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, 0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
        }
        if(P2)
        {
            if(Play2.playerCharacter2 == "Avocado")
            {
                if(Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);
                }
                if (Play2.PlayerCharWeapon2 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-70, 180, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play2.PlayerCharWeapon2 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, -.75f,-.09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play2.PlayerCharWeapon2 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.03f,.34f,0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(0.05f, .34f, -0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play2.playerCharacter2 == "Tofu")
            {
                if (Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.05f, 0, 0.05f);
                    WepLoc.transform.Rotate(-87, 150, 30);
                    WepLoc.transform.localScale += new Vector3(.015f, .25f, .025f);
                }
                if (Play2.PlayerCharWeapon2 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.05f, -.09f, .05f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-.05f, -.09f, -.05f);
                    WepLoc.transform.Rotate(-87, 140, 40);
                    WepLocOff.transform.Rotate(-87, 160, 25);
                    WepLoc.transform.localScale += new Vector3(.0005f, .0009f, .0005f);
                    WepLocOff.transform.localScale += new Vector3(.0005f, .0009f, .0005f);
                }
                if (Play2.PlayerCharWeapon2 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.03f, .59f, .05f);
                    WepLoc.transform.Rotate(0, 190, 40);
                    WepLoc.transform.localScale += new Vector3(0, 0, 0.1f);
                }
                if (Play2.PlayerCharWeapon2 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.03f, 0, 0);
                    WepLoc.transform.Rotate(-85, 180, 10);
                    WepLoc.transform.localScale += new Vector3(0, 0.08f, 0f);
                }
                if (Play2.PlayerCharWeapon2 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, -.75f, .09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLoc.transform.localScale += new Vector3(0, .0009f, 0);
                }
                if (Play2.PlayerCharWeapon2 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.03f, .34f, 0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(0.05f, .34f, -0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            
            if (Play2.playerCharacter2 == "Broccoli")
            {
                if (Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, -7);
                }
                if (Play2.PlayerCharWeapon2 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play2.PlayerCharWeapon2 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, -.55f, -.09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play2.PlayerCharWeapon2 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.05f, .34f, 0.05f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(-0.05f, .34f, -0.05f);
                    WepLoc.transform.Rotate(-87, 140, 40);
                    WepLocOff.transform.Rotate(-87, 160, 25);
                    WepLoc.transform.localScale += new Vector3(0, 0, 0);
                    WepLocOff.transform.localScale += new Vector3(0, 0, 0);
                }
            }
            if (Play2.playerCharacter2 == "Seaweed")
            {
                if (Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 30);
                }
                if (Play2.PlayerCharWeapon2 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play2.PlayerCharWeapon2 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, -.75f, -.09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play2.PlayerCharWeapon2 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.03f, .34f, 0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(0.05f, .34f, -0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play2.playerCharacter2 == "ChilliPepper")
            {
                if (Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);
                }
                if (Play2.PlayerCharWeapon2 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-70, 180, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play2.PlayerCharWeapon2 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, -.75f, -.09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play2.PlayerCharWeapon2 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.03f, .34f, 0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(0.05f, .34f, -0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
            if (Play2.playerCharacter2 == "BlueBerry")
            {
                if (Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);
                }
                if (Play2.PlayerCharWeapon2 == "Cleaver")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-70, 180, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Rolling Pin")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-.09f, .45f, 0);
                    WepLoc.transform.Rotate(0, 190, 40);
                }
                if (Play2.PlayerCharWeapon2 == "Spoon")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-80, 180, 0);
                }
                if (Play2.PlayerCharWeapon2 == "Tenderizer")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(.09f, -.75f, -.09f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                }
                if (Play2.PlayerCharWeapon2 == "ToothPicks")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLocOff.transform.SetParent(Weaponpoint2.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLocOff.transform.parent = Weaponpoint2.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position + new Vector3(-0.03f, .34f, 0.01f);
                    WepLocOff.transform.position = Weaponpoint2.transform.position + new Vector3(0.05f, .34f, -0.01f);
                    WepLoc.transform.Rotate(-148, -80, -95);
                    WepLocOff.transform.Rotate(-148, -80, -95);
                }
            }
        }
    }

    private void Update()
    {

        if (spawnman.p1DevicePad.buttonWest.wasPressedThisFrame)
        {
            FastAttack();
        }
        if (spawnman.p2DevicePad.buttonWest.wasPressedThisFrame)
        {
            FastAttack2();
        }
        if (spawnman.p1DevicePad.buttonNorth.wasPressedThisFrame)
        {
            HeavyAttack();
        }
        if (spawnman.p2DevicePad.buttonNorth.wasPressedThisFrame)
        {
            HeavyAttack2();
        }
        if (spawnman.p1DevicePad.buttonEast.wasPressedThisFrame)
        {
            StabAttack();
        }
        if (spawnman.p2DevicePad.buttonEast.wasPressedThisFrame)
        {
            StabAttack2();
        }


        //horizontal p1
        if (spawnman.p1DevicePad.leftStick.x.ReadValue() > 0)
        {
            control.Player1.transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            anim.SetInteger("Condition", 1);
        }
        //vertical p1 
        if (spawnman.p1DevicePad.leftStick.y.ReadValue() > 0)
        {
            anim.SetInteger("Condition", 1);
            control.Player1.transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
        //horizontal p1
        if (spawnman.p1DevicePad.leftStick.x.ReadValue() < 0)
        {
            anim.SetInteger("Condition", 1);
            control.Player1.transform.position += Vector3.left * backSpeed * Time.deltaTime;
        }
        //vertical p1 
        if (spawnman.p1DevicePad.leftStick.y.ReadValue() < 0)
        {
            anim.SetInteger("Condition", 1);
            control.Player1.transform.position += Vector3.back * movementSpeed * Time.deltaTime;
        }

        //horizontal p2
        if (spawnman.p2DevicePad.leftStick.x.ReadValue() > 0)
        {
            anim.SetInteger("Condition2", 1);
            control.Player2.transform.position += Vector3.right * backSpeed * Time.deltaTime;
        }
        //vertical p2
        if (spawnman.p2DevicePad.leftStick.y.ReadValue() > 0)
        {
            anim.SetInteger("Condition2", 1);
            control.Player2.transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
        //horizontal p2
        if (spawnman.p2DevicePad.leftStick.x.ReadValue() < 0)
        {
            anim.SetInteger("Condition2", 1);
            control.Player2.transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        //vertical p2
        if (spawnman.p2DevicePad.leftStick.y.ReadValue() < 0)
        {
            anim.SetInteger("Condition2", 1);
            control.Player2.transform.position += Vector3.back * movementSpeed * Time.deltaTime;
        }

        if (spawnman.p1DevicePad.leftStick.y.ReadValue() == 0 && spawnman.p1DevicePad.leftStick.x.ReadValue() == 0)
        {
            anim.SetInteger("Condition", 0);
        }

        if (spawnman.p2DevicePad.leftStick.y.ReadValue() == 0 && spawnman.p2DevicePad.leftStick.x.ReadValue() == 0)
        {
            anim.SetInteger("Condition2", 0);
        }


    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }


    public void StabAttack()
    {
        Debug.Log("Stab1");
        anim.SetTrigger("StabbyStab");
    }
    public void StabAttack2()
    {
        Debug.Log("Stab2");
        anim.SetTrigger("StabbyStab2");
    }
    public void FastAttack()
    {
        Debug.Log("Fast1");
        anim.SetTrigger("FastAtt");
    }
    public void FastAttack2()
    {
        Debug.Log("Fast2");
        anim.SetTrigger("FastAtt2");
    }
    public void HeavyAttack()
    {
        Debug.Log("Heavy1");
        anim.SetTrigger("StrongAtt");
    }
    public void HeavyAttack2()
    {
        Debug.Log("Heavy2");
        anim.SetTrigger("StrongAtt2");
    }

    private void OnMove(InputValue value)
    {
        Moveet = value.Get<Vector2>();
    }
}