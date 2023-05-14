using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private static Vector3 startPosition;
    private static Vector3 finalPosition;
    private LineRenderer line;
    private static bool move;
    void Start()
    {
        player = GameObject.FindWithTag(GameEvent.PLAYER_TAG);
        line = player.GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move){
            line.SetPosition(0,startPosition);
            line.SetPosition(1,player.transform.position);
        }
        else
        {
            line.SetPosition(0,startPosition);
            line.SetPosition(1,finalPosition);
            Destroy(this);
        }
    }
    public static void SetStartPosition(Vector3 position)
    {
        startPosition = position;
    }
    public static void SetFinalPosition(Vector3 position)
    {
        finalPosition = position;
    }
    public static void SetTapeMove(bool state)
    {
        move = state;
    }
}
