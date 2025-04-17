
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using ProjectSystem;

namespace Pool
{
    public static class PoolInstance
    {
        private static readonly Dictionary<string, Queue<GameObject>> PoolDictionary =
            new Dictionary<string, Queue<GameObject>>(64);
        
        private static readonly Dictionary<string, Transform> ParentDictionary =
            new Dictionary<string, Transform>(64);

        private static readonly List<IPoolItem> ItemEventComponents = new List<IPoolItem>(8);
        private static readonly int DefaultPoolCapacity = 64;
        
        public static Action<GameObject> OnObjectSpawned;
        public static Action<GameObject> OnObjectDespawned;

        public static void InstallPoolItems(PoolPreset poolPreset)
        {
            ref var items = ref poolPreset.PoolItems;
            foreach (var poolItem in items)
            {
                var newPool = new Queue<GameObject>(DefaultPoolCapacity);
                var poolItemTag = poolItem.Tag;

                for (var i = 0; i < poolItem.SizePool; i++)
                    InstantiateIntoExistingPool(newPool, poolItem.Prefab, poolItemTag);

                PoolDictionary.Add(poolItemTag, newPool);
            }
        }

        public static GameObject Spawn(Component toSpawn, Vector3 position = default, Quaternion rotation = default, 
            bool checkForEvents = false)
        {
            return DefaultSpawn(toSpawn.gameObject, position, rotation, 
                null, false, checkForEvents);
        }
        
        public static GameObject Spawn(Component toSpawn, Transform parent, Quaternion rotation = default, 
            bool worldPositionStays = false, bool checkForEvents = false)
        {
            var position = parent ? parent.position : Vector3.zero;
            return DefaultSpawn(toSpawn.gameObject, position, rotation, parent, worldPositionStays, checkForEvents);
        }

        public static GameObject Spawn(GameObject toSpawn, Vector3 position = default, Quaternion rotation = default, 
            bool checkForEvents = false)
        {
            return DefaultSpawn(toSpawn, position, rotation, null, false, checkForEvents);
        }
        
        public static GameObject Spawn(GameObject toSpawn, Transform parent, Quaternion rotation = default, 
            bool worldPositionStays = false, bool checkForEvents = false)
        {
            var position = parent ? parent.position : Vector3.zero;
            return DefaultSpawn(toSpawn, position, rotation, parent, worldPositionStays, checkForEvents);
        }

        public static void Despawn(Component toDespawn, bool checkForEvents)
        {
            DefaultDespawn(toDespawn.gameObject, 0f, checkForEvents);
        }
        
        public static void Despawn(Component toDespawn, float delay = 0f, bool checkForEvents = false)
        {
            DefaultDespawn(toDespawn.gameObject, delay, checkForEvents);
        }
        
        public static void Despawn(GameObject toDespawn, bool checkForEvents)
        {
            DefaultDespawn(toDespawn.gameObject, 0f, checkForEvents);
        }

        public static void Despawn(GameObject toDespawn, float delay = 0f, bool checkForEvents = false)
        {
            DefaultDespawn(toDespawn, delay, checkForEvents);
        }

        public static async void DespawnAllThese(GameObject toDespawn, float delay = 0f, bool checkForEvents = false)
        {
            if (delay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(delay));

            var prefabName = toDespawn.name;
            var pool = PoolDictionary[prefabName].ToArray();
            
            foreach (var item in pool) 
                DefaultDespawn(item, 0, checkForEvents);
        }

