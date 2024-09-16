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
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, 0.25f);
            
            Color colorAlpha = Color.blue;
            colorAlpha.a = 0.5f;
            Gizmos.color = colorAlpha;
            Gizmos.DrawSphere(Vector3.zero, 0.25f);
        }
        
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
