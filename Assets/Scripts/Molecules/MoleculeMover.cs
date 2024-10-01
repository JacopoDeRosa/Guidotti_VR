using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Serialization;

namespace Molecules
{
    public class MoleculeMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _globalSpeed;
        [SerializeField] private Vector3 _minRotationSpeed;
        [SerializeField] private Vector3 _maxRotationSpeed;
        
        private Vector3 _rotationSpeed;

        private void Awake()
        {
            _rotationSpeed = new Vector3(
                Random.Range(_minRotationSpeed.x, _maxRotationSpeed.x),
                Random.Range(_minRotationSpeed.y, _maxRotationSpeed.y),
                Random.Range(_minRotationSpeed.z, _maxRotationSpeed.z)
            );
        }

        private void Update()
        {
            transform.Translate(_globalSpeed * Time.deltaTime, Space.World);
            transform.Rotate(_rotationSpeed * Time.deltaTime);
        }
    }
}