using UnityEngine;
using UnityEngine.UI;

public class SliderSnapping : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] float size = 0.1f;

   public void OnSliderSnapping(float value)
    {
        slider.value = Mathf.Round(value / size) * size;
    }
}
