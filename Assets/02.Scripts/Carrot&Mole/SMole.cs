using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMole : MonoBehaviour
{
    public int sMoleCarrotEatingTime;   // 3초

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어에게 밟힘
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }

        // 당근먹기/ 다시들어가기
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
