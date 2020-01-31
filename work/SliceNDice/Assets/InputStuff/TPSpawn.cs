using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TPSpawn : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public PlayerInputManager manager;
    public InputDevice p1Device;
    public InputDevice p2Device;
    public CinemachineTargetGroup targetGroup;
    public Align3DCam align;
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
        targetGroup = GameObject.Find("TargetGroup1").GetComponent<CinemachineTargetGroup>();
        align = GameObject.Find("TargetGroup1").GetComponent<Align3DCam>();
        CinemachineTargetGroup.Target target1;
        target1.target = P1.transform;
        target1.weight = 1;
        target1.radius = 1.7f;
        CinemachineTargetGroup.Target target2;
        target2.target = P2.transform;
        target2.weight = 1;
        target2.radius = 1.7f;
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            if (targetGroup.m_Targets[i].target == null)
            {
                targetGroup.m_Targets.SetValue(target1, 0);
                targetGroup.m_Targets.SetValue(target2, 1);
                align.A1 = P1.transform;
                align.A2 = P2.transform;
                return;
            }
        }
          





    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnJoin()
    {
       //manager.playerPrefab = P2.gameObject;
       // manager.JoinPlayer(1);

    }

}
