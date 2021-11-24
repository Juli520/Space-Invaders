using System;
using UnityEngine;
using Photon.Pun;

public class EnemyShip : MonoBehaviourPun
{
    public float speed;
    public float fireRate;
    public int points = 500;
    public Projectile missile;
    public Action<EnemyShip> killed;

    [SerializeField] [HideInInspector] private float _currentFireRate;
    [SerializeField] [HideInInspector] private bool _goRight;

    private void Awake()
    {
        _currentFireRate = fireRate;
    }

    private void Start()
    {
        killed += Invaders.Instance.ShipKilled;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        
        transform.position += _goRight ? 
            Vector3.right * speed * Time.deltaTime : 
            Vector3.left * speed * Time.deltaTime;
        
        Shoot();
    }

    private void Shoot()
    {
        if (_currentFireRate <= 0)
        {
            PhotonNetwork.Instantiate(missile.name, transform.position, Quaternion.identity);
            _currentFireRate = fireRate;
        }
        else
            _currentFireRate -= Time.deltaTime;
    }

    public void SetSide(bool goRight)
    {
        _goRight = goRight;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!photonView.IsMine) return;
        
        if (other.gameObject.layer == 9)
        {
            //killed.Invoke(this);
            ScoreManager.Instance.AddScore(points);
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.gameObject.layer == 12)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}