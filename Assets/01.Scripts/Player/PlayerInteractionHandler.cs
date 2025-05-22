using UnityEngine;
/// <summary>
/// [요청/이벤트 처리] 상호작용 이벤트 후처리, E버튼과 레이캐스트로 접근시 2가지 이벤트를 관리
/// </summary>
public class PlayerInteractionHandler : MonoBehaviour
{
    // 상호작용에 필요한 변수
    [SerializeField] private CameraRaycaster raycaster;
    [SerializeField] private PlayerInputHandler inputHandler;
    private IInteractable curInteractable;
    
    private void Reset()
    {
        raycaster = transform.parent.GetComponentInChildren<CameraRaycaster>();
        inputHandler = transform.parent.GetComponentInChildren<PlayerInputHandler>();
    }

    private void Awake()
    {
        raycaster =  transform.parent.GetComponentInChildren<CameraRaycaster>();
        inputHandler = transform.parent.GetComponentInChildren<PlayerInputHandler>();
    }
    
    private void OnEnable()
    {
        raycaster.OnInteractChanged += InteractChanged;
        inputHandler.OnInteractionInput += InteractInput; 
    }

    private void OnDisable()
    {
        raycaster.OnInteractChanged -= InteractChanged;
        inputHandler.OnInteractionInput -= InteractInput;
    }

    #region 상호작용 관련

    // 상호작용 E 버튼 눌렀을 때 상호작용처리
    public void InteractInput()
    {
        if (curInteractable != null)
            curInteractable.OnInteract();
        else
            UIManager.Instance.ShowDescriptionPrompt("상호작용할 수 있는 대상이 아닙니다");
    }
    
    // 상호작용 가까이 갔을 때
    private void InteractChanged(IInspectable inspectable)
    {
        if (inspectable != null)
        {
            UIManager.Instance.ShowDescriptionPrompt(inspectable.GetPromptText());
            
            // 상호작용 대상 설정
            // interactable이 아니면 null로 명확히 초기화
            curInteractable = inspectable is IInteractable interactable ? interactable : null;
        }
        else
        {
            UIManager.Instance.HideDescriptionPrompt();
            curInteractable = null;
        }
    }
    
    #endregion

}
