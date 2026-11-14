# The game

`context.Game` is the game as players know it. Every type and member has its documentation in your editor: hover over it to read what it does and when. The [reference](../Reference/ModRunners.Api.md) has the same documentation, to browse without a project.

## A few rules first

- **Everything happens on the game's thread.** Every event is raised there, so your handlers can use the whole Api without locks. Do not block it: a slow handler slows the game. Work on another thread is fine, but come back to the Api from an event.
- **A handler that throws is removed.** The error goes to `Runtime.log`, and your other handlers keep working. The game never crashes because of a mod's exception.
- **Read things when you need them.** A runner, a slot or an item in play is the game's live state. Some are replaced as the game goes on: each type's documentation says how long it stays valid.
- **Two kinds of units.** The level is in level units: a map's tile is 16 of them (`Level.TileSize`), and positions scroll and zoom with the camera. The screen is in pixels of the game's window. Y grows downward in both.

## What is there

| Member | What it is |
|---|---|
| `Screen`, `ScreenChanged` | The screen the player is on: main menu, character selection, match... |
| `MainMenu` | Buttons you add to the main menu, with their `Pressed` event |
| `Keyboard`, `Mouse` | The keys as gameplay reads them, and the mouse as the menus do |
| `Lobby` | The current game's players and slots, from the character selection to the end of its matches |
| `OpenLobbies` | The lobbies other players host, which the player could join |
| `Match` | The match being played: its rounds, eliminations, results and XP screen |
| `Level`, `LevelChanged` | The map being played, and its tiles |
| `Tiles` | How tiles look on every map: palette, borders, outline |
| `Grapple` | The colors of the runners' grapples |
| `Movement` | The values every runner moves by: running, air, jumps, slide, swing, boost |
| `RunnerActed` | Each jump, landing, wall hang, slide, boost, hook shot, stumble... of every runner |
| `Items` | The items: the values they play by, the ones in play, and what happens to them |
| `Settings` | The game's settings, as the options menu groups them, and the launch options |

## Slots and runners

A lobby has slots, four in the game, each played by a player, a bot, or nobody. During a match, each played slot has a runner:

```csharp
foreach (var slot in game.Lobby.Slots)
{
    if (slot.Runner is { IsOut: false } runner)
        context.Log.Write($"{slot.Name} runs at {runner.Speed}");
}
```

`slot.IsLocal` tells the slots played from this computer: its players and their bots. Most actions on a runner, such as `MoveTo`, `GiveItem` or `Freeze`, are only allowed on the runners your game plays, since each runner belongs to the game of its player. [Multiplayer](Multiplayer.md) explains why.

## Following a match

```csharp
var match = context.Game.Match;

match.RoundStarted += (_, _) => context.Log.Write("GO!");
match.RunnerOut += (_, e) => context.Log.Write($"{e.Runner.Slot.Name} is out ({e.Cause})");
match.RoundEnded += (_, e) => context.Log.Write($"{e.Winner?.Name ?? "Nobody"} wins the round");
```

`match.Updated` is raised at each frame of a round, after the game has moved the runners: the place for anything that has to follow the race closely. `context.Drawing.Draw` is raised at every frame, in rounds or not. See [ExampleEvents](../Examples/ExampleEvents).

## Rules and exceptions

Many things work as a rule for everyone, with exceptions for some:

- `game.Movement` is how every runner moves, and `runner.Movement` one runner's own values, which follow the rule for everything you do not set.
- `game.Tiles.Palette` colors every tile type, and `SetPalette(type, ...)` one type.
- `game.Items.Rules` is how every runner's items play, and `runner.Items` one runner's own values.
- `game.Grapple.Colors` colors every runner's grapple, and `SetColors(slot, ...)` one slot's.

Each has a way back to the game's own values, such as `Reset()` or `ClearPalette(type)`, which your mod uses when it turns off. See [ExampleTiles](../Examples/ExampleTiles).

## Settings

`game.Settings` holds the options menu's settings and the launch options. Each one has its `Value`, its `Default`, and a `Changed` event. Depending on its kind, you can override it for the session, which never touches the player's saved options, or save a new value, which does. Prefer overriding: the player keeps their own choice once your mod is gone.
