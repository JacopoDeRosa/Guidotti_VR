using UnityEngine;

namespace Sockets
{
    public class Socket : MonoBehaviour
    {
        [SerializeField] private SocketType _socketType;

        public SocketType SocketType => _socketType;
    }
}
