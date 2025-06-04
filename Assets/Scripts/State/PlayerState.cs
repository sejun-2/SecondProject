using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    protected Player player; // �÷��̾� ��ũ��Ʈ -> �� �ȿ����� ����ϱ� ���� protected�� ����

    public PlayerState(Player _player)
    {
        player = _player; // �÷��̾� ��ũ��Ʈ �ʱ�ȭ
    }
    public override void Enter() { }
    public override void Update() 
    {
        if (player.isJumped && player.isGrounded) // ������ �����Ǹ�
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Jump]); // ���� ���·� ����
    }
    public override void Exit() { }
}

public class Player_Idle : PlayerState
{
    public Player_Idle(Player _player) : base(_player)  //�����ڿ��� �θ� Ŭ���� �ʱ�ȭ
    {
        HasPhysics = false; // ������ ��ȣ�ۿ��� �ʿ� �����Ƿ� false�� ����
    }

    public override void Enter()
    {
        player.animator.Play(player.IDLE_HASH); // ��� �ִϸ��̼� ����
        player.rigid.velocity = Vector2.zero;   // �÷��̾��� �ӵ��� 0���� �ʱ�ȭ
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.inputX) > 0.1f) // Mathf.Abs���밪 ��ȭ�� ���� �̵��� �����Ǹ�
        {
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Walk]); // �ȱ� ���·� ����
        }
    }
    public override void Exit() { }
}

public class Player_Walk : PlayerState
{
    public Player_Walk(Player _player) : base(_player) // �����ڿ��� �θ� Ŭ���� �ʱ�ȭ
    {
        HasPhysics = true; // ������ ��ȣ�ۿ��� �ʿ���
    }
    public override void Enter()
    {
        player.animator.Play(player.WALK_HASH); // �ȱ� �ִϸ��̼� ����
    }
    public override void Update()
    {
        base.Update();
        // Idle ����.
        if (Mathf.Abs(player.inputX) < 0.1f) // ���� �̵��� �������� ������
        {
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Idle]); // ��� ���·� ����
        }
        // ���� ������ ���� ��ȯ.
        if (player.inputX < 0) // �������� �̵�
        {
            player.spriteRenderer.flipX = true; // ��������Ʈ ���� ��ȯ
            player.cinemachine.m_TrackedObjectOffset = new Vector3(-10, 2, 0); // ī�޶� ��ġ ����
        }
        else// ���������� �̵�
        {
            player.spriteRenderer.flipX = false; // ��������Ʈ ���� ��ȯ
            player.cinemachine.m_TrackedObjectOffset = new Vector3(10, 2, 0); // ī�޶� ��ġ ����
        }
    }
    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.inputX * player.moveSpeed, player.rigid.velocity.y); // ���� �̵�
    }
    public override void Exit() { }
}

public class Player_Jump : PlayerState
{
    public Player_Jump(Player _player) : base(_player)  // �����ڿ��� �θ� Ŭ���� �ʱ�ȭ
    {
        HasPhysics = true; // ������ ��ȣ�ۿ��� �ʿ���
    }

    public override void Enter()
    {
        player.animator.Play(player.JUMP_HASH);// ���� �ִϸ��̼� ����
        //player.rigid.AddForce(Vector2.up * player.jumpSpeed, ForceMode2D.Impulse);
        player.isGrounded = false;  // ���� ���� �ʱ�ȭ
        player.isJumped = false;    // �ٴڿ� ���� ����
    }

    public override void Update()
    {
        if (player.isGrounded) // �ٴڿ� ���������
            player.stateMachine.ChangeState(player.stateMachine.stateDic[EState.Idle]); // ��� ���·� ����

        if (player.inputX < 0) // �������� �̵�
        {
            player.spriteRenderer.flipX = true; // ��������Ʈ ���� ��ȯ
            player.cinemachine.m_TrackedObjectOffset = new Vector3(-10, 2, 0); // ī�޶� ��ġ ����
        }
        else if (player.inputX > 0) // ���� else �� ���, inputX�� 0�϶��� �ش� �������� ��.
        {
            player.spriteRenderer.flipX = false; // ��������Ʈ ���� ��ȯ
            player.cinemachine.m_TrackedObjectOffset = new Vector3(10, 2, 0);   // ī�޶� ��ġ ����
        }
    }

    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.inputX * player.moveSpeed, player.rigid.velocity.y);
    }

}
