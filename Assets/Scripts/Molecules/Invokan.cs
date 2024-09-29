using System;
using System.Collections;
using GameLogic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Molecules
{
    public class Invokan : MonoBehaviour
    {
        [SerializeField] private float _attachmentRange = 0.5f;
        [SerializeField] private LayerMask _receptorLayer;
        [SerializeField] private InvokanAnimation _animation;
        [SerializeField] private InteractableUnityEventWrapper _eventWrapper;

        public event Action OnHandPickUp;
        public event Action OnHandDrop;
        
        private void OnValidate()
        {
            if (_eventWrapper == null)
            {
                _eventWrapper = GetComponent<InteractableUnityEventWrapper>();
            }
        }

        private void Awake()
        {
           // StartCoroutine(DebugRoutine());
            
            _eventWrapper.WhenSelect.AddListener(OnPickUp);
            _eventWrapper.WhenUnselect.AddListener(OnDrop);
        }

        public void OnPickUp()
        {
            OnHandPickUp?.Invoke();
            _animation.enabled = false;
        }

        public void OnDrop()
        {
            OnHandDrop?.Invoke();
            TryPlugReceptor();
        }
        
        private void TryPlugReceptor()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _attachmentRange, _receptorLayer, QueryTriggerInteraction.Collide);
            foreach (Collider collider in colliders)
            {
                Receptor receptor = collider.GetComponent<Receptor>();
                if (receptor == null) continue;
                transform.position = receptor.transform.position;
                transform.rotation = receptor.transform.rotation;
                receptor.PlugReceptor(this);
                
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
        
        private IEnumerator DebugRoutine()
        {
            while (true)
            {
                TryPlugReceptor();
                yield return new WaitForSeconds(2f);
            }
        }
    }
}