using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnerScript : MonoBehaviour
{

    [SerializeField] GameObject coinPrefab;
    [SerializeField] float timeToFirstSpawn = 5.3f;

    void Start()
    {
        InvokeRepeating("SpawnCoins", timeToFirstSpawn, 5.3f);
    }
    
    private Vector2 RandomPosition()
    {
        float Y = Random.Range(-2.5f,2.5f);

        return new Vector2(transform.position.x,Y);
    }

    private void SpawnCoins()
    {
        if(CoinScript.nextCoinCanSpawn)
        {
            Instantiate(coinPrefab, RandomPosition(), Quaternion.identity);
            CoinScript.nextCoinCanSpawn = false;
        }
    }
}
