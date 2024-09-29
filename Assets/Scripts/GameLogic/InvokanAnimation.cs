using UnityEngine;

namespace GameLogic
{
    public class InvokanAnimation : MonoBehaviour
    {
        [SerializeField] private float _bobbingSpeed = 1f;
        [SerializeField] private float _spinSpeed = 55f;
        [SerializeField] private Vector3 _bobbingAmplitude = Vector3.one;

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
    }
}