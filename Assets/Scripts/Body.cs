using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    bool _nextPositionSave = true;

    Vector2 _lastPosition;

#region  Boundary Points

    float minX = -2.2f;
    float maxX = 2.2f;
    float minY = -4.2f;
    float maxY = 4.2f;

#endregion

    void OnEnable()
    {
        EventManager.onClickBody += AnimationController;
    }
    void OnDisable()
    {
        EventManager.onClickBody -= AnimationController;
    }

    void Update()
    {
        BoundaryChecker.boundaryChecker.BoundaryCheck(gameObject.transform,maxX,minX,maxY,minY);

        if(_nextPositionSave)
        {
            SaveLastPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            BodyAnimations.anims.CrushAnimation();
           BodyAnimations.anims.StopCrushAnim();
            Debug.Log("crush happened with " + other.gameObject.name);
        }
    }

    void OnMouseUp()
    {
        BodyAnimations.anims.IdleAnimation();
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
        _nextPositionSave = false;
        _lastPosition = transform.position;
        yield return new WaitForEndOfFrame();
        _nextPositionSave = true;
    }

}
