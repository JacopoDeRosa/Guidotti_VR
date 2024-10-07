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
            yield return null;
        }
    }
}