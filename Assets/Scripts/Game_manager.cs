using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Game_manager : MonoBehaviour
{
    public static event Action event_instantiate;
    public PlayableDirector playableDirector;
    [SerializeField] TimelineAsset[] timelines;
    [SerializeField] bool cutscene;
    GameObject _player;
    Player Player;
    private void Awake()
    {
        //GameObject player= (GameObject)Instantiate(Resources.Load(Constants_used.Player), transform.position, Quaternion.identity);
        _player = (GameObject)Instantiate(Resources.Load("Player1"), transform.position, Quaternion.identity);
        _player.transform.parent = this.transform;
        Player = _player.GetComponentInChildren<Player>();
        Instantiate(Resources.Load(Constants_used.UI), transform.position, Quaternion.identity);
        if (cutscene) { event_setup(); animation_start(playableDirector); }
    }

    private void Start()
    {
        if (cutscene) { play(); }
    }
    private void event_setup()
    {
        playableDirector.stopped += animation_stop;
        playableDirector.played += animation_start;
    }

    public void play()
    {
        playableDirector.Play();
    }
    public void timeline_select(int number = 1)
    {
        playableDirector.Play(timelines[number - 1]);
    }
    void animation_start(PlayableDirector director) { Player.set_animator(true); }
    void animation_stop(PlayableDirector director) { Player.set_animator(false); }
    public void place_object()
    {
        event_instantiate?.Invoke();
    }
    public void zero_gravity()
    {
        Player.set_gravity(true);
    }
    public void full_gravity()
    {
        Player.set_gravity(false);
    }
}
