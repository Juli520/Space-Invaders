using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public Player player;
    public Bunker bunker;
    public Transform[] spawnPoint;
    public Transform[] spawnsBunkers;
    
    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            PhotonNetwork.Instantiate(player.name, spawnPoint[0].position, Quaternion.identity);
        else if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
            PhotonNetwork.Instantiate(player.name, spawnPoint[1].position, Quaternion.identity);

        for (int i = 0; i < spawnsBunkers.Length; i++)
        {
            PhotonNetwork.Instantiate(bunker.name, spawnsBunkers[i].position, Quaternion.identity);
        }
    }
}
