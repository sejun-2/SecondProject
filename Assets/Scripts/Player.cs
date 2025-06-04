using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;   //���߾� ī�޶�

    [SerializeField] public float moveSpeed; // �����̴� ���ǵ�
    //[SerializeField] public float jumpSpeed;

    private Coroutine jumpChargeCoroutine;  // ���� ���� �ڷ�ƾ
    private float jumpChargeTime;   // ���� ���� �ð�
    private float minJumpForce = 5f;  // �ּ� ���� ��
    private float maxJumpForce = 16f; // �ִ� ���� ��

    private float fallingGravityScale = 5f; // �ϰ� �� �߷� ������ -> ������ ����������
    private float defaultGravityScale;  // �⺻ �߷� ������

    public StateMachine stateMachine;   // ���� �ӽ� ����

    public Animator animator;   // �ִϸ����� ����
    public Rigidbody2D rigid;   
    public SpriteRenderer spriteRenderer;   // ��������Ʈ ������ ����

    public float inputX;    // ���� �̵��� ���� ����
    public bool isJumped;   // ���� ���θ� Ȯ���ϴ� ����
    public bool isGrounded; // ���� ��� �ִ��� Ȯ���ϴ� ����
    public CinemachineFramingTransposer cinemachine; // ī�޶� ��ġ ���� ���ؼ� �ó׸ӽ� ������Ʈ ��������

    public readonly int IDLE_HASH = Animator.StringToHash("Idle");  // ��� �ִϸ��̼� �ؽ�
    public readonly int WALK_HASH = Animator.StringToHash("Walk");  // �ȱ� �ִϸ��̼� �ؽ�
    public readonly int JUMP_HASH = Animator.StringToHash("Jump");  // ���� �ִϸ��̼� �ؽ�

    void Start()
    {
        animator = GetComponent<Animator>();    // �ִϸ����� ������Ʈ ��������
        rigid = GetComponent<Rigidbody2D>();    // ������ٵ� ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();    // ��������Ʈ ������ ������Ʈ ��������
        cinemachine = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();   // �ó׸ӽ� ������Ʈ ��������
        StateMachineInit(); // ���� �ӽ� �ʱ�ȭ

        defaultGravityScale = rigid.gravityScale;   // �⺻ �߷� ������ ����
    }

    private void StateMachineInit()
    {
        stateMachine = new StateMachine();  // ���� �ӽ� �ν��Ͻ� ����
        stateMachine.stateDic.Add(EState.Idle, new Player_Idle(this));  // ��� ���� �߰�
        stateMachine.stateDic.Add(EState.Walk, new Player_Walk(this));  // �ȱ� ���� �߰�
        stateMachine.stateDic.Add(EState.Jump, new Player_Jump(this));  // ���� ���� �߰�
        stateMachine.CurState = stateMachine.stateDic[EState.Idle]; // �ʱ� ���¸� ��� ���·� ����
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");   // ���� �̵��� ���� �Է� �� ��������

        // �����̽��ٸ� ���� �� �ڷ�ƾ ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpChargeCoroutine == null && isGrounded)
        {
            jumpChargeCoroutine = StartCoroutine(JumpChargeRoutine());  // ���� ���� �ڷ�ƾ ����
            isJumped = true;    // ���� ���θ� true�� ����
        }
        // �����̽��ٸ� ���� �ڷ�ƾ ���� �� ���� ����
        else if (Input.GetKeyUp(KeyCode.Space) && jumpChargeCoroutine != null)  // �����̽��ٸ� ����
        {
            StopCoroutine(jumpChargeCoroutine); // ���� ���� �ڷ�ƾ ����
            jumpChargeCoroutine = null; // �ڷ�ƾ ���� �ʱ�ȭ
            Jump(jumpChargeTime);   // ���� ����
            jumpChargeTime = 0f;    // ���� ���� �ð� �ʱ�ȭ
        }

        if (rigid.velocity.y < 0) // �ϰ� ��
        {
            rigid.gravityScale = fallingGravityScale;   // �ϰ� �� �߷� ������ ����
        }
        else // ��� ��
        {
            rigid.gravityScale = defaultGravityScale;   // �⺻ �߷� ������ ����
        }

        stateMachine.Update();  // ���� �ӽ� ������Ʈ


    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate(); // ���� �ӽ��� ���� ������Ʈ
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // �ٴڿ� ����� ��
        {
            isGrounded = true;  // �ٴڿ� ������� ǥ��
        }
    }

    IEnumerator JumpChargeRoutine()
    {
        jumpChargeTime = 0f;    // ���� ���� �ð� �ʱ�ȭ
        while (true)
        {
            jumpChargeTime += Time.deltaTime;   // ���� ���� �ð� ����
            yield return null;

            //if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    break;
            //}
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
