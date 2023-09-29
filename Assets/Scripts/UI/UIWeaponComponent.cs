using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeaponComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    { 
        [NonSerialized] public Button LevelUpButton;
        [NonSerialized] public Image WeaponIcon;
        [NonSerialized] public TextMeshProUGUI LevelText;
        public Weapon Weapon_;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            Transform tempTransform = gameObject.transform.Find("LevelUpWeapon");
            if (tempTransform != null)
            {
                LevelUpButton = tempTransform.GetComponent<Button>();
                LevelUpButton.onClick.AddListener(LevelUpWeapon);
                LevelUpButton.gameObject.SetActive(false);
            }

            tempTransform = gameObject.transform.Find("Icon");
            if (tempTransform != null) WeaponIcon = tempTransform.GetComponent<Image>();

            tempTransform = gameObject.transform.Find("LevelText");
            if (tempTransform != null) LevelText = tempTransform.GetComponent<TextMeshProUGUI>();

        }

        public void SetWeapon(Weapon weapon)
        {
            Weapon_ = weapon;
            WeaponIcon.sprite = weapon.IconSprite;
            LevelText.text = weapon.Level.ToString();
        }

        private void LevelUpWeapon()
        {
            Weapon_.LevelUp();
            UIEXPBar.Instance.SkillPoint -= 1;
            LevelText.text = Weapon_.Level.ToString();
        }

        public void ShowButton()
        {
            LevelUpButton.gameObject.SetActive(true);
        }

        public void HideButton()
        {
            LevelUpButton.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (UIEXPBar.Instance.SkillPoint > 0) ShowButton();
            else HideButton();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UITooltip.Instance.SetText(Weapon_.Name, $"{Weapon_.Description} \nNext Level: {Weapon_.Tooltip}");
            UITooltip.Instance.ShowTooltip(transform.position + new Vector3(60.0f, 0.0f, 0.0f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UITooltip.Instance.HideTooltip();
        }
    }
}
