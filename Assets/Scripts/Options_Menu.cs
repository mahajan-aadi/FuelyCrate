using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Options_Menu : MonoBehaviour
{
    [SerializeField] Button health, shield;
    const int SHIELD = 50, HEALTH = 10;
    public static event Action<int> event_health_cost, event_shield_cost;
    private void Start()
    {
        Player_UI.event_player_dead += reload_scene;
    }
    public void left_character() 
    {
        Constants_used.Player = Constants_used.player1; 
        Constants_used.get_max_life = 100;
        Constants_used.shield = false;
        next_scene(); 
    }
    public void right_character()
    {
        Constants_used.Player = Constants_used.player2;
        Constants_used.get_max_life = 200;
        Constants_used.shield = false;
        next_scene(); 
    }
    public void increase_heath() 
    {
        if (Constants_used.get_life == Constants_used.get_max_life)
        {
            health.interactable = false;
            return;
        }
        if (Constants_used.get_score < HEALTH) { return; }
        Constants_used.get_score = Constants_used.get_score - HEALTH;
        Constants_used.get_life += (int)(Mathf.Ceil(0.1f * Constants_used.get_max_life));
        if (Constants_used.get_life>= Constants_used.get_max_life)
        {
            Constants_used.get_life = Constants_used.get_max_life;
            health.interactable = false;
        }
        event_health_cost?.Invoke(-1 * HEALTH);
    }
    public void activate_shield() 
    {
        if (Constants_used.shield)
        {
            shield.interactable = false;
            return;
        }
        if (Constants_used.get_score < SHIELD) { return; }
        Constants_used.get_score = Constants_used.get_score - SHIELD;
        Constants_used.shield = true; 
        shield.interactable = false;
        event_shield_cost?.Invoke(-1 * SHIELD);
    }
    public void next_scene() { Constants_used.level += 1; SceneManager.LoadScene(Constants_used.level); }
    public void reload_scene() { SceneManager.LoadScene(Constants_used.level); }
    public void Shop() { SceneManager.LoadScene("Shop"); }
}
