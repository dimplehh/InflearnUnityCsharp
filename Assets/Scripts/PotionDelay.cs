using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDelay : MonoBehaviour
{
    public bool isDelay;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isDelay == false)
            {
                isDelay = true;
                Debug.Log("HP�� 50 ȸ�� �Ǿ����ϴ�.");
                StartCoroutine(Drink());
            }
            else
            {
                Debug.Log("���� ������ ����� �� �����ϴ�.");
            }
        }
    }

    IEnumerator Drink()
    {
        yield return new WaitForSeconds(5.0f);
        isDelay = false;
    }
}
