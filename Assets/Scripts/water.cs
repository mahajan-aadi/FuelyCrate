using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : Enemy
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            float dir = Vector2.Dot(transform.right, collision.transform.right);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * dir * 1000);
            event_start();
            StartCoroutine(harm());
        }
    }

    IEnumerator harm()
    {
        event_start();
        yield return new WaitForSeconds(1f);
        StartCoroutine(harm());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
    }
}
