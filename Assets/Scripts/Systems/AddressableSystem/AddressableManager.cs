using System;
using System.Collections.Generic;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Systems.AddressableSystem
{
    public class AddressableManager : Singleton<AddressableManager>
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handleCache = new();
        
        private readonly Dictionary<string, ScriptableObject> _soCache = new();

        public void LoadScriptableObject<T>(string key, Action<T> callback) where T : ScriptableObject
        {
            if (_soCache.TryGetValue(key, out ScriptableObject cachedSO))
            {
                callback?.Invoke((T)cachedSO);
                return;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    _soCache[key] = op.Result;
                    _handleCache[key] = op;
                    callback?.Invoke(op.Result);
                }
                else
                {
                    Debug.LogError($"Failed to load ScriptableObject with key: {key}");
                }
            };
        }

        public void LoadAsset<T>(string key, Action<T> callback) where T : UnityEngine.Object
        {
            if (_handleCache.TryGetValue(key, out AsyncOperationHandle existingHandle))
            {
                callback?.Invoke(existingHandle.Result as T);
                return;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    _handleCache[key] = op;
                    callback?.Invoke(op.Result);
                }
                else
                {
                    Debug.LogError($"Failed to load asset with key: {key}");
                }
            };
        }

        public void Release(string key)
        {
            if (_handleCache.TryGetValue(key, out AsyncOperationHandle handle))
            {
                Addressables.Release(handle);
                _handleCache.Remove(key);
            }

            if (_soCache.ContainsKey(key))
            {
                _soCache.Remove(key);
            }
        }

        public void ReleaseAll()
        {
            foreach (AsyncOperationHandle handle in _handleCache.Values)
            {
                Addressables.Release(handle);
            }
            
            _handleCache.Clear();
            _soCache.Clear();
        }
    }
}
