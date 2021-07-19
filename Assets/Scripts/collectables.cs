using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class collectables : MonoBehaviour
{
    public event Action<int> event_increase_points;
    [SerializeField] int _points;
    void Start()
    {
        FindObjectOfType<UI_handler>().add_score_event(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            event_increase_points?.Invoke(_points);
        }
        Destroy(this.gameObject);
    }
}
