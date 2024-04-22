using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] stepsSounds;
    [SerializeField]
    private float stepVolume = 1f;
    [SerializeField]
    private float stepWaitTime = 1f;

    private List<AudioSource> stepSources = new List<AudioSource>();

    void Start()
    {
        if (stepsSounds.Length != 5) throw new System.Exception("There has to be 5 steps sounds.");

        foreach (AudioClip sound in stepsSounds)
        {
            GameObject audioObject = new GameObject("Step Audio");
            audioObject.transform.parent = transform;

            AudioSource objectAudio = audioObject.AddComponent<AudioSource>();
            objectAudio.volume = stepVolume;
            objectAudio.clip = sound;
            objectAudio.playOnAwake = false;

            stepSources.Add(objectAudio);
        }
    }


    public void playStepsSound(int range, bool gettingClosser)
    {
        if (range > stepsSounds.Length - 3) throw new System.Exception("Range out of bounds.");

        List<AudioSource> stepsToPlay = stepSources.GetRange(range, 3);
        if (!gettingClosser) stepsToPlay.Reverse();

        StartCoroutine(playSteps(stepsToPlay.ToArray()));
    }

    IEnumerator playSteps(AudioSource[] stepsPlay)
    {
        foreach(AudioSource step in stepsPlay)
        {
            step.Play();

            yield return new WaitForSeconds(stepWaitTime);
        }
    }
}
