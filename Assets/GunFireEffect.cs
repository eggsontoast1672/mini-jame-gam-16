using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GunFireEffect : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private ParticleSystem[] particles;

    private bool isPlaying;
    public void Play()
    {
        if(isPlaying) return;
        StartCoroutine("Effect");
    }
    public IEnumerator Effect()
    {
        if (isPlaying) yield break;
        isPlaying = true;

        foreach (var particle in particles)
        {
            particle.Play();
        }

        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        isPlaying = false;
    }
}
