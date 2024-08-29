using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform[] sockets;
    public WeaponInfo myWeapon;

    void Start()
    {
        myWeapon.weaponType = WeaponType.None;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myWeapon.weaponType != WeaponType.None)
        {
            Fire();
        }

        if(Input.GetKeyDown(KeyCode.F) && myWeapon.weaponType != WeaponType.None)
        {
             DropMyWeapon();

        }

    }

    void Fire()
    {
        // ���콺 �� Ŭ���� �ϸ� ī�޶��� �߾��� �������� �������� ����ĳ��Ʈ�� �Ѵ�.
        // ����, ���� ������Ʈ�� �±װ� "Enemy"���...
        // ������ ó���� �Ѵ�.

        // �׷��� �ʴٸ�, ���� ��ġ�� ���� ����Ʈ�� ����Ѵ�.

        // �Ѿ��� ������ 1 ���ҽ�Ų��.(��, 0 ���ϰ� ���� �ʵ��� �Ѵ�.)
    }

    void DropMyWeapon()
    {
        // ���� ���⿡ �ִ� WeaponData ������Ʈ�� DropWeapon �Լ��� �����Ѵ�.

        // ���� ����(myWeapon ����)�� �ʱ�ȭ�Ѵ�.

    }
}
