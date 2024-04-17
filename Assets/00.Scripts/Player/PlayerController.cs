using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform hitBox;
    public Vector2 hitBoxSize;

    string pressKey = "";
    [Header("Movement Valriables")]
    private Rigidbody2D pRb;
    private Vector2 moveVector;
    public float jumpForce = 300.0f;
    public float moveSpeed = 8.0f;
    private float originMoveSpeed;

    [Header("Animation Check")]
    private Animator pAni;
    [SerializeField]bool isGrounded = false;
    [SerializeField]bool IsFastRun = false;
    [SerializeField]bool IsBlock = false;
    [SerializeField] bool IsRun = false;
    
    [Header("Dash Valriables")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown = 1.5f;
    [SerializeField] float dashGravity;

    [Header("Skill Object")]
    [SerializeField] List<float> skillCooltime;
    [SerializeField] List<float> currentCooltime;
    [SerializeField] MagicBall magicBall;
    [SerializeField] Meteor meteor;
    //------------------------------------------
    private SpriteRenderer pSprite;

    private int jumpCount = 0;
    private Vector2 inputMovement = Vector2.zero;

    private float timeSinceAttack = 0.6f;
    private float timeSinceDash = 0.5f;
    private int currentAttack = 1;
    public int damage = 1;
    private bool isleft = false;

    private void Awake()
    {
        pSprite = GetComponent<SpriteRenderer>();
        pRb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();

        currentCooltime.AddRange(skillCooltime);

        meteor = Instantiate<Meteor>(meteor);
        meteor.gameObject.SetActive(false);
        magicBall = Instantiate<MagicBall>(magicBall);
        magicBall.gameObject.SetActive(false);

        originMoveSpeed = moveSpeed;
        canDash = true;
    }

    private void Update()
    {
        if(isDashing && IsBlock)
            return;
        for(int i  = 0; i < currentCooltime.Count; i++)
        {
            if (currentCooltime[i] > 0f)
                currentCooltime[i] -= Time.deltaTime;
        }

        timeSinceAttack += Time.deltaTime;
        timeSinceDash += Time.deltaTime;

        Vector2 moveMovement = inputMovement * moveSpeed * Time.deltaTime;
        transform.Translate(moveMovement);

        pAni.SetFloat("AirSpeed", pRb.velocity.y);
    }

    //이동
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();

        if (context.started)
        {
            pSprite.flipX = inputMovement.x == -1;
            isleft = inputMovement.x == -1;
            if (canDash && timeSinceDash < 0.3f && context.control.name == pressKey)
            {
                //PlayerAfterImagePool.instance.GetFromPool();
                StartCoroutine(Dash());
            }
            IsRun = true;
            pAni.SetBool("Run", IsRun);
            timeSinceDash = 0;
        }
        else if (context.canceled)
        {
            IsRun = false;
            pAni.SetBool("Run", IsRun);
        }
        pressKey = context.control.name;
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
        PlayerAfterImagePool.instance.GetFromPool();
        float originalGravity = pRb.gravityScale;
        pRb.gravityScale = 0;
        pRb.AddForce(new Vector2(isleft? -dashPower : dashPower, 0));
        yield return new WaitForSeconds(dashTime);
        pRb.velocity = Vector2.zero;
        pRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void OnTeleport(InputAction.CallbackContext context)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Teleport");

        try
        {
            transform.position = gameObjects[int.Parse(context.control.name)-1].transform.position;
        }
        catch(Exception e)
        {
            Debug.Log($"{e} : 범위 벗어남");
        }
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
                if (item.tag == "Enemy")
                {
                    item.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (item.tag == "EnemyStructure")
                {
                    item.GetComponent<SpawnTower>().BrokeTower(damage);
                }
            }

            pAni.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0.0f;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started && !IsBlock)
        {
            IsBlock = true;
            pAni.SetBool("Block", IsBlock);
        }
        else if (context.canceled && IsBlock)
        {
            IsBlock = false;
            pAni.SetBool("Block", IsBlock);
        }
    }

    //달리기
    public void OnFastRun(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            moveSpeed *= 1.5f;
            IsFastRun = true;
            pAni.SetBool("FastRun", IsFastRun);
        }
        else if(context.canceled)
        {
            moveSpeed = originMoveSpeed;
            IsFastRun = false;
            pAni.SetBool("FastRun", IsFastRun);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        pAni.SetBool("Grounded", isGrounded);
        jumpCount = 0;

        if(collision.collider.tag == "Enemy")
        {
            pAni.SetTrigger("Hit");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!isGrounded)
        {
            isGrounded = true;
            pAni.SetBool("Grounded", isGrounded);
        }
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
        if(context.started)
        {
            if (context.control.name == "a" && currentCooltime[0] <= 0f)
            {
                pAni.SetTrigger("Skill1");

                magicBall.isleft = isleft;
                magicBall.transform.position = gameObject.transform.position;

                magicBall.gameObject.SetActive(false);
                magicBall.gameObject.SetActive(true);
                magicBall.time = 0;
                currentCooltime[0] +=skillCooltime[0];
                /*StartCoroutine(Fireballs());
                Invoke(nameof(Fireballs), 0.1f);*/
            }
            else if (context.control.name == "s" && currentCooltime[1] <= 0f)
            {
                pAni.SetTrigger("Skill1");
                meteor.transform.position = transform.position + new Vector3(0, 4, 0);
                meteor.SetSkill(isleft);

                currentCooltime[1] += skillCooltime[1];
            }   
        }
    }
}
