using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batterfly : Enemy
{
    [Header("movement")]
    [SerializeField] Vector3 move;
    [SerializeField] float cycles_x = 2;
    [SerializeField] float cycles_y = 2;
    Vector3 main_pos;

    public override void Start()
    {
        main_pos = transform.position;
        base.Start();
    }

    void Update()
    {
        moving();
    }
    void moving()
    {
        const float TAU = Mathf.PI * 2f;
        Vector2 __previous_pos = transform.position;
        float _offset_x = Mathf.Sin(Time.timeSinceLevelLoad * TAU / cycles_x);
        float _Pose_x = main_pos.x + _offset_x * move.x;
        float _offset_y = Mathf.Sin(Time.timeSinceLevelLoad * TAU / cycles_y);
        float _Pose_y = main_pos.y + _offset_y * move.y;
        transform.position = new Vector2(_Pose_x, _Pose_y);
        flip();

        void flip()
        {
            Vector2 __newpos = transform.position;
            if (__previous_pos.x > __newpos.x) { transform.localScale = new Vector3(-1, 1, 1); }
            else { transform.localScale = Vector3.one; }
        }
    }
}
