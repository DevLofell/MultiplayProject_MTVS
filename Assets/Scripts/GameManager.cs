using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPun
{
    public TMP_Text text_playerList;

    void Start()
    {
        StartCoroutine(SpawnPlayer());

        // OnPhotonSerializeView 에서 데이터 전송 빈도 수 설정하기(per seconds)
        PhotonNetwork.SerializationRate = 30;
        // 대부분의 데이터 전송 빈도 수 설정하기(per seconds)
        PhotonNetwork.SendRate = 30;
    }

    IEnumerator SpawnPlayer()
    {
        // 룸에 입장이 완료될 때까지 기다린다.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });

        Vector2 randomPos = Random.insideUnitCircle * 5.0f;
        Vector3 initPosition = new Vector3(randomPos.x, 1.0f, randomPos.y);

        GameObject player = PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);

      
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

        text_playerList.text = "<size=60><color=#ff0000>" + masterName + "</size></color>\n";
        foreach (string name in playerNames)
        {
            text_playerList.text += name + "\n";
        }
    }
}
