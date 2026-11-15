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

## Groups

A mod with many options can group them. Each group gets its own tab on the options page:

```csharp
var speed = context.Options.Group("Speedometer");
var colors = context.Options.Group("Colors");

var isShown = speed.OnOff("Shown", "Show the speed", true);
var tiles = colors.Color("Tiles", "Tiles", new Color(12, 16, 58));
```

- Calling `Group` again with the same label gives the same tab.
- Tabs are in the order of their first option. Players switch tabs with Page Up and Page Down, the shoulder buttons, or a click.
- Options declared outside any group go under a tab named General. With a single tab, the page shows no tabs at all.
- Ids must be unique across the whole mod, not just within a group. Moving an option to another group keeps its saved value.
- `speed.Lobby` declares lobby options under the same tab.

Groups need ModRunners 0.2 or later.

## Lists

A list option holds items the player adds and removes, each made of fields of the kinds above. Declare its fields on the list, then its default items:

```csharp
var bands = context.Options.List("SpeedBands", "Speed colors", maxItems: 10);
var speed = bands.Number("Speed", "Up to speed", 0, 0, 1500, 10);
var color = bands.Color("Color", "Color", new Color(255, 255, 255));

bands.SortedBy(speed);
bands.AddDefault(band => band.Set(speed, 200).Set(color, new Color(255, 255, 255)));
bands.AddDefault(band => band.Set(speed, 450).Set(color, new Color(63, 242, 236)));
```

- Read the items with `bands.Items`, and each value with `item.Get(field)`. `Changed` is raised when the player edits the list.
- Each field keeps its kind's rules, so a number stays in its range: the player cannot make an invalid item.
- `SortedBy` keeps the items in the order of a number, choice, key or text field, and no two items can share its value. Without it, items stay in the order the player puts them in: new items go at the end, and Up and Down move them.
- The options page shows the list in its tab, inside a frame: each item as a card with its fields and its buttons, and Add. While the player edits a sorted list, its items stay where they are on the page, and take their order once the player leaves the list. `Items` is always sorted.
- Declare the fields and default items in `OnLoad`, before reading `Items`.

Lists need ModRunners 0.3 or later.

## Lobby options

Options that change how the game plays must be the same on every game of a match. Declare them on `context.Options.Lobby` instead:

```csharp
var gravity = context.Options.Lobby.Number("Gravity", "Gravity (%)", 100, 25, 200, 5);
```

In a modded lobby, the host's values apply to every game, and the options page shows them without letting the other players change them. Each player gets their own values back when they leave. Keep keys, colors and everything else that concerns only the player on the mod's own options. [Multiplayer](Multiplayer.md) says more.
