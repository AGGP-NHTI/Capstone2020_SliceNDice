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

    public Transform Weaponpoint1;
    public Transform weaponpoint2;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    private void Update()
    { 

        Move();

     
       
    }
    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.StabAttack.performed += _ => StabAttack();
        controls.Player.FastAttack.performed += _ => FastAttack();
        controls.Player.HeavyAttack.performed += _ => HeavyAttack();
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

    public void FastAttack()
    {
        Debug.Log("Fast1");
        anim.SetTrigger("FastAtt");  
    }

    public void HeavyAttack()
    {
        Debug.Log("Heavy1");
        anim.SetTrigger("StrongAtt");
    }


    private void OnMove(InputValue value)
    {
        Moveet = value.Get<Vector2>();
    } 
}
