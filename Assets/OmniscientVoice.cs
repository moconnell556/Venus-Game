using UnityEngine;

public class OmniscientVoice : MonoBehaviour
{
    public AudioSource source;
    public AudioClip intro;
    public AudioClip warning;
    public AudioClip lost;
    public AudioClip won;

    private bool introPlayed;
    private bool warned;
    private bool ended;

    void Update()
    {
        var s = HabitatSim.Instance;
        if (s == null) return;

        if (!introPlayed && s.TotalDays > 0.01f)
        {
            introPlayed = true;
            Play(intro);
        }

        if (!warned && s.Health < 40f && s.Status == GameStatus.Running)
        {
            warned = true;
            Play(warning);
        }

        if (!ended && s.Status == GameStatus.Lost)
        {
            ended = true;
            Play(lost);
        }
        else if (!ended && s.Status == GameStatus.Won)
        {
            ended = true;
            Play(won);
        }
    }

    void Play(AudioClip clip)
    {
        if (clip == null || source == null) return;
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}