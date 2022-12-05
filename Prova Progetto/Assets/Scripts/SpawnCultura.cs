using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCultura : MonoBehaviour
{

    // Durante l'inseguimento, ogni tanto, giusto per non farci mancare nulla
    // Faccio spawnare tonnellate di libri intorno al personaggio

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject bimbo;

    private bool inseguimento = true;
    private int updates = 0;
    void Start()
    {
      
    }

    // Update is called once per frame
    async void Update()
    {
        Vector3 bimboPosition = bimbo.transform.position;
        if (updates % 1000 == 927){
            GameObject x = Instantiate(prefab) as GameObject;
            Vector3 spawnPosition = new Vector3(Random.Range(bimboPosition.x-1,bimboPosition.x+1), 1.5f, Random.Range(bimboPosition.z-1,bimboPosition.z+1));
            x.transform.position = spawnPosition;
        }
        updates++;    
                
        
    }

}
