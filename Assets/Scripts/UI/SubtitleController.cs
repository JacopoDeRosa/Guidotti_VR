using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SubtitleController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _subtitleText;
        [SerializeField] private float _letterSpeed = 0.02f;
        
        
        public void ReadSubtitle(Subtitle subtitle)
        {
            StopAllCoroutines();
            StartCoroutine(WriteSubtitleRoutine(subtitle));
        }
        
        private IEnumerator WriteSubtitleRoutine(Subtitle subtitle)
        {
            WaitForSeconds wait = new WaitForSeconds(_letterSpeed);
            _subtitleText.text = "";
            foreach (char letter in subtitle.Text)
            {
                _subtitleText.text += letter;
                yield return wait;
            }
            
            yield return new WaitForSeconds(subtitle.Duration);
            
            _subtitleText.text = "";
            
            
        }
    }
}