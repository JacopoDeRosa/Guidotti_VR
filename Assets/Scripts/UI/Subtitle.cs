using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "UI/Subtitle", fileName = "New Subtitle")]
    public class Subtitle : ScriptableObject
    {
        [SerializeField, TextArea] private string _text;
        [SerializeField] private float _duration = 3f;
        
        public string Text => _text;
        public float Duration => _duration;
    }
}