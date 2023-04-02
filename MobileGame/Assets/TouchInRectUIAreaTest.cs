using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInRectUIAreaTest : MonoBehaviour
{
    [SerializeField] private Image image;
    public PlayerInputs PlayerInputs;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputs = new PlayerInputs();
        if (PlayerInputs == null) return;
        PlayerInputs.Enable();
        PlayerInputs.GenericInputs.PressTouch.performed += (ctx) =>
        {
            Debug.Log(ctx.ReadValue<Vector2>());
            Debug.Log(image.rectTransform.rect.Contains(ctx.ReadValue<Vector2>()));
        }; 
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("screen width" + Screen.width);
        Debug.Log("screen height " + Screen.height);
        Debug.Log(image.rectTransform.rect);
    }
}
