using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninja : Enemy
{
    [SerializeField]  Transform[] _locations;
    [SerializeField] GameObject _fire;
    Collision2D this_obj;
    Animator Animator;
    GameObject _weapon_holder;
    public override void Start()
    {
        base.Start();
        _weapon_holder = new GameObject("Weapon Holder");
        Animator = GetComponent<Animator>();
        StartCoroutine(_delay());
    }

    IEnumerator _delay()
    {
        Animator.SetBool(Constants_used.Ninja_transport, false);
        yield return new WaitForSeconds(0.3f);

        GameObject __fire= Instantiate(_fire, transform.position, Quaternion.identity);
        __fire.transform.parent = _weapon_holder.gameObject.transform;
        Animator.SetBool(Constants_used.Ninja_transport, true);
        yield return new WaitForSeconds(0.5f);

        int __ran_no = Random.Range(0, _locations.Length );
        transform.position = _locations[__ran_no].position;

        StartCoroutine(_delay());
    }
    IEnumerator _end()
    {
        Animator.SetTrigger(Constants_used.Ninja_attack);
        yield return new WaitForSeconds(0.4f);
        Destroy(_weapon_holder);
        base.OnCollisionEnter2D(this_obj);
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            this_obj = collision;
            StartCoroutine(_end());
        }
    }
}
