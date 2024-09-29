using UnityEngine;

namespace UI
{
    public class HudPivot : MonoBehaviour
    {
        [SerializeField] private Transform _anchor;
        [SerializeField] private float _rotationHardness = 4f;
        
        private void Update()
        {
            transform.position = _anchor.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, _anchor.rotation, _rotationHardness * Time.deltaTime);
        }
    }
}