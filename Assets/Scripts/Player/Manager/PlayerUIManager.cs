using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* PlayerにHPのUI実装
 * ・HPの作成
 * ・コードからHPゲージの変更：HPを取得/変更
*/
public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.hp;
        hpSlider.value = playerManager.hp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }

}