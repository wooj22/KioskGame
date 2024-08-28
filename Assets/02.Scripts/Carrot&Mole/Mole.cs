using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public int moleCarrotEatingTime;
    public int moleLife;
    public int moleHitCount;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� ����
        if (other.tag == "Player")
        {
            moleHitCount++;
            if(moleHitCount >= moleLife)
            {
                moleHitCount = 0;
                this.gameObject.SetActive(false);
            }
        }

        // ��ٸԱ�/ �ٽõ���
        if(other.tag == "Carrot")
        {
            StartCoroutine(EatingCarrot(other.gameObject));
        }
    }

    private IEnumerator EatingCarrot(GameObject carrot)
    {
        yield return new WaitForSeconds(moleCarrotEatingTime);

        if (this.gameObject.activeSelf)
        {
            carrot.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
