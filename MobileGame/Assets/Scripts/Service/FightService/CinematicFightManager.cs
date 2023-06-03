using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.UI;

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
    [SerializeField] private Image _playerBannerImage;
    [SerializeField] private Image _enemyBannerImage;
    [SerializeField] private Sprite _playerBannerSprite;
    [SerializeField] private GameObject _fadeGameObject;
    [SerializeField] private GameObject _versusGameObject;
    [SerializeField] private GameObject _ultimateGameObject;
    [SerializeField] private GameObject _roundWonGameObject;

    private Transform _player;
    private Transform _boss;
    private Animator _playerAnimator;
    private Animator _enemyAnimator;
    private EnemyGlobalSO _enemyGlobalSo;

    public void Init(Animator playerAnimator, Animator enemyAnimator, Transform player, Transform boss,
        EnemyGlobalSO enemyGlobalSo)
    {
        _camera = Camera.main;
        _playerAnimator = playerAnimator;
        _enemyAnimator = enemyAnimator;
        _player = player;
        _boss = boss;
        _enemyGlobalSo = enemyGlobalSo;
        _playerBannerImage.sprite = _playerBannerSprite;
        _enemyBannerImage.sprite = enemyGlobalSo.BannerSprite;
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
        _playerBannerImage.gameObject.SetActive(true);
        _enemyBannerImage.gameObject.SetActive(true);
        _fadeGameObject.SetActive(true);
        _player.parent = _playerPivot;
        _player.localPosition = Vector3.zero;
        _player.localRotation = Quaternion.identity;
        _boss.parent = _bossPivot;
        _boss.localPosition = Vector3.zero;
        _boss.localRotation = Quaternion.identity;
        _volume.profile = null;
        _camera.enabled = false;
        _playableDirector.playableAsset = timelineAsset;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_playableDirector.duration);
        _playerBannerImage.gameObject.SetActive(false);
        _enemyBannerImage.gameObject.SetActive(false);
        _fadeGameObject.SetActive(false);
        _versusGameObject.SetActive(false);
        _ultimateGameObject.SetActive(false);
        _roundWonGameObject.SetActive(false);
        _playerAnimator.Play("Idle");
        _enemyAnimator.Play("Idle");
        _player.parent = null;
        _boss.parent = null;
        _camera.enabled = true;
        _playerPivot.position = Vector3.zero;
        _playerPivot.rotation = Quaternion.identity;
        _bossPivot.position = Vector3.zero;
        _bossPivot.rotation = Quaternion.identity;
        endCinematicCallback?.Invoke();
    }

    public void StopCinematic(Action endCinematicCallback)
    {
        StopAllCoroutines();
        _playerBannerImage.gameObject.SetActive(false);
        _enemyBannerImage.gameObject.SetActive(false);
        _fadeGameObject.SetActive(false);
        _versusGameObject.SetActive(false);
        _ultimateGameObject.SetActive(false);
        _roundWonGameObject.SetActive(false);
        _playerAnimator.Play("Idle");
        _enemyAnimator.Play("Idle");
        _playableDirector.Stop();
        _player.parent = null;
        _boss.parent = null;
        _camera.enabled = true;
        _enemyAnimator.Play("Idle");
        _playerPivot.position = Vector3.zero;
        _playerPivot.rotation = Quaternion.identity;
        _bossPivot.position = Vector3.zero;
        _bossPivot.rotation = Quaternion.identity;
        endCinematicCallback?.Invoke();
    }
}