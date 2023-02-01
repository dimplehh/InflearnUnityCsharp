using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;
    [SerializeField]
    float _scanRange = 10;
    [SerializeField]
    float _attackRange = 2;//공격사정거리
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)//components하면 안됨
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();//Manager에서 갖고오기(기존보다 성능향상)
        if (player == null)
            return;

        float distance = (player.transform.position - transform.position).magnitude;
        if(distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);//attack시작하면 player을 밀지 않도록
                State = Define.State.Skill;
                return;
            }
        }
        //이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);//내가 갈 상대 목표를 지정(nevmesh가 목표로 길을 찾아감)
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if(_lockTarget != null)
        {
            //체력 깎는 부분
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
            if(targetStat.Hp > 0)
            {
                //멀어지지 않았으면 또 때리고 아니면 돌아가기
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }    
        else
        {

        }
    }
}
