using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManagers
{
    public Action KeyAction = null; //리스너 패턴
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;

    public void OnUpdate() //루트가 한번만 돌게 되는 장점. 이벤트 방식으로 처리 시 break point로 누가 실행되는지 체크 가능
    {
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if(Input.GetMouseButton(0))//마우스왼버튼클릭
            {
                if(!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if(Time.time <= _pressedTime + 0.2f) //떼고 나서 0.2초정도 지나면 클릭으로 인정하겠다.
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
