using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractionShere : MonoBehaviour
    {
        [SerializeField] private float _interactionTime = 1;
        [SerializeField] private Color _calmColor = Color.cyan;
        [SerializeField] private Color _interactingColor = Color.red;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private bool _disableOnInteract = true;

        public UnityEvent OnInteract;

        private bool _isInteracting;

        private float _interactedTime;
        
        private void OnTriggerEnter(Collider other)
        {
            _isInteracting = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _isInteracting = false;
            _interactedTime = 0;
            _renderer.material.color = _calmColor;
        }
        
        private void Update()
        {
            if (_isInteracting)
            {
                _interactedTime += Time.deltaTime;
                _renderer.material.color = Color.Lerp(_calmColor, _interactingColor, _interactedTime / _interactionTime);
                
                if (_interactedTime >= _interactionTime)
                {
                    OnInteract.Invoke();
                    _interactedTime = 0;
                    if (_disableOnInteract) gameObject.SetActive(false);
                }
            }
        }
        
        
    }
}