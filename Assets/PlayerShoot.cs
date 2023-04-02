using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject akPivot;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform gunAim;
    [SerializeField] private GameObject[] disableObjects;
    [SerializeField] private GunFireEffect gunFireEffect;
    [SerializeField] private GameObject missEffect;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float dropOff;
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
        if (LevelManager.Instance.State == LevelManager.GameState.Pre) return;
        if (Input.GetMouseButtonDown(0))
        {
            ShowWeapon();
        }
        if (Input.GetMouseButton(0))
        {
            GunFaceMouse();
            Shoot();
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
        if (!gunFireEffect.Play()) return;
        if (Physics.Raycast(gunTip.position, (gunAim.position -gunTip.position).normalized, out var hit, dropOff, LayerMask.GetMask("Enemy")))
        {
            var gameObject = hit.transform.gameObject;
            EnemyAi? enemyAi = gameObject.GetComponent<EnemyAi>();
            enemyAi?.TakeHit();

            hitEffect.transform.position = hit.transform.position;
            foreach (var partSys in hitEffect.GetComponentsInChildren<ParticleSystem>())
            {
                partSys.Play();
            }
        }
        else
        {
            //miss effect
            var missLocation = gunTip.position + ((gunAim.position - gunTip.position).normalized * dropOff);
            missEffect.transform.position = missLocation + (Vector3.up * 0.15f);
            foreach (var partSys in missEffect.GetComponentsInChildren<ParticleSystem>())
            {
                partSys.Play();
            }
        }
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
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunTip.position, gunTip.position + (gunAim.position - gunTip.position).normalized);
    }

#endif
}
