using UnityEngine;
using System;
public class Shield : MonoBehaviour
{
    void Start()
    {
        Constants_used.Shield_using = true;
        Invoke(nameof(Destroy), 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.GetComponent<Enemy>() != null)
        {
            if (coll.GetComponent<water>() == null && coll.GetComponent<final_fight>() == null)
            { Destroy(collision.gameObject); }
        }

    }
    private void Destroy()
    {
        Constants_used.Shield_using = false;
        Constants_used.shield = false;
        Destroy(this.gameObject);
    }
}
