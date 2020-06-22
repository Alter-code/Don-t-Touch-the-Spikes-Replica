using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public static bool nextCoinCanSpawn = true;

    private AudioManager audioManager;

    [SerializeField] private GameObject picupParticle;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Instantiate(picupParticle, transform.position, Quaternion.identity);
            audioManager.Play("CoinPickup");
            nextCoinCanSpawn = true;
            PlayerMovementScript.scoreAmount += 10;
            Destroy(gameObject);
        }
    }

}
