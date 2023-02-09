using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private const char NullCharacter = default;
    private const char ReturnCharacter = (char)10;
    
    public event EventHandler<char> KeyPressed;
    public event EventHandler ReturnPressed;
    public event EventHandler BackspacePressed;

    private readonly List<KeyCode> _pressedKeyCodes = new();

    public void OnGUI()
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.KeyUp)
        {
            ReleasePressedKey(currentEvent.keyCode);
            return;
        }

        if (currentEvent.type != EventType.KeyDown)
            return;

        // Ignoring key releasing events.
        if (IsKeyPressed(currentEvent.keyCode))
            return;

        ProcessPressEvent(currentEvent);
    }

    private void ProcessPressEvent(Event pressEvent)
    {
        switch (pressEvent.keyCode)
        {
            case KeyCode.Return:
            {
                AddPressedKey(pressEvent.keyCode);
                ReturnPressed?.Invoke(this, EventArgs.Empty);
                break;
            }
            case KeyCode.Backspace:
            {
                AddPressedKey(pressEvent.keyCode);
                BackspacePressed?.Invoke(this, EventArgs.Empty);
                break;
            }
            default:
            {
                if (pressEvent.character is NullCharacter or ReturnCharacter)
                    break;

                AddPressedKey(pressEvent.keyCode);
                KeyPressed?.Invoke(this, pressEvent.character);
                break;
            }
        }
    }
    
    private void AddPressedKey(KeyCode keyCode)
    {
        if (keyCode == KeyCode.None)
            return;
        
        _pressedKeyCodes.Add(keyCode);
    }

    private bool IsKeyPressed(KeyCode keyCode)
    {
        return _pressedKeyCodes.Contains(keyCode);
    }

    private void ReleasePressedKey(KeyCode keyCode)
    {
        _pressedKeyCodes.Remove(keyCode);
    }
}