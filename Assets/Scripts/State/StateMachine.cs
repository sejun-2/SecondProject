using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // ���¸� ������ ���� �� �Դϴ�.
    public Dictionary<EState, BaseState> stateDic;
    // �� ���¸� �޾Ƽ� ���ǿ� ���� ���¸� ���̽��� �ٰ��Դϴ�.
    public BaseState CurState;
    public StateMachine()
    {
        stateDic = new Dictionary<EState, BaseState>();
    }

    public void ChangeState(BaseState changedState)
    {
        if (CurState == changedState)
            return;

        CurState.Exit(); // �ٲ�� ���� ���� ���¿��� Ż��.
        CurState = changedState;
        CurState.Enter(); // �ٲ� ���� ����.
    }
    // �� ������ Enter, Update, Exit...�� ��������ٰ��Դϴ�.

    public void Update() => CurState.Update();

    public void FixedUpdate()
    {
        if(CurState.HasPhysics)
        CurState.FixedUpdate();
    }
}
