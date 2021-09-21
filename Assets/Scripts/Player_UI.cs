using UnityEngine;
using UnityEngine.UI;
using System;

public class Player_UI : Life_UI
{
    public static event Action event_player_dead;
    static Text _score_text;
    static int _score;
    AudioClip _hurt;
    AudioSource _AudioSource;
    void Start()
    {
        max_health = Constants_used.get_max_life;
        _score = Constants_used.get_score;
        _life_left = Constants_used.get_life;
        _hurt = (AudioClip)Resources.Load(Constants_used.Player_Hurt);
        _AudioSource = GetComponent<AudioSource>();
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
        else { Hurt_Audio(); update_life(increase); }
    }
    void Hurt_Audio()
    {
        if (!_AudioSource) { return; }
        if (_AudioSource.isPlaying) { return; }
        _AudioSource.PlayOneShot(_hurt);
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
