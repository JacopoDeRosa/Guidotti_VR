using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _glucoseText;
        [SerializeField] private TMP_Text _sodiumText;
        [SerializeField] private GameController _gameController;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private float _timeToMainMenu = 5f;
        
        
        private void OnEnable()
        {
            int glucose = (int) _gameController.GlucoseLevel;
            int sodium = (int) _gameController.SodiumLevel;
            _glucoseText.text = $"Glucose: {glucose}%";
            _sodiumText.text = $"Sodium: {sodium}%";
            StartCoroutine(OnEnableRoutine());
        }
        
        
        private IEnumerator OnEnableRoutine()
        {
            yield return new WaitForSeconds(_timeToMainMenu);
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}