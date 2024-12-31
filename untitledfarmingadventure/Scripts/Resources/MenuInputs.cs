using Godot;
using System;

[GlobalClass]
public partial class MenuInputs : Resource
{
    public event Action OpenInventory, CloseInventory;
    
    public void HandleOpenInventory() => OpenInventory?.Invoke();
    public void HandleCloseInventory() => CloseInventory?.Invoke();
}
