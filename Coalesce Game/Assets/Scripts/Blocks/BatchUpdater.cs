using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coalesce
{
    public class BatchUpdater : MonoBehaviour
    {
        private static Dictionary<Type, List<BatchUpdatable>> _behaviours = new Dictionary<Type, List<BatchUpdatable>>();

        private static bool _isUpdating;
        private static List<BatchUpdatable> _removeAfterUpdate = new List<BatchUpdatable>();

        public static void RegisterForUpdating<T>(BatchUpdatable updatable)
        {
            if (!_behaviours.ContainsKey(typeof(T)))
                _behaviours.Add(typeof(T), new List<BatchUpdatable>());

            _behaviours[typeof(T)].Add(updatable);
        }

        public static void UnregisterForUpdating<T>(BatchUpdatable updatable)
        {
            if (!_behaviours.ContainsKey(typeof(T)))
                return;

            if (_isUpdating)
                _removeAfterUpdate.Add(updatable);
            else
                _behaviours[typeof(T)].Remove(updatable);
        }

        public static void RequestBatchUpdatingForScene()
        {
            if (_instance != null)
                return;

            var obj = new GameObject("BatchUpdater");
            obj.AddComponent<BatchUpdater>();
        }

        private bool _notifyBatchStarting = true;

        private void Update()
        {
            if (Time.timeSinceLevelLoad < Settings.Game.BatchUpdaterTimeDelay)
                return;

            if (_notifyBatchStarting)
                NotifyBatchStarting();

            BatchUpdate();
        }

        private static void BatchUpdate()
        {
            _isUpdating = true;
            using (var ptr = _behaviours.GetEnumerator())
                while (ptr.MoveNext())
                    for (int i = 0; i < ptr.Current.Value.Count; i++)
                        ptr.Current.Value[i].BatchUpdate();
            _isUpdating = false;
            foreach (var updateable in _removeAfterUpdate)
                _behaviours[updateable.GetType()].Remove(updateable);
            _removeAfterUpdate.Clear();
        }

        private void NotifyBatchStarting()
        {
            _notifyBatchStarting = false;
            using (var ptr = _behaviours.GetEnumerator())
                while (ptr.MoveNext())
                    for (int i = 0; i < ptr.Current.Value.Count; i++)
                        ptr.Current.Value[i].BatchStarting();
        }

        #region Singleton Contract
        private static BatchUpdater _instance;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }

    public interface BatchUpdatable
    {
        void BatchStarting();
        void BatchUpdate();
    }
}
