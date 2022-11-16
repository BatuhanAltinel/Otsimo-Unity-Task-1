using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    SpringJoint2D spring;


    void Awake()
    {
        spring = GetComponent<SpringJoint2D>(); 
        spring.connectedAnchor = gameObject.transform.position;
    }
    void OnMouseDrag()        
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        spring.connectedAnchor = cursorPosition;
    }
}
