using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallThrower : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    public Transform ball;
    public Transform marker;
    public Transform swingPoint;
    public Slider slider;
    public Toggle toggle;
    public Vector3 centrePoint;
    void Start()
    {

    }

    float timeElapsed;
    bool once = true;
    bool hit = false;
    bool swing = true;

    Vector3 dir;
    public float t;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            SceneManager.LoadScene("TestScene");
        }
        if (Input.GetKeyDown(KeyCode.Return)){
            rb.useGravity = true;
            dir = marker.position - transform.position;
            dir.y = 0f;
            rb.AddForce(dir, ForceMode.Impulse);
        }

        if (!hit && swing && rb.useGravity){
            var sp = transform.position;
            var swp = swingPoint.position;

            centrePoint = Vector3.Lerp(sp, swp, t);
            ball.position = Vector3.Lerp(centrePoint, sp, t);
            t += Time.deltaTime;
        }
    }
    
    void OnCollisionEnter(Collision col){
        hit = true;
        if (swing){
            rb.AddForce((transform.position - swingPoint.position)+dir, ForceMode.Impulse);
        }else{
            rb.AddForce(-(transform.position - swingPoint.position)+dir, ForceMode.Impulse);
        }
    }

    public void SwingAmount(){

        swingPoint.position = new Vector3(slider.value, swingPoint.position.y, swingPoint.position.z);
    }
    public void OnToggle(){
        swing = toggle.isOn;
    }
}
