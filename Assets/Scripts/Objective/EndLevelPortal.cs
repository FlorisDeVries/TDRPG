﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelPortal : UnitySingleton<EndLevelPortal>
{
    [SerializeField]
    [Tooltip("What attacks does the player recieve as rewards")]
    private List<AnAttack> _missionRewards = new List<AnAttack>();

    [SerializeField]
    private GameObject _rewardUIPrefab = default;

    [SerializeField]
    private Transform _rewardUIParent = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        GameStateManager.Instance.SetGameState(GameState.Victory);

        foreach (AnAttack a in _missionRewards)
        {
            ProgressionManager.Instance.UnlockAttack(a);
            GameObject rewardUI = Instantiate(_rewardUIPrefab);
            rewardUI.transform.SetParent(_rewardUIParent);
            rewardUI.GetComponent<Image>().sprite = a.Sprite;
        }
    }

    public void AddReward(AnAttack attack)
    {
        _missionRewards.Add(attack);
    }
}
