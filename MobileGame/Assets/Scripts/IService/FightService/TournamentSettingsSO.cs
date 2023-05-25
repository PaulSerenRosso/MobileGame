using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TournamentSettingsGameSO", fileName = "new TournamentSettingsGameSO")]
public class TournamentSettingsSO : ScriptableObject
{
    public List<FriendUser> FakeNames;

    public int CoinsAmountWhenWinTournament;
    public int[] ExpAmountWhenWinStepTournament;
    public int[] ExpAmountWhenLoseStepTournament;
}

[Serializable]
public class FriendUser
{
    public string name;
    public Sprite picture;
}

/*
 * "moxxi42",
 * "enitic34",
 * "kayz98",
 * "coco63",
 * "oneshoot16",
 * "turlupin55",
 * "wetime87",
 * "eraz03" 
 */