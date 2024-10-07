using UnityEngine;
using UnityEngine.Playables;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "New Tutorial Bundle", menuName = "New Tutorial Bundle", order = 0)]
    public class TutorialBundle : ScriptableObject
    {
        [SerializeField] private PlayableAsset[] _tutorialSteps;
        
        public PlayableAsset[] GetSteps()
        {
            return _tutorialSteps;
        }
    }
}