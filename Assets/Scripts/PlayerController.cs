using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer pSprite;
    private Rigidbody2D pRb;
    private Animator pAni;

    private int jumpCount = 0;
    public float moveSpeed = 8.0f;
    public float jumpForce = 300.0f;
    private Vector2 inputMovement = Vector2.zero;
    bool IsRun = false;
    bool IsFastRun = false;
    bool isGrounded = false;

    private int currentAttack = 1;
    private float timeSinceAttack = 0.6f;

    private void Awake()
    {
        pSprite = GetComponent<SpriteRenderer>();
        pRb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        Vector2 moveMovement = inputMovement * moveSpeed * Time.deltaTime;
        transform.Translate(moveMovement);

        pAni.SetFloat("AirSpeed", pRb.velocity.y);

        if (inputMovement.x == 0)
        {
            IsRun = false;
            pAni.SetBool("Run", IsRun);
        }
        if (Input.GetKeyUp("left shift"))
        {
            IsFastRun = false;
            pAni.SetBool("FastRun", IsFastRun);
        }
    }

    //�̵�
    private void OnMove(InputValue inputValue)
    {
        inputMovement = inputValue.Get<Vector2>();
        pSprite.flipX = inputMovement.x == -1;

        IsRun = true;
        pAni.SetBool("Run", IsRun);
    }

    // ����
    void OnJump(InputValue inputValue)
    {
        if(inputValue.isPressed)
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

    //����
    // ���� ���ȸ� �̵��ӵ� ������ ����
    void OnAttack(InputValue inputValue)
    {
        if(inputValue.isPressed && timeSinceAttack > 0.25f)
        {
            currentAttack++;
            
            if (currentAttack > 3 || timeSinceAttack > 0.6f)
                currentAttack = 1;

            pAni.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0.0f;
        }
    }


    //�޸���
    void OnFastRun(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            IsFastRun = true;
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


    private void OnSkill(InputValue inputValue)
    {
        if(inputValue.)
    }
}

/*
 ����������
1. ������� Ű�� ���ε�
2. ĳ���͸� �����¿�� ����������.
�� ���¿� ���� �ִϸ��̼�
3. Ÿ�ϸ��� ����ؼ� ���� ��´�.

4. ���� ������ ����
4. ��ֹ� ������ �� ��.
5. ����

 */