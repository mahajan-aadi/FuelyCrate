﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants_used : MonoBehaviour
{
    static int _score = 0;
    static int _life = 100;
    static int _max_life = 100;
    public static string player1 = "Player1";
    public static string player2 = "Player2";
    public static string background_layer = "Background";
    public static string foreground_layer = "Ground";
    public static string hazard_layer = "Hazard";
    public static string player_layer = "Player";
    public static string climb_layer = "climb";
    public static string Ninja_transport = "transport";
    public static string Ninja_attack = "Sword";
    public static string Vertical = "Vertical";
    public static string Horizontal = "Horizontal";
    public static string SpaceBar = "Space";
    public static string UI = "UI";
    public static string Shield = "Shield";
    public static string score_prefix = "Score: ";

    public static int get_score { get { return _score; } set { _score = value; } }
    public static int get_life { get { return _life; } set { _life = value; } }
    public static int get_max_life { get { return _max_life; } set { _max_life = value; } }
    public static string Player { get; set; }
    public static bool shield { get; set; }
    public static int level { get; set; }
}
