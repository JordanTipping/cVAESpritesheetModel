using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode] 
public class GridSnapper : MonoBehaviour
{

    public float gridSize = 0.16f;  

    void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Round(position.x / gridSize) * gridSize;
        position.y = Mathf.Round(position.y / gridSize) * gridSize;

        transform.position = position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
