using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public WeaponInfo weaponInfo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ����� Layer�� Player���...

        // �÷��̾��� ������ ���� ��ġ�� ���� ������Ų��.
        // - �ڽ��� ������ �ڽ� ������Ʈ�� ����ϰ�
        // - ���� �������� (0, 0, 0)���� �����.
        // - �ڽ��� �ڽ� ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
        // - ���� ������ �÷��̾�� �����Ѵ�.

    }

    public void DropWeapon(WeaponInfo currentInfo)
    {
        // �÷��̾��� ���Ͽ��� ���� �����.
        // - �θ�� ��ϵ� ������Ʈ�� ���� ������ ó���Ѵ�.
        // - �ڽ��� �ڽ� ������Ʈ�� Ȱ��ȭ�Ѵ�.
        // - �÷��̾��� ���� ������ �޾ƿ´�.

    }
}
