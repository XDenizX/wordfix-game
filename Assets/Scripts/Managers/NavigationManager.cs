using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [SerializeField]
    private List<KeyValueItem<ScreenKind, Screen>> screens;

    private void Start()
    {
        ChangeScreen(ScreenKind.MainMenu);
    }
    
    public void ChangeScreen(ScreenKind screenKind)
    {
        screens.ForEach(screenItem => screenItem.value.gameObject.SetActive(false));
        
        var screen = screens.FirstOrDefault(x => x.key == screenKind);
        if (screen == null)
        {
            throw new KeyNotFoundException($"Can not found screen by kind '{screenKind}'");
        }
        
        screen.value.gameObject.SetActive(true);
    }
    
    public void ChangeScreen(Screen screen)
    {
        screens.ForEach(screenItem => screenItem.value.gameObject.SetActive(false));
        screen.gameObject.SetActive(true);
    }
}