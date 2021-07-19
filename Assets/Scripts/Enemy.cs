using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] int _hit=10;
    public event Action<int> event_enemy_collision;
    protected Rigidbody2D _Rigidbody2D;
    protected CapsuleCollider2D _body;
    [SerializeField] protected float _speed = 1;
    public virtual void Start()
    {
        UI_handler ui_Handler = FindObjectOfType<UI_handler>();
        if (ui_Handler != null) { ui_Handler.life_event(this); }
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _body = GetComponent<CapsuleCollider2D>();
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()!=null)
        {
            float dir = Vector2.Dot(transform.right, collision.transform.right);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left*dir * 1000);
            event_enemy_collision?.Invoke(_hit);
            Destroy(this.gameObject);
        }
    }
    protected void event_start() { event_enemy_collision?.Invoke(_hit); }
}