using System;
using UnityEngine;

public class BaseView : MonoBehaviour//, IButtonClick
{
    public void ClickButton(ButtonType state)
    {
        if(state.name.ToString()=="Start")
        Debug.Log(state.name.ToString());
    //    throw new NotImplementedException();
    }
}