# Unity-NGO-Example
Unity NGO Example

---

## NetworkComponent 구조체 && NetworkComponent 구조체 Extension

`NetworkComponent` 구조체는 Unity Netcode for GameObjects를 사용하는 게임에서 `NetworkBehaviour` 객체를 쉽게 참조하고 관리할 수 있도록 도와줍니다. 이 구조체는 네트워크 객체의 ID와 행동 ID를 통해 네트워크 행동을 간편하게 관리할 수 있습니다.

### 주요 기능

- **네트워크 객체 ID와 행동 ID 관리**: `NetworkObject`와 `NetworkBehaviour`의 ID를 통해 네트워크 행동을 쉽게 참조하고 비교할 수 있습니다.
- **직렬화 및 역직렬화**: 네트워크 동기화를 위한 직렬화 및 역직렬화 기능을 제공합니다.

### 사용 시 주의사항

`NetworkComponent`를 사용할 때는 다음 사항에 유의해야 합니다:

1. **NetworkManager 초기화**: `NetworkManager`가 초기화되어 있어야 합니다.
2. **네트워크 실행 중**: 네트워크가 실행 중이어야 합니다.
3. **올바른 초기화**: `NetworkComponent`는 올바르게 초기화된 `NetworkBehaviour`를 필요로 합니다.

### 클래스 및 메서드 설명

#### 클래스 설명

- **`NetworkComponent<T>`**: `T`는 항상 `NetworkBehaviour`를 상속받아야 합니다. 이 구조체는 네트워크 행동을 쉽게 참조하고 관리할 수 있도록 합니다.

#### 생성자

- **`NetworkComponent(T networkBehaviour)`**:
  - `networkBehaviour`: 초기화할 `NetworkBehaviour` 객체.
  - `NetworkBehaviour` 객체를 통해 `NetworkComponent`를 초기화합니다.

- **`NetworkComponent(ulong networkObjectId, ushort networkBehaivourId)`**:
  - `networkObjectId`: `NetworkObject` ID.
  - `networkBehaivourId`: `NetworkBehaviour` ID.
  - 네트워크 객체 ID와 행동 ID를 통해 `NetworkComponent`를 초기화합니다.

#### 프로퍼티

- **`T Value { get; set; }`**:
  - `NetworkBehaviour`의 값을 가져오거나 설정합니다.

#### 메서드

- **`bool Equals(ulong networkObjectId, ushort networkBehaivourId)`**:
  - 주어진 `NetworkObject` ID와 행동 ID와 비교합니다.
  - 반환 값: 같으면 `true`, 다르면 `false`.

- **`bool Equals(T networkBehaviour)`**:
  - 주어진 `NetworkBehaviour`와 비교합니다.
  - 반환 값: 같으면 `true`, 다르면 `false`.

- **`bool Equals(NetworkComponent<T> other)`**:
  - 두 `NetworkComponent` 객체를 비교합니다.
  - 반환 값: 같으면 `true`, 다르면 `false`.

- **`void NetworkSerialize<T1>(BufferSerializer<T1> serializer) where T1 : IReaderWriter`**:
  - 객체를 직렬화 및 역직렬화합니다.


---

## NetworkComponentExtension 클래스

`NetworkComponentExtension` 클래스는 Unity Netcode for GameObjects를 사용하는 게임에서 `NetworkList`와 관련된 여러 유용한 확장 메서드를 제공합니다. 이 확장 메서드는 네트워크 행동(NetworkBehaviour)을 관리하고 조작하는 작업을 더 쉽게 만듭니다.

### 주요 기능

- **네트워크 리스트에서 NetworkBehaviour 찾기**: 주어진 조건에 따라 첫 번째 또는 모든 `NetworkBehaviour`를 찾습니다.
- **네트워크 리스트에 추가 및 삽입**: 새로운 `NetworkBehaviour`를 리스트에 추가하거나 지정된 인덱스에 삽입합니다.
- **네트워크 리스트에서 제거**: 주어진 조건에 따라 `NetworkBehaviour`를 제거합니다.
- **네트워크 리스트 변환**: 네트워크 리스트를 배열 또는 일반 리스트로 변환합니다.
- **리스트 항목에 대한 액션 수행**: 각 항목에 대해 지정된 액션을 수행합니다.
- **조건을 만족하는 항목 존재 여부 확인**: 리스트 내에서 조건을 만족하는 항목이 있는지 확인합니다.

### 메서드 설명

#### Find 메서드
- **`T Find<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate) where T : NetworkBehaviour`**:
  - 주어진 조건을 만족하는 첫 번째 `NetworkBehaviour`를 반환합니다.
  - 조건을 만족하는 항목이 없으면 `null`을 반환합니다.

#### FindAll 메서드
- **`List<T> FindAll<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate) where T : NetworkBehaviour`**:
  - 주어진 조건을 만족하는 모든 `NetworkBehaviour`를 리스트로 반환합니다.

#### Add 메서드
- **`void Add<T>(this NetworkList<NetworkComponent<T>> networkList, T networkBehaviour) where T : NetworkBehaviour`**:
  - 네트워크 리스트에 새로운 `NetworkBehaviour`를 추가합니다.

#### Insert 메서드
- **`void Insert<T>(this NetworkList<NetworkComponent<T>> networkList, int index, T networkBehaviour) where T : NetworkBehaviour`**:
  - 지정된 인덱스에 새로운 `NetworkBehaviour`를 삽입합니다.

#### Remove 메서드
- **`bool Remove<T>(this NetworkList<NetworkComponent<T>> networkList, T networkBehaviour) where T : NetworkBehaviour`**:
  - 네트워크 리스트에서 지정된 `NetworkBehaviour`를 제거합니다.
  - 제거에 성공하면 `true`, 실패하면 `false`를 반환합니다.

#### RemoveAll 메서드
- **`int RemoveAll<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate) where T : NetworkBehaviour`**:
  - 주어진 조건을 만족하는 모든 `NetworkBehaviour`를 제거합니다.
  - 제거된 요소의 수를 반환합니다.

- **`int RemoveAll<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate, out List<T> removeList) where T : NetworkBehaviour`**:
  - 주어진 조건을 만족하는 모든 `NetworkBehaviour`를 제거하고, 제거된 요소의 리스트를 반환합니다.
  - 제거된 요소의 수를 반환합니다.

#### ToArray 메서드
- **`T[] ToArray<T>(this NetworkList<NetworkComponent<T>> networkList) where T : NetworkBehaviour`**:
  - 네트워크 리스트를 배열로 변환합니다.

#### ToList 메서드
- **`List<T> ToList<T>(this NetworkList<NetworkComponent<T>> networkList) where T : NetworkBehaviour`**:
  - 네트워크 리스트를 일반 리스트로 변환합니다.

#### Foreach 메서드
- **`void Foreach<T>(this NetworkList<NetworkComponent<T>> networkList, Action<T> action) where T : NetworkBehaviour`**:
  - 네트워크 리스트의 각 요소에 대해 주어진 액션을 수행합니다.

#### Any 메서드
- **`bool Any<T>(this NetworkList<NetworkComponent<T>> networkList, Predicate<T> predicate) where T : NetworkBehaviour`**:
  - 네트워크 리스트의 요소 중 조건을 만족하는 요소가 있는지 확인합니다.
  - 조건을 만족하는 요소가 하나라도 있으면 `true`, 아니면 `false`를 반환합니다.

---
