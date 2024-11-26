using Godot;
using System;

public partial class TestingGrounds : Node2D
{
    [Export] private AudioStream backgroundMusic01;

    public override void _Ready()
    {
        MusicManager.Instance.PlayMusic(backgroundMusic01);
    }
}
