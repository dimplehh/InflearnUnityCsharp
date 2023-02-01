using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    //stack 구조로 popup 관리
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;
    //popup이라는 컴포넌트를 들고 있는게 GameObject를 들고있는거보다 깔끔하니 이렇게 설정

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

public void SetCanvas(GameObject go, bool sort = true)//기존 UI와 우선순위 정하는 역할
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;//캔버스 랜더모드랑 카메라 설정해주기
            
        return Utils.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene //UI_Popup상속받는 T로만 (1.팝업 나타나게 하는 함수)
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Utils.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);
        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T: UI_Popup //UI_Popup상속받는 T로만 (1.팝업 나타나게 하는 함수)
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utils.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup) //안전빵 버전 (2.팝업 닫는 함수)
    {
        if (_popupStack.Count == 0)
            return;
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI() //(3.팝업 여는 함수)
    {
        if (_popupStack.Count == 0)
            return;
        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()//(4.팝업창 싹다 지우기)
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
