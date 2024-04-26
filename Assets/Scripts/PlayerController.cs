using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidbody;

    public float jumpPower = 10;
    public float jumpInterval = 0.5f;

    private float jumpCoolDown = 0;

    // Start is called before the first frame update
    void Start()
    {
       thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        jumpCoolDown -= Time.deltaTime;
        bool isGameActive = GameManager.Instance.IsGameActive();
        bool canJump = jumpCoolDown <= 0 && isGameActive;

        if (canJump){
            bool jumpInput = Input.GetKey(KeyCode.Space);
            if(jumpInput){
                Jump();
            }
        }   

        //Toggle gavity
        thisRigidbody.useGravity = isGameActive;

    }
    
    void OnCollisionEnter(Collision other){
       onCustomCollisionEnter(other.gameObject);
    }


    void OnTriggerEnter(Collider other){
      onCustomCollisionEnter(other.gameObject);
    }
    private void onCustomCollisionEnter(GameObject other){
        bool isSensor = other.CompareTag("Sensor");

        if(isSensor){
            GameManager.Instance.score++;
            Debug.Log("Score " + GameManager.Instance.score);
        } else {
            //game over
            GameManager.Instance.EndGame();
            
            
        }
    }

    private void Jump(){
        jumpCoolDown = jumpInterval;

        thisRigidbody.velocity = Vector3.zero;

        thisRigidbody.AddForce(new Vector3(0,jumpPower,0), ForceMode.Impulse);
    }
}
