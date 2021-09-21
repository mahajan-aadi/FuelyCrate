using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;
public class SubtitleTrack_Mixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        string base_string = "";
        float alpha = 0f;
        bool blend = false;
        Image image = playerData as Image;
        Color color = new Color(0, 0, 0, 0);
        if (!image) { return; }
        int input_count = playable.GetInputCount();
        for(int i = 0; i < input_count; i++)
        {
            float input_weight = playable.GetInputWeight(i);
            if (input_weight > 0f)
            {
                ScriptPlayable<Subtitle_Behaviour> input = (ScriptPlayable<Subtitle_Behaviour>)playable.GetInput(i);
                Subtitle_Behaviour subtitle_Behaviour = input.GetBehaviour();
                base_string = subtitle_Behaviour.subtitle_text;
                alpha = input_weight;
                blend = subtitle_Behaviour.blend;
                color = subtitle_Behaviour.Color;
                if(blend)
                    color *= input_weight;
            }
        }
        image.GetComponentInChildren<TextMeshProUGUI>().text = base_string;
        image.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, alpha);
        image.color = color;
    }
}
