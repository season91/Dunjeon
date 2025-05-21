using UnityEngine;
/// <summary>
/// [요청/이벤트 처리] 상호작용 이벤트 후처리
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

    // 상호작용 E 버튼 눌렀을 때
    public void InteractInput()
    {
        if (curInteractable != null)
        {
            curInteractable.OnInteract();
        }
    }
    
    // 상호작용 후처리
    private void InteractChanged(IInteractable interactable)
    {
        curInteractable = interactable;
        if (interactable != null)
        {
            UIManager.Instance.ShowDescriptionPrompt(curInteractable.GetPromptText());
        }
        else
        {
            UIManager.Instance.HideDescriptionPrompt();
        }
    }
    
    #endregion

}
