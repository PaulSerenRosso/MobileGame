using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendComponent : MonoBehaviour
{
    [SerializeField] private Image _profilePicture;
    [SerializeField] private TextMeshProUGUI _nameFriend;

    public void SetValue(Sprite picture, string name)
    {
        _nameFriend.text = name;
        _profilePicture.sprite = picture;
    }
}
