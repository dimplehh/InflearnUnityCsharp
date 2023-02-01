using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar

    }

    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);//Collider높이만큼 더해주기
        transform.rotation = Camera.main.transform.rotation;//카메라랑 똑같은 회전값을 가지도록 강제로 세팅

        float ratio = _stat.Hp / (float)_stat.MaxHp; //둘다 int면 결과를 int로 뱉으므로 하나는 float으로 형변환해줘야함
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
