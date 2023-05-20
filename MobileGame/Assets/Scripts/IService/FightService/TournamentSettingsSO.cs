using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TournamentSettingsGameSO", fileName = "new TournamentSettingsGameSO")]
public class TournamentSettingsSO : ScriptableObject
{
    public List<string> FakeNames = new()
    {
        "moxxi42",
        "enitic34",
        "kayz98",
        "coco63",
        "oneshoot16",
        "turlupin55",
        "wetime87",
        "eraz03"
    };

    public int CoinsAmountWhenWinTournament;
    public int[] ExpAmountWhenWinStepTournament;
    public int[] ExpAmountWhenLoseStepTournament;


}
