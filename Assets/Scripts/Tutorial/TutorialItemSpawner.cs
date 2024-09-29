using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tutorial
{
    public class TutorialItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab;
        
        private GameObject _spawnedItem;
        
        
        
        public void SpawnItem()
        {
            StartCoroutine(SpawnItemRoutine());
        }
        
        private IEnumerator SpawnItemRoutine()
        {
            _spawnedItem = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            
            _spawnedItem.transform.localScale = Vector3.zero;
            
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                _spawnedItem.transform.localScale = Vector3.one * t;
                yield return null;
            }
            
        }
    }
}