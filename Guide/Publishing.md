# Publishing

Read the rules in #rules on the Discord server first: they say what a mod may do, and what gets it taken down.

## Uploading

1. **Raise the version** in `manifest.json` if you uploaded this mod before: each upload needs a higher one.
2. **Build in Release,** with the game closed:

    ```
    dotnet build -c Release
    ```

3. **Start the game,** open the Mods page under **Replays** on the main menu, select your mod, and press **Publish**, then **Confirm publish**.

The upload is your mod's folder in `ModRunners/Mods/`, exactly as it is: keep only your mod's files in it. ModRunners signs you in with your Steam account, and the service checks the upload:

- a virus scan, which refuses it if anything is found;
- your manifest and icon, as ModRunners reads them;
- your mod's id: the first upload of an id makes it yours, and only you can upload it again;
- your dll: the same file as someone else's mod is refused as a copy;
- what your code uses: a mod that uses the network, reads or writes files, runs programs, uses reflection or patches code is **held** for my review before anyone can download it. Otherwise it is published at once.

The Mods page tells you the result. A published mod shows in every player's Mod Browser, where you can also **Unpublish** it.

## Getting validated for online play

Published is not validated: players with your mod can only play online once I validated it ([Multiplayer](Multiplayer.md)).

- I read the whole mod, so it can take time: please be patient.
- A validation covers one version, the files exactly as uploaded. A new version needs a new validation, so upload new versions when they bring or fix something that matters.
- A mod that gives an unfair advantage online is never validated for every online play. It can still be validated for modded lobbies, if it says what it does.

## New versions of ModRunners

Your mod is built against the Api of the ModRunners you have. ModRunners updates itself on every player's game, so players nearly always have the latest.

- A mod built against a newer Api than a player's ModRunners does not load on their game, and their log says to update. That only lasts until their ModRunners updates.
- A mod built against an older Api keeps loading: rebuild it when you want what a new version of the Api brings. #updates says what changed.
