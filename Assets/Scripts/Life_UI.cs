using UnityEngine;
using UnityEngine.UI;

public abstract class Life_UI : MonoBehaviour
{
    [Header("health")]
    [SerializeField] Slider slider;
    [SerializeField] Image life;
    [SerializeField] Gradient Gradient;
    protected int _life_left;
    protected int max_health { get; set; }
    protected void update_life(int increase)
    {
        if (!life) { return; }
        _life_left -= increase;
        float __life_value = _life_left /(float) max_health;
        life.color = Gradient.Evaluate(__life_value);
        slider.value = __life_value;
    }
}
