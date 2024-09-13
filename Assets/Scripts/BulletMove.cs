using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletMove : MonoBehaviourPun, IPunObservable
{
    public  float moveSpeed = 10;

    Vector3 position;

    void Start()
    {
        
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = position;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            // ����, �ε��� ����� Enemy ���̾ ������ �ִٸ�...
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // �÷��̾��� ü���� ���ҽ�Ų��(TakeDamage) ������: 20
                PlayerMove pm = other.transform.GetComponent<PlayerMove>();

                if (pm != null)
                {
                    pm.RPC_TakeDamage(20, photonView.ViewID);
                }
            }

            // �ڱ� �ڽ��� �����Ѵ�.
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if(stream.IsReading)
        {
            position = (Vector3)stream.ReceiveNext();
        }
    }
}
