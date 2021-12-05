using System;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public float speed = 5f;
    public Projectile laser;
    public bool laserActive;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.position += Vector3.left * speed * Time.deltaTime;
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.position += Vector3.right * speed * Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        if (!laserActive)
            PhotonNetwork.Instantiate(laser.name, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }

    public void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!photonView.IsMine) return;
        
       if (other.gameObject.layer == 11)
        {
            _levelManager.SumPlayers();
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void DestroyPlayer()
    {
        photonView.RPC("DestroyPlayerRPC", photonView.Owner);
    }

    [PunRPC]
    public void DestroyPlayerRPC()
    {
        _levelManager.SumPlayers();
        PhotonNetwork.Destroy(gameObject);
    }
}
