using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    public Camera cam;
    public bool isOrange;
    public float distance = 0.3f;
    void Start()
    {
        if (isOrange==false)
        {
            //set destination to the portals position
            destination = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        }else
        {
            destination = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<Transform>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Vector3.Distance(transform.position, other.transform.position)> distance)
        {
            //move player to destination 
            other.transform.position = new Vector3(destination.position.x, destination.position.y,
                destination.position.z);
            other.transform.rotation = Quaternion.Euler(0, 180, 0);
            cam.transform.rotation = Quaternion.Euler(0, 180, 0);
        } 
    }

}
