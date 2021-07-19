using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    void Start()
    {
        Invoke(nameof(Destroy), 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>()!=null)
            Destroy(collision.gameObject);
    }
    private void Destroy()
    {
        Constants_used.shield = false;
        Destroy(this.gameObject);
    }
}
