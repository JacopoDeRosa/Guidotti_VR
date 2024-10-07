using System;
using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class ItemDeleter : MonoBehaviour
    {
        [SerializeField] private string _deleteTag = "Invokana";
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;
        [SerializeField, HideInInspector] private BoxCollider _collider;
        
        private void OnValidate()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
                _rigidbody.isKinematic = true;
                _rigidbody.useGravity = false;
            }
            
            if (_collider == null)
            {
                _collider = GetComponent<BoxCollider>();
                _collider.isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag(_deleteTag))
            {
                Destroy(other.gameObject);
            }
        }
    }
}