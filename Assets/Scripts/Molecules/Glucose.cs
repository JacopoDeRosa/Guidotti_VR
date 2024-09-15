using System;
using UnityEngine;

namespace Molecules
{
    [RequireComponent(typeof(Rigidbody))]
    public class Glucose : MonoBehaviour
    {
        [SerializeField] private Vector3 _initialForce;
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;
        
        private bool _reserved = false;
        
        public bool Reserved => _reserved;
        
        private void OnValidate()
        {
            if(_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void Start()
        {
            _rigidbody.AddForce(_initialForce, ForceMode.Impulse);
        }
        
        public void Reserve()
        {
            _reserved = true;
        }
    }
}
