using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;   //버추얼 카메라

    [SerializeField] public float moveSpeed; // 움직이는 스피드
    //[SerializeField] public float jumpSpeed;

    private Coroutine jumpChargeCoroutine;  // 점프 충전 코루틴
    private float jumpChargeTime;   // 점프 충전 시간
    private float minJumpForce = 5f;  // 최소 점프 힘
    private float maxJumpForce = 16f; // 최대 점프 힘

    private float fallingGravityScale = 5f; // 하강 중 중력 스케일 -> 빠르게 떨어지도록
    private float defaultGravityScale;  // 기본 중력 스케일

    public StateMachine stateMachine;   // 상태 머신 변수

    public Animator animator;   // 애니메이터 변수
    public Rigidbody2D rigid;   
    public SpriteRenderer spriteRenderer;   // 스프라이트 렌더러 변수

    public float inputX;    // 수평 이동을 위한 변수
    public bool isJumped;   // 점프 여부를 확인하는 변수
    public bool isGrounded; // 땅에 닿아 있는지 확인하는 변수
    public CinemachineFramingTransposer cinemachine; // 카메라 위치 조정 위해서 시네머신 컴포넌트 가져오기

    public readonly int IDLE_HASH = Animator.StringToHash("Idle");  // 대기 애니메이션 해시
    public readonly int WALK_HASH = Animator.StringToHash("Walk");  // 걷기 애니메이션 해시
    public readonly int JUMP_HASH = Animator.StringToHash("Jump");  // 점프 애니메이션 해시

    void Start()
    {
        animator = GetComponent<Animator>();    // 애니메이터 컴포넌트 가져오기
        rigid = GetComponent<Rigidbody2D>();    // 리지드바디 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();    // 스프라이트 렌더러 컴포넌트 가져오기
        cinemachine = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();   // 시네머신 컴포넌트 가져오기
        StateMachineInit(); // 상태 머신 초기화

        defaultGravityScale = rigid.gravityScale;   // 기본 중력 스케일 저장
    }

    private void StateMachineInit()
    {
        stateMachine = new StateMachine();  // 상태 머신 인스턴스 생성
        stateMachine.stateDic.Add(EState.Idle, new Player_Idle(this));  // 대기 상태 추가
        stateMachine.stateDic.Add(EState.Walk, new Player_Walk(this));  // 걷기 상태 추가
        stateMachine.stateDic.Add(EState.Jump, new Player_Jump(this));  // 점프 상태 추가
        stateMachine.CurState = stateMachine.stateDic[EState.Idle]; // 초기 상태를 대기 상태로 설정
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");   // 수평 이동을 위한 입력 값 가져오기

        // 스페이스바를 누를 때 코루틴 시작
        if (Input.GetKeyDown(KeyCode.Space) && jumpChargeCoroutine == null && isGrounded)
        {
            jumpChargeCoroutine = StartCoroutine(JumpChargeRoutine());  // 점프 충전 코루틴 시작
            isJumped = true;    // 점프 여부를 true로 설정
        }
        // 스페이스바를 떼면 코루틴 종료 및 점프 실행
        else if (Input.GetKeyUp(KeyCode.Space) && jumpChargeCoroutine != null)  // 스페이스바를 떼면
        {
            StopCoroutine(jumpChargeCoroutine); // 점프 충전 코루틴 중지
            jumpChargeCoroutine = null; // 코루틴 변수 초기화
            Jump(jumpChargeTime);   // 점프 실행
            jumpChargeTime = 0f;    // 점프 충전 시간 초기화
        }

        if (rigid.velocity.y < 0) // 하강 중
        {
            rigid.gravityScale = fallingGravityScale;   // 하강 중 중력 스케일 적용
        }
        else // 상승 중
        {
            rigid.gravityScale = defaultGravityScale;   // 기본 중력 스케일 적용
        }

        stateMachine.Update();  // 상태 머신 업데이트


    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate(); // 상태 머신의 물리 업데이트
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 바닥에 닿았을 때
        {
            isGrounded = true;  // 바닥에 닿았음을 표시
        }
    }

    IEnumerator JumpChargeRoutine()
    {
        jumpChargeTime = 0f;    // 점프 충전 시간 초기화
        while (true)
        {
            jumpChargeTime += Time.deltaTime;   // 점프 충전 시간 증가
            yield return null;

            //if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    break;
            //}
        }
    }

    void Jump(float chargeTime)
    {
        // 최소/최대 점프 힘으로 클램핑 (예: 누른 시간에 비례)
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, Mathf.Clamp01(chargeTime));    // 점프 힘 계산
        //float jumpForce = Mathf.Clamp(jumpChargeTime, 3f, 20f);

        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);    // 점프 힘 적용
        isGrounded = false;
        // 점프 애니메이션 실행 등 추가 가능
    }
}
