using System.Collections;
using Molecules;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    public class ReceptorSpawnPoint : Spawner
    {
        [SerializeField] private Receptor[] _spawnPool;
        [SerializeField] private Vector2 _respawnTime = new Vector2(5f, 10f);
        [SerializeField] private float _instantSpawnChance = 0.1f;
        
        private Receptor _currentReceptor;
        private bool _busy;
        
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
            _busy = true;
            yield return new WaitForSeconds(Random.Range(_respawnTime.x, _respawnTime.y));
            SpawnReceptor();
            _busy = false;
        }

        protected override void OnSetSpawning(bool value)
        {
            if (value)
            {
                StartCoroutine(SpawnRoutine());
                if(Random.value <= _instantSpawnChance) SpawnReceptor();
            }
            else
            {
                StopAllCoroutines();
                if(_currentReceptor != null) Destroy(_currentReceptor.gameObject);
            }
        }

        private void Update()
        {
            if(_busy || !_spawning) return;
            if(_currentReceptor == null)  StartCoroutine(SpawnRoutine());
        }

    }
}