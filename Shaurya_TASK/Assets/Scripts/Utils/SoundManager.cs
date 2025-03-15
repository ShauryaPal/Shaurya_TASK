using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void PlaySfx(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        source.PlayOneShot(clip);
        StartCoroutine(KillAudioSourceAfterFinishPlaying(source, clip));
    }

    IEnumerator KillAudioSourceAfterFinishPlaying(AudioSource source, AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        Destroy(source);
    }
}
