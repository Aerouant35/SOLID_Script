using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throbbing : MonoBehaviour
{
    #region variables
    private RectTransform m_rtThobberImg;
    
    [SerializeField] private float m_fRotateSpeed;
    #endregion
    
    // Start is called before the first frame update
    private void Start()
    {
        m_rtThobberImg = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_rtThobberImg.Rotate(0f, 0f, m_fRotateSpeed * Time.unscaledDeltaTime);
    }
}
