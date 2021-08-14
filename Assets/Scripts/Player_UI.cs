using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player_UI : Life_UI
{
    public static event Action event_player_dead;
    static Text _score_text;
    static int _score;
    void Start()
    {
        max_health = Constants_used.get_max_life;
        _score = Constants_used.get_score;
        _life_left = Constants_used.get_life;
        _score_text = GetComponentInChildren<Text>();
        update_life(0);
        update_score(0);
        Options_Menu.event_shield_cost += update_score;
        Options_Menu.event_health_cost += update_score;
        Options_Menu.event_health_cost += update_life;
        event_player_dead += Dead;
    }
    public void life_event_handler(Enemy enemy)
    {
        enemy.event_enemy_collision += life_check_update;
    }
    void life_check_update(int increase)
    {
        if (_life_left < increase) { event_player_dead?.Invoke(); }
        else { update_life(increase); }
    }
    public static void score_event_handler(collectables collectables)
    {
        collectables.event_increase_points += update_score;
    }
    static void update_score(int score)
    {
        _score += score;
        _score_text.text = Constants_used.score_prefix + _score.ToString();
    }
    private void OnDestroy()
    {
        Constants_used.get_score += _score;
        Constants_used.get_life = _life_left;
    }
    private void Dead()
    {
        _score = Constants_used.get_score;
        _life_left = Constants_used.get_max_life;
        Destroy(this.gameObject);
    }
}
