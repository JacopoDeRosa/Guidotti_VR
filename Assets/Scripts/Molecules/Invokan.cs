using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Molecules
{
    public class Invokan : MonoBehaviour
    {
        [SerializeField] private float _attachmentRange = 0.5f;

        public void OnPickUp()
        {
            
        }

        public void OnDrop()
        {
            TryPlugReceptor();
        }
        
        private void TryPlugReceptor()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _attachmentRange);
            foreach (Collider collider in colliders)
            {
                Receptor receptor = collider.GetComponent<Receptor>();
                if (receptor == null) continue;
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
    }
}