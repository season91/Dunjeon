using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterRegistry
{
    // 캐릭터 등록 전용 클래스
    // 호출 하는 곳에서 초기화 
    public static Player Player { get; private set; }
    public static void Register(Player player) => Player = player;
}
