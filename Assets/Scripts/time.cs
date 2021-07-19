using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{
    [SerializeField] int _time;
    Text time_text;
    public event Action event_time_up; 
    public event Action<int> event_change_height;
    private void Start()
    {
        event_handlers();
        time_text = GetComponent<Text>();
        update_time();
        StartCoroutine(start_time());
    }
    void event_handlers()
    {
        final_enemy enemy = FindObjectOfType<final_enemy>();
        enemy.Change_Height(this);
        enemy.Main_Destroy(this);
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
            case 60:
            case 20:
            case 10:
                event_change_height?.Invoke(-1);
                break;
            case 120:
            case 90:
            case 30:
                event_change_height?.Invoke(1);
                break;
        }
    }

    private void time_end()
    {
        event_time_up?.Invoke();
    }

    private void update_time()
    {
        int time_min = (int)Mathf.Floor(_time / 60);
        int time_sec = _time % 60;
        time_text.text = time_min + ":" + time_sec;
    }
}
