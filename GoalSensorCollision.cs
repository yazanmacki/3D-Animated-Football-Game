using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSensorCollision : MonoBehaviour
{

    int currentTurn;
    Camera currentCamera;

    public bool glovePlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTurn = GameObject.Find("Arrow").GetComponent<ArrowController>().turn; 
        if (currentTurn % 2 == 0) {
            currentCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        } else {
            currentCamera = GameObject.Find("Goalie Camera").GetComponent<Camera>();
        }

        if (GameObject.Find("Arrow").GetComponent<ArrowController>().turn % 2 == 1) {
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) && (GameObject.Find("Canvas/SaveButton").GetComponent<SaveButtonController>().saveButtonPressed == false))
            {
                Ray raycast = currentCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                { 

                    if (raycastHit.collider.name == "GoalSensor")
                    {
                        Debug.Log("Goal clicked");

                        Debug.Log("Tap Position = " + raycastHit.point);
                        GameObject.Find("Gloves").GetComponent<MeshRenderer>().enabled = true;
                        GameObject.Find("Gloves").GetComponent<Transform>().position = raycastHit.point; 
                        glovePlaced = true;
                    } 
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sphere") {
            //Debug.Log("GOAL");
            GameObject.Find("Arrow").GetComponent<ArrowController>().goal = true;
        }
    }

}
