using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    public static BoundaryChecker boundaryChecker;
    
    void Awake()
    {
        if(boundaryChecker == null)
            boundaryChecker = this;
        else
            Destroy(gameObject);
    }

    public void BoundaryCheck(Transform transform,float maxX,float minX,float maxY,float minY)
    {
        if(transform.position.x > maxX)
            transform.position = new Vector2(maxX, transform.position.y);

        if(transform.position.x < minX)
            transform.position = new Vector2(minX, transform.position.y);

        if(transform.position.y > maxY)
            transform.position = new Vector2(transform.position.x,maxY);

        if(transform.position.y < minY)
            transform.position = new Vector2(transform.position.x,minY);
    }
}
