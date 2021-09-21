using UnityEngine;

public class Instantiate_enemy : MonoBehaviour
{
    public GameObject _gameObject;
    public Vector3 _position;
    void Start()
    {
        Game_manager.event_instantiate += inatantiate_enemy;
    }
    void inatantiate_enemy()
    {
        Instantiate(_gameObject, _position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) { return; }
        FindObjectOfType<Game_manager>().place_object();
        Destroy(this.gameObject);
    }
}
