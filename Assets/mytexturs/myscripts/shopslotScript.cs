using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopslotScript : MonoBehaviour
{

    private bool insideShop = false;
    private bool Inplase = false;
    public Collider objShop;


    public Transform shoppoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objShop != null)
        {
            objShop.transform.SetParent(this.transform);
            objShop.transform.localPosition = shoppoint.localPosition;
            objShop.transform.localRotation = shoppoint.localRotation;
            objShop.attachedRigidbody.isKinematic = true;
        }
        

        if (Input.GetKeyDown("i"))
        {
            unhookShop();
        }

        
    }

    void unhookShop()
    {
        if (objShop != null)
        {
            objShop.attachedRigidbody.isKinematic = false;
            objShop.GetComponent<Rigidbody>().AddForce(objShop.transform.up * -2, ForceMode.Impulse);
            objShop.transform.SetParent(null);
            objShop = null;

            //objShop.GetComponent<myhgh>().enabled = true;
            //objShop.GetComponent<Valve.VR.InteractionSystem.Interactable>().enabled = true;
        }
        
    }

    public void OnTriggerEnter(Collider over)
    {
        if (objShop == null)
        {


            if (over.CompareTag("endShop"))
            {
                
                insideShop = true;
                objShop = over;

                objShop.transform.SetParent(this.transform);
                objShop.attachedRigidbody.isKinematic = true;
                objShop.transform.localPosition = shoppoint.localPosition;
                objShop.transform.localRotation = shoppoint.localRotation;
                Inplase = true;

                //objShop.GetComponent<myhgh>().enabled = false;
                //objShop.GetComponent<Valve.VR.InteractionSystem.Interactable>().enabled = false;
                
            }
        }
    }
}
