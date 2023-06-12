using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Firebase.Database;
using TMPro;
using Cinemachine;

public class ProfSettings : MonoBehaviour
{
    
    private bool interact = false;
    private GameObject player;
    private string tagInteraction = "";
    private Collider objectToDestroy;
    private int contEinteraction = 0;
    [SerializeField] private CinemachineVirtualCamera[] artCameras;
    [SerializeField] private CinemachineFreeLook mainCamera;
    private int cameraSwitch = 0;
    private bool editing = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interact)
        {
            if (Input.GetKeyUp(KeyCode.E) && !editing)
            {
                editing = true;
                RelativeMovement.SetInMission(true);
            }
            else if (Input.GetKeyUp(KeyCode.E) && editing)
            {
                editing = false;
                RelativeMovement.SetInMission(false);
            }

            if (tagInteraction == GameEvent.EASEL_TAG && editing)
            {
                
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    if (cameraSwitch < artCameras.Length)
                    {
                        artCameras[cameraSwitch].gameObject.SetActive(true);
                        if (cameraSwitch > 0)
                        {
                            artCameras[cameraSwitch-1].gameObject.SetActive(false);
                        }
                        else
                        {
                            artCameras[artCameras.Length-1].gameObject.SetActive(false);
                        }
                    }
                    if (cameraSwitch > artCameras.Length-1)
                    {
                        
                        artCameras[0].gameObject.SetActive(true);
                        cameraSwitch = 0;
                    }
                    ++cameraSwitch;
                }
            }
            else if (tagInteraction == GameEvent.EASEL_TAG && !editing)
            {
                contEinteraction = 0;
                for(int i=0; i<artCameras.Length; ++i)
                {
                    artCameras[i].gameObject.SetActive(false);    
                }
                mainCamera.gameObject.SetActive(true);
            }
        }
    }

    public void ActivateE(Collider objectRecived)
    {
        this.interact = true;
        this.tagInteraction = objectRecived.tag;
        this.objectToDestroy = objectRecived;
        SetOutline(objectRecived.gameObject, 4, Color.yellow);
    }

    public void DeactivateE(Collider objectRecived)
    {

        this.interact = false;
        this.tagInteraction = "";
        SetOutline(objectRecived.gameObject, 0, Color.yellow);
    }
    public void SetOutline(GameObject objectRecived, float power, Color color)
    {
        Outline outline = objectRecived.GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineColor = color;
            outline.OutlineWidth = power;
        }
    }
}
