using System;
using UnityEngine;
using UnityEngine.Events;
using Molecules;

namespace Sockets
{
    public class Socketable : MonoBehaviour
    {
        [SerializeField] private SocketType _minimumSocketType;
        [SerializeField]  private float _checkRadius = 0.5f;

        private Socket _activeSocket;

        public UnityEvent onSocketFound, onSocketLost;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _checkRadius);
        }

        private bool CheckIfSocketable(out Socket socket)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _checkRadius);

            socket = null;
            
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Socket sock) && sock.SocketType >= _minimumSocketType)
                {
                    socket = sock;
                    return true;
                }
            }

            return false;
        }

        public void OnDeselect()
        {
            if (CheckIfSocketable(out Socket socket))
            {
                _activeSocket = socket;
                transform.SetParent(socket.transform, true);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                onSocketFound.Invoke();
            }
        }

        public void OnSelect()
        {
            if (_activeSocket != null)
            {
                _activeSocket = null;
                transform.SetParent(null, true);
                onSocketLost.Invoke();
            }
         
        }
    }
}
