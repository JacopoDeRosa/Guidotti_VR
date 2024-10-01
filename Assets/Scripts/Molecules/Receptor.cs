using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Molecules
{
    public class Receptor : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _rejectionChance = 0.5f;
        [SerializeField] private float _rejectionForce = 10f;
        [SerializeField] private float _attractionForce = 10f;
        [SerializeField] private float _minPulseFrequency, _maxPulseFrequency;
        [SerializeField] private Vector3 _pulseHalfExtents = Vector3.one;
        [SerializeField] private Vector3 _pulseCenter = Vector3.zero;
        [SerializeField] private LayerMask _moleculeLayer;
        [SerializeField] private MoleculeTypeFlags _wantedMoleculeTypes;
        [SerializeField] private float _deathDelay = 1.0f;
        [SerializeField] private float _rejectionDelay = 1.0f;
        [SerializeField] private float _destroyDelay = 0.1f;
        [SerializeField] private float _pluggedTime = 1.0f;
        [SerializeField] private float _activationDelay = 1f;
        
        private float _nextPulseTime;
        private bool _plugged = false;
        private float _lifeTime;
        
        private MoleculeTypeFlags _reservedMoleculeThisPulse;
        private List<ReceptorMolecule> _reservedMoleculesThisPulse = new List<ReceptorMolecule>();
        
        public UnityEvent OnReceptorPlugged;
        public UnityEvent OnReceptorDestroyed;
        public UnityEvent OnMoleculeConsumed;
        public UnityEvent OnReceptorSpawned;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_pulseCenter, _pulseHalfExtents * 2);
        }

        private void Start()
        {
            _nextPulseTime = Time.time + Random.Range(_minPulseFrequency, _maxPulseFrequency);
            OnReceptorSpawned.Invoke();
        }

        private void Update()
        {
            _lifeTime += Time.deltaTime;
            
            if (_plugged || _lifeTime < _activationDelay) return;
            
            if (Time.time >= _nextPulseTime)
            {
                Pulse();
                _nextPulseTime = Time.time + Random.Range(_minPulseFrequency, _maxPulseFrequency);
            }
        }

        private void Pulse()
        {
            Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(_pulseCenter), _pulseHalfExtents, transform.rotation, _moleculeLayer);
            
            string collidersString = "Colliders: ";
            
            bool consumed = false;
            
            foreach (Collider collider in colliders)
            {
                collidersString += collider.gameObject.name + ", ";
                
                ReceptorMolecule molecule = collider.GetComponent<ReceptorMolecule>();
                
                if (IsMoleculeNotWanted(molecule)) continue;
                molecule.Reserve();
                _reservedMoleculesThisPulse.Add(molecule);
                _reservedMoleculeThisPulse |= (MoleculeTypeFlags) molecule.MoleculeType;


                if (_reservedMoleculeThisPulse == _wantedMoleculeTypes)
                {
                    Consume();
                    consumed = true;
                }
            }
            
            if(!consumed) ResetReceptor();
        }
        
        private bool IsMoleculeNotWanted(ReceptorMolecule molecule)
        {
            return molecule == null ||
                   molecule.Reserved ||
                   _reservedMoleculeThisPulse.HasFlag((MoleculeTypeFlags)molecule.MoleculeType) ||
                   !_wantedMoleculeTypes.HasFlag((MoleculeTypeFlags)molecule.MoleculeType);
        }

        private void Consume()
        {
            foreach (ReceptorMolecule molecule in _reservedMoleculesThisPulse)
            {
                molecule.Rigidbody.velocity = Vector3.zero;
                molecule.Rigidbody.AddForce((transform.position - molecule.transform.position).normalized * _attractionForce, ForceMode.Impulse);
                OnMoleculeConsumed.Invoke();
            }
            
            ResetReceptor(false);
        }
        
        private void ResetReceptor(bool freeReservedMolecules = true)
        {
            if (freeReservedMolecules)
            {
                foreach (ReceptorMolecule molecule in _reservedMoleculesThisPulse)
                {
                    molecule.Unreserve();
                }
            }
            
            _reservedMoleculeThisPulse = MoleculeTypeFlags.None;
            _reservedMoleculesThisPulse.Clear();
        }
        
        public void PlugReceptor(Invokan invokan)
        {
            StartCoroutine(ReceptorPluggedRoutine(invokan));
        }
        
        private void Reject(Invokan invokan)
        {
            invokan.LaunchMolecule(transform.up, _rejectionForce);
            invokan.DeleteMolecule(2);
            _plugged = false;
        }
        
        private IEnumerator ReceptorPluggedRoutine(Invokan invokan)
        {
            _plugged = true;
            
            if (_rejectionChance > 0)
            {
                yield return new WaitForSeconds(_rejectionDelay);
                
                if (Random.value < _rejectionChance)
                {
                    Reject(invokan);
                    yield break;
                }
            }
            
            yield return new WaitForSeconds(_pluggedTime);
            
            OnReceptorPlugged.Invoke();
            invokan.DeleteMolecule(_deathDelay);
            
            yield return new WaitForSeconds(_deathDelay);
            
            OnReceptorDestroyed.Invoke();
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_plugged) return;
            
            ReceptorMolecule molecule = other.GetComponent<ReceptorMolecule>();
            if (molecule == null) return;
            Destroy(molecule.gameObject, _destroyDelay);
        }
    }
}