using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoController : MonoBehaviour {

    private int diavoletto_score = 0;
    private int angioletto_score = 0;


    private CharacterController _charController;

    void Start()
    {
    }

    void Update()
    {

    }


    void UpdateDiavoletto(int i) {
        diavoletto_score+=i;
        Debug.Log(diavoletto_score);
    }


    void UpdateAngioletto(int i) {
        angioletto_score+=i;
        Debug.Log(angioletto_score);
    }
}
