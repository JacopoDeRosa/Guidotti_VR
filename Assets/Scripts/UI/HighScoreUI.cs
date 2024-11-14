using System;
using System.Collections.Generic;
using GameLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HighScoreUI : MonoBehaviour
    {
        [SerializeField] private HighScoreManager _highScoreManager;
        [SerializeField] private TMP_Text[] _highscoreListTexts;

        private void OnValidate()
        {
            if (_highScoreManager == null)
            {
                _highScoreManager = FindObjectOfType<HighScoreManager>();
            }
        }

        private void Awake()
        {
            if (_highScoreManager == null)
            {
                _highScoreManager = FindObjectOfType<HighScoreManager>();
            }
            _highScoreManager.OnFinalScoreUpdated += OnFinalScoreUpdated;
        }

        public void DisplayHighScores()
        {
            // Disable all the list texts
            foreach (TMP_Text text in _highscoreListTexts)
            {
                text.gameObject.SetActive(false);
            }
            
            List<PlayerScore> scores = _highScoreManager.GetTopScores(_highscoreListTexts.Length);
            for (int i = 0; i < scores.Count; i++)
            {
                _highscoreListTexts[i].gameObject.SetActive(true);
                _highscoreListTexts[i].text = $"[{scores[i].playerName}] Glucose: {scores[i].glucoseLevel}% Sodium: {scores[i].sodiumLevel}%";
            }
            
        }
        
        private void OnFinalScoreUpdated()
        {
            DisplayHighScores();
        }
    }
}