using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIHealthBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static UIHealthBar Instance { get; private set; }
    
    public Image mask;
    public TextMeshProUGUI hpText;

    private float _width;
    private float _hp;
    private float _maxHp;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _width = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float hp, float maxHp)
    {
        _hp = hp;
        _maxHp = maxHp;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (_hp/_maxHp) * _width);
    }

    public void ShowHpText()
    {
        hpText.gameObject.SetActive(true);
        hpText.text = $"{(int)_hp}/{(int)_maxHp}";
    }

    public void HideHpText()
    {
        hpText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHpText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideHpText();
    }
}
