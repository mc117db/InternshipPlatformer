using UnityEngine;
using System.Collections;
using System;

public class UtilGunShotSoundManager : MonoBehaviour, IRecyle {
    public AudioSource gunShotAudioSource;
    public delegate void GunShotAudioCallBack();

    public void Restart()
    {
        PlaySoundWithCallback(GunShotFinish);  
    }

    public void Shutdown()
    {
        
    }
    

    public void PlaySoundWithCallback(GunShotAudioCallBack callback)
    {
        gunShotAudioSource.Play();
        StartCoroutine(DelayedCallback(gunShotAudioSource.clip.length, callback));
    }
    private IEnumerator DelayedCallback(float time, GunShotAudioCallBack callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    void GunShotFinish()
    {
        GameObjectUtil.Destroy(gameObject);
    }
}
