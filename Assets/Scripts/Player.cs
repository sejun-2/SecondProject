using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    [SerializeField] public float moveSpeed; // �����̴� ���ǵ�
    //[SerializeField] public float jumpSpeed;

    private Coroutine jumpChargeCoroutine;
    private float jumpChargeTime;
    private float minJumpForce = 5f;  // �ּ� ���� ��
    private float maxJumpForce = 16f; // �ִ� ���� ��

    private float fallingGravityScale = 5f;
    private float defaultGravityScale;

    public StateMachine stateMachine;

    public Animator animator;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;

    public float inputX;
    public bool isJumped;
    public bool isGrounded;
    public CinemachineFramingTransposer cinemachine;

    public readonly int IDLE_HASH = Animator.StringToHash("Idle");
    public readonly int WALK_HASH = Animator.StringToHash("Walk");
    public readonly int JUMP_HASH = Animator.StringToHash("Jump");

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cinemachine = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        StateMachineInit();

        defaultGravityScale = rigid.gravityScale;
    }

    private void StateMachineInit()
    {
        stateMachine = new StateMachine();
        stateMachine.stateDic.Add(EState.Idle, new Player_Idle(this));
        stateMachine.stateDic.Add(EState.Walk, new Player_Walk(this));
        stateMachine.stateDic.Add(EState.Jump, new Player_Jump(this));
        stateMachine.CurState = stateMachine.stateDic[EState.Idle];
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        //isJumped = Input.GetKeyDown(KeyCode.Space);
        // �����̽��ٸ� ���� �� �ڷ�ƾ ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpChargeCoroutine == null && isGrounded)
        {
            jumpChargeCoroutine = StartCoroutine(JumpChargeRoutine());
            isJumped = true;
        }
        // �����̽��ٸ� ���� �ڷ�ƾ ���� �� ���� ����
        else if (Input.GetKeyUp(KeyCode.Space) && jumpChargeCoroutine != null)
        {
            StopCoroutine(jumpChargeCoroutine);
            jumpChargeCoroutine = null;
            Jump(jumpChargeTime);
            jumpChargeTime = 0f;
        }

        if (rigid.velocity.y < 0) // �ϰ� ��
        {
            rigid.gravityScale = fallingGravityScale;
        }
        else // ��� ��
        {
            rigid.gravityScale = defaultGravityScale;
        }

        stateMachine.Update();

        
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    IEnumerator JumpChargeRoutine()
    {
        jumpChargeTime = 0f;
        while (true)
        {
            jumpChargeTime += Time.deltaTime;
            yield return null;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                break;
            }
        }
    }

    void Jump(float chargeTime)
    {
        // �ּ�/�ִ� ���� ������ Ŭ���� (��: ���� �ð��� ���)
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, Mathf.Clamp01(chargeTime));    // ���� �� ���
        //float jumpForce = Mathf.Clamp(jumpChargeTime, 3f, 20f);

        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);    // ���� �� ����
        isGrounded = false;
        // ���� �ִϸ��̼� ���� �� �߰� ����
    }
}
