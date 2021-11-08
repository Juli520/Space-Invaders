using UnityEngine;

public class Projectile : MonoBehaviour
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
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.layer == 8)
            _player.laserActive = true;

        if(destroyed != null)
            _player.LaserDestroyed();
        Destroy(gameObject);
    }
}
