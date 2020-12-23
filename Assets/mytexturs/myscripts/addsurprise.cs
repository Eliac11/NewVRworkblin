using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addsurprise : MonoBehaviour
{
    public int kolStartSurprice;
    public GameObject[] forSpawn;

    public float timespawn;
    public int spawnregion;

    private int numchoice;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < kolStartSurprice; i++)
        {
            

            Vector3 startpos = new Vector3(transform.position.x, transform.position.y+i*2-10, transform.position.z);
            spawnSurprice(startpos);
        }
    }


    
    private float newtime = 0;
    // Update is called once per frame
    void Update()
    {
        newtime += Time.deltaTime;
        if (newtime >= timespawn)
        {
            Vector3 randpos = new Vector3(transform.position.x + Random.Range(-spawnregion, spawnregion), transform.position.y, transform.position.z + Random.Range(-spawnregion, spawnregion));
            spawnSurprice(randpos);

            newtime = 0;
        }
    }

    public void spawnSurprice(Vector3 spawpos)
    {
            numchoice = Random.Range(0, forSpawn.Length);
            Instantiate(forSpawn[numchoice], spawpos, Quaternion.identity);
    }
}
