using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    [SerializeField] UnityEvent shop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        shop?.Invoke();
    }
}
