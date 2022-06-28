using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    //use script on gun but not on final game 
    public float weponRange = 50f;
    private Camera fpsCam;

    void Start()
    {
        fpsCam = Component.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //to draw the line form the center
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward*weponRange, Color.green);
    }
}
