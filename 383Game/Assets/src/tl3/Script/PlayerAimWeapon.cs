using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    // Event that gets triggered when the player shoots.
    public event EventHandler<OnShootEventArgs> OnShoot;

    // Event arguments to pass useful data to listeners (like shell position, etc.)
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    // Prefab for shell visual effect and the position from which it is ejected
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform shellEjectPoint;

    // Prefab for muzzle flash and the fire point from where bullets are shot
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private Transform firePoint;

    // Internal transforms for aiming and visual references
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;

    private void Awake()
    {
        // Locate child objects by name for aiming and animation control
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update()
    {
        // Called every frame to handle aiming and input-based shooting
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        // Get mouse position in world space
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        // Calculate aiming direction based on mouse position
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Rotate weapon to face the aim direction
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Flip weapon sprite vertically if aiming left (to avoid upside-down sprites)
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
        // If left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            // Trigger shooting animation
            aimAnimator.SetTrigger("Shoot");

            // Add camera shake effect for impact
            UtilsClass.ShakeCamera(2f, 0.1f);

            // Instantiate muzzle flash effect at fire point
            if (muzzleFlashPrefab != null && firePoint != null)
            {
                GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                Destroy(flash, 0.05f); // Clean up effect after short time
            }

            // Create weapon tracer line (visual effect for bullets)
            Vector3 shootDir = (mousePosition - aimGunEndPointTransform.position).normalized;
            WeaponTracer.Create(aimGunEndPointTransform.position, shootDir);

            // Instantiate shell ejection effect
            if (shellPrefab != null && shellEjectPoint != null)
            {
                Instantiate(shellPrefab, shellEjectPoint.position, Quaternion.identity);
            }

            // Notify all observers that a shot occurred (Observer Pattern)
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                shellPosition = aimShellPositionTransform.position,
            });
        }
    }
}
