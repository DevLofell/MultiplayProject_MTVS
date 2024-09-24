using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetter : MonoBehaviourPun
{
    public SkinnedMeshRenderer mr;
    public Material[] characterMaterials;

    public void SetBodyColor(int matNumber)
    {
        if(photonView.IsMine)
        {
            photonView.RPC("RPC_SetBodyColor", RpcTarget.AllBuffered, matNumber);
        }    
    }

    [PunRPC]
    void RPC_SetBodyColor(int matNumber)
    {
        Material[] currentMats = mr.materials;
        currentMats[0] = characterMaterials[matNumber];
        mr.materials = currentMats;
    }    
}
