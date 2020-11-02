using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private int currentTrack = 0;
    public static AudioManager instance;
    public List<AudioClip> MusicClips;
    public List<AudioClip> Sounds;
    public AudioMixer mixer;
    public AudioSource MusicSource;
    public AudioMixerGroup SFXGroup;
    public AudioMixerSnapshot[] normal, musicOff;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
        if(instance != this)
        {
            instance = this;
        }
    }
    public void PlaySound(string name)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource AS in audioSources)
        {
            if (!AS.isPlaying)
            {
                AS.clip = Sounds.Find(x => x.name == name);
                AS.Play();
                return;
            }
            else if (AS.clip != null && AS.clip.name == name)
            {
                return;
            }
        }
        AudioSource As = gameObject.AddComponent<AudioSource>();
        As.outputAudioMixerGroup = SFXGroup;
        As.clip = Sounds.Find(x => x.name == name);
        As.Play();
    }
    private void Update()
    {
        transform.position = Camera.main.transform.position;
        if (MusicSource.isPlaying == false)
        {
            currentTrack = (currentTrack + 1) % MusicClips.Count;
            PlayMusic(MusicClips[currentTrack].name);
        }
    }
    public void PlayMusic(string name)
    {
        if (MusicSource.isPlaying)
        {
            MusicSource.clip = MusicClips.Find(x => x.name == name);
            StartCoroutine(VolumeTransition(true, true));
            MusicSource.PlayDelayed(1f);
        }
        else
        {
            StartCoroutine(VolumeTransition(false, true));
            MusicSource.clip = MusicClips.Find(x => x.name == name);
            MusicSource.Play();
        }
    }
    private IEnumerator VolumeTransition(bool transitionDown, bool transitionUp, float transitionTime = 2f)
    {
        if (transitionDown && transitionUp)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, transitionTime / 2);
            yield return new WaitForSeconds(transitionTime / 2);
            mixer.TransitionToSnapshots(normal, new float[] { 1 }, transitionTime / 2);
            yield break;
        }

        if (transitionDown)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, transitionTime / 2);
        }
        if (transitionUp)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, 0);
            mixer.TransitionToSnapshots(normal, new float[] { 1 }, transitionTime / 2);
        }
        yield break;
    }

}
