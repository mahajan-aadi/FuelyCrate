
public class Enemy_UI : Life_UI
{
    // Start is called before the first frame update
    void Start()
    {
        max_health = 5000;
        _life_left = max_health;
        update_life(0);
        final_fight.event_dead += Destroy;
        final_fight.event_hurt += update_life;
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        final_fight.event_dead -= Destroy;
        final_fight.event_hurt -= update_life;
    }
}
