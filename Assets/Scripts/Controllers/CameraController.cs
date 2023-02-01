using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField]
    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }
    void Start()
    {
        
    }

    void LateUpdate() //playerController가 실행된 이후에 실행되게 하는 코드임(카메라가 떨리는 현상 없어짐)
    {
        if(_mode == Define.CameraMode.QuarterView)
        {
            if(_player.IsVaild() == false)//Extension에서 gameObject가 삭제된 경우 처리
            {
                //더이상 component들고 있으면 안됨
                return;
            }
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
