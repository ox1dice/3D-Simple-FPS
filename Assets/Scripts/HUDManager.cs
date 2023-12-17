using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    public Sprite emptySlot;

    private Dictionary<Weapon.WeaponModel, Sprite> weaponSprites = new Dictionary<Weapon.WeaponModel, Sprite>();
    private Dictionary<Weapon.WeaponModel, Sprite> ammoSprites = new Dictionary<Weapon.WeaponModel, Sprite>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{activeWeapon.magazineSize / activeWeapon.bulletsPerBurst}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);

            activeWeaponUI.sprite = GetWeaponSprite(model);

            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }
        }
        else
        {
            string magazineAmmoText = "";
            string totalAmmoText = "";

            ammoTypeUI.sprite = emptySlot;

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        if (weaponSprites.ContainsKey(model))
        {
            return weaponSprites[model];
        }

        Sprite sprite = null;
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                sprite = Instantiate(Resources.Load<GameObject>("Pistol_Weapon")).GetComponent<SpriteRenderer>().sprite;
                break;

            case Weapon.WeaponModel.AssaultRifle:
                sprite = Instantiate(Resources.Load<GameObject>("AssaultRifle_Weapon")).GetComponent<SpriteRenderer>().sprite;
                break;
        }

        weaponSprites[model] = sprite;
        return sprite;
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        if (ammoSprites.ContainsKey(model))
        {
            return ammoSprites[model];
        }

        Sprite sprite = null;
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                sprite = Instantiate(Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;
                break;

            case Weapon.WeaponModel.AssaultRifle:
                sprite = Instantiate(Resources.Load<GameObject>("AssaultRifle_Ammo")).GetComponent<SpriteRenderer>().sprite;
                break;
        }

        ammoSprites[model] = sprite;
        return sprite;
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponslots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }

        return null;
    }
}
