using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingAI : MonoBehaviour
{
    public float speed = 0.2f;
    public float obstacleRange = 5.0f;
    private bool _alive;

    [SerializeField] private GameObject bimbo;
    void Start()
    {
        _alive=true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
        
        if(_alive && distance < 1)
        {
            transform.Translate(0, 0, speed*Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
           
        }
    }
    public void SetAlive(bool alive){
        _alive = alive;
    }
}
