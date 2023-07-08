using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallThrow : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject ghost;
    public Camera cam;
    public Slider slider;
    public Toggle toggle;
    public float throwSpeed = 1f;
    public float spin = 0f;
    public float steer = 0f;
    private bool hit = false;
    private bool inner = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ball();
    }

    void Ball(){
        throwSpeed += Input.GetAxisRaw("Vertical")*0.1f;
        // spin += Input.GetAxisRaw("Horizontal")*0.1f;
        steer += Input.GetAxisRaw("Horizontal")*0.1f;

        if (Input.GetKeyDown(KeyCode.Return)){
            ghost.SetActive(false);
            Physics.simulationMode = SimulationMode.FixedUpdate;
            rb.useGravity = true;
            rb.AddForce(throwSpeed*cam.transform.forward+cam.transform.right*steer, ForceMode.Impulse);
        }

        if (rb.useGravity){
            rb.AddForce(cam.transform.right*spin*0.9f, ForceMode.Force);
        }

        // print("Spin: " + spin.ToString() + ", throwSpeed: " + throwSpeed.ToString());
    }

    public void OnSwingValueChange(){
        spin = slider.value;
        // if (spin < steer){
        //     spin = 0f;
        // }
    }
    
    public void OnToogle(){
        inner = toggle.isOn;
        print("Inner: " + inner.ToString());
    }
    void OnCollisionEnter(Collision collider){
        if (collider.gameObject.tag == "Ground"){
            hit = true;
            print("Inner: " + inner.ToString());
            // if (inner){
            //     rb.AddForce(cam.transform.right*spin, ForceMode.Impulse);
            // }
        }
    }
}
