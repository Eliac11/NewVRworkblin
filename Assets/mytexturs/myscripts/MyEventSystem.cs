using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEventSystem : MonoBehaviour
{
    

    public float Playerdamagedone = 0;
    public GameObject mailpref;
    public GameObject punished;


    public Transform[] spawnpoints;
    public GameObject Evilsnowmanpref;


    public GameObject gulagvideo;
    void Start()
    {
        SnowManScript.takeDamage += addDamag;

        myPlayerScript.playerdied += onGulag;
    }



    private int kolsendmail = 0;
    private int kolEvilSnowM = 0;

    void addDamag(float damage)
    {
        Playerdamagedone += damage;


        if (Playerdamagedone > 1000f && kolsendmail == 0)
        {
            sendMail("правитель этого мира\nне потерпит насилия\nу тебя последний шанс прекратить");
            kolsendmail += 1;
        }

        if (Playerdamagedone > 2000f)
        {
            if (kolsendmail == 1)
            {
                sendMail("помни ты был предупрежден");
                kolsendmail += 1;
            }
            if (kolEvilSnowM < 5)
            {
                int numchoice = Random.Range(0, spawnpoints.Length);
                Transform sp = spawnpoints[numchoice];

                GameObject SM = Instantiate(Evilsnowmanpref,sp.position,sp.rotation);
                SM.GetComponent<SnowManScript>().Target = punished;


                kolEvilSnowM += 1;
            }

        }




    }

    void onGulag()
    {
        gulagvideo.SetActive(true);
    }

    void sendMail(string textmail)
    {
        GameObject mail = Instantiate(mailpref,punished.transform.position+new Vector3(0,10,0), punished.transform.rotation);
        mail.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshPro>().text = textmail;
    }


}
