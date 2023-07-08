using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Simulator : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;

    public Transform mark;
    public Slider slider;
    public bool thrown = false; 
    public bool hit = false; 

    public float throwSpeed;
    public float steer;
    public float spin;
    Vector3 startPos;
    public GameObject gh;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        thrown = true;
        print(Physics.simulationMode);
        Physics.simulationMode = SimulationMode.Script;
    }

    bool viz = true;
    bool cont = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        throwSpeed += Input.GetAxisRaw("Vertical")*0.1f;
        steer += Input.GetAxisRaw("Horizontal")*0.1f;

        if (Input.GetKeyDown(KeyCode.Return)){
            gh.SetActive(false);
            GetComponent<MeshRenderer>().enabled = true;
            Physics.simulationMode = SimulationMode.FixedUpdate;
            viz = false;
            rb.velocity = Vector3.zero;
            transform.position = startPos;
            thrown = true;
            hit = false;
        }

        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("CricketScene");
        }
        if (Physics.simulationMode == SimulationMode.Script){
            if (viz){
                for (int i = 0; i < 60; i++)
                {
                    Ball();
                    Physics.Simulate(Time.fixedDeltaTime/1f);   
                }
            }
        }else{
            if (cont){

                Ball();
            }
        }

    }

    void Ball(float e = 1f){

        if (thrown){
            thrown = false;
            rb.useGravity = true;
            rb.AddForce(throwSpeed*cam.transform.forward+cam.transform.right*steer, ForceMode.Impulse);
        }

        
        rb.AddForce(cam.transform.right*spin*e, ForceMode.Force);
        
        // print("Spin: " + BallThrow.spin.ToString() + ", throwSpeed: " + BallThrow.throwSpeed.ToString());
    }

    void OnCollisionEnter(Collision collider){
        if (collider.gameObject.tag == "Ground"){
            // print("Postion: " + transform.position.ToString());
            hit = true;
            thrown = true;

            if (Physics.simulationMode == SimulationMode.Script){
                rb.velocity = Vector3.zero;
                mark.position = transform.position; 
                transform.position = startPos;
            }else{
                // steer = 0;
                spin *= 0.75f;    
                print("YESSSSSSSSSSSSSSSS");
                cont = false;
            }



            // Physics.simulationMode = SimulationMode.Update;
        }
    }

    public void OnSliderChange(){
        spin = slider.value;
        print(spin);
    }
}
