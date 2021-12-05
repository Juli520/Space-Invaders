using System;
using Photon.Pun;
using UnityEngine;

public class Invador : MonoBehaviourPun
{
    public int points = 100;
    public System.Action killed;
    private Invaders _invaders;
    private int _damage = 5;

    private void Awake()
    {
        _invaders = FindObjectOfType<Invaders>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
        {
            Bunker bunker = other.gameObject.GetComponent<Bunker>();
            bunker.TakeDamage(_damage);
        }
    }

    public void SetParent()
    {
        photonView.RPC("SetParentRPC", RpcTarget.All);
    }

    [PunRPC]
    public void SetParentRPC()
    {
        var parent = FindObjectOfType<Invaders>();

        Transform invadersTransform = parent.transform;
        transform.SetParent(invadersTransform);
    }

    public void DestroyInvador()
    {
        ScoreManager.Instance.AddScore(points);
        photonView.RPC("DestroyInvadorRPC", photonView.Owner);
    }

    [PunRPC]
    public void DestroyInvadorRPC()
    {
        _invaders.InvaderKilled();
        
        PhotonNetwork.Destroy(gameObject);
    }
}
