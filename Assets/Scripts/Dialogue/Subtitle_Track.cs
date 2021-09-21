using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.Playables;

[TrackBindingType(typeof(Image))]
[TrackClipType(typeof(Subtitle_Clip))]
public class Subtitle_Track : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<SubtitleTrack_Mixer>.Create(graph, inputCount);
    }
}
