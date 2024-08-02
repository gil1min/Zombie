using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    private Animator anim;
    public Transform text;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Hide();
    }

    void Hide()
    {
        anim.enabled = false;
    }

    public void Show()
    {
        text.gameObject.SetActive(true);
        anim.enabled = true;
    }
}
