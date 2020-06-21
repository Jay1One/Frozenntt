using System;
using System.Collections;
using UnityEngine;


public class Inputmanager : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    public static float HorizontalAxis;
    public static event Action<float> JumpAction;
    public static event Action<string> FireAction;

    private float jumpTimer;
    private Coroutine waitJumpCoroutine;


    private void Start()
    {
        HorizontalAxis = 0;
    }

   

    private void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            //animator.SetTrigger("Jump");
            if (waitJumpCoroutine == null)
            {
                waitJumpCoroutine = StartCoroutine(WaitJump());
                return;
            }

            jumpTimer = Time.time;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FireAction?.Invoke("Fire1");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            FireAction?.Invoke("Fire2");
        }
    }
    
    private IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.2f);
        // if (JumpAction != null)
        // {
        //     float force = Time.time - jumpTimer <= 0.2f ? 1.25f : 1f; //TODO брать в константы
        //     JumpAction.Invoke(force);
        // }
        
        waitJumpCoroutine = null;
    }
}
