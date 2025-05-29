using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    protected Player player;

    public PlayerState(Player _player)
    {
        player = _player;
    }
    public override void Enter() { }
    public override void Update() 
    {
        if (player.isJumped && player.isGrounded)
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Jump]);
    }
    public override void Exit() { }
}

public class Player_Idle : PlayerState
{
    public Player_Idle(Player _player) : base(_player)
    {
        HasPhysics = false;
    }

    public override void Enter()
    {
        player.animator.Play(player.IDLE_HASH);
        player.rigid.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.inputX) > 0.1f)
        {
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Walk]);
        }
    }
    public override void Exit() { }
}

public class Player_Walk : PlayerState
{
    public Player_Walk(Player _player) : base(_player)
    {
        HasPhysics = true;
    }
    public override void Enter()
    {
        player.animator.Play(player.WALK_HASH);
    }
    public override void Update()
    {
        base.Update();
        // Idle ����.
        if (Mathf.Abs(player.inputX) < 0.1f)
        {
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Idle]);
        }
        // ���� ������ ���� ��ȯ.
        if (player.inputX < 0)
        {
            player.spriteRenderer.flipX = true;
            player.cinemachine.m_TrackedObjectOffset = new Vector3(-10, 2, 0);
        }
        else
        {
            player.spriteRenderer.flipX = false;
            player.cinemachine.m_TrackedObjectOffset = new Vector3(10, 2, 0);
        }
    }
    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.inputX * player.moveSpeed, player.rigid.velocity.y);
    }
    public override void Exit() { }
}

public class Player_Jump : PlayerState
{
    public Player_Jump(Player _player) : base(_player) 
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        player.animator.Play(player.JUMP_HASH);
        player.rigid.AddForce(Vector2.up * player.jumpSpeed, ForceMode2D.Impulse);
        player.isGrounded = false;
        player.isJumped = false;
    }

    public override void Update()
    {
        if (player.isGrounded)
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Idle]);

        if (player.inputX < 0)
        {
            player.spriteRenderer.flipX = true;
            player.cinemachine.m_TrackedObjectOffset = new Vector3(-10, 2, 0);
        }
        else if (player.inputX > 0) // ���� else �� ���, inputX�� 0�϶��� �ش� �������� ��.
        {
            player.spriteRenderer.flipX = false;
            player.cinemachine.m_TrackedObjectOffset = new Vector3(10, 2, 0);
        }
    }

    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.inputX * player.moveSpeed, player.rigid.velocity.y);
    }

}
