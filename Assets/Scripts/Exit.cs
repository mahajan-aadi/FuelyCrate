using UnityEngine;
using UnityEngine.Events;
using System;

public class Exit : MonoBehaviour
{
    [SerializeField] UnityEvent shop;
    public static Action Event_Level_finish;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Event_Level_finish?.Invoke();
        shop?.Invoke();
    }
}
