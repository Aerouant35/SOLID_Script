using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    public void PlayAnimationEventOnParent(string m_strFunctionName)
    {
        if(m_strFunctionName != null)
        {
            transform.parent.SendMessage(m_strFunctionName);
        }
    }

    //TODO : Same but with others parameters

    //for laters ( parameters )
    //    [SerializeField] float m_float;
    //    [SerializeField] int m_nbInt;
    //    [SerializeField] string m_strString;
    //    [SerializeField] Object m_objObject;
}
