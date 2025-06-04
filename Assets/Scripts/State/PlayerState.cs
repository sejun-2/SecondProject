using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    protected Player player; // 플레이어 스크립트 -> 이 안에서만 사용하기 위해 protected로 설정

    public PlayerState(Player _player)
    {
        player = _player; // 플레이어 스크립트 초기화
    }
    public override void Enter() { }
    public override void Update() 
    {
        if (player.isJumped && player.isGrounded) // 점프가 감지되면
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Jump]); // 점프 상태로 변경
    }
    public override void Exit() { }
}

public class Player_Idle : PlayerState
{
    public Player_Idle(Player _player) : base(_player)  //생성자에서 부모 클래스 초기화
    {
        HasPhysics = false; // 물리적 상호작용이 필요 없으므로 false로 설정
    }

    public override void Enter()
    {
        player.animator.Play(player.IDLE_HASH); // 대기 애니메이션 실행
        player.rigid.velocity = Vector2.zero;   // 플레이어의 속도를 0으로 초기화
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.inputX) > 0.1f) // Mathf.Abs절대값 변화로 수평 이동이 감지되면
        {
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Walk]); // 걷기 상태로 변경
        }
    }
    public override void Exit() { }
}

public class Player_Walk : PlayerState
{
    public Player_Walk(Player _player) : base(_player) // 생성자에서 부모 클래스 초기화
    {
        HasPhysics = true; // 물리적 상호작용이 필요함
    }
    public override void Enter()
    {
        player.animator.Play(player.WALK_HASH); // 걷기 애니메이션 실행
    }
    public override void Update()
    {
        base.Update();
        // Idle 전이.
        if (Mathf.Abs(player.inputX) < 0.1f) // 수평 이동이 감지되지 않으면
        {
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Idle]); // 대기 상태로 변경
        }
        // 왼쪽 오른쪽 방향 전환.
        if (player.inputX < 0) // 왼쪽으로 이동
        {
            player.spriteRenderer.flipX = true; // 스프라이트 방향 전환
            player.cinemachine.m_TrackedObjectOffset = new Vector3(-10, 2, 0); // 카메라 위치 조정
        }
        else// 오른쪽으로 이동
        {
            player.spriteRenderer.flipX = false; // 스프라이트 방향 전환
            player.cinemachine.m_TrackedObjectOffset = new Vector3(10, 2, 0); // 카메라 위치 조정
        }
    }
    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.inputX * player.moveSpeed, player.rigid.velocity.y); // 수평 이동
    }
    public override void Exit() { }
}

public class Player_Jump : PlayerState
{
    public Player_Jump(Player _player) : base(_player)  // 생성자에서 부모 클래스 초기화
    {
        HasPhysics = true; // 물리적 상호작용이 필요함
    }

    public override void Enter()
    {
        player.animator.Play(player.JUMP_HASH);// 점프 애니메이션 실행
        //player.rigid.AddForce(Vector2.up * player.jumpSpeed, ForceMode2D.Impulse);
        player.isGrounded = false;  // 점프 상태 초기화
        player.isJumped = false;    // 바닥에 닿지 않음
    }

    public override void Update()
    {
        if (player.isGrounded) // 바닥에 닿아있으면
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Idle]); // 대기 상태로 변경

        if (player.inputX < 0) // 왼쪽으로 이동
        {
            player.spriteRenderer.flipX = true; // 스프라이트 방향 전환
            player.cinemachine.m_TrackedObjectOffset = new Vector3(-10, 2, 0); // 카메라 위치 조정
        }
        else if (player.inputX > 0) // 기존 else 인 경우, inputX가 0일때도 해당 조건으로 들어감.
        {
            player.spriteRenderer.flipX = false; // 스프라이트 방향 전환
            player.cinemachine.m_TrackedObjectOffset = new Vector3(10, 2, 0);   // 카메라 위치 조정
        }
    }

    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.inputX * player.moveSpeed, player.rigid.velocity.y);
    }

}
