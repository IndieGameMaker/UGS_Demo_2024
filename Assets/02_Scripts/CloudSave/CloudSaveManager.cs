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
    [SerializeField] private Button multiDataSaveButton;

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
    }

    #region 싱글데이터 저장
    public async Task SaveSingleDataAsync()
    {
        // 저장할 데이터를 정의
        var data = new Dictionary<string, object>
        {
            {"player_name", "Zackiller"},
            {"level", 30},
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
    }
    #endregion
}
