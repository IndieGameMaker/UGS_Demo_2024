using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct PlayerData
{
    public string name;
    public int level;
    public int xp;
    public int gold;

    public List<ItemData> items;
}

[Serializable]
public struct ItemData
{
    public string name;
    public int count;
    public int value;
    public string icon;
}

public class CloudSaveManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button singleDataSaveButton;
    [SerializeField] private Button singleDataLoadButton;
    [SerializeField] private Button multiDataSaveButton;
    [SerializeField] private Button multiDataLoadButton;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    async void Awake()
    {
        singleDataSaveButton.interactable = false;

        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            singleDataSaveButton.interactable = true;

            string playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"로그인 성공 : {playerId}");
        };

        // 익명로그인 요청
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        // 버튼 이벤트 연결
        BindingEvents();
    }

    private void BindingEvents()
    {
        singleDataSaveButton.onClick.AddListener(async () => await SaveSingleDataAsync());
        multiDataSaveButton.onClick.AddListener(async () => await SaveMultiDataAsync<PlayerData>("PlayerData", playerData));

        singleDataLoadButton.onClick.AddListener(async () => await LoadData());
    }

    #region 싱글데이터 저장
    public async Task SaveSingleDataAsync()
    {
        // 저장할 데이터를 정의
        var data = new Dictionary<string, object>
        {
            {"player_name", "Zack"},
            {"level", 50},
            {"xp", 2000},
            {"gold", 100}
        };

        // 저장 요청
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("데이터 저장 완료");
    }
    #endregion

    #region 복수데이터 저장
    public async Task SaveMultiDataAsync<T>(string key, T saveData)
    {
        var data = new Dictionary<string, object>
        {
            {key, saveData}
        };

        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("복수 데이터 저장 완료");

        // PlayerData 스트럭쳐 초기화
        playerData = new PlayerData();
    }
    #endregion

    #region 싱글데이터 로드
    /*
        HashSet<T> 자료형
        - 중복값을 허용하지 않는다.
        - Hash 기반 빠른 속도
        - TryGetValue 사용

        playerId.Add(1);
        playerId.Add(2);
        playerId.Add(1); // X

        playerId[3] // X
    */

    public async Task LoadData()
    {
        var keys = new HashSet<string>
        {
            "player_name", "level", "xp"
        };

        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

        if (data.TryGetValue("player_name", out var playerName))
        {
            Debug.Log($"PlayerName : {playerName.Value.GetAs<string>()}");
        }
        if (data.TryGetValue("level", out var level))
        {
            Debug.Log($"Level : {level.Value.GetAs<int>()}");
        }
    }

    #endregion

    public async Task<T> LoadData<T>(string key)
    {
        var loadData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });
    }
}
