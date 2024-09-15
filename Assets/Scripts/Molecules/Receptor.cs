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
        [SerializeField] private float _pulseFrequency = 1f;
        [SerializeField] private float _pulseRange = 1f;
        [SerializeField] private LayerMask _moleculeLayer;

        private float _timeSinceLastPulse;
        private bool _plugged = false;


        private void Update()
        {
            _timeSinceLastPulse += Time.deltaTime;
            if (_timeSinceLastPulse >= 1 / _pulseFrequency)
            {
                _timeSinceLastPulse = 0;
                Pulse();
            }
        }

        private void Pulse()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _pulseRange, _moleculeLayer);
            foreach (Collider collider in colliders)
            {
                Glucose molecule = collider.GetComponent<Glucose>();
                if (molecule == null || molecule.Reserved) continue;
                
                
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
   
    }
}