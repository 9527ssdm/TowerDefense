using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public TowerData towerData;
    private AudioSource buildSource;
    public AudioClip buildAudio;
    // 是否已建造防御塔
    private bool occupied = false;

    private void Awake()
    {
        buildSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        //如果已经建造了防御塔，就不允许再次建造
        if (occupied)
        {
            return;
        }
        // 检查玩家是否有足够的金币
        if (!GameManager.Instance.SendMoney(towerData.cost))
        {
            Debug.Log("金币不足，无法建造防御塔");
            return;
        }
        // 在建造位置生成防御塔
        Instantiate(towerData.prefab, transform.position, Quaternion.identity);

        if(buildAudio!=null)
        {
            buildSource.PlayOneShot(buildAudio);
        }
        //表示该位置已经建造了防御塔
        occupied = true;
        Debug.Log("建造成功，剩余金币: " + GameManager.Instance.playerMoney);
    }
}
