using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTrackingList : MonoBehaviour
{
    public UDPConnector conn;


    void Start()
    {
        
    }

    void Update()
    {
        // ����, Ʈ��ŷ�� �����Ͱ� �ִٸ�...
        if(conn.receivedPoseList.Count > 0)
        {
            // Ʈ��ŷ�� ���� ���� ��� �ڽ� ������Ʈ�� ���� ��ġ ������ �����Ѵ�.
            for(int i = 0; i < conn.receivedPoseList.Count; i++)
            {
                transform.GetChild(i).localPosition = conn.receivedPoseList[i];
            }
        }
    }
}
