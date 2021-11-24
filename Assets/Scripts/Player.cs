using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public float speed = 5f;
    public Projectile laser;
    public System.Action killed;
    public bool laserActive { get; private set; }

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
        {
            laserActive = true;
            
            PhotonNetwork.Instantiate(laser.name, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            laser.destroyed += LaserDestroyed;
        }
    }

    public void LaserDestroyed(Projectile laser)
    {
        laserActive = false;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(!photonView.isMine) return;
    //
    //    if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
    //    {
    //        if(killed != null)
    //            killed.Invoke();
    //    }
    //}
}