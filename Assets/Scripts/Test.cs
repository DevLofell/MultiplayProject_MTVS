using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public InputField input_test;
    

    void Start()
    {
        input_test.onSubmit.AddListener(TestInput);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            input_test.OnPointerClick(new PointerEventData(EventSystem.current));
        }


        print(EventSystem.current.alreadySelecting);

        GameObject selection = EventSystem.current.currentSelectedGameObject;
        string result = selection == null ? "Null" : selection.name;


        print(result);
    }

    void TestInput(string s)
    {
        input_test.text = "";
        EventSystem.current.SetSelectedGameObject(null);
    }
}
