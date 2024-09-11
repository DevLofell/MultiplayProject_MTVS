using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager main_ui;
    public TMP_Text[] weaponInfo;
    public Button btn_leave;

    private void Awake()
    {
        if(main_ui == null)
        {
            main_ui = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        btn_leave.onClick.AddListener(LeaveCurrentRoom);
    }

    public void SetWeaponInfo(WeaponInfo info)
    {
        weaponInfo[0].text = $"Ammo: <color=#dceb15>{info.ammo}</color>";
        weaponInfo[1].text = $"Damage: <color=#ff0000>{info.attakPower}</color>";
        weaponInfo[2].text = $"Weapon Type: <i>{info.weaponType}</i>";
    }

    public void LeaveCurrentRoom()
    {
        // 현재 방을 떠난다.
        PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene(0);
        PhotonNetwork.LoadLevel(0);
    }
}