        public static async void DespawnAll(float delay = 0f, bool checkForEvents = false)
        {
            if (delay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            foreach (var part in PoolDictionary)
            {
                var pool = part.Value.ToArray();
                
                foreach (var item in pool)
                    DefaultDespawn(item, 0, checkForEvents);
            }
        }
        
        public static void Reset()
        {
            ItemEventComponents?.Clear();
            PoolDictionary?.Clear();
            ParentDictionary?.Clear();
            OnObjectSpawned?.SetNull();
            OnObjectDespawned?.SetNull();
        }
        
        private static GameObject DefaultSpawn(GameObject toSpawn, Vector3 position, Quaternion rotation, 
            Transform parent, bool worldPositionStays, bool checkForEvents)
        {
            var prefabName = toSpawn.name;
            var status = PoolDictionary.ContainsKey(prefabName);

            if (!status)
                return InstantiateGameObjectWithNewPool(
                    toSpawn, prefabName, position, rotation, parent, worldPositionStays);

            var newObject = GetDisabledObjectFromPool(toSpawn, prefabName);
            
            SetupTransform(newObject.transform, position, rotation, parent, worldPositionStays);
            CheckForSpawnEvents(newObject, checkForEvents);
            
            return newObject;
        }
        
        private static async void DefaultDespawn(GameObject toDespawn, float delay = 0f, bool checkForEvents = false)
        {
            var prefabName = toDespawn.name;
            var status = PoolDictionary.ContainsKey(prefabName);

            if (delay > 0) 
                await UniTask.Delay(TimeSpan.FromSeconds(delay));

            if (!toDespawn) return;
            
            toDespawn.SetActive(false);
            toDespawn.transform.SetParent(GetPoolParent(toDespawn.name));
            
            CheckForDespawnEvents(toDespawn, checkForEvents);

            if (status) return;

            var newPool = new Queue<GameObject>(DefaultPoolCapacity);
            newPool.Enqueue(toDespawn);
            
            PoolDictionary.Add(prefabName, newPool);
        }
        
        private static void SetupTransform(Transform transform, Vector3 position, Quaternion rotation, 
            Transform parent = null, bool worldPositionStays = false)
        {
            if (parent) transform.SetParent(parent, worldPositionStays);
            transform.SetPositionAndRotation(position, rotation);
        }
        
        private static void CheckForSpawnEvents(GameObject toCheck, bool allow)
        {
            OnObjectSpawned?.Invoke(toCheck);
            
            if (!allow) return;

            toCheck.GetComponentsInChildren(ItemEventComponents);
            foreach (var eventComponent in ItemEventComponents) eventComponent?.OnSpawn();
        }
        
        private static void CheckForDespawnEvents(GameObject toCheck, bool allow)
        {
            OnObjectDespawned?.Invoke(toCheck);
            
            if (!allow) return;

            toCheck.GetComponentsInChildren(ItemEventComponents);
            foreach (var eventComponent in ItemEventComponents) eventComponent?.OnDespawn();
        }

        private static GameObject InstantiateGameObjectWithNewPool(GameObject toSpawn, string name, Vector3 position, 
            Quaternion rotation, Transform parent = null, bool worldPositionStays = false)
        {
            var newPool = new Queue<GameObject>(DefaultPoolCapacity);
            var newPoolItemObject = InstantiateIntoExistingPool(newPool, toSpawn, name, true);
            
            SetupTransform(newPoolItemObject.transform, position, rotation, parent, worldPositionStays);

            PoolDictionary.Add(name, newPool);
            OnObjectSpawned?.Invoke(newPoolItemObject);
                
            return newPoolItemObject;
        }

        private static GameObject GetDisabledObjectFromPool(GameObject toSpawn, string prefabName, bool active = true)
        {
            var pool = PoolDictionary[prefabName];
            var poolItems = pool.ToArray();
            
            foreach (var freeObject in poolItems)
                if (freeObject) if (!freeObject.activeInHierarchy)
                {
                    pool.Enqueue(freeObject);
                    freeObject.SetActive(active);
                    return freeObject;
                }
            
            return InstantiateIntoExistingPool(PoolDictionary[prefabName], toSpawn, prefabName, active);
        }
        
        private static GameObject InstantiateIntoExistingPool(
            Queue<GameObject> pool, GameObject toSpawn, string prefabName, bool active = false)
        {
            var newObject =
                Object.Instantiate(toSpawn, GetPoolParent(prefabName));
                    
            newObject.name = prefabName;
            newObject.SetActive(active);
            pool.Enqueue(newObject);

            return newObject;
        }
        
        private static Transform GetPoolParent(string prefabName)
        {
            return ParentDictionary.ContainsKey(prefabName) ? 
                ParentDictionary[prefabName] : NewPoolParent(prefabName);
        }

        private static Transform NewPoolParent(string prefabName)
        {
            var poolParent =
                new GameObject("[NightPool] " + prefabName).transform;
            
            ParentDictionary.Add(prefabName, poolParent);
            
            return poolParent;
        }
    }
}