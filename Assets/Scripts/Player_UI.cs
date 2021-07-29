using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player_UI : Life_UI
{
    static event Action<int> events_life;
    static event Action<int> events_score;
    [SerializeField] Text _score_text;
    int _score;
    void Start()
    {
        events_life += update_life;
        max_health = Constants_used.get_max_life;
        _score = Constants_used.get_score;
        _life_left = Constants_used.get_life;
        update_score(0); update_life(0);
    }
    public static void life_event_handler(Enemy enemy)
    {
        enemy.event_enemy_collision += events_life;
    }
    public static void score_event_handler(collectables collectables)
    {
        collectables.event_increase_points += events_score;
    }
    void update_score(int score)
    {
        _score += score;
        _score_text.text = Constants_used.score_prefix + _score.ToString();
    }
    private void OnDestroy()
    {
        Constants_used.get_score += _score;
        Constants_used.get_life = _life_left;
    }
}
