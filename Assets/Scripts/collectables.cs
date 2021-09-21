using UnityEngine;
using System;

public class collectables : MonoBehaviour
{
    public event Action<int> event_increase_points;
    [SerializeField] int _points;
    void Start()
    {
        Player_UI.score_event_handler(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            event_increase_points?.Invoke(_points);
            Destroy(this.gameObject);
        }
    }
}
