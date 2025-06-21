RED+: Remove Empty Directories Plus
===================================

RED+ is a fork of [Jonas John's](http://www.jonasjohn.de/) RED  
RED+ was created by [Robert 'NotBob' Bookerby](https://github.com/BookOfBeasts)

It has had some new features added, some UI tweaks, been made fully portable (it uses a local config file rather than one stored in some back-water of %appdata%) and had the name matching routines completely rewritten in order to allow more sophisticated matching.

RED+ finds, displays, and deletes empty directories recursively below a given start folder. Furthermore, 
it allows you to create custom rules for keeping and deleting folders (e.g. treat directories with empty files as empty).


## Features

- Simple user interface
- Shows empty directories before deleting them
- Supports multiple delete modes (including Delete to recycle bin)
- Allows whitelisting and blacklisting of directories by using filter lists
- Can detect directories with empty files as empty


## System requirements

- Windows 7 or later
- Microsoft .NET Framework 4.8
- There is no installer. Just unzip into a folder of your choice and run it.
	- If you place it into a protected folder (such as 'Program Files' or 'Program Files (x86)') then Windows will prevent writing of the local config file.

## How to contribute to the project

You are welcome to contribute code, translations, or anything else :)

Here are some tasks you can help with:
- Look at open issues and try to fix or implement them
	- Also look at issues for the [original RED](https://github.com/hxseven/Remove-Empty-Directories/issues)
- Fix typos and general wording
- Optimize user interface
- Add unit tests

## History

The first version of RED was created by [Jonas John](http://www.jonasjohn.de/) around 2005. 

Since then [a small team of contributors](https://github.com/hxseven/Remove-Empty-Directories/graphs/contributors) helped to fix bugs and add new features.

In July 2024 Robert 'NotBob' Bookerby created RED+ using the core code of RED.
In June 2025 RED+ was made available on GitHub

## Changelog

25.0.0.0
- The 1st release of RED+, which is based on [Jonas John's](http://www.jonasjohn.de/) RED 2.3.0 beta
- Completely rewrote the directory and file name matching routines allowing the use of an extended syntax for more sophisticated matches
- Added a new directory filter to allowing specifying that a directory is **never** to be treated as empty but still check any sub-directories
- Added a dedicated grid for the display & editing of filter rules
- Tweaked the UI layout and some of the icons
- Settings load/save replaced with custom routine allowing truly portable use
	- The settings file has the same name as the executable but with an extension of '.cfg'
	- If you want the settings saved to the %appdata% folder then create a dummy cfg file and set its Read Only attribute to true.
- Added the foundations for translations using industry standard .po files

_**History of Jonas John's RED**_

2.3
- Disabled settings during active search or deletion process
- Refactored the interface to improve the design and usability
  - Divided packed settings tab into three separate tabs
  - Renamed some options and captions to make more sense 
  - Added more descriptions and examples to explain settings
  - Increased default window size and added more whitespace to make it look less crowded
- Optimized config defaults
  - Set pause between deletions to zero because the default delete to recycle bin method is slow enough to not overwhelm the GUI
  - Deleted some unnecessary entries and updated some values
  - Removed *.tmp as default pattern to make the default settings safer because those files could still contain valuable data in some cases.
- Long paths support and other improvements (contributed by gioccher, see #5)
  - Fix crash due to case sensitivity of paths 
  - Speed up crawl and deletion by disabling UI updates (dubbed fast mode)
  - Long path support by switching to AlphaFS 
  - And more minor improvements (see closed pull request #5 for details)
- Ignore folders newer than N hours #3 (contributed by jsytniak, see #3)

2.2
- Improved error handling
- Added logging of errors and deleted directories
- Added multiple delete modes (e.g. delete to recycle bin)
- Implemented a function to delete a single empty directory
- Implemented optional function to detect paths in clipboard
- Infinite loop detection
- Added a few new configuration settings
- Removed counting method to increase speed
- Replaced old custom settings module with the default settings framework of .NET to be more standard-compliant (This should fix problems some users had when starting RED)

2.1
- Implemented a "Protect" and "Unprotect" function to let the user choose folders to keep
- Implemented an update button for a fast update check

2.0
- Created the installer (using NSIS)
- Updated this readme file

1.9
- Added better-looking icons to the GUI
- Corrected and updates some texts

1.8
- Finished the main parts of the application
- Added XML configuration file

1.7
- Removed some main parts of the new application and started
using the "BackgroundWorker" for threading support.

1.6
- Finished the first prototype of the C# version

1.5
- Started the development of an entirely new version of RED by using Microsoft Visual C# (.NET 2.0)

1.4
- Updated the readme and changed the license from GPL to LGPL
- fixed some small issues

1.2
- Fixed the gauge in the process window
- implemented a second safety check to prevent deleting filled folders

1.1
- renamed the program to RED (Remove empty directories)
- made a new icon

1.0
- changes some structure things, renamed functions, renamed variables
- corrected code, fixed some issues...
- optional logfile implemented
- other minor changes
- updated version history -> complete rewrite ;)

0.9
- Added a readme, the licenses
- Translated the readme into English

0.8
- Cleaned the directories and sorted the files
- Renamed some functions and variables, to make it look better

0.6 + 0.7
- I learned about WinBinder (A native Windows binding for PHP) and converted
the program to PHP with a Windows GUI using WinBinder

0.5
- Used NSIS Install System (http://nsis.sourceforge.net/) to create a
GUI for the perl script

0.2 - 0.4
- Minor changes
- Added filters to exclude some folders like the recycler

0.1
- I made a simple perl script to delete empty folders, I called it "DEF" (Delete Empty Folders)


## Credits

Third-party components
- File system calls are powered by the [AlphaFS library](https://github.com/alphaleonis/AlphaFS)
- The Installer is made by using [Inno Setup](https://jrsoftware.org/isinfo.php) & [Inno Setup Dependency Installer](https://github.com/DomGries/InnoDependencyInstaller)

Icon sources
- Nuvola icons (GNU LGPL 2.1. license)
- NuoveXT icons (GPL license)
- [famfamfam silk icons](http://www.famfamfam.com/lab/icons/ "famfamfam silk icons") (Creative Commons Attribution license) 
- [Coffee icon](https://www.freeimages.com/de/photo/coffee-and-desserts-1571223 "Coffee icon") by Ivan Freaner
- Ignore list icon taken from "Primo Icon Set" made by [Webdesigner Depot](http://www.webdesignerdepot.com/)
  - License: Free of charge for personal or commercial purposes


## License

RED is free software; you can redistribute it and/or modify it under the terms of the
[GNU Lesser General Public License](http://www.gnu.org/licenses/lgpl.html) as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.