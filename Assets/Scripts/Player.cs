using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    Rigidbody2D _Rigidbody2D;
    Animation_controller _animator;
    BoxCollider2D _feet;
    [SerializeField] float _speed = 7;
    [SerializeField] float _jump_speed = 25;
    [SerializeField] float _climb_speed = 3;
    bool _Animation = false;
    Game_manager _game_Manager;
    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = transform.parent.parent.GetComponent<Animation_controller>();
        _feet = GetComponent<BoxCollider2D>();
        _game_Manager = GetComponentInParent<Game_manager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){ _game_Manager.pause_check(); }
        if (Constants_used.Pause) { return; }
        if (_Animation) { return; }
         _getmovement();
        _jump();
        _climb();

        if (Constants_used.shield && Input.GetKeyDown(KeyCode.Q))
        {
            GameObject shield = (GameObject)Instantiate(Resources.Load(Constants_used.Shield), transform.position, Quaternion.identity);
            shield.transform.parent = this.transform;
        }

    }

    private void _climb()
    {
        if (_feet.IsTouchingLayers(LayerMask.GetMask(Constants_used.climb_layer)))
        {
            float __y = CrossPlatformInputManager.GetAxis(Constants_used.Vertical);
            float __x = CrossPlatformInputManager.GetAxis(Constants_used.Horizontal);
            Vector2 __vel = new Vector2(__x, __y * _climb_speed);
            _Rigidbody2D.velocity = __vel;
            _animator.climb();
            _Rigidbody2D.gravityScale = 0f;
        }
        else
        {
            _animator.climb(false);
            _Rigidbody2D.gravityScale = 10f;
        }
    }

    private void _getmovement()
    {
        //input
        float __x = CrossPlatformInputManager.GetAxis(Constants_used.Horizontal);
        Vector2 __vel = new Vector2(__x * _speed, _Rigidbody2D.velocity.y);
        _Rigidbody2D.velocity = __vel;
        //flip
        Vector3 __new_scale = new Vector3(Mathf.Sign(__x)*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        this.transform.localScale = __new_scale;
        //animation
        bool __state = Mathf.Abs(_Rigidbody2D.velocity.x) > Mathf.Epsilon;
        _animator.run(__state);

    }
    void _jump()
    {
        if (_feet.IsTouchingLayers(LayerMask.GetMask(Constants_used.foreground_layer)))
        {
            bool __y = CrossPlatformInputManager.GetButtonDown(Constants_used.SpaceBar);
            if (__y)
            {
                Vector2 __vel = new Vector2(0, _jump_speed);
                _Rigidbody2D.velocity += __vel;
            }
        }
    }
    public void set_animator(bool value) 
    {
        _Animation = value;
        if(_Rigidbody2D)
            _Rigidbody2D.velocity = Vector2.zero; 
    }
    public void set_gravity(bool value) 
    {
        if (value)
        {
            _Rigidbody2D.gravityScale = 0f;
            transform.localPosition = Vector3.zero;
        }

        else
            _Rigidbody2D.gravityScale = 10f;        
    }
}
