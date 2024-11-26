using Godot;
using System;

[GlobalClass]
public partial class PlayerVariablesRes : Resource
{
    [Export] public float WalkSpeed;
    [Export] public float DashSpeed { get; set; } = 300f;
}
