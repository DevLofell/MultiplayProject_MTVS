using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public InputField input_test;
    public Image testImg;

    void Start()
    {
        input_test.onSubmit.AddListener(TestInput);
    }

    
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Tab))
        //{
        //    input_test.OnPointerClick(new PointerEventData(EventSystem.current));
        //}
        
        //GameObject selection = EventSystem.current.currentSelectedGameObject;
        //string result = selection == null ? "Null" : selection.name;

        //if(EventSystem.current.IsPointerOverGameObject())
        //{
        //    print("UI ��� ���� ���콺 Ŀ���� ����");
        //}

        print(input_test.isFocused);
    }

    void TestInput(string s)
    {
        input_test.text = "";
        //EventSystem.current.SetSelectedGameObject(null);
    }
}
