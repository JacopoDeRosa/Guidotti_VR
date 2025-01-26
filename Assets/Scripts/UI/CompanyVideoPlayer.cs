using System;
using GameLogic;
using UnityEngine;
using UnityEngine.Video;

namespace UI
{
    public enum PlayMode
    {
        StartOnly,
        EndGame,
        StartAndEndGame
    }
    
    [RequireComponent(typeof(VideoPlayer))]
    public class CompanyVideoPlayer : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private GameController _gameController;
        [SerializeField] private PlayMode _playMode;
        
        private void OnValidate()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
            
            if(_playMode == PlayMode.StartOnly || _playMode == PlayMode.StartAndEndGame)
            {
                PlayVideo();
            }
            if(_playMode == PlayMode.EndGame || _playMode == PlayMode.StartAndEndGame)
            {
                _gameController.OnGameOver += PlayVideo;
            }
            
        }
        
        private void PlayVideo()
        {
            gameObject.SetActive(true);
            _videoPlayer.Play();
        }

        private void OnVideoEnd(VideoPlayer source)
        {
            gameObject.SetActive(false);
        }
    }
}