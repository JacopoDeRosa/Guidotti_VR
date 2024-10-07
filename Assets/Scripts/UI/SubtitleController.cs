using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SubtitleController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _subtitleText;
        [SerializeField] private float _letterSpeed = 0.02f;
        [SerializeField] private SubtitleBundle _subtitleBundle;
        
        
        public void SetSubtitleBundle(SubtitleBundle subtitleBundle)
        {
            _subtitleBundle = subtitleBundle;
        }
        
        public void ReadSubtitle(int index)
        {
            ReadSubtitle(_subtitleBundle.GetSubtitle(index));
        }
        
        public void ReadSubtitle(Subtitle subtitle)
        {
            StopAllCoroutines();
            StartCoroutine(WriteSubtitleRoutine(subtitle));
        }
        
        public void ClearSubtitle()
        {
            StopAllCoroutines();
            _subtitleText.text = "";
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