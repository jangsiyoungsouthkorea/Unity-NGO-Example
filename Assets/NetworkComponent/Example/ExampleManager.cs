using System;
using Unity.Netcode;
using UnityEngine;

namespace NetworkComponent.Example
{
    public class ExampleManager : NetworkBehaviour
    {
        private NetworkList<NetworkComponent<ExampleNetworkBehaviour>> m_examples;

        private void Awake()
        {
            // NetworkList 초기화 (Initialize NetworkList)
            m_examples = new NetworkList<NetworkComponent<ExampleNetworkBehaviour>>();

            // 리스트 변경 이벤트 핸들러 추가 (Add list change event handler)
            m_examples.OnListChanged += @event =>
            {
                if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.Add)
                {
                    Debug.Log("On Examples is added");
                }
                else if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.Insert)
                {
                    Debug.Log("On Examples is inserted");
                }
                else if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.Remove)
                {
                    Debug.Log("On Examples is removed");
                }
                else if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.RemoveAt)
                {
                    Debug.Log("On Examples is removedAt");
                }
                else if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.Value)
                {
                    Debug.Log("On Examples is valueChanged");
                }
                else if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.Clear)
                {
                    Debug.Log("On Examples is cleared");
                }
                else if (@event.Type == NetworkListEvent<NetworkComponent<ExampleNetworkBehaviour>>.EventType.Full)
                {
                    // 클라이언트가 연결되었을 때 호출됩니다. (Called when client connected)
                    Debug.Log("On Examples is fullRefresh");
                }
            };
        }

        private void Start()
        {
            // 호스트 시작 (Start the host)
            NetworkManager.Singleton.StartHost();
        }

        public ExampleNetworkBehaviour example;
        
        [Header("Add")]
        public bool add;

        [Header("Insert")] 
        public int insertIndex;
        public bool insert;
        
        [Header("Remove")]
        public ExampleNetworkBehaviour removeExample;
        public bool remove;

        [Header("RemoveAt")]
        public int removeIndex;
        public bool removeAt;

        [Header("Value")]
        public int valueIndex;
        public bool value;
        
        [Header("Clear")]
        public bool clear;
        
        private void OnValidate()
        {
            // Add 예제 (Add example)
            if (add)
            {
                var instance = Instantiate(example);
                instance.NetworkObject.Spawn();
                m_examples.Add(instance);
                add = false;
            }
            
            // Remove 예제 (Remove example)
            if (remove)
            {
                if (removeExample != null)
                {
                    m_examples.Remove(removeExample);
                }

                remove = false;
                removeExample = null;
            }

            // RemoveAt 예제 (RemoveAt example)
            if (removeAt)
            {
                m_examples.RemoveAt(removeIndex);
                removeAt = false;
            }

            // Insert 예제 (Insert example)
            if (insert)
            {
                var instance = Instantiate(example);
                instance.NetworkObject.Spawn();
                m_examples.Insert(insertIndex, instance);
                insert = false;
            }
            
            // Clear 예제 (Clear example)
            if (clear)
            {
                m_examples.Clear();
                clear = false;
            }

            // Value 변경 예제 (Change value example)
            if (value)
            {
                var instance = Instantiate(example);
                instance.NetworkObject.Spawn();
                m_examples[valueIndex] = new NetworkComponent<ExampleNetworkBehaviour>(instance);
                value = false;
            }
        }
        
        public override void OnDestroy()
        {
            // NetworkList를 해제합니다. (Dispose of the NetworkList)
            m_examples.Dispose();
        }
    }
}
