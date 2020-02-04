using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera Camera1;
    public Transform Player1;
    public Transform Player2;
    float offset = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        FixedCameraFollowSmooth(Camera1, Player1, Player2);
    }

    public void FixedCameraFollowSmooth(Camera Cam, Transform t1, Transform t2)
    {
        float zoomFactor = 1.5f;
        float followTimeDelta = 0.8f;


        Vector3 midpoint = (t1.position + t2.position) / 2f;

        

        float distance = (t1.position - t2.position).magnitude;

        Vector3 cameraDestination = midpoint - Cam.transform.forward * distance * zoomFactor;

        Vector3 cameracloseDestination = midpoint - Cam.transform.forward * (offset+2.5f);

        Debug.Log("Cam destination = " + cameraDestination);

        if (Cam.orthographic)
        {
            Cam.orthographicSize = distance;
            Debug.Log("Camera is Orthographic");
        }

        if(distance > offset)
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
            Debug.Log("Camera Should Snap");
        }
            





    }
}
