using System;
using GameLogic;
using Molecules;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _director;
        [SerializeField] private PlayableAsset[] _tutorialSteps;
        
        private int _currentStep = 0;
        
        public UnityEvent OnTutorialStart;
        public UnityEvent OnTutorialEnd;
        

        private void Awake()
        {
            _director.stopped += OnDirectorEnd;
        }

        public void LoadBundle(TutorialBundle bundle)
        {
            _tutorialSteps = bundle.GetSteps();
        }
        
        public void StartTutorial()
        {
            _currentStep = 0;
            _director.playableAsset = _tutorialSteps[_currentStep];
            _director.Play();
            OnTutorialStart.Invoke();
        }

        public void NextStep()
        {
            _currentStep++;
            _director.playableAsset = _tutorialSteps[_currentStep];
            _director.Play();
        }
        
        private void OnDirectorEnd(PlayableDirector obj)
        {
            if (_currentStep == _tutorialSteps.Length - 1) OnTutorialEnd.Invoke();
        }
        
        public void SkipTutorial()
        {
            _director.Stop();
            _director.playableAsset = null;
            OnTutorialEnd.Invoke();
            FindObjectOfType<GameController>().StartGame();
            foreach (var invokan in FindObjectsOfType<Invokan>())
            {
                Destroy(invokan.gameObject);
            }
        }
    }
}