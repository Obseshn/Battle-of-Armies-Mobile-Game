using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;

    public void SetMaxHealth(float health)
    {
        Slider.maxValue = health;
        Slider.value = health;

        Fill.color = Gradient.Evaluate(1f);
    }
    public void SetHealth(float health) 
    {
        Slider.value = health;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
    
}
