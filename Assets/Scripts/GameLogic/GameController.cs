using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Spawner[] _spawners;
        [SerializeField] private float _initialGlucoseLevel = 50f;
        [SerializeField] private float _initialSodiumLevel = 50f;
        [SerializeField] private float _totalGameTime = 180f;
        
        private float _glucoseLevel = 0f;
        private float _sodiumLevel = 0f;
        private float _timeLeft;
        
        public event Action<float> OnGlucoseChanged;
        public event Action<float> OnSodiumChanged; 
        public event Action<float> OnTimeLeftChanged;

        private void Awake()
        {
            SetGlucose(_initialGlucoseLevel);
            SetSodium(_initialSodiumLevel);
            SetTimeLeft(_totalGameTime);
        }

        public void StartGame()
        {
            foreach (Spawner spawner in _spawners)
            {
                spawner.SetSpawning(true);
            }

            SetGlucose(_initialGlucoseLevel);
            SetSodium(_initialSodiumLevel);
            SetTimeLeft(_totalGameTime);
        }

        private void OnValidate()
        {
            RefreshSpawners();
        }
        
        public void RefreshSpawners()
        {
            _spawners = FindObjectsOfType<Spawner>();
        }

        public void StopGame()
        {
            foreach (Spawner spawner in _spawners)
            {
                spawner.SetSpawning(false);
            }
            
            _glucoseLevel = 0;
            _sodiumLevel = 0;
            OnGlucoseChanged?.Invoke(_glucoseLevel);
            OnSodiumChanged?.Invoke(_sodiumLevel);
        }
        
        public void AddGlucose(float value)
        {
            _glucoseLevel += value;
            _glucoseLevel = Mathf.Clamp(_glucoseLevel, 0, 100);
            OnGlucoseChanged?.Invoke(_glucoseLevel);
        }
        
        public void AddSodium(float value)
        {
            _sodiumLevel += value;
            _sodiumLevel = Mathf.Clamp(_sodiumLevel, 0, 100);
            OnSodiumChanged?.Invoke(_sodiumLevel);
        }

        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                OnTimeLeftChanged?.Invoke(_timeLeft);
                
                if (_timeLeft <= 0)
                {
                    StopGame();
                }
            }
        }
        
        private void SetSodium(float value)
        {
            _sodiumLevel = value;
            OnSodiumChanged?.Invoke(_sodiumLevel);
        }
        
        private void SetGlucose(float value)
        {
            _glucoseLevel = value;
            OnGlucoseChanged?.Invoke(_glucoseLevel);
        }
        
        private void SetTimeLeft(float value)
        {
            _timeLeft = value;
            OnTimeLeftChanged?.Invoke(_timeLeft);
        }
    }
}