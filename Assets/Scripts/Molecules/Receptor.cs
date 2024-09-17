using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Molecules
{
    public class Receptor : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _rejectionChance = 0.5f;
        [SerializeField] private float _rejectionForce = 10f;
        [SerializeField] private float _attractionForce = 10f;
        [SerializeField] private float _minPulseFrequency, _maxPulseFrequency;
        [SerializeField] private float _pulseRange = 1f;
        [SerializeField] private float _destroyDelay = 0.1f;
        [SerializeField] private LayerMask _moleculeLayer;
        

        private float _nextPulseTime;
        private bool _plugged = false;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, _pulseRange);
        }

        private void Update()
        {
            if (_plugged) return;
            
            if (Time.time >= _nextPulseTime)
            {
                Pulse();
                _nextPulseTime = Time.time + Random.Range(_minPulseFrequency, _maxPulseFrequency);
            }
        }

        private void Pulse()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _pulseRange, _moleculeLayer);
            foreach (Collider collider in colliders)
            {
                Glucose molecule = collider.GetComponent<Glucose>();
                if (molecule == null || molecule.Reserved) continue;
                molecule.Rigidbody.velocity = Vector3.zero;
                molecule.Reserve();
                molecule.Rigidbody.AddForce((transform.position - molecule.transform.position).normalized * _attractionForce, ForceMode.Impulse);
            }
        }
        
        public void PlugReceptor(Invokan invokan)
        {
           if(Random.value < _rejectionChance)
           {
               Debug.Log("Rejected");
           }
           else
           {
               _plugged = true;
           }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_plugged) return;
            Glucose molecule = other.GetComponent<Glucose>();
            if (molecule == null) return;
            Destroy(molecule.gameObject, _destroyDelay);
            Debug.Log("Glucose absorbed");
        }
    }
}