using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;
    public AudioSource emptyMagazineSound;

    public AudioClip shootingSoundPistol;
    public AudioClip shootingSoundAR;

    public AudioClip reloadingSoundPistol;
    public AudioClip reloadingSoundAR;

    public AudioSource zombieChannel;

    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;

    public AudioSource playerChannel;

    public AudioClip playerHurt;
    public AudioClip playerDie;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                ShootingChannel.PlayOneShot(shootingSoundPistol);
                break;
            case WeaponModel.AssaultRifle:
                ShootingChannel.PlayOneShot(shootingSoundAR);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                ShootingChannel.PlayOneShot(reloadingSoundPistol);
                break;
            case WeaponModel.AssaultRifle:
                ShootingChannel.PlayOneShot(reloadingSoundAR);
                break;
        }
    }
}
