using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIGameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Button closeBtn;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(OnHide);
    }

    void OnHide()
    {
        Destroy(gameObject);
    }
}
