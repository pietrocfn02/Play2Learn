using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Coroutines...
    //
    IEnumerator TutorialMovement()
    {
        isActive[0] = true;
        Debug.Log("Benvenuto nel tutorial...");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Iniziamo a vedere come muoversi.");
        yield return new WaitForSeconds(2);
        Debug.Log("Per prima cosa usa i comandi ( W,A,S,D, [SPACE] ) per muoverti e saltare.");
        yield return new WaitForSeconds(5);
        Debug.Log("Vai verso l'oggetto illuminato e collezionalo premendo E.");
        yield return new WaitForSeconds(2);
        
    }
    //Vado verso l'oggetto illuminato, entra nel trigger e avvio la spiegazione dei collezionabili
    /*IEnumerator TutorialCollectibles()
    {

    }*/

    // Variables...
    //
    
    // Indica quale missione è attiva, in modo che il player può svolgere solo una missione per volta
    private bool[] isActive = {false, false, false, false, false, false, false, false, false, false, false, false, false, false};
    // IT-1, IT2
  
}
