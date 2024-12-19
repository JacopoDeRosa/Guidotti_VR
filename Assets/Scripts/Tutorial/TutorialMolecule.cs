using System;
using System.Collections;
using UnityEngine;

namespace Tutorial
{
    public class TutorialMolecule : MonoBehaviour
    {
        [SerializeField] private float _bobbingSpeed = 1f;
        [SerializeField] private float _spinSpeed = 1f;
        [SerializeField] private Vector3 _bobbingAmplitude = Vector3.one;
        [SerializeField] private Transform _disappearPosition;
        [SerializeField] private Vector3 _disappearScale = new Vector3(0.25f, 0.25f, 0.25f);
        [SerializeField] private bool _appearOnEnable;

        private Vector3 _initialPosition;
        private Vector3 _topPosition;

        private void Awake()
        {
            _initialPosition = transform.position;
            _topPosition = _initialPosition + _bobbingAmplitude;
        }

        private void Update()
        {
            float t = (Mathf.Sin(Time.time * _bobbingSpeed) + 1) * 0.5f;
            transform.position = Vector3.Lerp(_initialPosition, _topPosition, t);
            transform.Rotate(Vector3.up, _spinSpeed * Time.deltaTime);
        }

        public void Appear()
        {
            Debug.Log("Appear");
            StartCoroutine(AppearanceRoutine());
        }
        
        public void Disappear()
        {
            Debug.Log("Disappear");
            StartCoroutine(DisappearanceRoutine());
        }
        
        private IEnumerator AppearanceRoutine()
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                transform.localScale = Vector3.one * t;
                yield return null;
            }
        }
        
        private IEnumerator DisappearanceRoutine()
        {
            float t = 0;
            Vector3 initialScale = transform.localScale;
            Vector3 initialPosition = transform.position;
            while (t < 1)
            {
                t += Time.deltaTime;
                transform.localScale = Vector3.Lerp(initialScale, _disappearScale, t);
                transform.position = Vector3.Lerp(initialPosition, _disappearPosition.position, t);
                yield return null;
            }

            transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            if(_appearOnEnable) Appear();
        }
    }
}