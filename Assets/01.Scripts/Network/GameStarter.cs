using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// NetworkManager에 연결
/// </summary>
public class GameStarter : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    
    // NetworkPrefabRef는 ScriptableObject이라서 컴포넌트가 아님
    // 이 방식은 실서버에서 사용
    // [SerializeField] private NetworkPrefabRef _playerPrefab;

    async void Start()
    {
        GameMode mode;
        _runner = GetComponent<NetworkRunner>();
        _runner.ProvideInput = true;
        
        // Create the NetworkSceneInfo from the current scene
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        
        if (scene.IsValid) {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

#if UNITY_EDITOR
        mode = GameMode.Host;
#else
        mode = GameMode.Client;
#endif

        // Fusion 세션 시작(StartGame)
        await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode, // 에디터 호스트 모드
                SessionName = "9to9in9",   // 세션 이름
                Scene = scene,
                SceneManager = GetComponent<NetworkSceneManagerDefault>()
            }
        );
        
        Debug.Log($"Fusion Start Mode: {mode}");
    }
    
    
    // Player 프리팹 연결하여 생성, 스폰
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        var prefab = Resources.Load<NetworkObject>("Prefabs/Player");
        
        Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 3, 1, 0);
        
        if (prefab == null)
        {
            Debug.LogError("XXX 프리팹 없음");
            return;
        }

        runner.Spawn(prefab, spawnPosition, Quaternion.identity, player);
    }
    
    // INetworkRunnerCallbacks의 나머지 함수들은 일단 비워둬도 됨
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){ }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){ }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data){ }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress){ }

}
