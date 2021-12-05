using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    public Vector3 direction = Vector3.up;
    public float speed;
    private int _damage = 1;

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!photonView.IsMine) return;

        if (other.gameObject.layer == 8)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.DestroyPlayer();
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.gameObject.layer == 11)
        {
            Invador invador = other.gameObject.GetComponent<Invador>();
            invador.DestroyInvador();
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.gameObject.layer == 13)
        {
            Bunker bunker = other.gameObject.GetComponent<Bunker>();
            bunker.TakeDamage(_damage);
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.gameObject.layer == 14)
        {
            EnemyShip ship = other.gameObject.GetComponent<EnemyShip>();
            ship.DestroyShip();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
