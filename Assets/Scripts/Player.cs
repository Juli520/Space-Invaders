using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public float speed = 5f;
    public Projectile laser;
    private bool _laserActive;

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
        if (!_laserActive)
        {
            Projectile projectile = Instantiate(laser, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            projectile.destroyed = LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }
}
