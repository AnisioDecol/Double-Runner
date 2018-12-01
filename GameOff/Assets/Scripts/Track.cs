using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : MonoBehaviour {

    public GameObject[] obstacles;
    public GameObject[] coins;
    public Vector2 numberofObstacles;
    public Vector2 numberofCoins;

    public int minPos = 0;
    public int maxPos;

    public List<GameObject> newObstacle;
    public List<GameObject> newCoin;

    // Use this for initialization
    void Start () {

        int newNumberofObstacles = (int)Random.Range(numberofObstacles.x, numberofObstacles.y);

        for (int i = 0; i < newNumberofObstacles; i++)
        {
            newObstacle.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacle[i].SetActive(false);
        }

        PositionateObstacles();

        int newNumberofCoins = (int)Random.Range(numberofCoins.x, numberofCoins.y);

        for (int i = 0; i < newNumberofCoins; i++)
        {
            newCoin.Add(Instantiate(coins[Random.Range(0, coins.Length)], transform));
            newCoin[i].SetActive(false);
        }

        PositionateCoins();
    }


    void PositionateObstacles()
    {
        for (int i = 0; i < newObstacle.Count; i++)
        {
            float posZMin = (374f / newObstacle.Count) + (374f / newObstacle.Count) * i;
            float posZMax = (374f / newObstacle.Count) + (374f / newObstacle.Count) * i + 1 ;
            newObstacle[i].transform.localPosition = new Vector3(Random.Range(minPos, maxPos = Random.Range(-1, 3)), -3.12f, Random.Range(posZMin, posZMax));
            newObstacle[i].SetActive(true);
        }
    }

    void PositionateCoins()
    {
        for (int i = 0; i < newCoin.Count; i++)
        {
            float posZMin = (374f / newCoin.Count) + (374f / newCoin.Count) * i;
            float posZMax = (374f / newCoin.Count) + (374f / newCoin.Count) * i + 1;
            newCoin[i].transform.localPosition = new Vector3(Random.Range(minPos, maxPos = Random.Range(-1, 3)), -2.72f, Random.Range(posZMin, posZMax));
            newCoin[i].SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector3(0, 0, transform.position.z + 374 * 2);
            PositionateObstacles();
        };
    }
}
