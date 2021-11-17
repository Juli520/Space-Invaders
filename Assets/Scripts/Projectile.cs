using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    public Vector3 direction = Vector3.up;
    public float speed;
    private Player _player;

    public System.Action destroyed;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!photonView.IsMine) return;
        
        if (other.gameObject.layer == 8)
            _player.laserActive = true;

        if(destroyed != null)
            _player.LaserDestroyed();
     
        PhotonNetwork.Destroy(gameObject);
    }
}
