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
    
    Rigidbody2D _ballRB;
    
    Dragger dragger;
    public float animationDuration = 0.5f;
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
        lastPosition = transform.position;
        _ballRB = GetComponent<Rigidbody2D>();
        dragger = GetComponent<Dragger>();
    }
    
    void Update()
    {
        BoundaryCheck();
        if (nextSave)
        {
            StartCoroutine("SaveLastPosition");
        }
    }

    void OnMouseUp()
    {
        BallAnimationStop();
    }
    
    void FixedUpdate()
    {
        if (dragger.canBePushed)
        {
            dragger.canBePushed = false;
            _ballRB.velocity = (dragger.GetMousePosition() - transform.position) * _moveSpeed;
        }
    }
 
    IEnumerator SaveLastPosition()
    {
        if(dragger.canBePushed)
        {
            nextSave = false;
            lastPosition = transform.position;
            yield return new WaitForSeconds(saveDelay);
            nextSave = true;
        }
        
    }

    void BallAnimationStart()
    {
        transform.DOScaleY(0.017f,animationDuration).SetEase(Ease.InOutBounce);
    }

    void BallAnimationStop()
    {
        transform.DOScaleY(0.02f,animationDuration).SetEase(Ease.InOutBounce);
    }

    void BoundaryCheck()
    {
        if(transform.position.x > 2.3f)
            transform.position = new Vector2(2.3f, transform.position.y);
        if(transform.position.x < -2.3f)
            transform.position = new Vector2(-2.3f, transform.position.y);
        if(transform.position.y > 4.5f)
            transform.position = new Vector2(transform.position.x,4.5f);
        if(transform.position.y < -4.5f)
            transform.position = new Vector2(transform.position.x,-4.5f);
    }
}
