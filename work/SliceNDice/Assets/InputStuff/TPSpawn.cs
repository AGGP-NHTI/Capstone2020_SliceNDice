using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TPSpawn : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public PlayerInputManager manager;
    public InputDevice p1Device;
    public InputDevice p2Device;
    public CameraControl control;

    public const int NOSPLITSCREEN = -1;
    public const string NOCONTROLSCHEME = null; 

    void Start()
    {
        Gamepad[] pads = Gamepad.all.ToArray();
        if (pads.Length < 2)
        {
            Debug.LogError("Connect More Controllers, Sucka!!!!!!!!");
            return; 
        }

        p1Device = pads[0].device;
        p2Device = pads[1].device;

        manager.playerPrefab = P1.gameObject;
        manager.JoinPlayer(0, NOSPLITSCREEN, NOCONTROLSCHEME, p1Device);
        manager.playerPrefab = P2.gameObject;
        manager.JoinPlayer(1, NOSPLITSCREEN, NOCONTROLSCHEME, p2Device);
        control = GameObject.Find("CameraManager").GetComponent<CameraControl>();

        control.Player1 = GameObject.Find(P1.name + "(Clone)").GetComponent<Transform>();
        control.Player2 = GameObject.Find(P2.name + "(Clone)").GetComponent<Transform>();









    }

  
    void Update()
    {

    }

    private void OnJoin()
    {
  

    }

}
