using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
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
    private Transform _player;
    private Transform _boss;
    private Animator _playerAnimator;
    private Animator _enemyAnimator;
    private EnemyGlobalSO _enemyGlobalSo;
    [SerializeField] private TextMeshProUGUI _playerBannerText;
    [SerializeField] private string _playerBannerName;
    [SerializeField] private TextMeshProUGUI  _enemyBannerText;
    [SerializeField] private Image _playerBannerImage;
    [SerializeField] private Image _enemyBannerImage;
   [SerializeField] private Image _enemyBannerCharacterImage;
     [SerializeField] private Image _playerBannerCharacterImage;
    [SerializeField] private Sprite _playerBannerCharacterSprite;
    [SerializeField] private Sprite _playerBannerSprite;
    public void Init(Animator playerAnimator, Animator enemyAnimator, Transform player, Transform boss, EnemyGlobalSO enemyGlobalSo)
    {
        _camera = Camera.main;
        _playerAnimator = playerAnimator;
        _enemyAnimator = enemyAnimator;
        _player = player;
        _boss = boss;
        _enemyGlobalSo = enemyGlobalSo;
        _playerBannerText.text = _playerBannerName;
        _enemyBannerText.text = _enemyGlobalSo.Name;
        _playerBannerImage.sprite = _playerBannerSprite;
        _enemyBannerImage.sprite = enemyGlobalSo.BannerSprite;
        _playerBannerCharacterImage.sprite = _playerBannerCharacterSprite;
        _enemyBannerCharacterImage.sprite = enemyGlobalSo.BannerCharacterSprite;
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
        _player.localRotation = Quaternion.identity;
        _boss.parent = _bossPivot;
        _boss.localPosition = Vector3.zero;
        _boss.localRotation = Quaternion.identity;
        _volume.profile = null;
        _camera.enabled = false;
        _playableDirector.playableAsset = timelineAsset;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_playableDirector.duration);
        _player.parent = null;
        _boss.parent = null;
        _camera.enabled = true;
        _playerPivot.position = Vector3.zero;
        _playerPivot.rotation = Quaternion.identity;
        _bossPivot.position = Vector3.zero;
        _bossPivot.rotation = Quaternion.identity;
        endCinematicCallback?.Invoke();
    }
}