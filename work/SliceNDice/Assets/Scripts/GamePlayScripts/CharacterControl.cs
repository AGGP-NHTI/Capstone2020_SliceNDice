using System.Collections;
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
    public GameObject Weaponpoint1;
    public GameObject weaponpoint2;
    GameObject managerob;
    public CameraControl control;

    public Player1Data Play1;
    public Player2Data Play2;

    Vector3 moveDirection;
    Vector3 movement;

    public GameObject Weapon;
    GameObject WepLoc;
    public GameObject Wep;

    TPSpawn spawnman;

    Quaternion offset = new Quaternion(-40, 180, 0, 0);
    private void Awake()
    {
        managerob = GameObject.Find("PlayerManager");
      
        controls = new PlayerControls();

        Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
        WepLoc = GameObject.Find(Weapon.name + "(Clone)");
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
            if(Play1.playerCharacter1 == "Avacado" || Play1.playerCharacter1 == "NEGA Avacado")
            {
                if(Play1.PlayerCharWeapon1 == "Knife")
                {
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.rotation = new Quaternion(60, 180, -40, 0);
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                }
            }
            
        }
        else
        {
            if(Play2.playerCharacter2 == "Avacado" || Play2.playerCharacter2 == "NEGA Avacado")
            {
                if(Play2.PlayerCharWeapon2 == "Knife")
                {
                    WepLoc.transform.SetParent(Weaponpoint1.transform);
                    WepLoc.transform.parent = Weaponpoint1.transform;
                    WepLoc.transform.position = Weaponpoint1.transform.position;
                    WepLoc.transform.Rotate(-55, 180, 30);
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