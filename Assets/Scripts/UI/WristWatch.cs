using System;
using GameLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WristWatch : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private FillBar _sodiumBar;
        [SerializeField] private FillBar _glucoseBar;
        [SerializeField] private TMP_Text _glucoseText;
        [SerializeField] private TMP_Text _sodiumText;
        [SerializeField] private Color _sodiumColor;
        [SerializeField] private Color _glucoseColor;

        private void Awake()
        {
            GameController gameController = FindObjectOfType<GameController>();
            gameController.OnGlucoseChanged += OnGlucoseChanged;    
            gameController.OnSodiumChanged += OnSodiumChanged;
            gameController.OnTimeLeftChanged += OnTimerChanged;
        }

        private void OnSodiumChanged(float value)
        {
            _sodiumBar.SetValue(value);
            int val = (int) value;
            _sodiumText.text = "Sodium: " + val + "%";
        }
        
        private void OnGlucoseChanged(float value)
        {
            _glucoseBar.SetValue(value);
            int val = (int) value;
            _glucoseText.text = "Glucose: " + val + "%";
        }

        private void OnTimerChanged(float value)
        {
            TimeSpan time = TimeSpan.FromSeconds(value);
            _timerText.text = time.ToString(@"mm\:ss");
        }
        
        private void OnValidate()
        {
            _sodiumBar.SetColor(_sodiumColor);
            _glucoseBar.SetColor(_glucoseColor);
            _glucoseText.color = _glucoseColor;
            _sodiumText.color = _sodiumColor;
        }
    }
}