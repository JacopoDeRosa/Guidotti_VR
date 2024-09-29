using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _director;
        [SerializeField] private PlayableAsset[] _tutorialSteps;
        
        private int _currentStep = 0;

        private void Awake()
        {
            _director.playableAsset = _tutorialSteps[_currentStep];
            _director.Play();
        }

        public void NextStep()
        {
            _currentStep++;
            _director.playableAsset = _tutorialSteps[_currentStep];
            _director.Play();
        }
    }
}