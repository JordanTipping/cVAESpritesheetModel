using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; 
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(0, 1, -10);

    void LateUpdate()
    {
        if (target == null) return; 

  
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
