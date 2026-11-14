# ModRunners modding kit

ModRunners lets players run mods in SpeedRunners. This repository is everything you need to make one: a template, example mods, this guide, and the Api's reference.

A mod is a small C# library built against the ModRunners Api. ModRunners loads it when the game starts, and the Api gives it the game as players know it: lobbies, matches, runners, items, tiles, settings, the screen to draw on. A mod never touches the game's code itself.

ModRunners is in alpha, and the Api will still grow. Questions, ideas and bugs are welcome on the ModRunners Discord server: https://discord.gg/ezD7pZTF6

## Quick start

With SpeedRunners and ModRunners installed, and the [.NET SDK](https://dotnet.microsoft.com/download) 8 or newer:

```
dotnet new install <this repository>\Template
dotnet new modrunners-mod -n MyMod --modName "My Mod" --modAuthor "Your name"
cd MyMod
dotnet build
```

Start the game: on a clean install the main menu says "loaded (1 mod)", and `ModRunners/Logs/Runtime.log` has your mod's first line. [Getting started](Guide/GettingStarted.md) explains each step.

## The guide

1. [Getting started](Guide/GettingStarted.md): the template, the project, building and trying your mod.
2. [The manifest](Guide/Manifest.md): your mod's id, version, icon and capabilities.
3. [The game](Guide/TheGame.md): what the Api shows of the game, its events, its units, and the rules every mod follows.
4. [Drawing](Guide/Drawing.md): text, shapes and images on the screen and in the level.
5. [Options](Guide/Options.md): settings players change on the options page.
6. [Items](Guide/Items.md): changing the game's items, and adding your own.
7. [Multiplayer](Guide/Multiplayer.md): how online play works with mods, lobby options and messages between games.
8. [Publishing](Guide/Publishing.md): uploading your mod, getting it validated for online play, and new versions.
9. [Troubleshooting](Guide/Troubleshooting.md): logs, the console, and the usual mistakes.

## The reference

[Every type and member of the Api](Reference/ModRunners.Api.md), by namespace, with its documentation. It is the same documentation your editor shows as you type, for the latest ModRunners version.

## The examples

Each one shows one part of the Api, in a single short file, and builds on its own like the template.

| Example | What it shows |
|---|---|
| [ExampleHello](Examples/ExampleHello) | The smallest mod: the manifest, `IMod`, the log |
| [ExampleDrawing](Examples/ExampleDrawing) | An image on the menu, and text above each runner |
| [ExampleOptions](Examples/ExampleOptions) | One option of each kind, and reacting to changes |
| [ExampleEvents](Examples/ExampleEvents) | Screens, rounds and eliminations as they happen |
| [ExampleTiles](Examples/ExampleTiles) | Recoloring the levels: rules, exceptions, and restoring the game's look |
| [ExampleItem](Examples/ExampleItem) | A new item in the item pickups |
| [ExampleMoonJump](Examples/ExampleMoonJump) | One runner's own movement, and messages to the other games |

## License

The guide, the template and the examples are under the [MIT License](LICENSE): copy anything you need into your own mods. The Api itself is under the MIT License too, and ships with ModRunners.
