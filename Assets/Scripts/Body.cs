using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    Vector2 _lastPosition;
    Dragger dragger;
    BoundaryChecker boundaryChecker;

#region  Boundary Points

    float minX = -2.2f;
    float maxX = 2.2f;
    float minY = -4.2f;
    float maxY = 4.2f;

#endregion


    void Awake()
    {
        dragger = GetComponent<Dragger>();
        boundaryChecker = GetComponent<BoundaryChecker>();
    }
    void OnEnable()
    {
        EventManager.onClickBody += AnimationController;
        EventManager.onClickBody += SaveLastPosition;
        EventManager.onClickBody += BodySpringPositioning;
    }
    void OnDisable()
    {
        EventManager.onClickBody -= AnimationController;
        EventManager.onClickBody -= SaveLastPosition;
        EventManager.onClickBody -= BodySpringPositioning;
    }
    
    void Update()
    {
       boundaryChecker.BoundaryCheck(gameObject.transform,maxX,minX,maxY,minY);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            BodyAnimations.anims.CrushAnimation();
            BodyAnimations.anims.StopCrushAnim();
        }
    }

    void OnMouseUp()
    {
        BodyAnimations.anims.IdleAnimation();
    }

    void BodySpringPositioning()
    {
        SpringJoint2D _bodySpring = gameObject.GetComponent<SpringJoint2D>();
        _bodySpring.connectedAnchor = dragger.GetMousePosition();
    }

#region Animation Controller

    void AnimationController()
    {
        if((_lastPosition.x - transform.position.x) < 0)
            BodyAnimations.anims.MoveLeftAnimation();
        else if((_lastPosition.x - transform.position.x) > 0)
            BodyAnimations.anims.MoveRightAnimation();
        else
            BodyAnimations.anims.IdleAnimation();
    }
    

#endregion

    void SaveLastPosition()
    {
        StartCoroutine(SaveLastPositionRoutine());
    }

     IEnumerator SaveLastPositionRoutine()
    {
        yield return new WaitForEndOfFrame();
        _lastPosition = transform.position;
    }

}
