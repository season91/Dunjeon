/// <summary>
/// [ITriggerReactive] Object에 접촉만 하면 자동으로 발동
/// 점프패드, 포탈, 회복존 등
/// </summary>
public interface IJumpable
{
    void Launch(float force); // 점프
}
