using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCultura : MonoBehaviour
{
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
        Debug.Log("CULTURA: "+updates);
        Vector3 bimboPosition = bimbo.transform.position;
        if (updates % 100 > 80 && updates % 100 < 99){
            GameObject x = Instantiate(prefab) as GameObject;
            Vector3 spawnPosition = new Vector3(Random.Range(bimboPosition.x-1,bimboPosition.x+1), 1.7f, Random.Range(bimboPosition.z-1,bimboPosition.z+1));
            x.transform.position = spawnPosition;
            Debug.Log("SPAWN AT :"+ spawnPosition.x + ' '+ spawnPosition.z);
        }
        updates++;    
                
        
    }

}
