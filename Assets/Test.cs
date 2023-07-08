using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // var rot = transform.rotation.x + 0.1f;
        transform.Rotate(new Vector3(1f, 0, 0));
    }
}
