using System;
using Unity.Netcode;
using UnityEngine;

namespace NetworkComponent
{
    /// <summary>
    /// NetworkComponent 구조체는 NetworkBehaviour을 쉽게 참조할 수 있도록 합니다.
    /// 이 구조체를 사용할 때는 다음 사항에 주의하십시오:
    /// 1. NetworkManager가 초기화되어 있어야 합니다.
    /// 2. 네트워크가 실행 중이어야 합니다.
    /// 3. NetworkComponent는 올바르게 초기화된 NetworkBehaviour를 필요로 합니다.
    ///
    /// The NetworkComponent struct provides an easy reference to network behaviors.
    /// Please note the following when using this struct:
    /// 1. The NetworkManager must be initialized.
    /// 2. The network must be running.
    /// 3. NetworkComponent requires a properly initialized NetworkBehaviour.
    /// </summary>
    /// <typeparam name="T">T는 항상 NetworkBehaviour를 상속받아야 합니다. (T must always inherit from NetworkBehaviour)</typeparam>
    [Serializable]
    public struct NetworkComponent<T> : IEquatable<NetworkComponent<T>>, INetworkSerializable where T : NetworkBehaviour
    {
        [SerializeField]
        private ulong m_networkObjectId; // NetworkObject ID (Network Object ID)

        [SerializeField]
        private ushort m_networkBehaivourId; // NetworkBehaviour ID (Network Behaviour ID)

        // NetworkBehaviour의 값을 가져오거나 설정합니다.
        // Retrieves or sets the value of the network behaviour.
        public T Value
        {
            get => NetworkManager.Singleton.SpawnManager.SpawnedObjects[m_networkObjectId]
                    .GetNetworkBehaviourAtOrderIndex(m_networkBehaivourId) as T;

            set
            {
                m_networkObjectId = value.NetworkObjectId;
                m_networkBehaivourId = value.NetworkBehaviourId;
            }
        }

        // 주어진 NetworkObject ID와 행동 ID와 비교 (Compares with the given NetworkObject ID and NetworkBehaviour ID)
        public bool Equals(ulong networkObjectId, ushort networkBehaivourId)
        {
            return m_networkObjectId == networkObjectId && m_networkBehaivourId == networkBehaivourId;
        }

        // 주어진 NetworkBehaviour과 비교 (Compares with the given NetworkBehaviour)
        public bool Equals(T networkBehaviour)
        {
            return m_networkObjectId == networkBehaviour.NetworkObjectId && m_networkBehaivourId == networkBehaviour.NetworkBehaviourId;
        }

        // 두 NetworkComponent 객체를 비교 (Compares two NetworkComponent objects)
        public bool Equals(NetworkComponent<T> other)
        {
            return m_networkObjectId == other.m_networkObjectId && m_networkBehaivourId == other.m_networkBehaivourId;
        }

        // 객체를 직렬화 및 역직렬화 (Serializes and deserializes the object)
        public void NetworkSerialize<T1>(BufferSerializer<T1> serializer) where T1 : IReaderWriter
        {
            serializer.SerializeValue(ref m_networkObjectId);
            serializer.SerializeValue(ref m_networkBehaivourId);
        }
        
        /// <summary>
        /// NetworkBehaviour을 통해 생성자 초기화 (Constructor initializing through network behaviour)
        /// </summary>
        /// <param name="networkBehaviour">초기화할 NetworkBehaviour (The network behaviour to initialize with)</param>
        public NetworkComponent(T networkBehaviour)
        {
            m_networkObjectId = networkBehaviour.NetworkObjectId;
            m_networkBehaivourId = networkBehaviour.NetworkBehaviourId;
        }
        
        /// <summary>
        /// ID를 통해 생성자 초기화 (Constructor initializing through network object ID and behaviour ID)
        /// </summary>
        /// <param name="networkObjectId">NetworkObject ID (Network object ID)</param>
        /// <param name="networkBehaivourId">NetworkBehaviour ID (Network behaviour ID)</param>
        public NetworkComponent(ulong networkObjectId, ushort networkBehaivourId)
        {
            m_networkObjectId = networkObjectId;
            m_networkBehaivourId = networkBehaivourId;
        }
    }
}
