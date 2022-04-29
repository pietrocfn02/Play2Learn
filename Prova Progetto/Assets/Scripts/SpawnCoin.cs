using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    // Evil Coin
    [SerializeField] private GameObject evilCoinPrefab;
    private GameObject[] _evilCoins;
    public int evilCount;

    // Good Coin
    //[SerializeField] private GameObject coinPrefab;
    //private GameObject[] _coins;
    //public int coinsCount;
    
    void Start()
    {
        _evilCoins = new GameObject[evilCount];
    }

    // Update is called once per frame
    async void Update()
    {
        for(int i=0; i<_evilCoins.Length; i++)
        {
            if(_evilCoins[i] == null)
            {
                _evilCoins[i] = Instantiate(evilCoinPrefab) as GameObject;
                _evilCoins[i].transform.position = new Vector3(Random.Range(1f,5f), 1f, Random.Range(1f,5f));
                float angle = Random.Range (0, 360f);
                _evilCoins[i].transform.Rotate(0, angle, 0);
            }
        }        
    }

}
