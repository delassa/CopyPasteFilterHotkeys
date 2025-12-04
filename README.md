# Copy Paste Filter Hotkeys

A small mod for schedule one to help with the hassle of filtering storages.

Hover over an item slot and press the copy key to copy the filter, hover over a different slot and press the paste key to paste it.

Keybindings default to '[' for copy and ']' for paste.

Settings are saved in (Schedule 1 directory)/UserData/MelonPreferences.cfg after first run and keys can be changed there.

# Building

Clone Project
Adjust directories in CopyPasteFilterHotkeys.csproj to point to schedule 1 install for alternate or main

Select build config either IL2CPP or Mono

Post build script copies dll to mods folder, kills game if running and starts game