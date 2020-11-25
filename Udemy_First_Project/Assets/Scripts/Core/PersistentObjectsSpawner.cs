using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        //using static instead of singleton
        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned)
            {
                return;
            }
            else
            {
                SpawnPersistentObjects();
                hasSpawned = true;
            }
        }
        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
