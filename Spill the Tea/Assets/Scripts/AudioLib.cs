using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Audio
{
 
    public static class AudioLib
    {
        public static class FadeAudioSource {
            public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
            {
                float currentTime = 0;
                float start = audioSource.volume;
                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                    yield return null;
                }
                yield break;
            }
        }
    }

    public static class FadeMixerGroup
    {
        public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration,
            float targetVolume)
        {
            float currentTime = 0;
            float currentVol;
            audioMixer.GetFloat(exposedParam, out currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                SetGroupVol(audioMixer, exposedParam, currentVol, targetValue, currentTime / duration);
                yield return null;
            }

            yield break;
        }
public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration,
            float targetVolume,float waitForSeconds)
        {
            yield return new WaitForSeconds(waitForSeconds);
            float currentTime = 0;
            float currentVol;
            audioMixer.GetFloat(exposedParam, out currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                SetGroupVol(audioMixer, exposedParam, currentVol, targetValue, currentTime / duration);
                yield return null;
            }

            yield break;
        }

        public static void SetGroupVol(AudioMixer audioMixer, string exposedParam, float oldVol, float newVol, float value)
        {
            float setVol = Mathf.Lerp(oldVol, newVol, value);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(setVol) * 20);
            // float debugVol = 666;
            // audioMixer.GetFloat(exposedParam, out debugVol);
        }
        
        public static void SetGroupVol2(AudioMixer audioMixer, string exposedParam, float value)
        {
            
            audioMixer.SetFloat(exposedParam, Mathf.Log10(value) * 20);
            // float debugVol = 666;
            // audioMixer.GetFloat(exposedParam, out debugVol);
        }
        
        // public static IEnumerator DelayedInvoke(float waitForSecons, Action action){    
        //     yield return new WaitForSeconds(waitForSecons);
        //     
        // }
    }
    
    
}
