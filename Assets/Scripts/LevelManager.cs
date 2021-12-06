using System;
using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public Player player;
    public Bunker bunker;
    public Transform[] spawnPoint;
    public Transform[] spawnBunker;

    [Header("Change Scenes")]
    [HideInInspector] public int playersDead;
    public string loseScene;
    public string winScene;

    private Invaders _invaders;

    private void Awake()
    {
        _invaders = GetComponent<Invaders>();
    }

    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            PhotonNetwork.Instantiate(player.name, spawnPoint[0].position, Quaternion.identity);
        else if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
            PhotonNetwork.Instantiate(player.name, spawnPoint[1].position, Quaternion.identity);
        
        if(!photonView.IsMine) return;

        for (int i = 0; i < spawnBunker.Length; i++)
            PhotonNetwork.Instantiate(bunker.name, spawnBunker[i].position, Quaternion.identity);
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if(_invaders.AmountAlive == 0)
            photonView.RPC("LoadWinSceneRPC", RpcTarget.All);
        
        if(playersDead == 2)
            photonView.RPC("LoadLoseSceneRPC", RpcTarget.All);
    }

    public void SumPlayers()
    {
        photonView.RPC("SumPlayersRPC", RpcTarget.All);
    }

    [PunRPC]
    public void SumPlayersRPC()
    {
        playersDead++;
    }

    [PunRPC]
    public void LoadLoseSceneRPC()
    {
        if(loseScene == string.Empty) return;
        
        PhotonNetwork.LoadLevel(loseScene);
    }    
    
    [PunRPC]
    public void LoadWinSceneRPC()
    {
        if(winScene == string.Empty) return;
        
        PhotonNetwork.LoadLevel(winScene);
    }
}
