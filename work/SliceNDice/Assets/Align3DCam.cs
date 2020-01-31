using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Align3DCam : MonoBehaviour
{
    public Transform A1;
    public Transform A2;

    public CinemachineVirtualCamera virtualCamera;

    public CinemachineTransposer transposer;

    public bool hasVirtualCamera;

    public Vector3 framingNormal;

    public float distance;

    public float transposerLinearSlope;

    public float transposerLinearOffset;

    public float minDistance;

    public float minCamDistance;

    public float secondaryDistance;

    public float secondaryCamDistance;

    void CalculateSlopes()
    {
        if(virtualCamera == null)
        {
            return;
        }
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        if(transposer == null)
        {
            return;
        }

        if (!Application.isPlaying)
        {
            minDistance = Vector3.Distance(A1.position, A2.position);
            distance = minDistance;
            minCamDistance = transposer.m_FollowOffset.magnitude;
        }

        transposerLinearSlope = (secondaryCamDistance - minCamDistance) / (secondaryDistance - minDistance);
        transposerLinearOffset = minCamDistance - (transposerLinearSlope * minDistance);
    }



    private void Awake()
    {
        hasVirtualCamera = virtualCamera != null;
        if (hasVirtualCamera)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if(transposer == null)
            {
                hasVirtualCamera = false;
            }
            else
            {
                framingNormal = transposer.m_FollowOffset;
                framingNormal.Normalize();
            }
        }
    }

    void Update()
    {
        Vector3 diff = A1.position - A2.position;
        distance = diff.magnitude;
        diff.y = 0f;
        diff.Normalize();
        if (hasVirtualCamera)
        {
            transposer.m_FollowOffset = framingNormal * (Mathf.Max(minDistance, distance) * transposerLinearSlope * transposerLinearOffset);

        }
        if(Mathf.Approximately(0f, diff.sqrMagnitude))
        {
            return;
        }

        Quaternion q = Quaternion.LookRotation(diff, Vector3.up) * Quaternion.Euler(0, 90, 0);

        Quaternion qA = q * Quaternion.Euler(0, 180, 0);

        float angle = Quaternion.Angle(q, transform.rotation);

        float angleA = Quaternion.Angle(qA, transform.rotation);

        if(angle < angleA)
        {
            transform.rotation = q;
        }
        else
        {
            transform.rotation = qA;
        }
    }

  
   
   
}
