using UnityEngine;
using System.Collections;

namespace Materials
{
    public class FadeInMaterialController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private int[] _materialIndexes;
        [SerializeField] private float _fadeOutPosition = -0.5f;
        [SerializeField] private float _fadeInPosition = 1f;
        
        private void Awake()
        {
            FadeIn(1f);
        }
        
        public void FadeIn(float duration)
        {
            foreach (var index in _materialIndexes)
            {
                var material = _meshRenderer.materials[index];
                StartCoroutine(FadeInMaterial(material, duration));
            }
        }
        
        private IEnumerator FadeInMaterial(Material material, float duration)
        {
            float t = 0;
            while (t < _fadeInPosition)
            {
                t += Time.deltaTime / duration;
                material.SetFloat("_Bar_Position", t);
                yield return null;
            }
        }
        
        public void FadeOut(float duration)
        {
            foreach (var index in _materialIndexes)
            {
                var material = _meshRenderer.materials[index];
                StartCoroutine(FadeOutMaterial(material, duration));
            }
        }
        
        private IEnumerator FadeOutMaterial(Material material, float duration)
        {
            float t = 1;
            while (t > _fadeOutPosition)
            {
                t -= Time.deltaTime / duration;
                material.SetFloat("_Bar_Position", t);
                yield return null;
            }
        }
    }
}