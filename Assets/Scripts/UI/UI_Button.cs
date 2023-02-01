using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup //UI_Base 를 상속
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject, //이름만 프리팹에 있는 것과 잘 맞춰주면 됨
    }

    enum Images
    {
        Icon,
    }

    public override void Init()
    {

        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.Icon).gameObject;

        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; });
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });

    }

    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}
