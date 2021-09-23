using UnityEngine;
using UnityEngine.UI;
using System;

public class Player_UI : Life_UI
{
    public static event Action event_player_dead;
    public static event Action<int> event_health;
    static Text _score_text, _health_pack_text;
    static int _score,_health_pack;
    AudioClip _hurt;
    AudioSource _AudioSource;
    void Start()
    {
        max_health = Constants_used.get_max_life;
        _score = Constants_used.get_score;
        _health_pack = Constants_used.get_health_pack;
        _life_left = Constants_used.get_life;
        _hurt = (AudioClip)Resources.Load(Constants_used.Player_Hurt);
        _AudioSource = GetComponent<AudioSource>();

        _score_text = GetComponentsInChildren<Text>()[0];
        _health_pack_text = GetComponentsInChildren<Text>()[1];

        update_life(0);
        update_score(0);
        update_health_pack(0);
        Options_Menu.event_shield_cost += update_score;
        Options_Menu.event_health_cost += update_score;
        Options_Menu.event_health_cost += update_health_pack;
        event_health += update_life;
        event_health += update_health_pack;
        event_player_dead += Dead;
    }
    public void life_event_handler(Enemy enemy)
    {
        enemy.event_enemy_collision += life_check_update;
    }
    public void remove_life_event_handler(Enemy enemy)
    {
        enemy.event_enemy_collision -= life_check_update;
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
    public static void remove_score_event_handler(collectables collectables)
    {
        collectables.event_increase_points -= update_score;
    }
    static void update_score(int score)
    {
        if (!_score_text) { return; }
        _score -= score;
        _score_text.text = Constants_used.score_prefix + _score.ToString();
    }
    public void increase_heath()
    {
        if (_life_left == Constants_used.get_max_life)
        {
            return;
        }
        if (Constants_used.get_health_pack < 1)
        {
            return;
        }
        Constants_used.get_health_pack = Constants_used.get_health_pack - 1;
        int __life_left = (int)(Mathf.Ceil(0.1f * Constants_used.get_max_life));
        int __life = _life_left + __life_left;
        if (__life >= Constants_used.get_max_life)
        {
            __life_left = __life_left - (__life - Constants_used.get_max_life);
        }
        event_health?.Invoke(-1 * __life_left);
    }
    static void update_health_pack(int health_pack)
    {
        if (!_health_pack_text) { return; }

        if (health_pack > 0) { health_pack = 1; }
        if (health_pack < 0) { health_pack = -1; }
        _health_pack += health_pack;
        _health_pack_text.text = Constants_used.health_prefix + _health_pack.ToString();
    }
    private void OnDestroy()
    {
        Constants_used.get_score = _score;
        Constants_used.get_life = _life_left;
        Constants_used.get_health_pack = _health_pack;
        event_player_dead -= Dead;
        Options_Menu.event_shield_cost -= update_score;
        Options_Menu.event_health_cost -= update_score;
        event_health -= update_life;
        Options_Menu.event_health_cost -= update_health_pack;
        event_health -= update_health_pack;

    }
    private void Dead()
    {
        _score = Constants_used.get_score;
        _life_left = Constants_used.get_max_life;
        _health_pack = Constants_used.get_health_pack;
        Destroy(this.gameObject);
    }
}
