using System;
using System.Collections;
using System.Collections.Generic;
using ObjectManager;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIEXPBar : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler
{
    public static UIEXPBar Instance { get; private set; }

    public Image mask;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;

    private float _maxExp;
    private float _exp;
    
    private float _speed = 1.0f;
    
    private float _target;
    
    private float _current;

    private int _currentLevel = 1;

    private int _targetLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        _exp = 0.0f;
        _maxExp = 100.0f;
        _target = 0.0f;
        _current = 0.0f;
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_targetLevel > _currentLevel)
        {
            float deltaProgress = Time.deltaTime * _speed;
            _current += deltaProgress;
            if (_current > 1.0f)
            {
                _current -= 1.0f;
                _currentLevel += 1;
            }
            levelText.text = _currentLevel.ToString();
            if (_currentLevel >= 10) levelText.fontSize = 45;
        }
        else if (_current < _target)
        {
            float deltaProgress = Mathf.Min(Time.deltaTime * _speed, _target - _current);
            _current += deltaProgress;
        }
        mask.fillAmount = _current;
    }

    public void SetProgress(float exp, float maxExp, int level)
    {
        _exp = exp;
        _maxExp = maxExp;
        _target = exp/maxExp;
        _targetLevel = level;
    }

    public void ShowExpText()
    {
        expText.gameObject.SetActive(true);
        expText.text = $"{(int)_exp}/{(int)_maxExp}";
    }

    public void HideExpText()
    {
        expText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowExpText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideExpText();
    }
}
