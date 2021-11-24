using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviourPun
{
    private int _currentLife;
    private int _startLife = 5;

    private void Awake()
    {
        _currentLife = _startLife;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!photonView.IsMine) return;
        
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            if (_currentLife <= 0)
                PhotonNetwork.Destroy(gameObject);
            else
                _currentLife--;
        }
        
        if(other.gameObject.layer == 11)
            PhotonNetwork.Destroy(gameObject);
    }
}