using Controllers;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectManager
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance { get; private set; }

        public GameObject[] itemPool;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        void Awake()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GenerateItem(ItemType it, float[] values, Vector2 position)
        {
            GameObject item = Instantiate(itemPool[(int)it], position, Quaternion.identity);
            item.GetComponent<ItemController>().SetItemEntity(it, values);
            item.SetActive(true);
        }

        public GameObject GenerateLoot(float prob, ItemType it, params float[] values)
        {
            if (Random.value >= prob) return null;
            GameObject item = itemPool[(int)it];
            item.GetComponent<ItemController>().SetItemEntity(it, values);
            return item;
        }
    }
}
