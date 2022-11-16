using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    SpringJoint2D _bodySpring;

    Animator anim;

    bool nextSave = true;

    Vector2 _lastPosition;
    void OnEnable()
    {
        EventManager.onClickBody += BodyAnimationController;
    }
    void OnDisable()
    {
        EventManager.onClickBody -= BodyAnimationController;
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        _bodySpring = GetComponent<SpringJoint2D>(); 
        _bodySpring.connectedAnchor = gameObject.transform.position;
    }

    void Update()
    {
        if(nextSave)
        {
            StartCoroutine(Save_LastPosition());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            CrushAnimation();
            StartCoroutine(StopCrushAnimRoutine());
            Debug.Log("crush happened with " + other.gameObject.name);
        }
    }

    void OnMouseDrag()        
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        _bodySpring.connectedAnchor = cursorPosition;
    }

    void BodyAnimationController()
    {
        if((_lastPosition.x - transform.position.x) < 0)
            MoveLeftAnimation();
        else if((_lastPosition.x - transform.position.x) > 0)
            MoveRightAnimation();
        else
            IdleAnimation();
    }

    public void MoveRightAnimation()
    {
        anim.SetBool("MoveLeft",true);
        anim.SetBool("MoveRight",false);
    }
    public void MoveLeftAnimation()
    {
        anim.SetBool("MoveLeft",false);
        anim.SetBool("MoveRight",true);
    }

    public void CrushAnimation()
    {
        anim.SetBool("IsCrush",true);
    }

    public void IdleAnimation()
    {
        anim.SetBool("MoveLeft",false);
        anim.SetBool("MoveRight",false);
    }
    

    IEnumerator StopCrushAnimRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("IsCrush",false);
    }

     IEnumerator Save_LastPosition()
    {
        nextSave = false;
        _lastPosition = transform.position;
        yield return new WaitForSeconds(0.05f);
        nextSave = true;
    }

}
