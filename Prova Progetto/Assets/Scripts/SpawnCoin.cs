using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    //TODO Controllare se si puo cancellare
    // Perche mi sa che non ci serve piu

    
    //Spawn di oggetti "coin" da prefab passato tramite inspector
    //A caso nella mappa

    // Evil Coin
    [SerializeField] private GameObject evilCoinPrefab;
    private GameObject[] _evilCoins;
    public int evilCount;

    // Good Coin
    [SerializeField] private GameObject goodCoinPrefab;
    private GameObject[] _goodCoins;
    public int goodCount;
    
    void Start()
    {
        _evilCoins = new GameObject[evilCount];
        _goodCoins = new GameObject[goodCount];
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
        for(int i=0; i<_goodCoins.Length; i++)
        {
            if(_goodCoins[i] == null)
            {
                _goodCoins[i] = Instantiate(goodCoinPrefab) as GameObject;
                _goodCoins[i].transform.position = new Vector3(Random.Range(-20f,20f), Random.Range(-1.4f, -0.2f), Random.Range(-20f,20f));
                _goodCoins[i].transform.localScale += new Vector3(8f,8f,1f);
                float angle = Random.Range (0, 360f);
                _goodCoins[i].transform.Rotate(0, angle, 0);
            }
        }     
    }

}
