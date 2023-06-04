using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPlayer : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private float _speed = 0.2f;
    
    private Vector2 _beginPosition;
    private GameObject _player;

    private ScrollRect _scroll;

    public void Init(GameObject player)
    {
        _player = player;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float speed = Mathf.Sign(eventData.position.x - _beginPosition.x) * _speed;
        _player.transform.Rotate(Vector3.up * -speed, Space.World);
        _beginPosition = eventData.position;
        
    }
}
