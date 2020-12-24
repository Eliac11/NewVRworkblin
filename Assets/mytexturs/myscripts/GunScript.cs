using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunScript : MonoBehaviour
{

    public GameObject shopslot;
    public GameObject bolt;

    public GameObject bulletspawn;
    public GameObject bulletpref;

    public GameObject Shellpref;
    public GameObject Shellspawn;


    public ParticleSystem fireEfect;

    public float firespeed;
    public float startspeed;

    public bool inhand = false;

    private int chargelevels = 0;
    private bool ischarge = false;


    public void inhandSet(bool i)
    {
        inhand = i;
    }



    // Start is called before the first frame update
    void Start()
    {
        bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        boltmove();
        if (SteamVR_Actions._default.Fire.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (inhand)
            {
                if (ischarge)
                {
                    Fire();
                    ischarge = false;
                }
            }
        }
    }




    void boltmove()
    {
        if (chargelevels != 2)
        {
            if (chargelevels == 0 & bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value == 1)
            {
                chargelevels = 1;
            }
            if (chargelevels == 1 & bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value == 0)
            {
                chargelevels = 2;
            }
        }
        else
        {
            if (shopslot.GetComponent<shopslotScript>().objShop != null)
            {
                if (shopslot.GetComponent<shopslotScript>().objShop.GetComponent<ShopScript>().numberBullet > 0)
                {
                    ischarge = true;
                    shopslot.GetComponent<shopslotScript>().objShop.GetComponent<ShopScript>().numberBullet -= 1;
                    outShell();
                }
            }
            chargelevels = 0;
        }

        if (bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value > 0)
                {
                    bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value -= firespeed * Time.deltaTime;
                    if (bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value < 0)
                    {
                        bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value = 0;
                    }
                }
    }

    void Fire()
    {
        bolt.GetComponent<Valve.VR.InteractionSystem.LinearMapping>().value = 1;
        fireEfect.Play();

        GameObject bullsp =  Instantiate(bulletpref, bulletspawn.transform.position, bulletspawn.transform.rotation);
        bullsp.GetComponent<Rigidbody>().AddForce(bullsp.transform.up * startspeed, ForceMode.Impulse);
        bullsp.GetComponent<BulletScript>().myholder = "Player";

        gameObject.GetComponent<Rigidbody>().AddTorque(gameObject.transform.forward * 2);
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * -0.1f , ForceMode.Impulse);
    }
    void outShell()
    {
        GameObject shellsp = Instantiate(Shellpref, Shellspawn.transform.position, Shellspawn.transform.rotation);
        shellsp.GetComponent<Rigidbody>().AddForce(shellsp.transform.up * 2, ForceMode.Impulse);

        shellsp.GetComponent<Rigidbody>().AddTorque(shellsp.transform.up * 2);
    }
}
