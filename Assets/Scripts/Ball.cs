using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    Rigidbody2D ballRb;
    Dragger dragger;
    BoundaryChecker boundaryChecker;

    [SerializeField] float _moveSpeed = 5f;

    [SerializeField] float _animationDuration = 0.5f;

    [SerializeField] float _animScaleSizeX = 0.017f;
    [SerializeField] float _animScaleSizeY = 0.022f;
    [SerializeField] float _normalScaleSize = 0.022f;
    

#region Boundary Points

    float maxX = 2.3f;
    float minX = -2.3f;
    float maxY = 4.5f;
    float minY = -4.5f;

#endregion


    void OnEnable()
    {
        EventManager.onClickBall += BallAnimationStart;
    }
    void OnDisable()
    {
        EventManager.onClickBall -= BallAnimationStart;
    }
    void Awake()
    {
        ballRb = GetComponent<Rigidbody2D>();
        dragger = GetComponent<Dragger>();
        boundaryChecker = GetComponent<BoundaryChecker>();
    }
    
    void Update()
    {
        boundaryChecker.BoundaryCheck(gameObject.transform,maxX,minX,maxY,minY);
    }

    void FixedUpdate()
    {
        if (dragger._canThrow)
        {
            dragger._canThrow = false;
            ballRb.velocity = (dragger.GetMousePosition() - transform.position) * _moveSpeed;
        }
    }
    
    void OnMouseUp()
    {
        BallAnimationStop();
        dragger._canThrow = true;
    }

#region  Ball Animations
    void BallAnimationStart()
    {
        transform.DOScaleY(_animScaleSizeY,_animationDuration).SetEase(Ease.InOutBounce);
        transform.DOScaleX(_animScaleSizeX,_animationDuration).SetEase(Ease.InOutBounce);
        ballRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void BallAnimationStop()
    {
        transform.DOScale(_normalScaleSize,_animationDuration).SetEase(Ease.InOutBounce);
        ballRb.constraints = RigidbodyConstraints2D.None;
    }

#endregion

}
