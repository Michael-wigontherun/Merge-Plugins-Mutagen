# MergePluginsMutagen
 Merge Plugins Using Mutagen


Please do not use this yet its not fully finished or tested.

## The Does and Does Not of this
Does Not:
- Merge NavMeshes records
- Merge Cells records
- Merge Worldspaces records
- Merge NavigationInfoOnMap records

It does not do any of those things.

Q: Why?

A: Because those are things that I have always considered really bad things to change and try to replace, especially with some scripted system.
This is why Loot will flag things with deleted NavMeshes as something that needs to be cleaned, It is prone to crashes mid game or worse before the game even loads.

Does do:
- Merges Placed objects inside of Cells
- Merges Placed objects inside of Worldspaces
- Copy and override existing NavMeshes
- Merges all other record types except NavigationInfoOnMap

Q: You said it does not merge Cells and Worldspaces?

A: Yes but it does merge the placed objects so all those Lux Patches and all those micro patches for a mod and USSEP/USMP can be merged comfortably.
I made this with the intent to merge patches and other things that I wish were a single plugin

# Requirements
1. [.Net 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
2. [BSA Browser](https://www.nexusmods.com/skyrimspecialedition/mods/1756)

# Installation
1. Download and install .Net 9 from Microsoft
2. Download and install BSA Browser
3. Download this and extract somewhere other then your data folder
   - Do not place this in your data folder or a mod folder
4. Edit MergePluginsMutagen.ini, every setting under the [mod] header is required to run properly

# Usage

## First for MO2 users, you need to run MergePluginsMutagen.exe through MO2

## Setting up the merge
Currently this is a CLI tool only
You need to manually create a text file with what ever name you want the merged plugin to be
Example name of text file: TemsPatchesMerge.esp.txt

Then add what ever plugin names you want to be merged and in what ever order you want them to be merged.
Example text file:
[TemsPatchesMerge.esp.txt](https://github.com/user-attachments/files/20130170/TemsPatchesMerge.esp.txt)
```
------------Tems Patches Start-----------------.esp
Tem's Olava's House - eFPS patch.esp
Tem Fuzz Heimskr House - eFPS patch.esp
Lux - Tem's Houses - Uthgerd.esp
Lux - Tem's Houses - Ysolda.esp
Tems Houses - Rorik Manor - Lux.esp
Lux - Tem's Houses - Olava The Feeble.esp
Whiterun Stables - Tem's Houses - Lux Patch.esp
Lux - TemBeed - Heimskr's House.esp
Lux - Tem's Houses - Left-Hand Mine.esp
Lux - Tem's Houses - Sarethi Farm.esp
Tems Patches Manual Patch Merge.esp
------------Tems Patches End-----------------.esp
```

## setting up the CLI
CLI takes 1-3 arguments

### Argument 1: 
Is a the path to the text file 

Example: 
 ```
 "merges\TemsPatchesMerge.esp.txt"
```

### Argument 2: 
If you want to use a sperate location of the ini. you can set the path to a different location.

Example: 
 ```
 "merges\TemsPatchesMerge.esp.txt.ini"
 ```

### Argument 3: 
Is the set for if you want the console to close on successful merging

Example:
 ```
-np
 ```

If you do not want to use a seperate ini but you still want to skip the closing pause, you do not need to include the ini path.

Example CLI run with required arguments
```
MergePluginsMutagen.exe "merges\TemsPatchesMerge.esp.txt"
```

Example with all arguments
```
MergePluginsMutagen.exe "merges\TemsPatchesMerge.esp.txt" "merges\TemsPatchesMerge.esp.txt.ini" -np
```

Example without custom ini and skipping the end pause
```
MergePluginsMutagen.exe "merges\TemsPatchesMerge.esp.txt" -np
```



