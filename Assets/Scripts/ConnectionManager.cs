using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;
using System;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    void Start()
    {
        Screen.SetResolution(640, 480, FullScreenMode.Windowed);
    }

    void Update()
    {
        
    }

    public void StartLogin()
    {
        // 접속을 위한 설정
        if (LobbyUIController.lobbyUI.input_nickName.text.Length > 0)
        {
            PhotonNetwork.GameVersion = "1.0.0";
            PhotonNetwork.NickName = LobbyUIController.lobbyUI.input_nickName.text;
            PhotonNetwork.AutomaticallySyncScene = true;

            // 접속을 서버에 요청하기
            PhotonNetwork.ConnectUsingSettings();
            LobbyUIController.lobbyUI.btn_login.interactable = false;
        }
        
    }

    public override void OnConnected()
    {
        base.OnConnected();

        // 네임 서버에 접속이 완료되었음을 알려준다.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        // 실패 원인을 출력한다.
        Debug.LogError("Disconnected from Server - " + cause);
        LobbyUIController.lobbyUI.btn_login.interactable = true;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // 마스터 서버에 접속이 완료되었음을 알려준다.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");

        // 서버의 로비로 들어간다.
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        // 서버 로비에 들어갔음을 알려준다.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
        LobbyUIController.lobbyUI.ShowRoomPanel();
    }

    public void CreateRoom()
    {
        string roomName = LobbyUIController.lobbyUI.roomSetting[0].text;
        int playerCount = Convert.ToInt32(LobbyUIController.lobbyUI.roomSetting[1].text);

        if (roomName.Length > 0 && playerCount > 1)
        {
            // 나의 룸을 만든다.
            RoomOptions roomOpt = new RoomOptions();
            roomOpt.MaxPlayers = playerCount;
            roomOpt.IsOpen = true;
            roomOpt.IsVisible = true;

            PhotonNetwork.CreateRoom(roomName, roomOpt, TypedLobby.Default);
        }
    }

    public void JoinRoom()
    {
        string roomName = LobbyUIController.lobbyUI.roomSetting[0].text;

        if (roomName.Length > 0)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        // 성공적으로 방이 개설되었음을 알려준다.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
        LobbyUIController.lobbyUI.PrintLog("방 만들어짐!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // 성공적으로 방에 입장되었음을 알려준다.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
        LobbyUIController.lobbyUI.PrintLog("방에 입장 성공!");

        // 방에 입장한 친구들은 모두 1번 씬으로 이동하자!
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        // 룸에 입장이 실패한 이유를 출력한다.
        Debug.LogError(message);
        LobbyUIController.lobbyUI.PrintLog("입장 실패..." + message);
    }

    // 룸에 다른 플레이어가 입장했을 때의 콜백 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        string playerMsg = $"{newPlayer.NickName}님이 입장하셨습니다.";
        LobbyUIController.lobbyUI.PrintLog(playerMsg);
    }

    // 룸에 있던 다른 플레이어가 퇴장했을 때의 콜백 함수
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        string playerMsg = $"{otherPlayer.NickName}님이 퇴장하셨습니다.";
        LobbyUIController.lobbyUI.PrintLog(playerMsg);
    }

    // 현재 로비에서 룸의 변경사항을 알려주는 콜백 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach(RoomInfo room in roomList)
        {
            // 만일, 갱신된 룸 정보가 제거 리스트에 있다면...
            if (room.RemovedFromList)
            {
                // cachedRoomList에서 해당 룸을 제거한다.
                cachedRoomList.Remove(room);
            }
            // 그렇지 않다면...
            else
            {
                // 만일, 이미 cachedRoomList에 있는 방이라면...
                if (cachedRoomList.Contains(room))
                {
                    // 기존 룸 정보를 제거한다.
                    cachedRoomList.Remove(room);
                }
                // 새 룸을 cachedRoomList에 추가한다.
                cachedRoomList.Add(room);
            }
        }

        foreach(RoomInfo room in cachedRoomList)
        {
            string text = $"Room Name: {room.Name}, Player Limit: {room.MaxPlayers}. Current Player Count: {room.PlayerCount}";
            print(text);
            LobbyUIController.lobbyUI.PrintLog(text);
        }
    }

}
