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
                Debug.Log("HP가 50 회복 되었습니다.");
                StartCoroutine(Drink());
            }
            else
            {
                Debug.Log("아직 포션을 사용할 수 없습니다.");
            }
        }
    }

    IEnumerator Drink()
    {
        yield return new WaitForSeconds(5.0f);
        isDelay = false;
    }
}
