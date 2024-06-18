using System;
using System.Collections.Generic;
using Unity.Netcode;

namespace NetworkComponent
{
    public static class NetworkComponentExtension
    {
        /// <summary>
        /// 주어진 조건을 만족하는 첫 번째 NetworkBehaviour를 반환합니다.
        /// Finds the first NetworkBehaviour that matches the specified condition.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="predicate">조건을 확인할 함수 (The function to test each element for a condition)</param>
        /// <returns>조건을 만족하는 첫 번째 NetworkBehaviour (The first NetworkBehaviour that matches the condition)</returns>
        public static T Find<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate)
            where T : NetworkBehaviour
        {
            for (var i = 0; i < networkList.Count; i++)
            {
                if (predicate(networkList[i].Value))
                {
                    return networkList[i].Value;
                }
            }

            return null;
        }
        
        /// <summary>
        /// 주어진 조건을 만족하는 모든 NetworkBehaviour를 리스트로 반환합니다.
        /// Finds all NetworkBehaviours that match the specified condition and returns them as a list.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="predicate">조건을 확인할 함수 (The function to test each element for a condition)</param>
        /// <returns>조건을 만족하는 모든 NetworkBehaviour의 리스트 (A list of all NetworkBehaviours that match the condition)</returns>
        public static List<T> FindAll<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate)
            where T : NetworkBehaviour
        {
            var list = new List<T>();
            
            for (var i = 0; i < networkList.Count; i++)
            {
                if (predicate(networkList[i].Value))
                {
                    list.Add(networkList[i].Value);
                }
            }

            return list;
        }
        
        /// <summary>
        /// 네트워크 리스트에 새로운 NetworkBehaviour를 추가합니다.
        /// Adds a new NetworkBehaviour to the network list.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="networkBehaviour">추가할 네트워크 행동 (The network behaviour to add)</param>
        public static void Add<T>(this NetworkList<NetworkComponent<T>> networkList, T networkBehaviour)
            where T : NetworkBehaviour
        {
            networkList.Add(new NetworkComponent<T>(networkBehaviour));
        }
        
        /// <summary>
        /// 지정된 인덱스에 새로운 NetworkBehaviour를 삽입합니다.
        /// Inserts a new NetworkBehaviour at the specified index.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="index">삽입할 위치의 인덱스 (The index at which to insert the element)</param>
        /// <param name="networkBehaviour">삽입할 네트워크 행동 (The network behaviour to insert)</param>
        public static void Insert<T>(this NetworkList<NetworkComponent<T>> networkList, int index, T networkBehaviour)
            where T : NetworkBehaviour
        {
            networkList.Insert(index, new NetworkComponent<T>(networkBehaviour));
        }
        
        /// <summary>
        /// 네트워크 리스트에서 지정된 NetworkBehaviour를 제거합니다.
        /// Removes the specified NetworkBehaviour from the network list.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="networkBehaviour">제거할 네트워크 행동 (The network behaviour to remove)</param>
        /// <returns>제거에 성공하면 true, 실패하면 false를 반환 (True if removal is successful, otherwise false)</returns>
        public static bool Remove<T>(this NetworkList<NetworkComponent<T>> networkList, T networkBehaviour)
            where T : NetworkBehaviour
        {
            int index = -1;
            
            for (var i = 0; i < networkList.Count; i++)
            {
                if (networkList[i].Equals(networkBehaviour))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                return false;
            }
            else
            {
                networkList.Remove(networkList[index]);
                return true;
            }
        }

        /// <summary>
        /// 주어진 조건을 만족하는 모든 NetworkBehaviour를 제거합니다.
        /// Removes all NetworkBehaviours that match the specified condition.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="predicate">조건을 확인할 함수 (The function to test each element for a condition)</param>
        /// <returns>제거된 요소의 수 (The number of elements removed)</returns>
        public static int RemoveAll<T>(this NetworkList<NetworkComponent<T>> networkList , Predicate<T> predicate) where T : NetworkBehaviour
        {
            var removeList = new List<T>();

            for (var i = 0; i < networkList.Count; i++)
            {
                if (predicate(networkList[i].Value))
                {
                    removeList.Add(networkList[i].Value);
                }
            }
            
            for (var i = 0; i < removeList.Count; i++)
            {
                networkList.Remove(removeList[i]);
            }

            return removeList.Count;
        }
        
        /// <summary>
        /// 주어진 조건을 만족하는 모든 NetworkBehaviour를 제거하고 제거된 요소의 리스트를 반환합니다.
        /// Removes all NetworkBehaviours that match the specified condition and returns the list of removed elements.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="predicate">조건을 확인할 함수 (The function to test each element for a condition)</param>
        /// <param name="removeList">제거된 요소의 리스트 (The list of removed elements)</param>
        /// <returns>제거된 요소의 수 (The number of elements removed)</returns>
        public static int RemoveAll<T>(this NetworkList<NetworkComponent<T>> networkList , Predicate<T> predicate , out List<T> removeList) where T : NetworkBehaviour
        {
            removeList = new List<T>();

            for (var i = 0; i < networkList.Count; i++)
            {
                if (predicate(networkList[i].Value))
                {
                    removeList.Add(networkList[i].Value);
                }
            }
            
            for (var i = 0; i < removeList.Count; i++)
            {
                networkList.Remove(removeList[i]);
            }

            return removeList.Count;
        }
        
        /// <summary>
        /// 네트워크 리스트를 배열로 변환합니다.
        /// Converts the network list to an array.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <returns>NetworkBehaviour 객체의 배열 (An array of NetworkBehaviour objects)</returns>
        public static T[] ToArray<T>(this NetworkList<NetworkComponent<T>> networkList) where T : NetworkBehaviour
        {
            var array = new T[networkList.Count];

            for (var i = 0; i < networkList.Count; i++)
            {
                array[i] = networkList[i].Value;
            }

            return array;
        }

        /// <summary>
        /// 네트워크 리스트를 리스트로 변환합니다.
        /// Converts the network list to a List.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <returns>NetworkBehaviour 객체의 리스트 (A list of NetworkBehaviour objects)</returns>
        public static List<T> ToList<T>(this NetworkList<NetworkComponent<T>> networkList) where T : NetworkBehaviour
        {
            var list = new List<T>(networkList.Count);

            for (var i = 0; i < networkList.Count; i++)
            {
                list.Add(networkList[i].Value);
            }

            return list;
        }

        /// <summary>
        /// 네트워크 리스트의 각 요소에 대해 주어진 액션을 수행합니다.
        /// Performs the given action on each element of the network list.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="action">각 요소에 수행할 액션 (The action to perform on each element)</param>
        public static void Foreach<T>(this NetworkList<NetworkComponent<T>> networkList, Action<T> action)
            where T : NetworkBehaviour
        {
            for (var i = 0; i < networkList.Count; i++)
            {
                action(networkList[i].Value);
            }
        }

        /// <summary>
        /// 네트워크 리스트의 요소 중 조건을 만족하는 요소가 있는지 확인합니다.
        /// Checks if any element in the network list satisfies the given condition.
        /// </summary>
        /// <typeparam name="T">NetworkBehaviour를 상속받는 타입 (Type that inherits from NetworkBehaviour)</typeparam>
        /// <param name="networkList">네트워크 리스트 (The network list)</param>
        /// <param name="predicate">조건을 확인할 함수 (The function to test each element for a condition)</param>
        /// <returns>조건을 만족하는 요소가 하나라도 있으면 true, 아니면 false (True if any element satisfies the condition, otherwise false)</returns>
        public static bool Any<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate) where T : NetworkBehaviour
        {
            for (var i = 0; i < networkList.Count; i++)
            {
                if (predicate(networkList[i].Value))
                    return true;
            }

            return false;
        }
    }
}
