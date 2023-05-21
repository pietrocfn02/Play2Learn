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
    [SerializeField] private Camera camera;
    private LayerMask mask;
    private GameObject calculator;
    void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 100f;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        Debug.DrawRay(transform.position, mousePosition - transform.position, Color.blue);

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(mousePosition);
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit, 100, mask))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}
