using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Game_manager : MonoBehaviour
{
    public static event Action event_instantiate;
    public static event Action event_Pause;
    public static event Action event_Resume;
    public PlayableDirector playableDirector;
    [SerializeField] TimelineAsset[] timelines;
    [SerializeField] bool cutscene;
    GameObject _player, _pause_menu;
    Player Player;
    private void Awake()
    {
        Cursor.visible = false;
        Instantiation();
        event_set_up();
        if (cutscene) { timeline_event_setup(); animation_start(playableDirector); }
    }
    private void OnDestroy()
    {
        event_Pause -= pause;
        event_Resume -= resume;
        if (cutscene)
        {
            playableDirector.stopped -= animation_stop;
            playableDirector.played -= animation_start;
        }
    }
    private void Instantiation()
    {
        //_player= (GameObject)Instantiate(Resources.Load(Constants_used.Player), transform.position, Quaternion.identity);
        _player= (GameObject)Instantiate(Resources.Load(Constants_used.player1), transform.position, Quaternion.identity);
        _player.transform.parent = this.transform;
        Player = _player.GetComponentInChildren<Player>();
        Instantiate(Resources.Load(Constants_used.UI), transform.position, Quaternion.identity);
        _pause_menu = (GameObject)Instantiate(Resources.Load(Constants_used.Pause_Menu), transform.position, Quaternion.identity);
        Button _button = _pause_menu.GetComponentInChildren<Button>();
        _button.onClick.AddListener(FindObjectOfType<Options_Menu>().Main_Menu);
        _pause_menu.transform.parent = this.transform;
       // UnityEditor.Events.UnityEventTools.AddPersistentListener()), 
        _pause_menu.SetActive(false);
        Constants_used.Shield_using = false;
    }

    private void Start()
    {
        if (cutscene) { play(); }
    }
    private void timeline_event_setup()
    {
        playableDirector.stopped += animation_stop;
        playableDirector.played += animation_start;
    }
    void event_set_up()
    {
        event_Pause += pause;
        event_Resume += resume;
    }
    public void pause_check()
    {
        if (Constants_used.Pause) { event_Resume?.Invoke(); }
        else { event_Pause?.Invoke(); }
    }
    void pause()
    {
        _pause_menu.SetActive(true);
        Time.timeScale = 0f;
        Constants_used.Pause = true;
    }
    void resume()
    {
        _pause_menu.SetActive(false);
        Time.timeScale = 1f;
        Constants_used.Pause = false;
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
