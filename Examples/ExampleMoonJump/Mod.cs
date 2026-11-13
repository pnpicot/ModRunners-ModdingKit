using System.Diagnostics;
using ModRunners.Api;
using ModRunners.Api.Input;
using ModRunners.Api.Messaging;
using ModRunners.Api.Options;
using ModRunners.Api.Runners;

namespace ExampleMoonJump;

/// <summary>
/// A key gives the player's runner moon gravity for a few seconds, and tells the other games of a modded lobby, so that their copy of the runner floats too.
/// </summary>
/// <remarks>
/// The game never sends a runner's movement values: each game simulates every runner with its own. Without the message, the other games would see the runner fall at the normal speed, and keep pulling it back toward where its own game puts it.
/// </remarks>
public sealed class Mod : IMod
{
    private const float MoonGravity = 0.25f;

    /// <summary>
    /// A message: the runner's slot, then 1 when the moon starts and 0 when it ends.
    /// </summary>
    private const int MessageLength = 2;

    private static readonly TimeSpan MoonTime = TimeSpan.FromSeconds(5);

    private readonly Stopwatch _moon = new();

    private IGame _game = null!;
    private IMessages _messages = null!;
    private IOption<Key> _moonKey = null!;

    public void OnLoad(IModContext context)
    {
        _game = context.Game;
        _messages = context.Messages;
        _moonKey = context.Options.Key("MoonKey", "Moon gravity", Key.M);

        _game.Keyboard.KeyPressed += (_, e) =>
        {
            if (e.Key == _moonKey.Value)
                StartMoon();
        };

        _game.Match.Updated += (_, _) => EndMoonOnTime();
        _messages.Received += OnMessageReceived;
    }

    /// <summary>
    /// The runner of the first player on this computer.
    /// </summary>
    private IRunner? Player =>
        _game.Lobby.Slots.FirstOrDefault(slot => slot.IsLocal && !slot.IsBot)?.Runner;

    private void StartMoon()
    {
        if (_moon.IsRunning
            || !_game.Match.IsRoundInPlay
            || Player is not { IsOut: false } player)
            return;

        SetMoon(player, true);
        Send(player, true);
        _moon.Restart();
    }

    private void EndMoonOnTime()
    {
        if (!_moon.IsRunning || _moon.Elapsed < MoonTime)
            return;

        _moon.Reset();

        if (Player is { } player)
        {
            SetMoon(player, false);
            Send(player, false);
        }
    }

    private void SetMoon(IRunner runner, bool isOn)
    {
        // A runner's own values only change that runner. The others keep following IGame.Movement.
        if (isOn)
        {
            runner.Movement.Air.Gravity = _game.Movement.Air.Gravity * MoonGravity;
        } else
        {
            runner.Movement.Reset();
        }
    }

    /// <summary>
    /// Outside a modded lobby, there is nobody to tell and nothing is sent.
    /// </summary>
    private void Send(IRunner runner, bool isOn) =>
        _messages.Send([(byte)runner.Slot.Index, isOn ? (byte)1 : (byte)0]);

    private void OnMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        // A message about a runner is only trusted from the game that plays it.
        if (e.Message.Length != MessageLength
            || e.SenderSlots.FirstOrDefault(slot => slot.Index == e.Message[0]) is not { Runner: { } runner })
            return;

        SetMoon(runner, e.Message[1] == 1);
    }
}
