using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace GameLogic
{
    [Serializable]
    public class HighScoreData
    { 
        public PlayerScore[] scores;
    }
    
    public class HighScoreManager : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        
        public event Action OnFinalScoreUpdated;
        
        private string HighScoreFilePath => Application.persistentDataPath + "/highscores.json";
        [SerializeField] private string debugPath;
        [SerializeField] private HighScoreData _highScoreData;
        private PlayerScore _currentScore;
        
        public PlayerScore CurrentScore => _currentScore;

        private void OnValidate()
        {
            if(_gameController == null) _gameController = FindObjectOfType<GameController>();
            debugPath = HighScoreFilePath;
        }

        private void Awake()
        {
            if(_gameController == null) _gameController = FindObjectOfType<GameController>();
            CreateOrLoadHighScoreData();
            _gameController.OnGameStarted += OnGameStarted;
            _gameController.OnGameOver += OnGameOver;
        }

        private void CreateOrLoadHighScoreData()
        {
            if (System.IO.File.Exists(HighScoreFilePath))
            {
                string json = System.IO.File.ReadAllText(HighScoreFilePath);
                _highScoreData = JsonUtility.FromJson<HighScoreData>(json);
            }
            else
            {
                _highScoreData = new HighScoreData();
                _highScoreData.scores = new PlayerScore[0];
            }
        }
        
        public void SaveHighScoreData()
        {
            string json = JsonUtility.ToJson(_highScoreData);
            System.IO.File.WriteAllText(HighScoreFilePath, json);
        }
        
        public List<PlayerScore> GetTopScores(int count)
        {
            List<PlayerScore> scores = new List<PlayerScore>(_highScoreData.scores);
            scores.Sort((a, b) => b.TotalScore.CompareTo(a.TotalScore));
            scores.Reverse();
            return scores.GetRange(0, Mathf.Min(count, scores.Count));
        }
        
        private void AddScore(PlayerScore score)
        {
            PlayerScore[] oldScores = _highScoreData.scores;
            _highScoreData.scores = new PlayerScore[_highScoreData.scores.Length + 1];
            for (int i = 0; i < oldScores.Length; i++)
            {
                _highScoreData.scores[i] = oldScores[i];
            }
            _highScoreData.scores[_highScoreData.scores.Length - 1] = score;
            SaveHighScoreData();
        }
        
        private void OnGameStarted()
        {
            _currentScore = new PlayerScore();
            _currentScore.playerName = "Player " + _highScoreData.scores.Length;
        }
        
        private void OnGameOver()
        {
            _currentScore.glucoseLevel = (int) _gameController.GlucoseLevel;
            _currentScore.sodiumLevel = (int) _gameController.SodiumLevel;
            AddScore(_currentScore);
            OnFinalScoreUpdated?.Invoke();
        }
    }
    
    
}