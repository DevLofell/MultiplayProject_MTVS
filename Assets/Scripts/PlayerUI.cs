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
        // �׻� ���� ī�޶� ���̵��� ȸ�� ó���Ѵ�.
        transform.forward = Camera.main.transform.forward;
    }

    // �г��� ���� �÷��� �����ϴ� �Լ�
    public void SetNickName(string name, Color hpColor)
    {
        nickName.text = name;
        nickName.color = hpColor;
    }

    // �����̴��� �÷��� �����ϴ� �Լ�
    public void SetHpColor(Color hpColor)
    {
        fillImage.color = hpColor;
    }

    // ü�¹ٸ� �����ϴ� �Լ�
    public void SetHPValue(float curHP, float maxHP)
    {
        hpBar.value = curHP / maxHP;
    }
}
