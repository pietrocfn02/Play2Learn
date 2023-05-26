using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class CalculatorEvent : MonoBehaviour
{
    //[SerializeField] private GameObject calculator;
    [SerializeField] private TMP_Text valueText;
    private Camera camera;
    public LayerMask mask;
    private GameObject calculator;
    
    void Start()
    {
        camera = Camera.main;
        Debug.Log(camera.name);
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 100f;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        Debug.DrawRay(transform.position, mousePosition - transform.position, Color.blue);
        
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 100, mask))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit something");
            }
        }
    }
}
