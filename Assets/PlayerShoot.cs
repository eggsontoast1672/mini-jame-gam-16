using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject[] enableObjects;
    [SerializeField] private GameObject[] disableObjects;
    [SerializeField] private GunFireEffect gunFireEffect;

    public bool weaponDrawn = true;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        HideWeapon();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(0))
        {
            ShowWeapon();
        }
        if (Input.GetMouseButtonUp(0))
        {
            HideWeapon();
        }
    }

    private void ShowWeapon()
    {
        if (weaponDrawn)
        {
            return;
        }
        weaponDrawn = true;
        foreach (var enableObject in enableObjects)
        {
            enableObject.SetActive(true);
        }

        foreach (var disableObject in disableObjects)
        {
            disableObject.SetActive(false);
        }
    }

    private void Shoot()
    {
        gunFireEffect.Play();
    }

    private void HideWeapon()
    {
        if (!weaponDrawn)
        {
            return;
        }
        weaponDrawn = false;
        foreach (var enableObject in enableObjects)
        {
            enableObject.SetActive(false);
        }

        foreach (var disableObject in disableObjects)
        {
            disableObject.SetActive(true);
        }

    }
}
