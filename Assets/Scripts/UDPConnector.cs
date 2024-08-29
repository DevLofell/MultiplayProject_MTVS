using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;

public class UDPConnector : MonoBehaviour
{
    public int portNumber = 5000;
    public List<Vector3> receivedPoseList = new List<Vector3>();

    Thread udpThread;
    UdpClient receivePort;

    void Start()
    {
        InitializeUDPThread();
    }

    void InitializeUDPThread()
    {
        // ��׶��忡�� �� Thread�� �����ϰ� �ʹ�.
        udpThread = new Thread(new ThreadStart(ReceiveData));
        udpThread.IsBackground = true;
        udpThread.Start();
    }    

    void ReceiveData()
    {
        // ���� ���� �� ���� Ŭ���̾�Ʈ�� �����Ѵ�.
        receivePort = new UdpClient(portNumber);
        IPEndPoint remoteClient = new IPEndPoint(IPAddress.Any, portNumber);
        try
        {
            while (true)
            {
                // ��� ����� ���̳ʸ� �����͸� �޴´�.
                byte[] bins = receivePort.Receive(ref remoteClient);
                string binaryString = Encoding.UTF8.GetString(bins);
                //print($"���� ������: {binaryString}");

                PoseList jsonData = JsonUtility.FromJson<PoseList>(binaryString);

                receivedPoseList.Clear();

                // ��ȯ�� json �迭 �����͸� ���� ������ ����Ʈ�� �����Ѵ�.
                foreach(PoseData poseData in jsonData.landmarkList)
                {
                    Vector3 receiveVector = new Vector3(poseData.x, poseData.y, poseData.z);
                    receivedPoseList.Add(receiveVector);
                }
            }
        }
        catch (SocketException message)
        {
            // ��� ���� �ڵ� �� ���� ������ ����Ѵ�.
            Debug.LogError($"Error Code {message.ErrorCode} - {message}");
        }
        finally
        {
            receivePort.Close();
        }
    }

    private void OnDisable()
    {
        // UDP ��Ʈ���� �����Ѵ�.
        receivePort.Close();
    }
    void Update()
    {
        
    }
}


[System.Serializable]
public struct PoseData
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public struct PoseList
{
    public List<PoseData> landmarkList;
}