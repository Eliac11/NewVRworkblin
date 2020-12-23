using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManScript : MonoBehaviour
{

    public float lives;
    public GameObject Target;

    public bool isEvil;

    public GameObject sspoint;
    public GameObject psnow;
    public float ballspeed;
    public float firespeed;

    private float chargelevels = 0;

    private UnityEngine.AI.NavMeshAgent MeshAgent;

    public delegate void TakeDamage(float damage);
    public static event TakeDamage takeDamage;
    void Start()
    {
        MeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //MeshAgent.updateRotation = false;
    }

    void Update()
    {
        if (lives <= 0f)
        {
            died();
        }
        else
        {
            MeshAgent.SetDestination(Target.transform.position);
            //gameObject.transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));

            if (isEvil)
            {
                FireLogic();
            }
        }

        
    }


    private void OnCollisionEnter(Collision over)
    {
        if (over.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 10 && lives > 0 && over.gameObject.GetComponent<BulletScript>().myholder != "Showman")
        {
            if (over.gameObject.CompareTag("Bullet"))
            {
                float damage = over.gameObject.GetComponent<BulletScript>().damage * over.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                lives -= damage;
                takeDamage?.Invoke(damage);
            }
            else
            {
                float damage = 3 * over.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                lives -= damage;
                takeDamage?.Invoke(damage);
            }
            
        }

    }


   
    void FireLogic()
    {
        chargelevels += firespeed * Time.deltaTime;
        if (chargelevels >= 1)
        {
            if (MeshAgent.remainingDistance <= MeshAgent.stoppingDistance)
            {
                GameObject bullsp = Instantiate(psnow, sspoint.transform.position, sspoint.transform.rotation);
                bullsp.GetComponent<Rigidbody>().AddForce((bullsp.transform.position - Target.transform.position - new Vector3(0, Mathf.Sqrt(MeshAgent.remainingDistance/2), 0)).normalized * -ballspeed, ForceMode.Impulse);
                bullsp.GetComponent<BulletScript>().myholder = "Snowman";

                chargelevels = 0;
            }
        }
    }

    void died()
    {

        Destroy(sspoint);

        for (int i = 0;i != transform.childCount;i++)
        { 
            transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
        }
        
        gameObject.transform.DetachChildren();
        Destroy(gameObject);

    }

}
