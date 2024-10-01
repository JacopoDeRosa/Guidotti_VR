using System;
using System.Collections;
using Molecules;
using UnityEngine;

namespace GameLogic
{
    public class InvokanSpawner : Spawner
    {
        [SerializeField] private Invokan _invokanPrefab;
        [SerializeField] private float _spawnDelay = 1f;
        
        private Invokan _spawnedInvokan;

        private bool _busy;
        
        private void OnDrawGizmos()
        {
            Color color = Color.cyan;
            Gizmos.color = color;
            
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, 0.15f);
            color.a = 0.25f;
            Gizmos.color = color;
            Gizmos.DrawSphere(Vector3.zero, 0.15f);

        }
        

        private void SpawnInvokan()
        {
            StartCoroutine(SpawnInvokanRoutine());
        }
        
        private IEnumerator SpawnInvokanRoutine()
        {
            _busy = true;
            
            yield return new WaitForSeconds(_spawnDelay);
            
            _spawnedInvokan = Instantiate(_invokanPrefab, transform.position, transform.rotation);
            
            _spawnedInvokan.transform.localScale = Vector3.zero;
            
            _spawnedInvokan.OnHandPickUp += OnPickUp;
            
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                _spawnedInvokan.transform.localScale = Vector3.one * t;
                yield return null;
            }
            
            _busy = false;
        }
        
        private void Update()
        {
            if(_spawning && !_busy && _spawnedInvokan == null) SpawnInvokan();
        }
        
        private void OnPickUp()
        {
            _spawnedInvokan.OnHandPickUp -= OnPickUp;
            _spawnedInvokan = null;
        }
    }
    
}