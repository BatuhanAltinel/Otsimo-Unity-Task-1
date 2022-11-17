using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimations : MonoBehaviour
{
    public static BodyAnimations anims;
    Animator anim;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(anims == null)
            anims = this;
        else
            Destroy(gameObject);

        anim = GetComponent<Animator>();
    }

    #region  Body Animations
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
    
    public void StopCrushAnim()
    {
        StartCoroutine(StopCrushAnimRoutine());
    }
    IEnumerator StopCrushAnimRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("IsCrush",false);
    }
    #endregion

}
