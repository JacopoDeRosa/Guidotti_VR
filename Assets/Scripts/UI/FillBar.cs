using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        
        public void SetValue(float value)
        {
            _fillImage.fillAmount = value / 100;
        }
        
        public void SetColor(Color color)
        {
            _fillImage.color = color;
        }
    }
}