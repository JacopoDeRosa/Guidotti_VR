using System;
using System.Collections;
using GameLogic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using Materials;

namespace Molecules
{
    public class Invokan : MonoBehaviour
    {
        [SerializeField] private float _attachmentRange = 0.5f;
        [SerializeField] private LayerMask _receptorLayer;
        [SerializeField] private InvokanAnimation _animation;
        [SerializeField] private InteractableUnityEventWrapper _eventWrapper;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private FadeInMaterialController _fadeController;
        [SerializeField] private float _lifeTimeIfNotPicked = 15f;

        private bool _freeFloating = false;
        
        public event Action OnHandPickUp;
        public event Action OnHandDrop;

        public Rigidbody Rigidbody => _rigidbody;
        
        public bool FreeFloating => _freeFloating;
        
        private void OnValidate()
        {
            if (_eventWrapper == null)
            {
                _eventWrapper = GetComponent<InteractableUnityEventWrapper>();
            }
            
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
            
            if(_fadeController == null)
            {
                _fadeController = GetComponent<FadeInMaterialController>();
            }
        }

        private void Awake()
        {
            _eventWrapper.WhenSelect.AddListener(OnPickUp);
            _eventWrapper.WhenUnselect.AddListener(OnDrop);
            _fadeController.FadeIn(1);
        }

        public void OnPickUp()
        {
            OnHandPickUp?.Invoke();
            _animation.enabled = false;
            _freeFloating = false;
        }

        public void OnDrop()
        {
            OnHandDrop?.Invoke();
            TryPlugReceptor();
        }
        
        public void TryPlugReceptor()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _attachmentRange, _receptorLayer, QueryTriggerInteraction.Collide);
            foreach (Collider collider in colliders)
            {
                Receptor receptor = collider.GetComponent<Receptor>();
                if (receptor == null || receptor.Plugged) continue;
                transform.position = receptor.transform.position;
                transform.rotation = receptor.transform.rotation;
                receptor.PlugReceptor(this);
                DestroyAllColliders();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, _attachmentRange);

            Color colorAlpha = Color.red;
            colorAlpha.a = 0.5f;
            Gizmos.color = colorAlpha;
            Gizmos.DrawSphere(Vector3.zero, _attachmentRange);
        }
        
        public void DeleteMolecule(float delay)
        { 
            _fadeController.FadeOut(1);
            Destroy(gameObject, delay);
           
        }
        
        public void LaunchMolecule(Vector3 direction, float force)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
            DestroyAllColliders();
        }
        
        private void DestroyAllColliders()
        {
            foreach (Collider collider in GetComponents<Collider>())
            {
                Destroy(collider);
            }
        }
        
        public void SetFreeFloating(bool freeFloating)
        {
            _freeFloating = freeFloating;
        }

        private void Update()
        {
            if (_freeFloating)
            {
                _lifeTimeIfNotPicked -= Time.deltaTime;
                if (_lifeTimeIfNotPicked <= 0)
                {
                    DeleteMolecule(1);
                }
            }
        }
    }
}