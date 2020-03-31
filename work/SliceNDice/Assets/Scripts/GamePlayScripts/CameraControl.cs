using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { get; private set; }
    public Camera Camera1;
    public GameObject Player1;
    public GameObject Player2;
    float offset = 5f;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    public bool isCameraFollowing = true;

    public Vector3 midpoint;

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

    }

    void Update()
    {
        // Debug.Log(isCameraFollowing);

        if (isCameraFollowing)
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

        Vector3 cameraDestination = midpoint - Cam.transform.forward * distance * zoomFactor;

        Vector3 cameracloseDestination = midpoint - Cam.transform.forward * (offset + 2.5f);

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
}