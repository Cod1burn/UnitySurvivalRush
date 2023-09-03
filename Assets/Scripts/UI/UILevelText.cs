using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UILevelText : MonoBehaviour
{
    public static UILevelText Instance { get; private set; }

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(int level)
    {
        text.text = level.ToString();
    }
}
