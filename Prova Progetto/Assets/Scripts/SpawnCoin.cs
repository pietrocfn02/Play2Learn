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
                _evilCoins[i].transform.position = new Vector3(Random.Range(-20f,20f), Random.Range(-1.4f, -0.2f), Random.Range(-20f,20f));
                _evilCoins[i].transform.localScale += new Vector3(8f,8f,1f);
                float angle = Random.Range (0, 360f);
                _evilCoins[i].transform.Rotate(0, angle, 0);
            }
        }        
    }

}
