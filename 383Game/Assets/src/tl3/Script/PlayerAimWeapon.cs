using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform shellEjectPoint;

    [SerializeField] private GameObject muzzleFlashPrefab;     
    [SerializeField] private Transform firePoint;             

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if (mousePosition.x < transform.position.x)
        {
            aimTransform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            aimTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            aimAnimator.SetTrigger("Shoot");

            // 💥 Screen shake
            UtilsClass.ShakeCamera(2f, 0.1f);

            // 🔥 Muzzle flash using prefab
            if (muzzleFlashPrefab != null && firePoint != null)
            {
                GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                Destroy(flash, 0.05f); // auto-destroy after a short time
            }

            // ⚡ Tracer line
            Vector3 shootDir = (mousePosition - aimGunEndPointTransform.position).normalized;
            WeaponTracer.Create(aimGunEndPointTransform.position, shootDir);

            // 🔩 Shell ejection
            if (shellPrefab != null && shellEjectPoint != null)
            {
                Instantiate(shellPrefab, shellEjectPoint.position, Quaternion.identity);
            }

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                shellPosition = aimShellPositionTransform.position,
            });
        }
    }
}
