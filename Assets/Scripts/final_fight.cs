using System;
using System.Collections;
using UnityEngine;

public class final_fight : Enemy
{
    public static event Action<int> event_hurt;
    public static event Action event_dead;
    int _health = 5000;
    [SerializeField] float _fire_time=1f,_fire_speed;
    [SerializeField] int _partcle_hit=1;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject UI;
    GameObject _weapon_holder;
    Player player;
    AudioClip _hurt;
    AudioSource _AudioSource;
    public override void Start()
    {
        Instantiate(UI, transform.position, Quaternion.identity);
        _weapon_holder = new GameObject("Weapon Holder");
        _hurt = (AudioClip)Resources.Load(Constants_used.Enemy_Hurt);
        _AudioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        StartCoroutine(fire());
        base.Start();
    }
    private void Update()
    {
        movement();
    }
    void Hurt_Audio()
    {
        if (_AudioSource.isPlaying) { return; }
        _AudioSource.PlayOneShot(_hurt);
    }
    private void movement()
    {
        float __flip = Mathf.Abs(transform.localScale.x);
        float own_pos = transform.position.x;
        float player_pos = player.transform.position.x;
      //  if (player_pos < own_pos) { __flip = 1; }
        if (player_pos > own_pos) { __flip *= -1; }
        transform.localScale = new Vector2(__flip , transform.localScale.y);
        _Rigidbody2D.velocity = new Vector2(__flip * -1, 0);
    }

    IEnumerator fire()
    {
        GameObject _weapon = Instantiate(weapon, transform.position, Quaternion.identity);
        _weapon.transform.parent = _weapon_holder.transform;
        float randomness = UnityEngine.Random.Range(-0.5f, 0.05f);
        _weapon.GetComponent<Rigidbody2D>().velocity = -1 * transform.localScale.x * transform.right * _fire_speed;
        yield return new WaitForSeconds(_fire_time + randomness);
        StartCoroutine(fire());
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()!=null)
        { 
            float dir = Vector2.Dot(transform.right, collision.transform.right);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * dir * 1000);
            if (!Constants_used.Shield_using)
                event_start();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        _health -= _partcle_hit;
        Hurt_Audio();
        event_hurt?.Invoke(_partcle_hit);
        if (_health <= 0)
        {
            this.gameObject.SetActive(false);
            StopAllCoroutines();
            event_dead?.Invoke();
            FindObjectOfType<Game_manager>()?.timeline_select();
            Destroy(this.gameObject, 1f);
        }
    }
}
