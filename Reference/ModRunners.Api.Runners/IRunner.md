# IRunner interface

The character a slot's player, bot or replay controls in a match. A runner is created at the match's first countdown and kept for every round, until its player leaves or the match closes. Read it from [`Runner`](../ModRunners.Api.Lobbies/ISlot/Runner.md) when needed: once its player left, a runner kept from before keeps its last values.

```csharp
public interface IRunner
```

## Members

| name | description |
| --- | --- |
| [BoostMeter](IRunner/BoostMeter.md) { get; } | What its boost meter holds, from 0 to [`MaxBoostMeter`](./IRunner/MaxBoostMeter.md). |
| [CountsInRace](IRunner/CountsInRace.md) { get; set; } | Whether the runner takes part in the race: the camera follows it, it is ranked by its progress, it can be eliminated, and it keeps the round going. True by default. A runner left out still runs and touches the others, but it ranks with the eliminated runners, never wins, is never eliminated, and the round ends as if it were out. |
| [DrillTimeLeft](IRunner/DrillTimeLeft.md) { get; } | How long it keeps drilling, or zero when it does not drill. |
| [FacesRight](IRunner/FacesRight.md) { get; } | Facing right, or left. |
| [FrozenTimeLeft](IRunner/FrozenTimeLeft.md) { get; } | How long it stays frozen, or zero when it is not. It counts down only while the runner is not pulled by a golden hook or tumbling. |
| [HeldItem](IRunner/HeldItem.md) { get; } | The game's item it holds, or null when it holds none or a mod's ([`HeldModItem`](./IRunner/HeldModItem.md)). A runner keeps its item when it is eliminated, and into the next rounds. |
| [HeldModItem](IRunner/HeldModItem.md) { get; } | The mod item it holds, or null when it holds none or one of the game's ([`HeldItem`](./IRunner/HeldItem.md)). |
| [Hitbox](IRunner/Hitbox.md) { get; } | The box the runner collides with, in level units: 25 wide and 45 high for every character, or 25 high with the same bottom while it slides or is in a super boost. The runner is drawn around it. |
| [HookPosition](IRunner/HookPosition.md) { get; } | Where its grapple's hook is while it is out, in level units: the middle of the flying hook, then the point of the ceiling it holds once caught. Null while the hook is in. |
| [IsBoosting](IRunner/IsBoosting.md) { get; } | Spending its boost meter. The flag stays while the runner tumbles, is frozen or pulled, though nothing is spent then. |
| [IsDrilling](IRunner/IsDrilling.md) { get; } | Drilling, the InvinciDrill's effect: nothing hits, stuns, freezes or hooks it, it boosts for free, breaks through boxes and obstacles, and tackles every runner it runs into. |
| [IsFrozen](IRunner/IsFrozen.md) { get; } | Frozen by the ice. |
| [IsInAir](IRunner/IsInAir.md) { get; } | In the air: from a jump, a fall or a swing until it lands. |
| [IsInSuperBoost](IRunner/IsInSuperBoost.md) { get; } | Thrown by a super boost or a bounce pad, its inputs ignored. |
| [IsKnockedOut](IRunner/IsKnockedOut.md) { get; } | Knocked out in the current round: thrown up and falling, out of the race, until the camera eliminates it as it leaves the view (KnockedOut). Spikes, saws and lasers knock runners out in a ranked match or with the Lethal Spikes modifier, and [`KnockOut`](./IRunner/KnockOut.md) in any match. |
| [IsOut](IRunner/IsOut.md) { get; } | Eliminated in the current round. The runner stays where it went out, hidden, until the next round. |
| [IsSliding](IRunner/IsSliding.md) { get; } | Sliding, on the ground or in the air. |
| [IsSwinging](IRunner/IsSwinging.md) { get; } | Swinging from its grapple's hook, caught on a grapple ceiling. |
| [IsTumbling](IRunner/IsTumbling.md) { get; } | Tumbling from a hit, such as an item or a lethal obstacle: it only falls and slides until it recovers. |
| [IsWallHanging](IRunner/IsWallHanging.md) { get; } | Hanging on a wall, from which it can wall jump. |
| [Items](IRunner/Items.md) { get; } | The values this runner's items play by: the items it uses, and their effects on the runners they reach. Setting one gives the runner its own value, and the values it does not set keep following [`Rules`](../ModRunners.Api.Items/IItems/Rules.md). [`Reset`](../ModRunners.Api.Items/IItemRules/Reset.md) has it follow [`Rules`](../ModRunners.Api.Items/IItems/Rules.md) again for every value. |
| [ItemUsesLeft](IRunner/ItemUsesLeft.md) { get; } | The uses left of the item it holds: 3, 2 or 1 for the triple box and the triple rockets, 1 for the game's other items, from 1 to its [`MaxUses`](../ModRunners.Api.Items/IModItem/MaxUses.md) for a mod item, and 0 when it holds none. |
| [JumpsLeft](IRunner/JumpsLeft.md) { get; } | The jumps it can still make before landing: 2 on the ground, 1 after a jump or walking off a ledge, 0 after an air jump. A wall hang and a lock of the grapple give them back, and the jump off them uses one. |
| [MaxBoostMeter](IRunner/MaxBoostMeter.md) { get; } | What its boost meter can hold: [`MaxMeter`](./IBoostRules/MaxMeter.md), halved for the golden variant, and the sum of both teammates' in a team. |
| [Movement](IRunner/Movement.md) { get; } | The values this runner moves by. Setting one gives the runner its own value, and the values it does not set keep following [`Movement`](../ModRunners.Api/IGame/Movement.md). [`Reset`](./IMovementRules/Reset.md) has it follow [`Movement`](../ModRunners.Api/IGame/Movement.md) again for every value. |
| [Opacity](IRunner/Opacity.md) { get; set; } | How opaque the runner is drawn, from 0, invisible, to 1, the default. It covers the character, its effects, icons and hooks, and the player indicator, but not the rope, the trail or the popup texts. |
| [Position](IRunner/Position.md) { get; } | Where the runner stands, in level units: the middle of the bottom edge of its hitbox, which stays in place while it slides. |
| [PulledBy](IRunner/PulledBy.md) { get; } | The slot of the runner whose golden hook pulls it, or null when none does. |
| [RacePosition](IRunner/RacePosition.md) { get; } | The runner's place in the race, 0 for the front runner. The order is the progress within the lap, recomputed every frame. Runners going the wrong way rank behind the others, and eliminated runners last. |
| [ShockwaveCharge](IRunner/ShockwaveCharge.md) { get; } | How charged its shockwave is, from 0 to 1, while its player holds the item button with the shockwave: a full charge sends it 300 units further ahead. Only the game that plays the runner knows the charge: elsewhere, it stays at 0. |
| [Slot](IRunner/Slot.md) { get; } |  |
| [Speed](IRunner/Speed.md) { get; } | How fast the runner moves, in level units per second, whatever the direction. The game caps it at 1500, but it can read slightly more in the air, and a frozen, tumbling or pulled runner skips the cap. |
| [SwingCenter](IRunner/SwingCenter.md) { get; } | The center of its swing while it swings, in level units: 6 units right of and below the point its hook holds, as the game measures it. The middle of its [`Hitbox`](./IRunner/Hitbox.md) stays at the rope's length from it. Null when it does not swing. |
| [TakesItemPickups](IRunner/TakesItemPickups.md) { get; set; } | Whether the runner takes the item pickups it touches. True by default. When false, it runs through them and they stay on the map. |
| [TumbleTimeLeft](IRunner/TumbleTimeLeft.md) { get; } | How long it still tumbles, or zero when it does not. |
| [Velocity](IRunner/Velocity.md) { get; } | How fast the runner moves along each axis, in level units per second. Y grows downward. |
| [Eliminate](IRunner/Eliminate.md)() | Eliminates the runner from the round where it stands, as the camera does: it is hidden, lets go of its hooks, and stops. The game then shows it out and can start sudden death, as for any elimination. |
| [EndDrill](IRunner/EndDrill.md)() | Ends the runner's drilling now. It does nothing when the runner does not drill. |
| [EndTumble](IRunner/EndTumble.md)() | Ends the runner's tumble now, on every game. It does nothing when the runner does not tumble. |
| [Freeze](IRunner/Freeze.md)(…) | Freezes the runner, as the ice does, for a time: it slides to a stop and cannot act. Freezing a frozen runner starts its freeze again. |
| [GiveItem](IRunner/GiveItem.md)(…) | Gives the runner an item in place of the one it holds, as an item pickup does. |
| [GiveModItem](IRunner/GiveModItem.md)(…) | Gives the runner a mod's item in place of the one it holds, as an item pickup does. |
| [KnockOut](IRunner/KnockOut.md)() | Knocks the runner out, as a lethal spike does: it lets go of its hooks, is thrown up, falls through the level, and is eliminated as soon as it leaves the view. It works in any match, not only where the game makes hazards lethal. |
| [MoveTo](IRunner/MoveTo.md)(…) | Moves the runner so that it stands at a point, as the game places the runners between rounds: it stops, and lets go of its hook and its golden hook. The point is not checked: a runner moved into the terrain is pushed out of it by its collisions. |
| [RemoveItem](IRunner/RemoveItem.md)() | Takes the runner's item away without using it, as using it would. A runner keeps its item when it is eliminated, and into the next rounds. |
| [Revive](IRunner/Revive.md)() | Brings an eliminated runner back into the round, as the game does for every runner at the next round, but where it went out and without resetting it. It is shown again, collides with the level again, and its player controls it from the next frame. Move it with [`MoveTo`](./IRunner/MoveTo.md), since it went out off camera or below the level. |
| [StartDrill](IRunner/StartDrill.md)(…) | Makes the runner drill for a time, as the InvinciDrill does, without using an item: its drill, sound, free boost and invincibility ([`IsDrilling`](./IRunner/IsDrilling.md)). It blinks during the last half second. |
| [StopGoldenHookPull](IRunner/StopGoldenHookPull.md)() | Frees the runner from the golden hook that pulls it, on every game, as when the hook breaks. It does nothing when no golden hook pulls it. |
| [Tumble](IRunner/Tumble.md)(…) | Has the runner tumble for a time, as a hit does: it lets go of its hooks, is unfrozen, and only falls and slides. A tumble replaces the one under way. Every game has its copy tumble. |
| [Unfreeze](IRunner/Unfreeze.md)() | Ends the runner's freeze now, on every game. It does nothing when the runner is not frozen. |
| [UseItem](IRunner/UseItem.md)() | Presses the item button for the runner on its next frame, as its player or bot would. The game's rules for a press apply: nothing happens without an item, in the 1.5 seconds after GO, or while the runner tumbles, is frozen, pulled by a golden hook or taunts. The shockwave is released at once, nearly uncharged. Every game plays the use, as for a real press. |

## Remarks

Every game simulates every runner, so the values of the runners of other players and of their bots are this game's own simulation of them, close to but not exactly what their owners see.

## See Also

* namespace [ModRunners.Api.Runners](../ModRunners.Api.md)

<!-- DO NOT EDIT: generated by xmldocmd for ModRunners.Api.dll -->
