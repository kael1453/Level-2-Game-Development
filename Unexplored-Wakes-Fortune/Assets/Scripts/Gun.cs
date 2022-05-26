using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform muzzle;
    Pool bulletPool;

    public Vector3 hitEffectOffset;
    float timeSinceLastShot;

    private void Start()
    {
        bulletPool = new Pool(gunData.bulletPrefab, 10);

        PlayerShoot.shootInput += Shoot; // Call shoot when shootInput?.Invoke() is called.
        PlayerShoot.reloadInput += StartReload; // Same for the reload.

        gunData.reloading = false; // To make sure that we can reload.
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > (1f / (gunData.fireRate / 60f)) && gunData.currentAmmo > 0;
    
    public void Shoot()
    {
        if (CanShoot())
        {
            muzzleFlash.Play();

            // Activate the next bullet in the pool.
            bulletPool.ActivateNext(muzzle.transform.position, muzzle.transform.rotation);
            gunData.currentAmmo--;
            timeSinceLastShot = 0f;
            OnGunShot();
            /*
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, gunData.maxDistance))
            {
                

                GameObject hitEffectGameObject = Instantiate(impactEffect, hit.point + hitEffectOffset, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffectGameObject, 3f);
            }
            */

        }

    }

    private void OnGunShot()
    {
        // Something.
    }

    private void OnDisable() => gunData.reloading = false; // Stop reloading if the gun isn't equipped.

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true; // Start reloading.

        yield return new WaitForSeconds(gunData.reloadTime); // Wait until the reload time to finish reloading.

        gunData.currentAmmo = gunData.magazineSize; // The actual reloading.
        print("reloadingfinished");

        gunData.reloading = false; // Finish reloading, allowing us to reload again if we wish.
    }
}
