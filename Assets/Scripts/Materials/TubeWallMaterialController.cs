using System;
using GameLogic;
using UnityEngine;

namespace Materials
{
    public class TubeWallMaterialController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private GameController _gameController;
        
        public void SetInflammationLevel(float level)
        {
            float clampedLevel = Mathf.Clamp(level, 0, 1);
            _meshRenderer.material.SetFloat("_Blend", clampedLevel);
        }

        private void OnValidate()
        {
            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }
            
            if (_gameController == null)
            {
                _gameController = FindObjectOfType<GameController>();
            }
        }

        private void Awake()
        {
            _gameController.OnGlucoseChanged += OnGlucoseChanged;
        }
        
        private void OnGlucoseChanged(float glucose)
        {
            SetInflammationLevel(glucose / 100);
        }
    }
}