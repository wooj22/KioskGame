using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMole : MonoBehaviour
{
    public int sMoleCarrotEatingTime;   // 3��

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� ����
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }

        // ��ٸԱ�/ �ٽõ���
        if (other.tag == "Carrot")
        {
            StartCoroutine(EatingCarrot(other.gameObject));
        }
    }

    private IEnumerator EatingCarrot(GameObject carrot)
    {
        yield return new WaitForSeconds(sMoleCarrotEatingTime);

        if (this.gameObject.activeSelf)
        {
            carrot.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
