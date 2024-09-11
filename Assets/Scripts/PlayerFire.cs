using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerFire : MonoBehaviourPun, IPunObservable
{
    public Transform[] sockets;
    public WeaponInfo myWeapon;
    public Animator anim;

    PlayerUI playerUI;
    int weaponNumber = -1;

    void Start()
    {
        myWeapon.weaponType = WeaponType.None;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            UIManager.main_ui.SetWeaponInfo(myWeapon);
        }

        playerUI = GetComponentInChildren<PlayerUI>();

        // ������ �÷��̾��� �г��Ӱ� �÷��� �Է��Ѵ�. (��: ���, ����: ����)
        Color nameColor = photonView.IsMine ? new Color(0, 1, 0) : new Color(1, 0, 0);
        playerUI.SetNickName(photonView.Owner.NickName, nameColor);

        // ������ �÷��̾��� ü�� ���� ������ �Է��Ѵ�. (��: ���, ����: ��Ȳ��)
        Color healthColor = photonView.IsMine ? new Color(0, 1, 0) : new Color(1, 0.4f, 0);
        playerUI.SetHpColor(healthColor);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && photonView.IsMine && myWeapon.weaponType != WeaponType.None)
        {
            Fire();
        }

        if (photonView.IsMine && Input.GetKeyDown(KeyCode.F) && myWeapon.weaponType != WeaponType.None)
        {
            RPC_DropWeapon();
        }

    }

    void Fire()
    {
        // ī�޶��� �߾��� �������� �������� ����ĳ��Ʈ�� �Ѵ�.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        bool result = Physics.Raycast(ray, out hitInfo, myWeapon.range, ~(1 << 6));
        if (result)
        {
            // ����, ���� ������Ʈ�� �±װ� "Enemy"���...
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // ������ ó���� �Ѵ�.
                hitInfo.transform.GetComponent<PlayerMove>().RPC_TakeDamage(myWeapon.attakPower, photonView.ViewID);
            }

            // �׷��� �ʴٸ�, ���� ��ġ�� ���� ����Ʈ�� ����Ѵ�.
            else
            {

            }
        }
        // �Ѿ��� ������ 1 ���ҽ�Ų��.(��, 0 ���ϰ� ���� �ʵ��� �Ѵ�.)
        myWeapon.ammo = Mathf.Max(--myWeapon.ammo, 0);
        UIManager.main_ui.SetWeaponInfo(myWeapon);
    }

    void RPC_DropWeapon()
    {
        // ���� ���⿡ �ִ� WeaponData ������Ʈ�� DropWeapon �Լ��� �����Ѵ�.
        WeaponData data = sockets[(int)myWeapon.weaponType].GetChild(0).GetComponent<WeaponData>();

        if (data != null)
        {
            data.DropWeapon(myWeapon);
        }

        photonView.RPC("DropMyWeapon", RpcTarget.AllBuffered);
    }


    [PunRPC]
    void DropMyWeapon()
    {
        // ���� ����(myWeapon ����)�� �ʱ�ȭ�Ѵ�.
        if (photonView.IsMine)
        {
            myWeapon = new WeaponInfo();
            UIManager.main_ui.SetWeaponInfo(myWeapon);
        }

        anim.SetBool("GetPistol", false);
        anim.SetBool("GetRifle", false);
    }

    public void RPC_GetWeapon(int ammo, float atkPower, float range, int weaponType)
    {
        photonView.RPC("GetWeapon", RpcTarget.AllBuffered, ammo, atkPower, range, weaponType);
    }


    [PunRPC]
    public void GetWeapon(int ammo, float atkPower, float range, int weaponType)
    {
        // �÷��̾��� ������ ���� ��ġ�� ���� ������Ų��.
        // - �ڽ��� ������ �ڽ� ������Ʈ�� ����ϰ�
        // - ���� �������� (0, 0, 0)���� �����.
        // - �ڽ��� �ڽ� ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
        // - ���� ������ �÷��̾�� �����Ѵ�.

        myWeapon.SetInformation(ammo, atkPower, range, (WeaponType)weaponType);

        if (photonView.IsMine)
        {
            UIManager.main_ui.SetWeaponInfo(myWeapon);

            if (myWeapon.weaponType == WeaponType.PistolType)
            {
                anim.SetBool("GetPistol", true);
                anim.SetBool("GetRifle", false);
            }
            else if (myWeapon.weaponType == WeaponType.RifleType)
            {
                anim.SetBool("GetRifle", true);
                anim.SetBool("GetPistol", false);
            }
        }
        else
        {
            if (weaponType == 0)
            {
                anim.SetBool("GetPistol", true);
                anim.SetBool("GetRifle", false);
            }
            else if (weaponType == 1)
            {
                anim.SetBool("GetRifle", true);
                anim.SetBool("GetPistol", false);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext((int)myWeapon.weaponType);
        }
        else if (stream.IsReading)
        {
            weaponNumber = (int)stream.ReceiveNext();
        }
    }
}
