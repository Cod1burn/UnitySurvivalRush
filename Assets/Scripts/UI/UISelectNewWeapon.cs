using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISelectNewWeapon: MonoBehaviour
    {
        public static UISelectNewWeapon Instance { get; private set; }
        public GameObject menu;

        void Awake()
        {
            Instance = this;
        }
    }
}