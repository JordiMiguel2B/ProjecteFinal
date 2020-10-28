using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeapon : MonoBehaviour
{

    public int currentWeapon = 0;
    //public HUD hud;
    private Arma arma;
    public Metralleta metralleta;

    public bool hasMetralleta;


    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
        arma = GameObject.FindGameObjectWithTag("Arma").GetComponent<Arma>();
        //metralleta = GameObject.FindGameObjectWithTag("Metralleta").GetComponent<Metralleta>();
        hasMetralleta = false;

    }

    // Update is called once per frame
    public void Change()
    {

        if (hasMetralleta == true)
        {
            int previousSelectedWeapon = currentWeapon;

            if (currentWeapon >= transform.childCount - 1 && arma.reloading == false && metralleta.reloading == false)
            {
                currentWeapon = 0;
            }

            else
            {
                if (arma.reloading == false && metralleta.reloading == false)
                {
                    currentWeapon++;

                }
            }

            if (previousSelectedWeapon != currentWeapon && arma.reloading == false && metralleta.reloading == false)
            {
                SelectWeapon();
            }
        }

        else
        {
            return;
        }


    }

    public void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    public int ReturnCurrent()
    {
        return (currentWeapon);
    }
}
