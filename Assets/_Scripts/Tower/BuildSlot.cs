using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public GameObject towerPrefab;// 防御塔预制体
    public int towerCost = 20; // 防御塔价格

    private bool occupied = false;// 是否已建造防御塔

    void OnMouseDown()
    {
        if(occupied)
        {
            return;
        }

        if (!GameManager.Instance.SendMoney(towerCost))
        {
            Debug.Log("金币不足，无法建造防御塔");
            return;
        }

        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        occupied = true;
        Debug.Log("建造成功，剩余金币: " + GameManager.Instance.playerMoney);
    }
}
