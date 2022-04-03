using System;
using TMPro;
using UnityEngine;

public class ProjectilePseudo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tmpPseudo;

    public void LateUpdate()
    {
        if (Camera.main == null) return;
        
        transform.LookAt(transform.position + Camera.main.transform.forward);
        //transform.Rotate(0, 180, 0);
    }

    public void SetPseudo(string pseudo)
    {
        if (!string.IsNullOrEmpty(pseudo)) m_tmpPseudo.text = char.ToUpper(pseudo[0]) + pseudo.Substring(1);
    }
}
