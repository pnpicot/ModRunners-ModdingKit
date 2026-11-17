# Troubleshooting

## The logs

Everything ModRunners does goes to `<game>/ModRunners/Logs/Runtime.log`, rewritten at each start: the mods it found and loaded, why one did not load, and each line your mod writes, after its id:

```
[MyMod] My Mod 1.0.0 is loaded.
```

Write to it whenever something is worth knowing, with `context.Log.Write(...)`. It is safe from any thread.

To watch it live, add `--console` to the Steam launch option, before `%command%`: a console opens next to the game with ModRunners' logs as they are written, and the game's own messages. Closing the console closes the game, so quit from the game's menu.

The game writes its own errors in `%APPDATA%\SpeedRunnersLog.txt`.

## The usual problems

**The build says the dll is in use.** The game is running and holds your mod's dll. Close it, and build again.

**The build cannot find `ModRunners.Api`.** `GameFolder`, in your project file, is not where SpeedRunners and ModRunners are installed. In Steam, right-click SpeedRunners, then **Manage > Browse local files** to see the right folder.

**The mod does not load.** `Runtime.log` says why:
- the manifest has a problem, such as an `Id` that is not the folder's name;
- the assembly has no public class implementing `IMod`, or several, or the class has no public constructor without arguments;
- "built against ModRunners.Api ..., newer than this Framework's": your ModRunners is older than the Api you built against. Start the game once without `--no-update`, so that it updates.
- "is disabled": `OnLoad` threw, and the error follows.
- "uses the assembly of ... without listing ... in its manifest's Dependencies": your project references another mod, which your manifest must list ([Libraries](Libraries.md)).
- "Its assembly is named ..., as the assembly of ...": two mods' dlls have the same name. Rename yours: the template names it after your project.

**The Mods page says "needs a mod".** A mod your manifest lists in `Dependencies` is not installed, turned off, older than the version you ask, or failed to load. The line under its details says which ([Libraries](Libraries.md)).

**Something stops working during the game.** An event handler threw: `Runtime.log` has the error, and that handler was removed. Your other handlers keep working.

**My changes do nothing on the other players' games.** Each game runs the match on its own: see [Multiplayer](Multiplayer.md).

**Online play is refused with my mod.** Your mod is not validated yet, or you changed it since: see [Publishing](Publishing.md).

## Asking for help

Ask in #technical on the Discord server, with what you tried and the part of `Runtime.log` about your mod.
