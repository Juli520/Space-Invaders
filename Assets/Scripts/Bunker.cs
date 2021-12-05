using Photon.Pun;
using UnityEngine;

public class Bunker : MonoBehaviourPun
{
    private int _currentLife;
    private int _startLife = 5;

    private void Awake()
    {
        _currentLife = _startLife;
    }

    public void TakeDamage(int dmg)
    {
        photonView.RPC("TakeDamageRPC", photonView.Owner, dmg);
            
    }

    [PunRPC]
    public void TakeDamageRPC(int dmg)
    {
        if (_currentLife > 0)
            _currentLife -= dmg;
        else if(_currentLife <= 0)
            PhotonNetwork.Destroy(gameObject);
    }
}
