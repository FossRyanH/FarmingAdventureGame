using Godot;
using System;

[GlobalClass]
public partial class MenuInputs : Resource
{
    public static event Action OpenInventory, CloseInventory;
    
    public void HandleOpenInventory() => OpenInventory?.Invoke();
    public void HandleCloseInventory() => CloseInventory?.Invoke();
}
