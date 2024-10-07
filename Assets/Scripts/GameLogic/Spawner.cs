using UnityEngine;

namespace GameLogic
{
    public class Spawner : MonoBehaviour
    {
        protected bool _spawning = false;
        
        public void SetSpawning(bool value)
        {
            _spawning = value;
            OnSetSpawning(value);
        }
        
        protected virtual void OnSetSpawning(bool value)
        {
            
        }
    }
}