using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeaponOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Weapon Weapon;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(AddWeaponToPlayer);
        }

        /// <summary>
        /// Set the related weapon to this button, with weapon icon and tooltips.
        /// The option will be hided if there is no weapon related.
        /// </summary>
        /// <param name="w"></param>
        public void SetWeapon(Weapon w)
        {
            if (w == null)
            {
                gameObject.SetActive(false);
                return;
            }
            Weapon = w;
            GetComponent<Image>().sprite = Weapon.IconSprite;
            // GetComponent<Button>().onClick.RemoveAllListeners();
            // GetComponent<Button>().onClick.AddListener(AddWeaponToPlayer);
        }

        /// <summary>
        /// Add the weapon to player's weapon list.
        /// </summary>
        public void AddWeaponToPlayer()
        {
            WeaponManager.Instance.AddWeapon(Weapon);
            UIEXPBar.Instance.SkillPoint--;
            UITooltip.Instance.HideTooltip();
            UISelectNewWeapon.Instance.HideMenu();
            UISelectNewWeapon.Instance.OptionsAssigned = false;
        }

        /// <summary>
        /// Tooltip notification
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(Weapon != null) UITooltip.Instance.SetText(Weapon.Name, Weapon.Description);
            UITooltip.Instance.ShowTooltip(transform.position + new Vector3(0.0f, -25.0f, 0.0f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UITooltip.Instance.HideTooltip();
        }
    }
}