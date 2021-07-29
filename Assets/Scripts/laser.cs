using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class laser : MonoBehaviour
{
    [SerializeField] ParticleSystem bullets;
    ParticleSystem _BUL;
    ParticleSystem _BUL_L;
    ParticleSystem _BUL_R;
    void Start()
    {
         _BUL_R = Instantiate(bullets, FindObjectOfType<Player>().transform);
         _BUL_L = Instantiate(bullets, FindObjectOfType<Player>().transform);
        _BUL = _BUL_R;
        _BUL_L.transform.localScale = new Vector3(-1, _BUL.transform.localScale.y, _BUL.transform.localScale.z);
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetAxis(Constants_used.Horizontal) < 0) { bullets_check(true); }
        else { bullets_check(false); }
        if (Input.GetKey(KeyCode.X)){ _BUL.Play(); }
        if(Input.GetKeyUp(KeyCode.X)){ _BUL.Stop(); }
    }
    void disable_particles()
    {
        _BUL.Stop();
    }
    void bullets_check(bool left)
    {
        bullets = _BUL;
        if (Input.GetKey(KeyCode.X))
        {
            _BUL.Stop();
        }
        if (left) _BUL = _BUL_L;
        else _BUL = _BUL_R;
        if (Input.GetKey(KeyCode.X) && bullets!=_BUL)
        {
            _BUL.Play();
        }
    }
}
