# The manifest

Every mod folder has a `manifest.json` next to its dll. ModRunners reads it before loading the mod, and the Mod Browser shows it to players.

```json
{
    "Id": "MyMod",
    "Name": "My Mod",
    "Version": "1.0.0",
    "Author": "Your name",
    "Description": "What the mod does, as players read it in the Mod Browser.",
    "Assembly": "MyMod.dll",
    "Capabilities": ["Observe", "Present"]
}
```

| Field | |
|---|---|
| `Id` | Your mod's identity. It must be the name of its folder in `ModRunners/Mods/`, which the template makes the project's name. It never changes: the Mod Browser, players' saved options and online validation all know your mod by it. |
| `Name` | Its name, as players read it. |
| `Version` | Two to four numbers, such as `1.0.0`. Each upload needs a higher one. |
| `Author` | Optional. You, as players read it. |
| `Description` | Optional, and worth writing: what the mod does, and what it changes. |
| `Assembly` | The dll's file name. |
| `Capabilities` | What the mod does, from the list below. |

A broken manifest is reported in `Runtime.log` with the faulty field, and the mod is skipped.

## Capabilities

They say what kind of mod yours is. Today ModRunners logs them without enforcing them, but declare them honestly: I read them when I review a mod for online play.

| Capability | For a mod that... |
|---|---|
| `Observe` | reads the game and reacts to its events |
| `Present` | draws on the screen or changes how things look |
| `Local` | changes the player's own settings, such as the frame limit |
| `Gameplay` | changes how the game plays: movement, items, rules, game modes |
| `Unsafe` | reaches past the Api. Avoid it: such a mod is never validated for online play |

## The icon

Add an `icon.png` next to the manifest, and the Mods page and the Mod Browser show it at the start of your mod's row. The template's project copies it with each build.

- A PNG of 256 KB at most, and 512 pixels at most on each side.
- It is shown as a square, so a square image of 128 pixels looks best.

Without one, your mod gets ModRunners' default icon.

## Your mod's other files

Images and other files your mod reads go in its folder too. List each one in the project file, so that builds copy it:

```xml
<ItemGroup>
    <ModFile Include="Logo.png" />
</ItemGroup>
```

Everything in your mod's folder is part of the mod: players get all of it when they install your mod, and online validation covers all of it.
