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
        Constants_used.get_life = 300;
        Constants_used.shield = false;
        next_scene(); 
    }

    public void health_button()
    {
        if (Constants_used.get_score < HEALTH) { return; }
        Constants_used.get_score = Constants_used.get_score - HEALTH;
        Constants_used.get_health_pack = Constants_used.get_health_pack + 1;
        event_health_cost?.Invoke(HEALTH);
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
        event_shield_cost?.Invoke(SHIELD);
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
    public void Main_Menu()
    {
        Time.timeScale = 1;
        Constants_used.Pause = false;
        saving();
    }
    public void Main_Scene()
    {
        Constants_used.level = 0;
        Constants_used.get_score = 0;
        Constants_used.get_health_pack = 0;
        Constants_used.shield = false;
        Constants_used.Player = "";
        saving();
    }
    void saving()
    {
        PlayerPrefs.SetInt(Constants_used.LEVEL_ID, Constants_used.level);
        PlayerPrefs.SetString(Constants_used.PLAYER_ID, Constants_used.Player);
        PlayerPrefs.SetInt(Constants_used.SCORE_ID, Constants_used.get_score);
        PlayerPrefs.SetInt(Constants_used.LIFE_ID, Constants_used.get_life);
        PlayerPrefs.SetInt(Constants_used.MAX_LIFE_ID, Constants_used.get_max_life);
        PlayerPrefs.SetInt(Constants_used.SHIELD_ID, Constants_used.shield?1:0);
        PlayerPrefs.SetInt(Constants_used.HEALTH_PACKS, Constants_used.get_health_pack);
        Animator?.SetTrigger(animator_trigger);
        PlayerPrefs.Save();
        StartCoroutine(Level_load(0));
        Cursor.visible = true;
    }
    public void Start_Game()
    {
        if (PlayerPrefs.GetInt(Constants_used.LEVEL_ID) > 0)
        {
            load_settings();
            Animator.SetTrigger(animator_trigger);
            StartCoroutine(Level_load(PlayerPrefs.GetInt(Constants_used.LEVEL_ID)));
        }
        else
        {
            next_scene();
        }
    }

    private void load_settings()
    {
        Constants_used.level= PlayerPrefs.GetInt(Constants_used.LEVEL_ID);
        Constants_used.Player = PlayerPrefs.GetString(Constants_used.PLAYER_ID);
        Constants_used.get_score = PlayerPrefs.GetInt(Constants_used.SCORE_ID);
        Constants_used.get_life = PlayerPrefs.GetInt(Constants_used.LIFE_ID);
        Constants_used.get_max_life = PlayerPrefs.GetInt(Constants_used.MAX_LIFE_ID);
        Constants_used.shield = PlayerPrefs.GetInt(Constants_used.SHIELD_ID) == 1;
        Constants_used.get_health_pack=PlayerPrefs.GetInt(Constants_used.HEALTH_PACKS);
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
