using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T:UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)//그냥 gameObject찾아주는 코드
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T:UnityEngine.Object
    {
        if (go == null)
            return null;
        if (recursive == false) //직속자식만 찾는 경우
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);//i번째 자식 찾을것
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
}
