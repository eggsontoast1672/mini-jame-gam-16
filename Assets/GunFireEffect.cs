using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class GunFireEffect : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private AudioSource soundEffect;

    private bool isPlaying;
    public bool Play()
    {
        if(isPlaying) return false;
        StartCoroutine("Effect");
        return true;
    }
    public IEnumerator Effect()
    {
        if (isPlaying) yield break;
        isPlaying = true;
        soundEffect.pitch = (float)(new Random().NextDouble() % 0.3 + 0.85);
        soundEffect.Play();

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
