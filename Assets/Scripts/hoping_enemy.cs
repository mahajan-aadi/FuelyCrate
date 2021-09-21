using UnityEngine;

public class hoping_enemy : Enemy
{
    BoxCollider2D barrier;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        barrier = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _getmovement();
    }
    private void _getmovement()
    {
        bool __x = barrier.IsTouchingLayers(LayerMask.GetMask(Constants_used.foreground_layer));
        float _speed_now = _speed;
        if (!__x) _speed_now *= -1;
        //input
        Vector2 __vel = new Vector2(_speed_now * transform.localScale.x, _Rigidbody2D.velocity.y);
        _Rigidbody2D.velocity = __vel;
        //flip
        Vector3 __new_scale = new Vector3(Mathf.Sign(_speed_now) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        this.transform.localScale = __new_scale;
    }
}
