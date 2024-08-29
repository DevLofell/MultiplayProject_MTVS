using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetTrackingTransform : MonoBehaviour
{
    public UDPConnector conn;
    public PoseName targetPose;
    public PoseName hintPose;
    public Transform trackingList;

    void Start()
    {
        
    }

    void Update()
    {
        if(conn != null)
        {
            // �ڽ� ������Ʈ �߿��� ���� ���� ������ ����Ʈ���� �ش��ϴ� ��ȣ�� ���� ���� �����Ѵ�.
            //transform.GetChild(0).localPosition = conn.receivedPoseList[(int)targetPose];
            //transform.GetChild(1).localPosition = conn.receivedPoseList[(int)hintPose];

            transform.GetChild(0).position = trackingList.GetChild((int)targetPose).position;
            transform.GetChild(1).position = trackingList.GetChild((int)hintPose).position;
        }
    }
}
