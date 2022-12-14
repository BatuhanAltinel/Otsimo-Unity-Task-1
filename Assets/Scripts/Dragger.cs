using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour 
{
    [SerializeField] float _speed = 1000f;
    private Camera _cam;
    Vector3 _dragOffset;
    public bool _canThrow = false;
    
    void Awake()
    {
        _cam = Camera.main;
    }
    
    private void OnMouseDown() 
    {
        _dragOffset = transform.position - GetMousePosition();
        _canThrow = false;

        if(gameObject.CompareTag("Ball"))
            EventManager.onClickBall.Invoke();
        
    }
    
    private void OnMouseDrag() 
    {
        transform.position =Vector3.MoveTowards
                            (transform.position,GetMousePosition() + _dragOffset,_speed*Time.deltaTime);
       
        if(gameObject.CompareTag("Body"))
            EventManager.onClickBody.Invoke();
          
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    
}