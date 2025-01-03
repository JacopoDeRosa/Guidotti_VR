using System;
using UnityEngine;
using UnityEngine.Video;

namespace UI
{
    [RequireComponent(typeof(VideoPlayer))]
    public class IntroVideoPlayer : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        
        private void OnValidate()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            _videoPlayer.isLooping = false;
            _videoPlayer.loopPointReached += OnVideoEnd;
        }

        private void OnVideoEnd(VideoPlayer source)
        {
            gameObject.SetActive(false);
        }
    }
}