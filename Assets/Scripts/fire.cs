using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : Enemy
{
    public override void Start()
    {
        Destroy(this.gameObject, 5f);
        base.Start();
    }
    void Update()
    {
        if (_speed < Mathf.Epsilon) { return; }
        _Rigidbody2D.velocity = Vector2.left * _speed;
    }
}
