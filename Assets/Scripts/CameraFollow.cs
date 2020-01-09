using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //Propiedades
    public Transform Target { get; set; }

    private void Update()
    {
        transform.position = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
    }
}
