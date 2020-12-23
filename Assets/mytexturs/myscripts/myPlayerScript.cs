using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPlayerScript : MonoBehaviour
{


    public float lives = 100;



    public delegate void PDeaf();
    public static event PDeaf playerdied;
    
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }


    void saydied()
    {
        playerdied?.Invoke();
    }


    private void OnCollisionEnter(Collision over)
    {
        if (over.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 10 && lives > 0)
        {
            if (over.gameObject.CompareTag("Bullet"))
            {
                float damage = over.gameObject.GetComponent<BulletScript>().damage * over.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                lives -= damage;
            }
            else
            {
                float damage = 3 * over.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                lives -= damage;
            }
            if (lives <= 0)
            {
                gameObject.GetComponent<Animator>().enabled = true;
            }

        }

    }
}
