# Multiplayer

## Online play needs validation

SpeedRunners has no protection against cheating online. So a player can only play online while every mod they loaded is **validated**: I read the whole mod, and add that exact version to a signed list that every game downloads. There are two kinds:

- **For every online play:** the mod only changes what its player's game shows or how it runs, such as drawing, colors or the frame limit. Quick matches, ranked and custom matches work as usual.
- **For modded lobbies only:** the mod changes how the game plays. It plays online in custom matches where every player has the same mods, and never in quick matches or ranked.

Offline, any mod plays. [Publishing](Publishing.md) says how to ask for a validation.

## Every game simulates every runner

This is the one thing to understand before changing gameplay. In a match, each player's game runs the whole match on its own: every runner, every item, every round. The games only send each other what their own players do, and where their runners are.

So:
- **A change only agrees when every game makes it.** If your mod makes rockets faster on your game only, your rockets fly faster on your screen, and at the normal speed on everyone else's.
- **Values are never sent.** Movement values, item values and your mod's state stay on each game. Make the same change on every game: from [lobby options](Options.md#lobby-options), which the host decides for everyone, or from a message.
- **Each runner belongs to its player's game.** That game decides what happens to it, and the others follow. This is why most actions on a runner, such as `MoveTo` or `GiveItem`, need `slot.IsLocal`, and why events such as `RunnerActed` and `Items.Happened` say `IsDecidedHere`.

## Modded lobbies

A player whose mods are validated for modded lobbies hosts a custom match, and invites friends. To join, a game needs the same ModRunners version and the same mods for modded lobbies, in the same versions. Mods validated for every online play do not count, so players can keep their own. ModRunners checks it as each game joins.

## Messages

`context.Messages` sends bytes to your mod on the other games of a modded lobby, for what only one game knows or decides, such as a random choice or a key its player pressed.

```csharp
var messages = context.Messages;

// Tells the other games that this game's runner did something.
void Tell(IRunner runner) => messages.Send([(byte)runner.Slot.Index]);

messages.Received += (_, e) =>
{
    // Trust a message about a runner only from the game that plays it.
    if (e.Message.Length == 1 && e.SenderSlots.Any(slot => slot.Index == e.Message[0]))
        context.Log.Write($"Slot {e.Message[0]} did something");
};
```

- Messages from one game arrive reliably and in order, at the end of a frame, but not in a fixed order with the game's own: one sent right after an elimination can arrive before it.
- A message is at most `MaxLength` bytes. Outside a modded lobby, `Send` sends nothing.
- `e.SenderSlots` are the slots the sending game plays, and `e.IsFromHost` says whether it hosts the lobby.

See [ExampleMoonJump](../Examples/ExampleMoonJump): a key gives the player's runner moon gravity, and a message gives it to that runner's copy on every other game.
