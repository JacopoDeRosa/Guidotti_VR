using UnityEngine;

namespace Molecules
{
    public class MoleculeMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _globalSpeed;
        [SerializeField] private Vector3 _rotationSpeed;
        
        private void Update()
        {
            transform.Translate(_globalSpeed * Time.deltaTime, Space.World);
            transform.Rotate(_rotationSpeed * Time.deltaTime);
        }
    }
}