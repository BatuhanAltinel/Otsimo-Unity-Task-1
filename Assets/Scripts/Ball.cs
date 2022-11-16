using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    Vector2 lastPosition;
    [SerializeField] float saveDelay = 0.2f;
    [SerializeField] float _moveSpeed = 5f;
    bool nextSave = true;
 
    Rigidbody2D rb;
    Vector2 dir;
    
    Dragger dragger;
    
    void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        dragger = GetComponent<Dragger>();
    }
    
    void Update()
    {
        if (nextSave)
        {
            StartCoroutine("SavePosition");
        }
    }

    void OnMouseUp()
    {
        dragger.canBePushed = true;
    }
    
    void FixedUpdate()
    {
        if (dragger.canBePushed)
        {
            dragger.canBePushed = false;
            rb.velocity = (dragger.GetMousePosition() - transform.position) * _moveSpeed;
        }
    }
 
    IEnumerator SavePosition()
    {
        if(dragger.canBePushed)
        {
            nextSave = false;
            lastPosition = transform.position;
            yield return new WaitForSeconds(saveDelay);
            nextSave = true;
        }
        
    }
}
