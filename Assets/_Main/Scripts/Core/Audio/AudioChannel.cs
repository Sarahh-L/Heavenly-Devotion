using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChannel
{
    private const string track_container_name_format = "Channel - [{0}]";
    public int channelIndex { get; private set; }

    public Transform trackContainer { get; private set; } = null;

    public AudioTrack activeTrack { get; private set; } = null;
    private List<AudioTrack> tracks = new List<AudioTrack>();

    bool isLevelingVolume => co_volumeLeveling != null;
    Coroutine co_volumeLeveling = null;

    public AudioChannel(int channel)
    {
        channelIndex = channel;

        trackContainer = new GameObject(string.Format(track_container_name_format, channel)).transform;
        trackContainer.SetParent(AudioManager.instance.transform);
    }

    public AudioTrack PlayTrack(AudioClip clip, bool loop, float startingVolume, float volumeCap, float pitch, string filePath)
    {
        if (TryGetTrack(clip.name, out AudioTrack existingTrack))
        {
            if (!existingTrack.isPlaying)
                existingTrack.Play();

            SetAsActiveTrack(existingTrack);

            return existingTrack;
        }

        AudioTrack track = new AudioTrack(clip, loop, startingVolume, volumeCap, pitch, this, AudioManager.instance.musicMixer, filePath);
        track.Play();

        SetAsActiveTrack(track);

        return track;
    }

    private void SetAsActiveTrack(AudioTrack track)
    {
        if (!tracks.Contains(track))
            tracks.Add(track);

        activeTrack = track;

        TryStartVolumeLeveling();
    }

    public bool TryGetTrack(string trackName, out AudioTrack value)
    {
        trackName = trackName.ToLower();

        foreach (var track in tracks)
        {
            if (track.name.ToLower() == trackName)
            {
                value = track;
                return true;
            }
        }

        value = null;
        return false;
    }

    private void TryStartVolumeLeveling()
    {
        if (!isLevelingVolume)
            co_volumeLeveling = AudioManager.instance.StartCoroutine(VolumeLeveling());
    }

    private IEnumerator VolumeLeveling()
    {
        while ((activeTrack != null && (tracks.Count > 1 || activeTrack.volume != activeTrack.volumeCap)) || (activeTrack == null && tracks.Count > 0))
        {
            for (int i = tracks.Count - 1; i >= 0; i--)
            {
                AudioTrack track = tracks[i];

                float targetVol = activeTrack == track ? track.volumeCap : 0;

                if (track == activeTrack && track.volume == targetVol)
                    continue;

                track.volume = Mathf.MoveTowards(track.volume, targetVol, AudioManager.track_transition_speed * Time.deltaTime);

                if (track != activeTrack && track.volume == 0)
                {
                    DestroyTrack(track);
                }
            }
            yield return null;
        }

        co_volumeLeveling = null;
    }

    private void DestroyTrack(AudioTrack track)
    {
        if (tracks.Contains(track))
            tracks.Remove(track);

        Object.Destroy(track.root);
    }

    public void StopTrack(bool immediate = false)
    {
        if (activeTrack == null)
            return;

        if (immediate)
        {
            DestroyTrack(activeTrack);
            activeTrack = null;
        }
        else
        {
            activeTrack = null;
            TryStartVolumeLeveling();
        }
        
    }
}
