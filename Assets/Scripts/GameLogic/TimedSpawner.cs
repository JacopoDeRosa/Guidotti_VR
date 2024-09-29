using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    public class TimedSpawner : Spawner
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _minInterval, _maxInterval;
        [SerializeField] private float _spawnRadius;
    
        private float _nextSpawnTime = 0f;

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
    
        private void Update()
        {
            if (_spawning && Time.time >= _nextSpawnTime)
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

        protected override void OnSetSpawning(bool value)
        {
            if(value) _nextSpawnTime = Time.time + Random.Range(_minInterval, _maxInterval);
        }
    }
}
