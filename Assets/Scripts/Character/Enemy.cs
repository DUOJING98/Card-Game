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
        // プレイヤーターン開始時に敵の行動をランダムに決定する
        // actionDataSo.actions のリストからランダムに1つのアクションを選択し
        var randomIndex = Random.Range(0, actionDataSo.actions.Count);
        // そのアクションを現在の行動（currentAction）として設定し、後の実行または意図表示に使用する
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


    //アニメーションと実行効果を同期させる
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
