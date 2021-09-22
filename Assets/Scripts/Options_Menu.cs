using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Options_Menu : MonoBehaviour
{
    [SerializeField] Button health, shield;
    const int SHIELD = 50, HEALTH = 10;
    const float LEVEL_TIME = 0.8f;
    public static event Action<int> event_health_cost, event_shield_cost;
    const string SHOP = "Shop", Final_Level = "Final_Battle", animator_trigger = "End";
    Animator Animator;
    private void Start()
    {
        Player_UI.event_player_dead += reload_scene;
        Animator = GetComponentInChildren<Animator>();
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
        Constants_used.get_max_life = 300;
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
    public void next_scene()
    {
        if(SceneManager.GetActiveScene().name!=Final_Level)
            Animator.SetTrigger(animator_trigger);
        Constants_used.level += 1;
        StartCoroutine(Level_load(Constants_used.level));
    }
    public void reload_scene() 
    {
        StartCoroutine(Reload_Level(Constants_used.level));
    }
    public void Shop() 
    {
        StartCoroutine(Shop_Load()); 
    }
    public void Quit_Game()
    {
        Application.Quit();
    }
    public void Main_Scene()
    {
        Constants_used.level = 0;
        Constants_used.get_score = 0;
        Animator.SetTrigger(animator_trigger);
        StartCoroutine(Level_load(0));
        Cursor.visible = true;
    }
    IEnumerator Level_load(int level)
    {
        yield return new WaitForSeconds(LEVEL_TIME);
        SceneManager.LoadScene(level);
    }
    IEnumerator Reload_Level(int level)
    {
        yield return new WaitForSeconds(1f);
        Animator.SetTrigger(animator_trigger);
        yield return new WaitForSeconds(LEVEL_TIME);
        SceneManager.LoadScene(level);
    }
    IEnumerator Shop_Load()
    {
        yield return new WaitForSeconds(1f);
        Animator.SetTrigger(animator_trigger);
        yield return new WaitForSeconds(LEVEL_TIME);
        SceneManager.LoadScene(SHOP);
        Cursor.visible = true;
    }
    private void OnDestroy()
    {
        Player_UI.event_player_dead -= reload_scene;

    }
}
