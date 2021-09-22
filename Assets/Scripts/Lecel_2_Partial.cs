using UnityEngine;
public class Lecel_2_Partial : MonoBehaviour
{
    AudioSource AudioSource;
    [SerializeField] AudioClip main, vain;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        time.event_time_up += Play_Vain;
        final_fight.event_dead += Play_Vain;
        Game_manager.event_instantiate += Play_Main;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) { return; }
        Play_Vain();
    }

    private void Play_Vain()
    {
        AudioSource.clip = vain;
        AudioSource.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) { return; }
        Play_Main();
    }

    private void Play_Main()
    {
        AudioSource.clip = main;
        AudioSource.Play();
    }
    private void OnDestroy()
    {
        time.event_time_up -= Play_Vain;
        final_fight.event_dead -= Play_Vain;
        Game_manager.event_instantiate -= Play_Main;
    }
}
