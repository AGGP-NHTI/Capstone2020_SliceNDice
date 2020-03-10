using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public float movementSpeed = .01f;
    Vector2 Moveet;
    Animator anim;
    private PlayerControls controls = null;
    public Gamepad pDevice;
    public bool P1;
    public GameObject Weaponpoint1;
    public GameObject weaponpoint2;
    GameObject managerob;

    Vector3 moveDirection;
    Vector3 movement;

    public GameObject Weapon;
    GameObject WepLoc;

    CameraControl thecamref;

    TPSpawn spawnman;

    Quaternion offset = new Quaternion(-40, 180, 0, 0);
    private void Awake()
    {
        managerob = GameObject.Find("PlayerManager");
        controls = new PlayerControls();
        
    }

    private void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        spawnman = managerob.GetComponent<TPSpawn>();
        GameObject Wep = Instantiate(Weapon, Weaponpoint1.transform.position, Weaponpoint1.transform.rotation);
        WepLoc = GameObject.Find(Weapon.name + "(Clone)");
        if (P1)
        {
            WepLoc.transform.position = Weaponpoint1.transform.position;
            WepLoc.transform.rotation = new Quaternion(60, 180, -40, 0);
            WepLoc.transform.SetParent(Weaponpoint1.transform);
            WepLoc.transform.parent = Weaponpoint1.transform;
        }
        else
        {
            WepLoc.transform.SetParent(Weaponpoint1.transform);
            WepLoc.transform.parent = Weaponpoint1.transform;
            WepLoc.transform.position = Weaponpoint1.transform.position;
            WepLoc.transform.Rotate(-55, 180, 30);

        }
       
        
    }

    private void Update()
    {
        // Vector3 p1camangle = new Vector3()

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

         // horizontal spawnman.p1DevicePad.leftStick.x.ReadValue();
        // vertical spawnman.p1DevicePad.leftStick.y.ReadValue();



    }
    private void OnEnable()
    {  
        controls.Player.Enable();        
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    
    private void Move()
    {
        Vector3 moveDirection = new Vector3(Moveet.x, 0, Moveet.y);
        Vector3 movement = moveDirection * movementSpeed * Time.deltaTime;

         transform.Translate(movement);

         transform.position += moveDirection.x * transform.forward;


        if (movement.magnitude == 0)
        {
            anim.SetInteger("Condition", 0);
        }
        else if (movement.magnitude > 0 || movement.magnitude < 0)
        {
            anim.SetInteger("Condition", 1);
        }
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
