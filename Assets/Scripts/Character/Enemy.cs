using System.Collections;
using UnityEngine;

public class Enemy : CharacterBse
{
    public EnemyActionDataSo actionDataSo;

    public EnemyAction currentAction;

    protected Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void OnPlayerTurnBegin()
    {
        // �ץ쥤��`���`���_ʼ�r�˔����ЄӤ������˛Q������
        // actionDataSo.actions �Υꥹ�Ȥ���������1�ĤΥ����������x�k��
        var randomIndex = Random.Range(0, actionDataSo.actions.Count);
        // ���Υ���������F�ڤ��Єӣ�currentAction���Ȥ����O��������Όg�Фޤ�������ʾ��ʹ�ä���
        currentAction = actionDataSo.actions[randomIndex];
    }

    public virtual void OnEnemyTurnBegin()
    {
        switch (currentAction.effect.targetType)
        {
            case EffectTargetType.Self:
                Skill();
                break;
            case EffectTargetType.Target:
                Attack();
                break;
        }
    }

    public virtual void Skill()
    {
        //animator.SetTrigger("skill");
        //currentAction.effect.Execute(this, this);
        StartCoroutine(DelayAction("skill"));

    }

    public virtual void Attack()
    {
        //animator.SetTrigger("attack");
        //currentAction.effect.Execute(this, player);
        StartCoroutine(DelayAction("attack"));
    }


    //���˥�`�����Ȍg�Є�����ͬ�ڤ�����
    IEnumerator DelayAction(string actionName)
    {
        animator.SetTrigger(actionName);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f
                                        && !animator.IsInTransition(0)
                                        && animator.GetCurrentAnimatorStateInfo(0).IsName(actionName));
        if (actionName == "attack")
            currentAction.effect.Execute(this, player);
        else
            currentAction.effect.Execute(this, this);
    }
}
