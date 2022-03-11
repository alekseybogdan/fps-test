using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem impactParticleSystem;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private AudioClip shootSound;
    
    float timeSinceLastShot;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        ammoText.SetText($"{gunData.currentAmmo} / {gunData.magSize}");
    }

    private void Start() {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
        
        ammoText.SetText($"{gunData.currentAmmo} / {gunData.magSize}");
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                    
                    TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
                    _audioSource.PlayOneShot(shootSound);

                    StartCoroutine(SpawnTrail(trail, hitInfo));
                }

                gunData.currentAmmo--;
                ammoText.SetText($"{gunData.currentAmmo} / {gunData.magSize}");
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }
    
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Instantiate(impactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() {  }
}