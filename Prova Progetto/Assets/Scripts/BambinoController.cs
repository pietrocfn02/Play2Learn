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


    public void UpdateDiavoletto(int i) {
        diavoletto_score+=i;
        Messenger.Broadcast(GameEvent.DIAVOLETTO_UPDATE);

        Debug.Log(diavoletto_score);
    }


    public void UpdateAngioletto(int i) {
        angioletto_score+=i;
        Messenger.Broadcast(GameEvent.ANGIOLETTO_UPDATE);

        Debug.Log(angioletto_score);
    }


    public int getAngiolettoScore() {
        return angioletto_score;
    }

    public int getDiavolettoScore() {
        return diavoletto_score;
    }

}
