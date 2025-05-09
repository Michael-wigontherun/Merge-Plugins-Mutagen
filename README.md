# MergePluginsMutagen
 Merge Plugins Using Mutagen


Please do not use this yet its not fully finished or tested.

## the does and does not of this
Does Not:
- Merge NavMeshes records
- Merge Cells records
- Merge Worldspaces records
- Merge NavigationInfoOnMap records
It does not do any of those things.

Q: Why?

A: Because those are things that I have always considered really bad things to change try to replace, especially with some scripted system.
This is why Loot will flag things with deleted NavMeshes as something that needs to be cleaned, It is prone to crashes mid game or worse before the game even loads.

Does do:
- Merges Placed objects inside of Cells
- Merges Placed objects inside of Worldspaces
- Copy and override existing NavMeshes
- Merges all other record types

Q: You said it does not merge Cells and Worldspaces?

A: Yes but it does merge the placed objects so all those Lux Patches and all those micro patches for a mod and USSEP/USMP can be merged comfortably.
I made this with the intent to merge patches and other things that I wish were a s
