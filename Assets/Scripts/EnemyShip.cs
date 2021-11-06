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
    public Projectile missile;
    public Action killed;
    
    [SerializeField] [HideInInspector] private float _currentFireRate;
    [SerializeField] [HideInInspector] private bool _goRight;

    private void Awake()
    {
        _currentFireRate = fireRate;
    }

    private void Update()
    {
        transform.position += _goRight ? 
            Vector3.right * speed * Time.deltaTime : 
            Vector3.left * speed * Time.deltaTime;
        
        Shoot();
    }

    private void Shoot()
    {
        if (_currentFireRate <= 0)
            PhotonNetwork.Instantiate(missile.name, transform.position, Quaternion.identity);
        else
            _currentFireRate -= Time.deltaTime;
    }

    public void SetSide(bool goRight)
    {
        _goRight = goRight;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            killed.Invoke();
            gameObject.SetActive(false);
        }
    }
}