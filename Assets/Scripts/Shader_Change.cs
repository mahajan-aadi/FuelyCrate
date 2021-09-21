using System.Collections;
using UnityEngine;

public class Shader_Change : MonoBehaviour
{
    Material _Material;
    float _fade = 0f, _scale = 20f;
    Color _color = new Color32(0,13,191,255);
    const string FADE_STRING = "_Fade";
    const string SCALE_STRING = "_Scale";
    const string COLOR_STRING = "Color_EC039432";
    // Start is called before the first frame update
    void Start()
    {
        Player_UI.event_player_dead += lose_level;
        _Material = GetComponent<SpriteRenderer>().material;
        _set_up();
        StartCoroutine(Start_effect());
        Exit.Event_Level_finish += finish_level;
    }

    private void _set_up()
    {
        _Material.SetFloat(FADE_STRING, _fade);
        _Material.SetFloat(SCALE_STRING, _scale);
        _Material.SetColor(COLOR_STRING, _color);

    }

    void lost()
    {
        _fade = 1f;
        _scale = 100f;
        _color = new Color32(191, 19, 0, 255);
        _set_up();
    }

    IEnumerator Start_effect()
    {
        _fade += 0.1f;
        _Material.SetFloat(FADE_STRING, _fade);
        yield return new WaitForSeconds(0.2f);
        if (_fade <= 1f) { StartCoroutine(Start_effect()); }
        else { StopAllCoroutines(); }
    }
    IEnumerator Lose_effect()
    {
        _fade -= 0.1f;
        _Material.SetFloat(FADE_STRING, _fade);
        yield return new WaitForSeconds(0.1f);
        if (_fade >= 0f) { StartCoroutine(Lose_effect()); }
        else { StopAllCoroutines(); }
    }
    void finish_level()
    {
        StartCoroutine(Lose_effect());
    }
    void lose_level()
    {
        lost();
        StartCoroutine(Lose_effect());
    }
}
