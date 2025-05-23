# Unity 3D 프로젝트 - 프로젝트 구조 안내

이 프로젝트는 Unity 3D를 기반으로 개발되었으며, **모듈화와 명확한 책임 분리**를 중심으로 공부하며 개발했습니다.

## 📁 폴더 구조 설명
```
01.Scripts/
├── Camera/
│   ├── CameraFollow.cs            # 카메라 추적 로직
│   └── CameraRaycaster.cs         # 카메라 레이캐스트 처리
│
├── Interfaces/
│   ├── IInspectable.cs            # 상호작용 설명 표시 인터페이스
│   ├── IInteractable.cs           # 상호작용 가능한 오브젝트 인터페이스
│   └── ITriggerReactive.cs        # 트리거 반응 인터페이스
│
├── Manager/
│   ├── CharacterManager.cs        # 캐릭터 등록
│   ├── InventoryManager.cs        # 인벤토리 아이템 데이터 관리
│   └── UIManager.cs               # UI 갱신 및 표시 컨트롤
│
├── Network/
│   └── GameStarter.cs             # Fusion 기반 네트워크 시작 및 초기화
│
├── Object/
│   ├── ItemObject.cs              # 월드에 존재하는 아이템 프리팹
│   ├── JumpPad.cs                 # 점프 패드
│   └── Tree.cs                    # 나무 오브젝트
│
├── Player/
│   ├── Player.cs                  # 플레이어 본체 및 속성
│   ├── PlayerController.cs        # 이동 및 회전 입력 처리
│   ├── PlayerInputHandler.cs      # 입력 바인딩과 처리
│   ├── PlayerInteractionHandler.cs # 상호작용 시스템 핸들링
│   └── PlayerStatusHandler.cs     # 체력 등 플레이어 상태 로직
│
├── ScriptableObject/
│   ├── InspectableData.cs         # 설명 오브젝트 데이터
│   └── ItemData.cs                # 아이템 데이터 정의
│
├── UI/
│   ├── UIInventory.cs             # 인벤토리 메인 패널
│   ├── UIInventorySlot.cs         # 슬롯 단위 UI
│   ├── UIPrompt.cs                # 상호작용 설명 안내
│   ├── UIStatusBar.cs             # 체력 등 상태 표시바
│   └── UIStatusGroup.cs           # 상태 표시 UI 그룹
│
└── Util/
    └── CSVImporter.cs             # CSV 데이터 파일 (아이템 등)
```

## ✅ 주요 기능

- **3인칭 카메라 컨트롤** 및 캐릭터 이동
- **ScriptableObject 기반 아이템/데이터 관리**
- **직관적인 UI와 인벤토리 시스템**
- **인터페이스를 활용한 상호작용 처리 구조**

## 🔧 개발 환경

- Unity 2022.X 이상
- Photon Fusion 2
- C# 8.0 이상
