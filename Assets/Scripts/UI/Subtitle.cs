using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "UI/Subtitle", fileName = "Subtitles/New Subtitle")]
    public class Subtitle : ScriptableObject
    {
        [SerializeField, TextArea] private string _text;
        [SerializeField, TextArea] private string _references;
        [SerializeField] private string _code;
        [SerializeField] private float _duration = 3f;
        
        public string Text => _text;
        public string References => _references;
        public string Code => _code;
        public float Duration => _duration;
    }
}