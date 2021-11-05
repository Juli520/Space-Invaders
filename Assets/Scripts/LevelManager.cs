using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public Player player;
    public Transform[] spawnPoint;
    
    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            PhotonNetwork.Instantiate(player.name, spawnPoint[0].position, Quaternion.identity);
        else if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
            PhotonNetwork.Instantiate(player.name, spawnPoint[1].position, Quaternion.identity);
    }
}
