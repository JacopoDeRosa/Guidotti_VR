using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Molecules
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class ReceptorMolecule : MonoBehaviour
    {
        [SerializeField] private Vector3 _maxInitialForce, _minInitialForce;
        [SerializeField] private Vector3 _maxInitialTorque, _minInitialTorque;
        [SerializeField] private float _maxInitialScale, _minInitialScale;
        [SerializeField] private float _lifetime;
        [SerializeField] private MoleculeType _moleculeType;
        
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;
        [SerializeField, HideInInspector] private SphereCollider _collider;
        
        private bool _reserved = false;
        
        private float _timeAlive = 0f;
        
        public bool Reserved => _reserved;
        public Rigidbody Rigidbody => _rigidbody;
        public MoleculeType MoleculeType => _moleculeType;

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Color color = _reserved ? Color.red : Color.green;
                Gizmos.matrix = transform.localToWorldMatrix;
                
                Gizmos.color = color;
                Gizmos.DrawWireSphere(Vector3.zero, _collider.radius);
                
                color.a = 0.25f;
                Gizmos.DrawSphere(Vector3.zero, _collider.radius);
            }
        }

        private void OnValidate()
        {
            if(_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if(_collider == null) _collider = GetComponent<SphereCollider>();
            _rigidbody.useGravity = false;
        }

        private void Start()
        {
            // Randomize the initial torque
            AddRandomTorque();
            
            // Randomize the initial force
            AddRandomForce();
            
            // Randomize the initial scale
            float randomScale = Random.Range(_minInitialScale, _maxInitialScale);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
        
        private void AddRandomForce()
        {
            _rigidbody.AddForce(new Vector3(Random.Range(_minInitialForce.x, _maxInitialForce.x),
                Random.Range(_minInitialForce.y, _maxInitialForce.y),
                Random.Range(_minInitialForce.z, _maxInitialForce.z)), ForceMode.Impulse);
        }
        
        private void AddRandomTorque()
        {
            _rigidbody.AddTorque(new Vector3(Random.Range(_minInitialTorque.x, _maxInitialTorque.x),
                Random.Range(_minInitialTorque.y, _maxInitialTorque.y),
                Random.Range(_minInitialTorque.z, _maxInitialTorque.z)), ForceMode.Impulse);
        }
        
        public void Reserve()
        {
            _reserved = true;
        }
        
        public void Unreserve()
        {
            _reserved = false;
        }

        private void Update()
        {
            if(_reserved) return;
            
            _timeAlive += Time.deltaTime;
            if (_timeAlive >= _lifetime) Destroy(gameObject);
        }
    }
}