using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Transform hitBox;
    public Vector2 hitBoxSize;
    
    
    public int damage = 1;

    [Header("Movement Valriables")]
    private Rigidbody2D pRb;
    private Vector2 moveVector;
    public float jumpForce = 300.0f;
    public float moveSpeed = 8.0f;

    [Header("Animation Check")]
    private Animator pAni;
    bool isGrounded = false;
    bool IsFastRun = false;
    bool IsRun = false;

    [Header("Dash Valriables")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;
    private TrailRenderer dashTrail;
    [SerializeField] float dashGravity;
    private float normalGravity;
    private float waitTime = 0.5f;

    //------------------------------------------
    private SpriteRenderer pSprite;

    private int jumpCount = 0;
    private Vector2 inputMovement = Vector2.zero;

    private int currentAttack = 1;
    private float timeSinceAttack = 0.6f;
    private float DashCoolDown = 0.5f;

    private void Awake()
    {
        pSprite = GetComponent<SpriteRenderer>();
        pRb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        dashTrail = GetComponent<TrailRenderer>();
        normalGravity = pRb.gravityScale;
        
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        Vector2 moveMovement = inputMovement * moveSpeed * Time.deltaTime;
        transform.Translate(moveMovement);

        pAni.SetFloat("AirSpeed", pRb.velocity.y);

        if (inputMovement.x == 0)
        {
            
        }
    }

    //이동
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();

        if (context.started)
        {
            pSprite.flipX = inputMovement.x == -1;
            /*if (canDash)
            {
                Dash();
            }*/
            IsRun = true;
            pAni.SetBool("Run", IsRun);
        }
        else if (context.canceled)
        {
            IsRun = false;
            pAni.SetBool("Run", IsRun);
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        StartCoroutine(Dash());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (jumpCount < 2)
            {
                pRb.velocity = Vector2.zero;
                pRb.AddForce(new Vector2(0, jumpForce));
            }
            else if (pRb.velocity.y > 0)
            {
                pRb.velocity *= 0.5f;
            }
            jumpCount++;
            pAni.SetTrigger("Jump");
        }
    }


    //대시 -> wait 걸고 -> 대시 해제 및 반환 식의 코루틴
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = pRb.gravityScale;
        pRb.gravityScale = 0;
        pRb.velocity = new Vector2(transform.localScale.x * dashPower, 0);
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        dashTrail.emitting = false;
        pRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    //context.started (시작 시)
    //context.performed (작동 시)
    //context.canceled (중단 시)
    // 점프
    

    //공격
    // 공격 동안만 이동속도 반으로 감소
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started&& timeSinceAttack > 0.25f)
        {
            currentAttack++;
            
            if (currentAttack > 3 || timeSinceAttack > 0.6f)
                currentAttack = 1;

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(hitBox.position, hitBoxSize, 0);
            foreach(Collider2D item in collider2Ds)
            {
                if (item.tag =="Enemy")
                {
                    item.GetComponent<Enemy>().TakeDamage(damage);
                    Debug.Log($"{item.name}이 맞음");
                }
            }

            pAni.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0.0f;
        }
    }


    //달리기
    public void OnFastRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsFastRun = true;
            pAni.SetBool("FastRun", IsFastRun);
        }
        else if(context.canceled)
        {
            IsFastRun = false;
            pAni.SetBool("FastRun", IsFastRun);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        pAni.SetBool("Grounded", isGrounded);
        jumpCount = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        pAni.SetBool("Grounded", isGrounded);
    }


    //히트박스 관전 기즈모
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(hitBox.position, hitBoxSize);
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        
    }
}
