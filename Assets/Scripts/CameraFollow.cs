using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Transform lookAtTarget;
    public bool hasDelay = false;
    public float delaySpeed = 3.0f;
    public bool isShaking = false;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        // ����, follow Target�� �ִٸ�...
        if (followTarget != null)
        {
            // ����, hasDelay ���� true���...
            if (hasDelay)
            {
                // Lerp�� �̿��ؼ� ��¦ ������ �Ѿư���.
                transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * delaySpeed);
            }
            // �׷��� �ʴٸ�...
            else
            {
                // �ش� Ÿ���� ���� �̵��Ѵ�.
                transform.position = followTarget.position;
            }
        }
        // ����, LookAt Target�� �ִٸ�...
        if (lookAtTarget != null && !isShaking)
        {
            // �ش� Ÿ���� �׻� �ٶ󺸵��� ȸ���Ѵ�.
            //Vector3 dir = lookAtTarget.position - transform.position;
            //transform.forward = dir;
            transform.rotation = lookAtTarget.rotation;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            string now = DateTime.Now.ToString();
            now = now.Replace(":", "-");

            ScreenCapture.CaptureScreenshot(Path.Combine(Application.dataPath, now + ".png"));
        }
    }

    public void SetCameraLookAtTarget(Transform target)
    {
        lookAtTarget = target;
    }

    public void SetCameraFollowTarget(Transform target)
    {
        followTarget = target;
        print("FollowTarget: " + target.name);
    }
}
