using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Molecules
{
    public class ReceptorSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Receptor[] _spawnPool;
        [SerializeField] private Vector2 _respawnTime = new Vector2(5f, 10f);
        
        private Receptor _currentReceptor;
        
        private bool _spawning = false;
        
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Color color = Color.magenta;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(Vector3.zero, 0.12f);
            
            color.a = 0.25f;
            Gizmos.color = color;
            Gizmos.DrawSphere(Vector3.zero, 0.12f);
        }
        
        private void SpawnReceptor()
        {
           _currentReceptor = Instantiate(_spawnPool[Random.Range(0, _spawnPool.Length)], transform.position, transform.rotation);
        }
        
        private IEnumerator SpawnRoutine()
        {
            _spawning = true;
            yield return new WaitForSeconds(Random.Range(_respawnTime.x, _respawnTime.y));
            SpawnReceptor();
            _spawning = false;
        }
        
        private void Update()
        {
            if(_spawning) return;
            if(_currentReceptor == null)  StartCoroutine(SpawnRoutine());
        }

    }
}