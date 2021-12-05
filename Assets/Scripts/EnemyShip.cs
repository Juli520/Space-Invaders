using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class EnemyShip : MonoBehaviourPun
{
    public float speed;
    public float fireRate;
    public int points = 500;
    public Projectile missile;
    
    [SerializeField] [HideInInspector] private float _currentFireRate;
    [SerializeField] [HideInInspector] private bool _goRight;
    
    private void Update()
    {
        if(!photonView.IsMine) return;
        
        transform.position += _goRight ? 
            Vector3.right * speed * Time.deltaTime : 
            Vector3.left * speed * Time.deltaTime;
        
        Shoot();
    }

    private void Shoot()
    {
        if (_currentFireRate <= 0)
        {
            PhotonNetwork.Instantiate(missile.name, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Quaternion.identity);
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
        
        if (other.gameObject.layer == 12)
            PhotonNetwork.Destroy(gameObject);
    }

    public void DestroyShip()
    {
        ScoreManager.Instance.AddScore(points);
        photonView.RPC("DestroyShipRPC", photonView.Owner);
    }

    [PunRPC]
    public void DestroyShipRPC()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}