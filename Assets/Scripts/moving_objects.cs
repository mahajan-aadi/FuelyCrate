using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_objects : MonoBehaviour
{
    [Header("movement_x")]
    [SerializeField] Vector3 move_x;
    protected Vector3 main_pos; 
    [SerializeField] float cycles_x = 2;
    [Tooltip("the sprite is reversed when moving in reveerse")]
    [SerializeField] protected bool sprite_reversable = false;
    public virtual void Start()
    {
        main_pos = transform.position;
    }

    void Update()
    {
        moving();
    }
    void moving()
    {
        const  float TAU= Mathf.PI * 2f;
        Vector2 __previous_pos = transform.position;
        float offset = Mathf.Sin(Time.timeSinceLevelLoad * TAU / cycles_x);
        transform.position = main_pos + offset * move_x;
        if(sprite_reversable) flip();

        void flip()
        {
            Vector2 __newpos = transform.position;
            if (__previous_pos.x > __newpos.x) { transform.localScale = new Vector3(-1, 1, 1); }
            else { transform.localScale = Vector3.one; }
        }
    }
}
