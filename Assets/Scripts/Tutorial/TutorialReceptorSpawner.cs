using Molecules;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class TutorialReceptorSpawner : MonoBehaviour
    {
        [SerializeField] private Receptor _receptorPrefab;
        
        public UnityEvent ReceptorPlugged;
        
        private Receptor _receptorInstance;
        
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Color color = Color.magenta;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(Vector3.zero, 0.12f);
            
            color.a = 0.25f;
            Gizmos.color = color;
            Gizmos.DrawSphere(Vector3.zero, 0.12f);
        }
        
        public void SpawnReceptor()
        {
            _receptorInstance = Instantiate(_receptorPrefab, transform.position, transform.rotation);
            _receptorInstance.OnInvokanaAccepted.AddListener(OnReceptorPlugged);
        }
        
        private void OnReceptorPlugged()
        {
            ReceptorPlugged.Invoke();
        }
    }
}