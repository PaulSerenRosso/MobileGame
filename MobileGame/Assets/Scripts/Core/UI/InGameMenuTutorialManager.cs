using Service.Fight;
using UnityEngine;

namespace Service.UI
{
    public class InGameMenuTutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject _introPopupPanel;
        [SerializeField] private GameObject _fightPopupPanel;
        [SerializeField] private GameObject _movePopupPanel;
        [SerializeField] private GameObject _tauntPopupPanel;
        [SerializeField] private GameObject _ultimatePopupPanel;

        [SerializeField] private GameObject _fightOverlayPanel;
        [SerializeField] private GameObject _moveOverlayPanel;
        [SerializeField] private GameObject _tauntOverlayPanel;
        [SerializeField] private GameObject _ultimateOverlayPanel;

        private IFightService _fightService;
        
        public void Init(IFightService fightService)
        {
            _fightService = fightService;
            gameObject.SetActive(true);
            _fightService.ActivatePausePlayer();
            _fightService.DeactivatePauseEvent += OpenIntroPopup;
            _fightService.ActivatePauseEvent += DeactivateOverlays;
        }

        public void OpenIntroPopup()
        {
            _fightService.DeactivatePauseEvent -= OpenIntroPopup;
            DeactivateOverlays();
            _fightService.ActivatePausePlayer();
            _introPopupPanel.SetActive(true);
        }

        public void OpenFightPopup()
        {
            DeactivateOverlays();
            _fightService.ActivatePausePlayer();
            _fightPopupPanel.SetActive(true);
        }

        public void OpenMovePopup()
        {
            DeactivateOverlays();
            _fightService.ActivatePausePlayer();
            _movePopupPanel.SetActive(true);
        }

        public void OpenTauntPopup()
        {
            DeactivateOverlays();
            _fightService.ActivatePausePlayer();
            _tauntPopupPanel.SetActive(true);
        }

        public void OpenUltimatePopup()
        {
            DeactivateOverlays();
            _fightService.ActivatePausePlayer();
            _ultimatePopupPanel.SetActive(true);
        }

        private void DeactivateOverlays()
        {
            _fightOverlayPanel.SetActive(false);
            _moveOverlayPanel.SetActive(false);
            _tauntOverlayPanel.SetActive(false);
            _ultimateOverlayPanel.SetActive(false);
        }

        public void CloseIntroPopupPanel()
        {
            _fightService.DeactivatePausePlayer();
            _introPopupPanel.SetActive(false);
            OpenFightPopup();
        }

        public void CloseFightPopupPanel()
        {
            _fightService.DeactivatePausePlayer();
            _fightPopupPanel.SetActive(false);
            _fightOverlayPanel.SetActive(true);
        }

        public void CloseMovePopupPanel()
        {
            _fightService.DeactivatePausePlayer();
            _movePopupPanel.SetActive(false);
            _moveOverlayPanel.SetActive(true);
        }

        public void CloseTauntPopupPanel()
        {
            _fightService.DeactivatePausePlayer();
            _tauntPopupPanel.SetActive(false);
            _tauntOverlayPanel.SetActive(true);
        }

        public void CloseUltimatePopupPanel()
        {
            _fightService.DeactivatePausePlayer();
            _ultimatePopupPanel.SetActive(false);
            _ultimateOverlayPanel.SetActive(true);
        }
    }
}