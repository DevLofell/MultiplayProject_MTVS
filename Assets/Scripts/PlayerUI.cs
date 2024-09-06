using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text nickName;
    [SerializeField] Slider hpBar;
    [SerializeField] Image fillImage;

    private void Start()
    {
        hpBar.value = 1.0f;
    }

    private void Update()
    {
        // 항상 메인 카메라에 보이도록 회전 처리한다.
        transform.forward = Camera.main.transform.forward;
    }

    // 닉네임 값과 컬러를 지정하는 함수
    public void SetNickName(string name, Color hpColor)
    {
        nickName.text = name;
        nickName.color = hpColor;
    }

    // 슬라이더의 컬러를 지정하는 함수
    public void SetHpColor(Color hpColor)
    {
        fillImage.color = hpColor;
    }

    // 체력바를 조정하는 함수
    public void SetHPValue(float curHP, float maxHP)
    {
        hpBar.value = curHP / maxHP;
    }
}
