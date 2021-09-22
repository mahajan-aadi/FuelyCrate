using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class time : MonoBehaviour
{
    [SerializeField] int _time;
    [SerializeField] TMP_Text time_text;
    public static event Action event_time_up; 
    public static event Action<int> event_change_height;
    private void Start()
    {
        Game_manager.event_instantiate += Starting;
    }
    void Starting()
    {
        event_time_up += destoy;
        update_time();
        StartCoroutine(start_time());
    }
    IEnumerator start_time()
    {
        if (_time == 0)
        {
            time_end();
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        _time--;
        conditions_check();
        update_time();
        StartCoroutine(start_time());
    }

    private void conditions_check()
    {
        switch (_time)
        {
            case 150:
            case 30:
            case 20:
            case 8:
            case 5:
                event_change_height?.Invoke(-2);
                break;
            case 120:
            case 90:
            case 60:
            case 10:
                event_change_height?.Invoke(2);
                break;
        }
    }

    private void time_end()
    {
        event_time_up?.Invoke();
        FindObjectOfType<Game_manager>().timeline_select();
    }

    private void update_time()
    {
        int time_min = (int)Mathf.Floor(_time / 60);
        int time_sec = _time % 60;
        time_text.text = time_min + ":" + time_sec;
    }
    void destoy() { Destroy(this.gameObject); }
    private void OnDestroy()
    {
        event_time_up -= destoy;
        Game_manager.event_instantiate -= Starting;
    }
}
