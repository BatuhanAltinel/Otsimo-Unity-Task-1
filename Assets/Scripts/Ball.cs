using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    
    Rigidbody2D _ballRb;
    
    Dragger dragger;
    [SerializeField] float animationDuration = 0.5f;

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
    void Start()
    {
        _ballRb = GetComponent<Rigidbody2D>();
        dragger = GetComponent<Dragger>();
    }
    
    void Update()
    {
        BoundaryChecker.boundaryChecker.BoundaryCheck(gameObject.transform,maxX,minX,maxY,minY);
    }

    void FixedUpdate()
    {
        if (dragger._canThrow)
        {
            dragger._canThrow = false;
            _ballRb.velocity = (dragger.GetMousePosition() - transform.position) * _moveSpeed;
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
        transform.DOScaleY(0.017f,animationDuration).SetEase(Ease.InOutBounce);
    }

    void BallAnimationStop()
    {
        transform.DOScaleY(0.02f,animationDuration).SetEase(Ease.InOutBounce);
    }

#endregion

}
