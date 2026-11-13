# Items

`context.Game.Items` is the game's items: the values they play by, the ones in play, and what happens to them. `context.Items` adds your own.

## How items play

`Items.Rules` holds one group of values per item: `Ice`, `Fireball`, `Rockets`, `GoldenHook`, `Bomb`, `Shockwave`, `Box`, `InvinciDrill`, `Confetti`. Each value's documentation gives the game's default.

```csharp
var rules = context.Game.Items.Rules;

rules.Rockets.Speed = 1200;
rules.Ice.FreezeDuration = TimeSpan.FromSeconds(1);
```

That is the rule for every runner. `runner.Items` holds one runner's own values, which follow the rule for everything you do not set, and `Reset()` brings either back to the game's values.

## Items in play

Each item kind lists its instances in the level: `Items.Rockets.InPlay`, `Items.Bomb.InPlay`, `Items.Fireball.InPlay`... Each has its owner's slot, its hitbox, values of its own, and what the game itself can do to it, such as `Explode()` or `Break()`. Some kinds also say when one appears, such as `Items.Rockets.Fired` and `Items.Shockwave.Released`.

## What happens

- `Items.Happened` is raised for each pickup, use, swap, explosion, hit, freeze... of every runner, with `ItemMoment` saying which.
- `Items.Hitting` is raised just before an item hits a runner, and `e.Refuse()` cancels the hit: no push, no stun, no freeze.
- `Items.Pickups.Rolled` is raised when a runner of this game takes an item pickup, and `e.Replace(...)` gives it another item.
- `Items.Pickups` also sets how likely each item is to be rolled, and, for the host of a custom match, which items are enabled.

## Your own items

```csharp
var pistol = context.Items.Define("Pistol", "Pistol", context.Drawing.LoadImage("Pistol.png"), maxUses: 3);

pistol.RollWeight = 15;
pistol.Used += (_, e) => context.Log.Write($"{e.Runner.Slot.Name} fired, {e.UsesLeft} shots left");
```

- **Define them in `OnLoad`,** and never change their ids: every game of a match must agree on them.
- **The icon** is drawn in a 64-pixel square, in the HUD and above the runner.
- **`RollWeight`** is how likely an item pickup is to give it, on the scale of the game's items: the shockwave is about 15 for a runner in the middle of the race. At 0, the default, only your mod gives it, with `runner.GiveModItem(...)`.
- **`Used`** is raised on every game of a match, for its own copy of the runner, so each game plays the use itself. What the use does is up to you: here, every game fires the same rocket.

A game without your mod shows your item with the confetti's icon, and nothing happens when it is used. See [ExampleItem](../Examples/ExampleItem).

## Online

Changing how items play is gameplay. Every game simulates every runner's items with its own values, and no value is sent: a change only agrees across a match when every game makes it. Such a mod plays online in modded lobbies only, with its values as [lobby options](Options.md#lobby-options). [Multiplayer](Multiplayer.md) explains it.
