using UnityEngine;
using UnityEngine.Playables;
public class Subtitle_Clip : PlayableAsset
{
    public string subtitle_text;
    public enum color_available { red, green, mangetta, yellow, clear, nocolor };
    public color_available color;
    public bool blend = false;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Subtitle_Behaviour>.Create(graph);
        Subtitle_Behaviour Subtitle_Behaviour = playable.GetBehaviour();
        Subtitle_Behaviour.subtitle_text = subtitle_text;
        Subtitle_Behaviour.blend = blend;
        color_determine(Subtitle_Behaviour);
        return playable;
    }

    private void color_determine(Subtitle_Behaviour Subtitle_Behaviour)
    {
        Color __color_box = Color.black;
        float alpha_val = 0.47f;
        switch (color)
        {
            case color_available.red:
                __color_box = new Color(1, 0, 0, alpha_val);
                break;
            case color_available.green:
                __color_box = new Color(0, 1, 0, alpha_val); ;
                break;
            case color_available.mangetta:
                __color_box = new Color(1, 0, 0.9f, alpha_val);
                break;
            case color_available.yellow:
                __color_box = new Color(1, 1, 0, alpha_val);
                break;
            case color_available.clear:
                __color_box = new Color(1, 1, 1, alpha_val);
                break;
            case color_available.nocolor:
                __color_box = new Color(1, 1, 1, 0);
                break;
        }
        Subtitle_Behaviour.Color = __color_box;
    }
}
