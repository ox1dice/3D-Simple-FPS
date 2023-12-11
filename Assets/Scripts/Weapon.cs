using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponDamage;

    // Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 0.2f;

    // Burst
    public int bulletsPerBurst = 3;
    public int burstBulletLeft;

    // Spread
    public float spreadIntensity = 5;
    
    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 500;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    Animator animator;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            // Holding down left click
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || 
                 currentShootingMode == ShootingMode.Burst)
        {
            // Clicking left click once
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Recoil");
        SoundManager.Instance.pistolChannel.Play();
        
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate bullet
        GameObject bullet = Instantiate(
            bulletPrefab, 
            bulletSpawn.position, 
            Quaternion.identity
        );

        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        // Pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(
            bulletSpawn.forward.normalized * bulletVelocity,
            ForceMode.Impulse
        );

        // Destroy bullet after some time
        StartCoroutine(
            DestroyBulletAfterTime(bullet, bulletPrefabLifeTime)
        );

        // Check after done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletLeft > 1)
        {
            burstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Hitting something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting into the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Returns shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
