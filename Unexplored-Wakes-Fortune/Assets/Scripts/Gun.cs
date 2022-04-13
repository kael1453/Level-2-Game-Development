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

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        gunData.reloading = false;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > (1f / (gunData.fireRate / 60f));
    // && gunData.currentAmmo > 0
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

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magazineSize;
        print("reloadingfinished");

        gunData.reloading = false;
    }
}
