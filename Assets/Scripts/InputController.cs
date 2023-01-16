using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void KeyboardEventHandler(object sender, char pressedCharacter);

    public event KeyboardEventHandler KeyPressed;
    public event EventHandler ReturnPressed;
    public event EventHandler BackspacePressed;

    private bool _isKeyPressed;

    public void OnGUI()
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.KeyUp)
            _isKeyPressed = false;
        
        if (currentEvent.type != EventType.KeyDown)
            return;

        if (_isKeyPressed)
            return;

        switch (currentEvent.keyCode)
        {
            case KeyCode.Return:
            {
                ReturnPressed?.Invoke(this, EventArgs.Empty);
                _isKeyPressed = true;
                break;
            }
            case KeyCode.Backspace:
            {
                BackspacePressed?.Invoke(this, EventArgs.Empty);
                _isKeyPressed = true;
                break;
            }
            default:
            {
                if (currentEvent.character != 0)
                {
                    KeyPressed?.Invoke(this, currentEvent.character);
                    _isKeyPressed = true;
                }
                break;
            }
        }
    }
}