using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGoalkeeper : MonoBehaviour
{

    Animator a;
    bool madeDiveDecision = false;

    Vector3 shotPos;

    string xPos;
    string yPos;

    float randVal;

    public bool gotRand = false;
    
    // Start is called before the first frame update
    void Start()
    { 
         
    }

    void dive2() {
        Animator animator;
        animator = GetComponent<Animator>();   
        animator.SetInteger("dive", 2);
    }

    void dive4() {
        Animator animator;
        animator = GetComponent<Animator>();   
        animator.SetInteger("dive", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
        //GameObject.Find("Arrow").GetComponent<MeshRenderer>().enabled = false;
        

        a = GetComponent<Animator>();

        if (a.GetBool("restart") == true) {
            transform.position = new Vector3(0.12007f, 0.14f, 4.61f);
        }



        float deg = GameObject.Find("Arrow").GetComponent<ArrowController>().rY;
        var rad = deg * Mathf.PI / 180;
            
        float goalieZ = GameObject.Find("Goalkeeper").GetComponent<Transform>().position.z;
        float ballZ = GameObject.Find("Sphere").GetComponent<Transform>().position.z;

        float deltaZ = goalieZ - ballZ;

        float xFinal = deltaZ * Mathf.Tan(rad);

        Debug.Log("Deg Y = " + GameObject.Find("Arrow").GetComponent<ArrowController>().rX);
        Debug.Log("POWER = " + GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier); 

        if (GameObject.Find("Player").GetComponent<MovePlayer>().playerAboutToKick) {            

            Animator animator;
            animator = GetComponent<Animator>();
            //animator.SetBool("stepRight", false);

            madeDiveDecision = false;

            float positionX = transform.position.x; 
            
            bool topBins = GameObject.Find("Arrow").GetComponent<ArrowController>().topBins;

          
            if (!(GameObject.Find("Arrow").GetComponent<ArrowController>().rX < -48f && GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier > 0.7f && Mathf.Abs(xFinal) < 2.9f) ) {
                if (positionX < xFinal) {
                    animator.SetInteger("move", 1);
                    positionX += 0.035f;
                    animator.SetBool("stepRight", true);
                }
                if (positionX > xFinal) {
                    animator.SetInteger("move", -1);
                    positionX -= 0.035f;
                }
            } else {
                if (positionX < xFinal) {
                    animator.SetInteger("move", 1); 
                    positionX += 0.035f;
                }
                if (positionX > xFinal) {
                    animator.SetInteger("move", -1); 
                    positionX -= 0.035f;
                }
            }
        

            transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        }

        Debug.Log(GameObject.Find("Sphere").GetComponent<Transform>().position.z);

        if (GameObject.Find("Arrow").GetComponent<ArrowController>().shot) {
 
            Animator animator;
            animator = GetComponent<Animator>();           
                                
            shotPos = GameObject.Find("GhostBall").GetComponent<GhostBallController>().shotCoordinates;

            if (!gotRand) {
                randVal = Random.Range(0.0f, 1.0f);
                gotRand = true;
            }

            Debug.Log("RAND VAL = " + randVal);

            if (GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier > 0.3f) {
                if (randVal <= 0.7f) {
                   shotPos.x = Random.Range(-2.2f, 2.2f);
                   shotPos.y = Random.Range(0.0f, 2.0f);
                }
            } 

            //shotPos.x = Random.Range(shotPos.x - 2.5f, shotPos.x + 2.5f);
            //shotPos.y = Random.Range(shotPos.y - 2.5f, shotPos.y + 2.5f);

            if (shotPos.x > 1.3f) {
                xPos = "right";
            } else if (shotPos.x < -0.3f) {
                xPos = "left";
            } else {
                xPos = "center";
            } 

            if (shotPos.y < 0.85f) {
                yPos = "bottom";
            } else if (shotPos.y > 1.28f) {
                yPos = "top";
            } else {
                yPos = "middle";
            }
 
            
            if (xPos == "right" && yPos == "bottom") {
                animator.SetInteger("dive", 1);
            }

            if (xPos == "center" && yPos == "bottom") {
                if (shotPos.x > 0.5f) {
                    animator.speed = 0.8f;
                    animator.SetInteger("dive", 1);
                }
            }

            if (xPos == "right" && yPos == "middle") {
                /* if (shotPos.x < 1.0f) {
                    animator.speed = 0.65f;
                } else if (shotPos.x < 1.5f) {
                    animator.speed = 0.82f;                        
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.2f ;
                } else {
                    animator.speed = 1.0f;
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.2f ;
                }
                animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.6f ; */
                if (shotPos.x < 1.0f) {
                    animator.speed = 0.8f;
                    Invoke("dive2", (1-GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier)*1.5f);
                } else if (shotPos.x < 1.5f) {
                    animator.speed = 0.8f;
                    Invoke("dive2", (1-GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier)*1.6f);
                } else {
                    animator.speed = 0.8f;
                    Invoke("dive2", (1-GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier)*0.9f);
                }
                
                
            }
            
            if (xPos == "right" && yPos == "top") { 
                /* if (shotPos.x < 1.45f) {
                    Debug.Log("gg 1");
                    animator.speed = 0.7f;
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.35f ;
                    //animator.speed = animator.speed * (Mathf.Abs(GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier/0.86f));
                } else if (shotPos.x < 1.75f) {
                    Debug.Log("gg 2");
                    animator.speed = 0.82f;                        
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.35f ;
                } else if (shotPos.x < 2f) {
                    Debug.Log("gg 3");
                    animator.speed = 1.0f;                        
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.75f ;
                    //animator.speed = animator.speed - (Mathf.Abs(GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier/0.86f));
                } else {
                    animator.speed = 1.0f;
                    animator.speed = animator.speed * (Mathf.Abs(GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier/0.86f));
                } */

                
                if (shotPos.x < 1.45f) {
                    animator.speed = 0.7f;
                    Invoke("dive4", (0.9f -GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 1.1f);
                } else if (shotPos.x < 1.75f) {
                    Debug.Log("aaaight");
                    animator.speed = 0.7f;
                    Invoke("dive4", (0.9f-GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.8f);
                } else if (shotPos.x < 2f) {
                    animator.speed = 1f;
                    Invoke("dive4", (0.9f-GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.35f);
                } else {
                    animator.speed = 1f;
                    Invoke("dive4", (0.9f-GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.45f);
                }


            }
            if (xPos == "center" && ( (yPos == "top") || (yPos == "middle") ) ) { 
                if (shotPos.x > 0.5f) {
                    animator.speed = 1.0f;
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.6f ;
                    animator.SetInteger("dive", 3);
                }
            }


            
            if (xPos == "left" && yPos == "bottom") {
                animator.SetInteger("dive", -1);
            }
            

            if (xPos == "center" && yPos == "bottom") {
                if (shotPos.x < -0.2f) {
                    animator.speed = 0.8f;
                    animator.SetInteger("dive", -1);
                }
            }

            if (xPos == "left" && yPos == "middle") {
                if (shotPos.x > -0.7f) {
                    animator.speed = 0.65f;
                } else if (shotPos.x > -1.2f) {
                    animator.speed = 0.82f;                        
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.2f ;
                } else {
                    animator.speed = 1.0f;
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.2f ;
                }
                animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.6f ;
                animator.SetInteger("dive", -2);
            }
            
            if (xPos == "left" && yPos == "top") { 
                if (shotPos.x > -1.15f) {
                    Debug.Log("gg 1");
                    animator.speed = 0.7f;
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.35f ;
                    //animator.speed = animator.speed * (Mathf.Abs(GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier/0.86f));
                } else if (shotPos.x > -1.45f) {
                    Debug.Log("gg 2");
                    animator.speed = 0.82f;                        
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.35f ;
                } else if (shotPos.x > -1.7f) {
                    Debug.Log("gg 3");
                    animator.speed = 1.0f;                        
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.75f ;
                    //animator.speed = animator.speed - (Mathf.Abs(GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier/0.86f));
                } else {
                    animator.speed = 1.0f;
                    animator.speed = animator.speed * (Mathf.Abs(GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier/0.86f));
                }
                animator.SetInteger("dive", -4);
            }
            if (xPos == "center" && ( (yPos == "top") || (yPos == "middle") ) ) { 
                if (shotPos.x < -0.2f) {
                    animator.speed = 1.0f;
                    animator.speed = animator.speed - (1 - GameObject.Find("Arrow").GetComponent<ArrowController>().shotPowerMultiplier) * 0.6f ;
                    animator.SetInteger("dive", -3);
                }
            }


           

            madeDiveDecision = true;

        }

    }
}
