using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 5f;
    Vector2 Moveet;
    Animator anim;
    private PlayerControls controls = null;
    public InputDevice p1Device;

    public Transform Weaponpoint1;
    public Transform weaponpoint2;
    GameObject managerob;


    TPSpawn spawnman;
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

        Move();
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
        Vector3 movement = new Vector3(Moveet.x, 0, Moveet.y) * movementSpeed * Time.deltaTime;  
        transform.Translate(movement);

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
