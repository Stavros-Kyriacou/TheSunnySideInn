using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlackEvent : MonoBehaviour
{
    [SerializeField] private RectTransform fadeToBlack;
    
    public void DisableFadeToBlack()
    {
        this.fadeToBlack.gameObject.SetActive(false);
    }
}
