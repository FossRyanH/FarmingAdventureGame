using Godot;
using System;

public partial class MusicManager : Singleton<MusicManager>
{
    private AudioStreamPlayer _currentPlayer;
    private AudioStreamPlayer _nextPlayer;

    [Export] public float MusicVolumeDb = -10f;
    
    protected override void Initialize()
    {
        _currentPlayer = new AudioStreamPlayer();
        _nextPlayer = new AudioStreamPlayer();
        
        AddChild(_currentPlayer);
        AddChild(_nextPlayer);
        
        _currentPlayer.VolumeDb = MusicVolumeDb;
        _nextPlayer.VolumeDb = MusicVolumeDb;
    }

    public void PlayMusic(AudioStream newTrack, float fadeDuration = 1.0f)
    {
        if (_currentPlayer.Stream == newTrack) { return; }

        _nextPlayer.Stream = newTrack;
        // This starts the player muted.
        _nextPlayer.VolumeDb = -80f;
        _nextPlayer.Play();

        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(_nextPlayer, "volume_db", MusicVolumeDb, fadeDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(_currentPlayer, "volume_db", -80f, fadeDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);

        tween.TweenCallback(Callable.From(() =>
        {
            _currentPlayer.Stop();
            SwapPlayers();
        }));
    }

    public void StopMusic(float fadeDuration = 1.0f)
    {
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(_currentPlayer, "volume_db", -80f, fadeDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        tween.TweenCallback(Callable.From(() =>
        {
            _currentPlayer.Stop();
            _currentPlayer.Stream = null;
        }));
    }

    void SwapPlayers()
    {
        var tempPlayer = _currentPlayer;
        _currentPlayer = _nextPlayer;
        _nextPlayer = tempPlayer;
    }
}
