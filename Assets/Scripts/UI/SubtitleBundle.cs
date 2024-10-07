using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "New Subtitle Bundle", menuName = "Subtitles/New Bundle", order = 0)]
    public class SubtitleBundle : ScriptableObject
    {
        [SerializeField] private Subtitle[] _subtitles;
        
        public Subtitle GetSubtitle(int index)
        {
            return _subtitles[index];
        }
    }
}