# Getting started

## What you need

- **SpeedRunners with ModRunners installed,** from the ModRunners Discord server. Your mod is built against the Api that ModRunners installed, in `<game>/ModRunners/ModRunners.Api.dll`, so it always matches the version you play with.
- **The .NET SDK,** version 8 or newer: https://dotnet.microsoft.com/download. Visual Studio is not needed. Any editor works, and Visual Studio Code with the C# Dev Kit or JetBrains Rider show the Api's documentation as you type.

Mods target .NET Framework 4.8 (the version used by the game). The template sets that up, and still lets you use recent C#.

## Creating a mod

Install the template once, from your copy of this repository:

```
dotnet new install <this repository>\Template
```

Then, wherever you keep your projects:

```
dotnet new modrunners-mod -n MyMod --modName "My Mod" --modAuthor "Your name"
```

- `-n MyMod` is your mod's **id**: the project, its namespace, its dll and its folder in the game all take this name. Choose it well, since it cannot change once players have your mod. Letters and digits only.
- `--modName` and `--modAuthor` are what players read in the Mod Browser. You can change them later in `manifest.json`.
- If SpeedRunners is not in Steam's default folder, add `--gameFolder "D:\Games\SteamLibrary\steamapps\common\SpeedRunners"`, or change `GameFolder` in the project file later.

You get:

```
MyMod/
    MyMod.csproj     the project
    Mod.cs           your mod's code
    manifest.json    who your mod is (see The manifest)
```

## Building and trying it

```
dotnet build
```

Each build copies your mod into `<game>/ModRunners/Mods/MyMod/`: the dll, `manifest.json`, `icon.png` if you add one, and the files you list as `ModFile` in the project. Start the game from Steam: ModRunners loads every mod of that folder at startup.

**Close the game before building.** The game keeps your dll open while it runs, so the build cannot replace it, and mods only load when the game starts anyway.

The main menu says how many mods loaded. `ModRunners/Logs/Runtime.log` has a line for your mod, and the line it writes:

```
[MyMod] My Mod 1.0.0 is loaded.
```

## Your mod's code

```csharp
public sealed class Mod : IMod
{
    public void OnLoad(IModContext context)
    {
        context.Log.Write($"{context.Name} {context.Version} is loaded.");
    }
}
```

ModRunners creates your `Mod` class when the game starts, then calls `OnLoad` once. Everything starts from `context`:

| Member | What it is |
|---|---|
| `context.Game` | The running game: screens, lobbies, matches, runners, items, tiles, settings, keyboard and mouse |
| `context.Drawing` | Drawing on the screen each frame, and loading images |
| `context.Options` | Your mod's options, which players change in the game |
| `context.Messages` | Messages to your mod on the other players' games |
| `context.Items` | New items your mod adds |
| `context.Log` | Your lines in `Runtime.log` |
| `context.Id`, `Name`, `Version` | What your manifest says |

`OnLoad` runs before the game starts, so there is no lobby or match yet: subscribe to the events you need there, and react when they come. [The game](TheGame.md) is the next page.

Your mod's assembly has exactly one public class implementing `IMod`, with a public constructor that takes no argument. If `OnLoad` throws, your mod is disabled and the log says why.

## Turning your mod off

The Mods page, under **Replays** on the main menu, turns any mod off for the next start. You can also rename its folder in `ModRunners/Mods/` so that it starts with `_`, like `_MyMod`: a build then keeps deploying it there, still off.
