using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (endGamePanel.activeSelf)
        {
            endGamePanel.SetActive(false);
        }
        CoinScript.nextCoinCanSpawn = true;
    }
    public void EndGame()
    {
        endGamePanel.SetActive(true);
    }
}
