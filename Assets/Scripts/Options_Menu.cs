using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Options_Menu : MonoBehaviour
{
    [SerializeField] Button health, shield;
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
        Constants_used.get_life += (int)(Mathf.Ceil(0.1f * Constants_used.get_max_life)); 
        if(Constants_used.get_life>= Constants_used.get_max_life)
        {
            Constants_used.get_life = Constants_used.get_max_life;
            health.interactable = false;
        }
    }
    public void activate_shield() 
    {
        Constants_used.shield = true; 
        shield.interactable = false; 
    }
    public void next_scene() { Constants_used.level += 1; SceneManager.LoadScene(Constants_used.level); }
    public void Shop() { SceneManager.LoadScene("Shop"); }
}
