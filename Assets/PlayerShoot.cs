using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject akPivot;
    [SerializeField] private GameObject[] disableObjects;
    [SerializeField] private GunFireEffect gunFireEffect;
    private Camera cam;

    public bool weaponDrawn = true;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        anim = GetComponentInChildren<Animator>();
        HideWeapon();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
            GunFaceMouse();
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

        ak.transform.localPosition = new Vector3(0, 0, 0);

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

        ak.transform.localPosition = new Vector3(-10000, -10000, -10000);
        
        foreach (var disableObject in disableObjects)
        {
            disableObject.SetActive(true);
        }
    }

    public void GunFaceMouse()
    {
        var cameraPosition = CalculateCameraPosition();
        akPivot.transform.LookAt(cameraPosition);
    }
    
    private Vector3 CalculateCameraPosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        var mask = LayerMask.GetMask("Floor");
        var didHit = Physics.Raycast(ray, out var hit, 1000, mask);
        return didHit ? hit.point : Vector3.zero;
    }

}