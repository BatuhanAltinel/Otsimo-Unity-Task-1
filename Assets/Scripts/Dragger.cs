using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour 
{
    
    [SerializeField] float _speed = 1000f;
    private Camera _cam;
    Vector3 _dragOffset;
    public bool canBePushed = false;
    
    void Awake()
    {
        _cam = Camera.main;
    }
    
    private void OnMouseDown() 
    {
        _dragOffset = transform.position - GetMousePosition();
        canBePushed = false;
        Debug.Log(gameObject.name);
    }
    private void OnMouseDrag() 
    {
        transform.position =Vector3.MoveTowards(transform.position,GetMousePosition() + _dragOffset,_speed*Time.deltaTime);
    }

    public Vector3 GetMousePosition()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    private void OnMouseUp()
    {
        canBePushed = true;
    }
}