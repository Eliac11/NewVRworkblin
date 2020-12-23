using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingGift : MonoBehaviour
{


    public GameObject[] insides;
    public ParticleSystem openEffect;


    private int numchoice;
    private void OnCollisionEnter(Collision over)
    {
        if (over.gameObject.tag == "opener")
            {
                numchoice = Random.Range(0, insides.Length);
                Destroy(gameObject);
                Instantiate(insides[numchoice], transform.position, Quaternion.identity);
                Instantiate(openEffect, transform.position, Quaternion.identity);
        }
        
    }
}
