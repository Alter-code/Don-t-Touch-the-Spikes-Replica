using System.Collections;
using UnityEngine;

public class SpawnSpicesScrypt : MonoBehaviour
{
   
    [SerializeField] private Transform[] spikesPrefabs;

    [SerializeField] float newXPosition;

    private int setPlaces = 0; 
    private int hardLevel = 2; 

    private bool spawnNew = false;
    private bool wasSpawn = false;

    void Update()
    { 
        if(spawnNew)
        {
            StartCoroutine(coolDown());
        }
    }

    IEnumerator coolDown ()
    {
        repeatSpawn:
        wasSpawn = false;
        for (int i = 0; i < spikesPrefabs.Length; i++)
        {
            if (i <= spikesPrefabs.Length / hardLevel)
            {
                setPlaces = Random.Range(0, spikesPrefabs.Length);
            }
            if (i == setPlaces)
            {
                spikesPrefabs[i].position = new Vector3(newXPosition, spikesPrefabs[i].position.y, spikesPrefabs[i].position.z);
                wasSpawn = true;
            }
        }
        if(!wasSpawn)
        {
            goto repeatSpawn;
        }
        spawnNew = false;
        yield return new WaitForSeconds(5.3f);
        for (int i = 0; i < spikesPrefabs.Length; i++)
        {
            spikesPrefabs[i].position = new Vector3(10f, spikesPrefabs[i].position.y, spikesPrefabs[i].position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            spawnNew = true;
        }
    }

}
