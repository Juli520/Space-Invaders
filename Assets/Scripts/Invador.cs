using Photon.Pun;
using UnityEngine;

public class Invador : MonoBehaviourPun
{
    public int points = 100;
    public System.Action killed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!photonView.IsMine) return;

        if (other.gameObject.layer == 9)
        {
            killed.Invoke();
            
            ScoreManager.Instance.AddScore(points);
            PhotonNetwork.Destroy(gameObject);
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
}
