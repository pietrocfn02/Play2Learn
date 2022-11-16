using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOneTypeCoin : MonoBehaviour
{
    // Coin Prefab
    [SerializeField] private GameObject coinPrefab;
    private GameObject[] _coins;
    public int coinCount;
    
    void Start()
    {
        _coins = new GameObject[coinCount];
    }

    // Update is called once per frame
    async void Update()
    {
        for(int i=0; i<_coins.Length; i++)
        {
            if(_coins[i] == null)
            {
                _coins[i] = Instantiate(coinPrefab) as GameObject;
                _coins[i].transform.position = new Vector3(Random.Range(20f,28f), 1.80f, Random.Range(12f,20f));
                _coins[i].transform.localScale += new Vector3(3f,3f,1f);
                float angle = Random.Range (0, 360f);
                _coins[i].transform.Rotate(0, angle, 0);
            }
        }        
    }
}
