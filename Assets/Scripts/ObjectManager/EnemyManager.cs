using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefaultNamespace;
using Entities;
using UnityEngine;

namespace ObjectManager
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }
        private Dictionary<EnemyType, EnemyEntity> _enemyTable;
        public GameObject[] enemyObjects;
        public GameObject target;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            _enemyTable = new Dictionary<EnemyType, EnemyEntity>();
            LoadEnemiesFromFiles();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        
        }

        public void GenerateEnemy(int id, Vector2 position)
        {
            GameObject enemy = Instantiate(enemyObjects[id], position, Quaternion.identity);
            EnemyController _controller = enemy.GetComponent<EnemyController>();
            if (_controller != null)
            {
                enemy.SetActive(true);
                _controller.SetEntity(_enemyTable[_controller.type]);
                // Add Loot
                _controller.AddLoot(ItemType.ExpOrb, 1.0f, 80.0f);
            
            }
        
        }

        public List<GameObject> FindEnemies(int num)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("AliveEnemy");
            List<GameObject> sortedEnemies = enemies.OrderBy(EnemyDistance).ToList();
            if (num > sortedEnemies.Count) return sortedEnemies; 
            return sortedEnemies.GetRange(0, num);
        }

        float EnemyDistance(GameObject enemy)
        {
            return (enemy.transform.position - target.transform.position).magnitude;
        }

        public void LoadEnemiesFromFiles()
        {
            foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
            {
                EnemyEntity template =
                    EnemyEntity.ImportFromXML(Path.Combine(Application.dataPath, $"Resources/Data/Enemies/{type.ToString()}.xml"));
                _enemyTable[type] = template;
            }
        }
    }
}
