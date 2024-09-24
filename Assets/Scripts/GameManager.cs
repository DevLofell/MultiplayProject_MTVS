using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text text_playerList;
    public static GameManager gm;
    public GameObject myPlayer;
   

    string[] characterName = new string[3] { "Player_Red", "Player_Green", "Player_Blue" };

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        StartCoroutine(SpawnPlayer());

        // OnPhotonSerializeView 에서 데이터 전송 빈도 수 설정하기(per seconds)
        PhotonNetwork.SerializationRate = 30;
        // 대부분의 데이터 전송 빈도 수 설정하기(per seconds)
        PhotonNetwork.SendRate = 30;

        GameObject playerListUI = GameObject.Find("text_PlayerList");
        text_playerList = playerListUI.GetComponent<TMP_Text>();
    }

    IEnumerator SpawnPlayer()
    {
        // 룸에 입장이 완료될 때까지 기다린다.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });

        Vector2 randomPos = Random.insideUnitCircle * 5.0f;
        Vector3 initPosition = new Vector3(randomPos.x, 1.0f, randomPos.y);

        // 1. 프리팹 번호로 불러오기
        //int playerNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["CHARACTER_COLOR"];
        //myPlayer = PhotonNetwork.Instantiate(characterName[playerNum], initPosition, Quaternion.identity);

        // 2. 머티리얼만 바꾸기
        int matNumber = (int)PhotonNetwork.LocalPlayer.CustomProperties["CHARACTER_COLOR"];
        myPlayer = PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);
        myPlayer.GetComponent<PlayerSetter>().SetBodyColor(matNumber);
    }

    void Update()
    {
        Dictionary<int, Player> playerDict = PhotonNetwork.CurrentRoom.Players;

        List<string> playerNames = new List<string>();
        string masterName = "";

        foreach(KeyValuePair<int, Player> player in playerDict)
        {
            if (player.Value.IsMasterClient)
            {
                masterName = player.Value.NickName;
            }
            else
            {
                playerNames.Add(player.Value.NickName);
            }
        }
        playerNames.Sort();

        if(text_playerList == null)
        {
            GameObject playerListUI = GameObject.Find("text_PlayerList");
            text_playerList = playerListUI.GetComponent<TMP_Text>();
        }

        text_playerList.text = "<size=60><color=#ff0000>" + masterName + "</size></color>\n";
        foreach (string name in playerNames)
        {
            text_playerList.text += name + "\n";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        print($"{newPlayer.NickName}님이 입장하셨습니다.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        print($"{otherPlayer.NickName}님이 퇴장하셨습니다.");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Destroy(myPlayer);
        Destroy(gameObject);
    }
}
