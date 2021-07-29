using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : Enemy
{
    [SerializeField] float _health = 10f, _partcle_hit = 1f;
    public override void Start()
    {
        Destroy(this.gameObject, 2f);
        base.Start();
    }
    void Update()
    {
        if (Mathf.Abs(_speed) < Mathf.Epsilon) { return; }
        _Rigidbody2D.velocity = Vector2.left * _speed;
    }
    private void OnParticleCollision(GameObject other)
    {
        _health -= _partcle_hit;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
