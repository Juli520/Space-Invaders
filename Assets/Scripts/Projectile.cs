using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    public Vector3 direction = Vector3.up;
    public float speed;
    public System.Action<Projectile> destroyed;
    private Player _player;

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
        
        if (other.gameObject.layer == 11 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
        {
            //destroyed.Invoke(this);
            PhotonNetwork.Destroy(gameObject);
        }
        
        _player.LaserDestroyed(this);
    }
}
