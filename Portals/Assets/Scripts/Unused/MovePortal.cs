using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePortal : MonoBehaviour
{
    //used to determine where we want our portal to move
    private Vector3 target;
    private GameObject bluePortal;
    private GameObject orangePortal;
    private Transform bluePortaltr;
    private Transform orangePortaltr;

    void Start()
    {
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
        orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");
        bluePortaltr = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<Transform>();
        orangePortaltr = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //bluePortal.SetActive(true);
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bluePortaltr.position = new Vector3(target.x,target.y,target.z);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //orangePortal.SetActive(true);
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            orangePortaltr.position = new Vector3(target.x, target.y, target.z);
        }
    }
}
