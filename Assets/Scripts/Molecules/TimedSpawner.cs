using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _minInterval, _maxInterval;
    [SerializeField] private float _spawnRadius;
    
    private float _nextSpawnTime;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, _spawnRadius);
        
        Color colorAlpha = Color.yellow;
        colorAlpha.a = 0.5f;
        Gizmos.color = colorAlpha;
        Gizmos.DrawSphere(Vector3.zero, _spawnRadius);
    }

    private void Start()
    {
        _nextSpawnTime = Time.time + Random.Range(_minInterval, _maxInterval);
    }
    
    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            Spawn();
            _nextSpawnTime = Time.time + Random.Range(_minInterval, _maxInterval);
        }
    }
    
    private void Spawn()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * _spawnRadius;
        Instantiate(_prefab, spawnPosition, Quaternion.identity);
    }
}
