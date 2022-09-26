using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public bool playerReached = false;
    public Vector3 p;

    public bool resetPlayer = false;

    public Animator animator;

    Vector3 initialPosition;

    public bool playerAboutToKick = false;

    Animation anim;

    // Start is called before the first frame update
    void Start()
    { 
        string ply = GameObject.Find("Ground").GetComponent<SelectPlayer>().selectedPlayer; 
        Transform pt = GameObject.Find("Player/" + ply).GetComponent<Transform>();         
        animator = GameObject.Find("Player/" + ply).GetComponent<Animator>();
        initialPosition = pt.position;
    }

    void initPlayer() {
        string x = "";
        //transform.position = new Vector3(-0.8f, 0.2f, -4.6f); 
        string plyr = GameObject.Find("Ground").GetComponent<SelectPlayer>().selectedPlayer;
        GameObject.Find("Player/" + plyr).GetComponent<Transform>().position = new Vector3(-0.8f, 0.2f, -4.6f); 
        p = new Vector3(-0.8f, 0.2f, -4.6f); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log(playerReached);

   

        string ply = GameObject.Find("Ground").GetComponent<SelectPlayer>().selectedPlayer; 
        Transform pt = GameObject.Find("Player/" + ply).GetComponent<Transform>(); 

        //Debug.Log(ply);
                
        //Debug.Log("Pt = " + pt.position.z); 
        //Debug.Log("Transform = " + transform.position.z);


        p = pt.position; 

        
            //Debug.Log(p);

        //Debug.Log(p);

        //Debug.Log("IS RUN UP + " + animator.GetBool("isRunUp"));
        //Debug.Log("About to kick + " + animator.GetBool("aboutToKick"));
        

        //Debug.Log("GONNA SHOOT " + GameObject.Find("Arrow").GetComponent<ArrowController>().gonnaShoot);
        //Debug.Log(animator.GetBool("isRunUp"));

        if (GameObject.Find("Arrow").GetComponent<ArrowController>().gonnaShoot == false) {
            playerReached = false;
            animator.SetBool("sideWalkDone", false);
        }
            

        if (GameObject.Find("Arrow").GetComponent<ArrowController>().gonnaShoot == true) {



            animator.SetBool("isRunUp", true);
            playerReached = false;
            
            if (p.x < 0f) {
                p.x += 0.015f;
            } 
            
            else { 

                if (p.z < -3.3f) {
                    p.z += 0.03f;
                }
        
                animator.SetBool("sideWalkDone", true);

                if (p.z > -3.5f) {
                    playerAboutToKick = true;
                }
            
                string player = GameObject.Find("Ground").GetComponent<SelectPlayer>().selectedPlayer;
                Transform playerTransform = GameObject.Find("Player/" + player + "/mixamorig5:Hips").GetComponent<Transform>(); 
                
                //Debug.Log(playerTransform.position.z); 

                if (p.z >= -3.3f) {
                    playerAboutToKick = false;
                    //Debug.Log("ayyyy");
                    playerReached = true;
                    animator.SetBool("aboutToKick", true);
                    animator.SetBool("isRunUp", false);
                    //p = new Vector3(0f,0.1f,0f);
                }
               // if (anim["kick"].normalizedTime == 0.5f) {
                 //   playerAboutToKick = false; 
                 //   playerReached = true;
               // }
            }
             
        } 


       // Debug.Log("PZ = " + p.z);

        if (!resetPlayer) {
            pt.position = new Vector3(p.x, p.y, p.z);  
        } else {
            pt.position = initialPosition;
            resetPlayer = false;
        }

    }
}
