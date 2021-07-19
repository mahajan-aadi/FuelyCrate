using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_handler : MonoBehaviour
{
    [SerializeField] Text _score_text;
    [Header("health")]
    [SerializeField] Slider slider;
    [SerializeField] Image life;
    [SerializeField] Gradient Gradient;
    int _score, _life_left;
    void Start()
    {
        _score = Constants_used.get_score;
        _life_left = Constants_used.get_life;
        update_score(0);update_life(0);
    }
    void update_score(int score)
    {
        _score += score;
        _score_text.text = Constants_used.score_prefix+_score.ToString();
    }
    void update_life(int increase)
    {
        _life_left -= increase;
        float __life_value = _life_left / (float)Constants_used.get_max_life;
        life.color = Gradient.Evaluate(__life_value);
        slider.value = __life_value;
    }
    public void life_event(Enemy enemy_event)
    {
        enemy_event.event_enemy_collision += update_life;
    }
    public void add_score_event(collectables score_event)
    {
        score_event.event_increase_points += update_score;
    }
    private void OnDestroy()
    {
        Constants_used.get_score += _score;
        Constants_used.get_life = _life_left;
    }
}
