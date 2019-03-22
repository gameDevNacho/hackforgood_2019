using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources;

    public void PlayClip(AudioClip clip)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if(audioSources[i].clip == null)
            {
                StartCoroutine(PlayClipAndNull(audioSources[i], clip));
                break;
            }
        }
    }

    private IEnumerator PlayClipAndNull(AudioSource audioSource, AudioClip clip)
    {
        audioSource.clip = clip;

        yield return new WaitForSeconds(clip.length);

        audioSource.clip = null;
    }
}
