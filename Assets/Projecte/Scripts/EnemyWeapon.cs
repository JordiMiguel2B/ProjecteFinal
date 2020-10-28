using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    public float ammo;
    public float maxAmmo = 10;
    public float reloadTime = 3;
    public bool reloading;
    public bool reloaded;



    public GameObject bullet;

    private EnemyBehaviour enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();

        ammo = maxAmmo;
    }

    private void Update()
    {
        if (ammo <= 0)
        {
            Reload();
        }
    }

    public void Shoot()
    {

        if (ammo <= 0)
        {
            Reload();
        }

        if (reloading)
        {
            return;
        }

        if (!reloading)
        {
            ammo--;
            GameObject bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObject.transform.forward = enemy.transform.forward;

        }

    }

    public void Reload()
    {
        if (reloading || ammo == maxAmmo)
        {
            return;
        }

        reloading = true;
        StartCoroutine(WaitForReload());
    }


    IEnumerator WaitForReload()
    {

        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        reloading = false;
        reloaded = true;

    }


}
