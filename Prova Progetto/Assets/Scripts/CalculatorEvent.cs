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
    private TMP_Text valueText;
    private List<int> num = new List<int>();
    private string operation = ""; 
    private float total = 0f;
    private Camera camera;
    public LayerMask mask;
    private GameObject calculator;
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 100f;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        Debug.DrawRay(transform.position, mousePosition - transform.position, Color.blue);
        if (Input.GetMouseButtonUp(0))
        {
            if (valueText == null)
            {
                valueText = GameObject.Find("calculatorText").transform.GetComponent<TMP_Text>();
                Debug.Log(valueText.text);
            }        
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray,out hit))
            {
                if (valueText.text == "0")
                {
                    valueText.text = hit.transform.name;
                }
                else
                {
                    if( hit.transform.name != "X" && 
                        hit.transform.name != "/" && 
                        hit.transform.name != "-" && 
                        hit.transform.name != "+" && 
                        hit.transform.name != "=")
                    {
                        valueText.text += hit.transform.name;
                    }
                }

                if (hit.transform.name == "AC")
                {
                    valueText.text = "0";
                    total = 0;
                    num.Clear();
                }
                else if (hit.transform.name == "C")
                {
                    Debug.Log("C");
                }
                else if (hit.transform.name == "=")
                {
                    num.Add(int.Parse(valueText.text));
                    operate();
                }
                else if (hit.transform.name == "+")
                {
                    num.Add(int.Parse(valueText.text));
                    valueText.text = "0";
                    operation = "+";
                }
                else if (hit.transform.name == "-")
                {
                    num.Add(int.Parse(valueText.text));
                    valueText.text = "0";
                    operation = "-";
                }
                else if (hit.transform.name == "/")
                {
                    num.Add(int.Parse(valueText.text));
                    valueText.text = "0";
                    operation = "/";
                }
                else if (hit.transform.name == "X")
                {
                    num.Add(int.Parse(valueText.text));
                    valueText.text = "0";
                    operation = "X";
                }
                
            }
        }
    }

    private void operate()
    {
        switch (operation)
        {
            case "+":
                for(int i=0; i<num.Count;++i)
                {
                    total += num[i];
                    Debug.Log(num[i]);
                }
                valueText.text = total.ToString();
                break;
            case "-":
                for(int i=num.Count;i >= 0;--i)
                {
                    total -= num[i];
                    Debug.Log(num[i]);
                }
                valueText.text = total.ToString();
                break;
            case "/":
                if (num[1] >= num[2])
                {
                    total = num[1] / num[2];
                    valueText.text = total.ToString();
                }
                break;
            case "X":
                total = 1;
                for(int i=1; i<num.Count;++i)
                {
                    total *= num[i];
                    Debug.Log(num[i]);
                }
                valueText.text = total.ToString();
                break;
            default:
                break;
        }
        total = 0;
        num.Clear();
    }
}
