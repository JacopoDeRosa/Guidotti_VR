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
        [SerializeField] private TMP_Text _finalScoreText;

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

            if (scores.Count == 0)
            {
                _highscoreListTexts[0].gameObject.SetActive(true);
                _highscoreListTexts[0].text = "No high scores yet!";
                return;
            }
            
            for (int i = 0; i < scores.Count; i++)
            {
                _highscoreListTexts[i].gameObject.SetActive(true);
                _highscoreListTexts[i].text = $"[{scores[i].playerName}] Glucose: {scores[i].glucoseLevel}% Sodium: {scores[i].sodiumLevel}%";
            }
            
            if(_finalScoreText) _finalScoreText.text = $"[{_highScoreManager.CurrentScore.playerName}] Glucose: {_highScoreManager.CurrentScore.glucoseLevel}% Sodium: {_highScoreManager.CurrentScore.sodiumLevel}%";
        }
        
        private void OnFinalScoreUpdated()
        {
            DisplayHighScores();
        }
    }
}