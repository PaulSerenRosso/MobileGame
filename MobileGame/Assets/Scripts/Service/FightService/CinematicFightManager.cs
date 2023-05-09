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
    private Animator _playerAnimator;
    private Animator _enemyAnimator;

    public void Init(Animator playerAnimator, Animator enemyAnimator)
    {
        _camera = Camera.main;
        _playerAnimator = playerAnimator;
        _enemyAnimator = enemyAnimator;
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
        _volume.profile = null;
        _camera.enabled = false;
        _playableDirector.playableAsset = timelineAsset;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_playableDirector.duration);
        endCinematicCallback?.Invoke();
        _camera.enabled = true;
    }
}