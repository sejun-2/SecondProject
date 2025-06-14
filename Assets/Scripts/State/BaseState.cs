using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public bool HasPhysics; // 물리적 상호작용을 위한 변수

    // 상태가 시작될때.
    public abstract void Enter();
    
    // 해당 상태에서 동작을 담당
    public abstract void Update();
    // 사용하지 않는 상태들도 있기때문에 가상함수로 선언.
    public virtual void FixedUpdate() { }   // 기본적으로 아무것도 하지 않음
    
    // 상태가 끝날때.
    public abstract void Exit();
}

public enum EState
{
    Idle,Walk,Jump,Patrol
}
