using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetControls : MonoBehaviour
{
    public float jetSpeed = 10f;
    public Vector3 movCamTo;
    
    
    // Update is called once per frame
    void Update()
    {
        movCamTo = transform.position - transform.forward * 11.0f + Vector3.up * 7.0f;
        Camera.main.transform.position = movCamTo;
        Camera.main.transform.LookAt(transform.position);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * (jetSpeed * 2) * Time.deltaTime;
            movCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f ;
            Camera.main.transform.position = movCamTo;
            Camera.main.transform.LookAt(transform.position);
        }

        transform.position += transform.forward * jetSpeed * Time.deltaTime;
        transform.Rotate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), -Input.GetAxis("MyHorizontal")) ;

    }

}
