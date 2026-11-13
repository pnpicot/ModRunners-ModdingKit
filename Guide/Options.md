# Options

Your mod's options are settings players change in the game, on the options page: F1 anywhere, or **ModRunners options** under Game options in the game's Options. Each mod with options has its own entry there. ModRunners saves each value, so it is kept between starts.

## Declaring them

Options are declared in `OnLoad`, in the order the options page lists them:

```csharp
var options = context.Options;

var isShown = options.OnOff("Shown", "Show the speed", true);
var key = options.Key("ToggleKey", "Show or hide", Key.F3);
var size = options.Number("Size", "Size (%)", 100, 50, 200, 10);
var font = options.Choice("Font", "Font", Font.Kooky22);
var text = options.Text("Title", "Title", "Hello!", 40);
var color = options.Color("Color", "Color", new Color(255, 200, 0));
```

| Kind | Holds |
|---|---|
| `OnOff` | `true` or `false` |
| `Number` | A `double` from a minimum to a maximum, in steps |
| `Choice` | A value of an enum, yours or the Api's. The page writes its name with spaces between words. |
| `Key` | A key of the keyboard, or `Key.None` |
| `Text` | One line of text, up to a length |
| `Color` | An opaque color |

The first argument is the option's **id**, what its value is saved under. The label can change from one version to the next, but the id must not, or players lose the value they chose.

## Using them

`option.Value` is the value in effect, the player's or the default. Read it when you need it, or follow its changes:

```csharp
color.Changed += (_, e) => context.Log.Write($"From {e.Previous} to {e.Current}");
```

`Changed` is raised for each change on the options page, as the player makes it, and for your mod's own. Setting `Value` from your mod saves it, as if the player had changed it: a key that toggles an option does exactly that.

See [ExampleOptions](../Examples/ExampleOptions).

## Lobby options

Options that change how the game plays must be the same on every game of a match. Declare them on `context.Options.Lobby` instead:

```csharp
var gravity = context.Options.Lobby.Number("Gravity", "Gravity (%)", 100, 25, 200, 5);
```

In a modded lobby, the host's values apply to every game, and the options page shows them without letting the other players change them. Each player gets their own values back when they leave. Keep keys, colors and everything else that concerns only the player on the mod's own options. [Multiplayer](Multiplayer.md) says more.
