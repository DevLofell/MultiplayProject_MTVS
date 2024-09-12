using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.EventSystems;
using System;

public class ChatManager : MonoBehaviourPun, IOnEventCallback
{
    public ScrollRect scrollChatWindow;
    public TMP_Text text_chatContent;
    public TMP_InputField input_chat;

    Image img_chatBackground;
    const byte chattingEvent = 1;

    private void OnEnable()
    {
        //PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    void Start()
    {
        input_chat.text = "";
        text_chatContent.text = "";

        // ��ǲ �ʵ��� ���� �̺�Ʈ�� SendMyMessage �Լ��� ���ε��Ѵ�.
        input_chat.onSubmit.AddListener(SendMyMessage);

        // ���� �ϴ����� ����Ʈ ������Ʈ�� �ǹ��� �����Ѵ�.
        scrollChatWindow.content.pivot = Vector2.zero;
        img_chatBackground = scrollChatWindow.transform.GetComponent<Image>();
        img_chatBackground.color = new Color32(255, 255, 255, 10);
    }

    void Update()
    {
        // �� Ű�� ������ ��ǲ �ʵ带 �����ϰ� �Ѵ�.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventSystem.current.SetSelectedGameObject(input_chat.gameObject);
            input_chat.OnPointerClick(new PointerEventData(EventSystem.current));
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void SendMyMessage(string msg)
    {
        if (input_chat.text.Length > 0)
        {
            // �̺�Ʈ�� ���� ����
            string currentTime = DateTime.Now.ToString("hh:mm:ss");

            object[] sendContent = new object[] { PhotonNetwork.NickName, msg, currentTime};

            // �۽� �ɼ�
            RaiseEventOptions eventOptions = new RaiseEventOptions();
            eventOptions.Receivers = ReceiverGroup.All;
            //eventOptions.CachingOption = EventCaching.DoNotCache;

            // �̺�Ʈ �۽� ����
            PhotonNetwork.RaiseEvent(1, sendContent, eventOptions, SendOptions.SendUnreliable);

            print("Send!");
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


    // ���� ���� ����ڷκ��� �̺�Ʈ�� ���� �� ����Ǵ� �Լ�
    public void OnEvent(EventData photonEvent)
    {
        print("Recieve!");

        // ����, ���� �̺�Ʈ�� ä�� �̺�Ʈ���...
        if(photonEvent.Code == chattingEvent)
        {
            // ���� ������ "�г���: ä�� ����" �������� ��ũ�Ѻ��� �ؽ�Ʈ�� �����Ѵ�.
            object[] receiveObjects = (object[])photonEvent.CustomData;
            string recieveMessage = $"\n[{receiveObjects[2].ToString()}]{receiveObjects[0].ToString()}: {receiveObjects[1].ToString()}";
            print("���� ����: " + recieveMessage);

            text_chatContent.text += recieveMessage;
            input_chat.text = "";
        }

        img_chatBackground.color = new Color32(255, 255, 255, 50);
        StopAllCoroutines();
        StartCoroutine(AlphaReturn(2.0f));
    }

    IEnumerator AlphaReturn(float time)
    {
        yield return new WaitForSeconds(time);
        img_chatBackground.color = new Color32(255, 255, 255, 10);
    }


    private void OnDisable()
    {
        //PhotonNetwork.NetworkingClient.RemoveCallbackTarget(this);
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
}
