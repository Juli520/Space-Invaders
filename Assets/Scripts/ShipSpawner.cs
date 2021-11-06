using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class ShipSpawner : MonoBehaviourPun
{
    public float spawnTime = 15f;
    public EnemyShip ship;
    public List<Transform> spawnLocations;

    [SerializeField, HideInInspector] private float _currentSpawnTime;

    private void Awake()
    {
        _currentSpawnTime = spawnTime;
    }

    private void Update()
    {
        if (_currentSpawnTime <= 0)
            SpawnShip();
        else
            _currentSpawnTime -= Time.deltaTime;
    }

    private void SpawnShip()
    {
        bool goRight = new Random().Next(2) == 1;
        
        GameObject enemy =  PhotonNetwork.Instantiate(ship.name, goRight ? spawnLocations[0].position : spawnLocations[1].position, Quaternion.identity);
        enemy.GetComponent<EnemyShip>().SetSide(goRight);

        _currentSpawnTime = spawnTime;
    }
}
