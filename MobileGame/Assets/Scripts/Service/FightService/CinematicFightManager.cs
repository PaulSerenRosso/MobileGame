using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class CinematicFightManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private TimelineAsset _fightEntry;
    [SerializeField] private TimelineAsset _playerUltimate;
    [SerializeField] private TimelineAsset _enemyUltimate;
    [SerializeField] private Volume _volume;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerPivot;
    [SerializeField] private Transform _bossPivot;
    private Transform _player;
    private Transform _boss;
    private Animator _playerAnimator;
    private Animator _enemyAnimator;

    public void Init(Animator playerAnimator, Animator enemyAnimator, Transform player, Transform boss)
    {
        _camera = Camera.main;
        _playerAnimator = playerAnimator;
        _enemyAnimator = enemyAnimator;
        _player = player;
        _boss = boss;

    }

    public void LaunchFightEntryCinematic(Action endCinematicCallBack)
    {
        StartCoroutine(PlayCinematic(_fightEntry, endCinematicCallBack));
    }

    public void LaunchPlayerUltimateCinematic(Action endCinematicCallBack)
    {
        StartCoroutine(PlayCinematic(_playerUltimate, endCinematicCallBack));
    }

    public void LaunchEnemyUltimateCinematic(Action endCinematicCallBack)
    {
        StartCoroutine(PlayCinematic(_enemyUltimate, endCinematicCallBack));
    }

    public void ChangePostProcessVolume(VolumeProfile volumeProfile) => _volume.profile = volumeProfile;

    public void LaunchPlayerAnimation(string stateName) => _playerAnimator.Play(stateName);

    public void LaunchEnemyAnimation(string stateName) => _enemyAnimator.Play(stateName);

    private IEnumerator PlayCinematic(TimelineAsset timelineAsset, Action endCinematicCallback)
    {
        _player.parent = _playerPivot;
        _player.localPosition = Vector3.zero;
        _boss.parent = _bossPivot;
        _boss.localPosition = Vector3.zero;
        _volume.profile = null;
        _camera.enabled = false;
        _playableDirector.playableAsset = timelineAsset;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_playableDirector.duration);
        endCinematicCallback?.Invoke();
        _player.parent = null;
        _boss.parent = null;
        _camera.enabled = true;
    }
}