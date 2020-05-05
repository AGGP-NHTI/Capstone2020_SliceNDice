using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public InputDevice p1Device;
    public InputDevice p2Device;
    public Gamepad p1DevicePad;
    public Gamepad p2DevicePad;

    GameObject P1;
    GameObject P2;
    public bool matchP1Won = false;
    public bool matchP2Won = false;

    public static CameraControl Instance { get; private set; }
    public Camera Camera1;
    public GameObject Player1;
    public GameObject Player2;
    float offset = 5f;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    public bool isCameraFollowing = true;

    public Vector3 midpoint;
    public Vector3 camoffset = new Vector3(0, 1, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
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
        p1DevicePad = pads[0];
        p2DevicePad = pads[1];
    }

    void Update()
    {
        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");
        // Debug.Log(isCameraFollowing);

        if (P1.GetComponent<Character>().playerHealth <= 0 && P2.GetComponent<Character>().playerHealth > 0) //p2wins
        {
            matchP2Won = true;
            //transform.LookAt(Player2.transform);
            FixedCameraFollowSmooth(Camera1, Player2.transform, Player2.transform, Player2, Player2);

        }

        if (P1.GetComponent<Character>().playerHealth > 0 && P2.GetComponent<Character>().playerHealth <= 0) //p1wins
        {
            matchP1Won = true;
            //transform.LookAt(Player1.transform);
            FixedCameraFollowSmooth(Camera1, Player1.transform, Player1.transform, Player1, Player1);

        }

        if (isCameraFollowing && matchP1Won == false && matchP2Won == false)
        {
            FixedCameraFollowSmooth(Camera1, Player1.transform, Player2.transform, Player1, Player2);
        }

    }

    public void FixedCameraFollowSmooth(Camera Cam, Transform t1, Transform t2, GameObject p1, GameObject p2)
    {
        float zoomFactor = 1.5f;
        float followTimeDelta = 0.8f;

        midpoint = (t1.position + t2.position) / 2f;

        float distance = (t1.position - t2.position).magnitude;

        targetRotation = Quaternion.LookRotation(midpoint, Vector3.up);

        Vector3 cameraDestination = (midpoint + camoffset) - Cam.transform.forward * distance * zoomFactor;

        Vector3 cameracloseDestination = (midpoint + camoffset) - Cam.transform.forward * (offset + 2.5f);

        if (Cam.orthographic)
        {
            Cam.orthographicSize = distance;

        }





        if ((t1.position - midpoint).magnitude > .5)
        {
            p1.transform.LookAt(midpoint, Vector3.up);
        }

        if ((t2.position - midpoint).magnitude > .5)
        {
            p2.transform.LookAt(midpoint, Vector3.up);
        }





        if (distance > offset)
        {
            Cam.transform.position = Vector3.Slerp(Cam.transform.position, cameraDestination, followTimeDelta);
        }
        if (distance <= offset)
        {
            Cam.transform.position = Vector3.Slerp(Cam.transform.position, cameracloseDestination, followTimeDelta);
        }


        //Debug.Log("Camera Should Move");

        if ((cameraDestination - Cam.transform.position).magnitude <= 0.05f)
        {
            Cam.transform.position = cameraDestination;

        }

    }

    public void FollowPlayerOne()
    {

    }

    
}