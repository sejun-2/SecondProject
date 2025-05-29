using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public bool HasPhysics;

    // ���°� ���۵ɶ�.
    public abstract void Enter();
    
    // �ش� ���¿��� ������ ���
    public abstract void Update();
    // ������� �ʴ� ���µ鵵 �ֱ⶧���� �����Լ��� ����.
    public virtual void FixedUpdate() { }
    
    // ���°� ������.
    public abstract void Exit();
}

public enum EState
{
    Idle,Walk,Jump,Patrol
}
