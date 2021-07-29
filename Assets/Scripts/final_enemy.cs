using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_enemy : moving_objects
{
    Vector3 _current_pos;
    [Header("weapon")]
    [SerializeField] GameObject weapon;
    [SerializeField] float _speed_weapon,_weapon_time;
    GameObject _weapon_holder;
    public override void Start()
    {
        base.Start();
        time.event_change_height += change_height;
        time.event_time_up += destroy;
        _current_pos = main_pos;
        _weapon_holder = new GameObject("Weapon Holder");
        StartCoroutine(fire());
    }

    IEnumerator fire()
    {
        float _factor = 1;
        if (main_pos.y > _current_pos.y) { _factor = 0.75f; }
        else if (main_pos.y < _current_pos.y) { _factor = 1.25f; }

        GameObject _weapon= Instantiate(weapon,transform.position, Quaternion.identity);
        _weapon.transform.parent = _weapon_holder.transform;
        float randomness = UnityEngine.Random.Range(-0.5f, 0.05f);
        _weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * _speed_weapon*_factor+randomness);
        yield return new WaitForSeconds(_weapon_time*(1/_factor)+randomness);
        StartCoroutine(fire());
    }

    void change_height(int increment)
    {
        main_pos = new Vector3(main_pos.x, main_pos.y + increment, main_pos.z);

    }
    void destroy()
    {
        StopAllCoroutines();
        Destroy(_weapon_holder);
        Destroy(this.gameObject);
    }
}
