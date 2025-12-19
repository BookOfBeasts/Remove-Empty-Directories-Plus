RED+: Remove Empty Directories Plus
===================================

[Download RED+](https://github.com/BookOfBeasts/Remove-Empty-Directories-Plus/releases)

RED+ is a fork of [Jonas John's](http://www.jonasjohn.de/) RED  
RED+ was created by [Robert 'NotBob' Bookerby](https://github.com/BookOfBeasts)  

It has had some new features added, some UI tweaks, been made fully portable (it uses a local config file rather than one stored in some back-water of %appdata%) and had the name matching routines completely rewritten in order to allow more sophisticated matching.

RED+ finds, displays, and deletes empty directories recursively below a given start folder. Furthermore, 
it allows you to create custom rules for keeping and deleting folders (e.g. treat directories with empty files as empty).

### You use this software entirely at your own risk!
I've been using versions of the original RED for many years, and this enhanced RED+ since July 2024 with no significant issues. But I offer no guarantee that it will work for you on your system. 


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
- If the config file (**RED+.cfg**) isn't found it will prompt you to create one:
	- Portable Mode stores the config in the same folder as the executable
	- %APPDATA% stores the config in the a subfolder of Windows %APPDATA% (non-portable)
	- If you place RED+ in a protected folder (such as 'Program Files' or 'Program Files (x86)') then Windows will prevent writing of the portable config file. Select %APPDATA% instead.

## How to contribute to the project

You are welcome to contribute code, translations, or anything else :)

Here are some tasks you can help with:  
- Look at open issues and try to fix or implement them
	- Also look at issues for the [original RED](https://github.com/hxseven/Remove-Empty-Directories/issues)
- Fix typos and general wording
- Optimize user interface
- Documentation

-------
## History

The first version of RED was created by [Jonas John](http://www.jonasjohn.de/) around 2005. 

Since then [a small team of contributors](https://github.com/hxseven/Remove-Empty-Directories/graphs/contributors) helped to fix bugs and add new features.

In July 2024 Robert 'NotBob' Bookerby created RED+ using the core code of RED.  
In June 2025 RED+ was made available on GitHub

## Changelog
25.3.0.0    (*2025 December*)
- Save Prompt was not being actioned on program exit
- Reset Settings/Filters not being actioned correctly
- Reset Settings no longer resets filters
- Change how Explorer Integration is handled. 
	- Read the help file for more info (Settings - Advanced Settings - Windows Explorer Integration)
- Prevent exit if Search or Delete is active
- Updates to help documentation

25.2.0.0   (*2025 November*)
- Improve command line parsing
- Updates to basic help

25.1.0.0   (*2025 November*)
- Added basic help documentation (help\index.htm)
	- NOTE: Incomplete and very much a work-in-progress
- Added microseconds to display of runtime after a search
- Disable search related buttons when not on search tab
- Added *-autosearch* command line switch

25.0.1.0   (*2025 November*)  
- Fixed issue with Last Used Directory not being restored on program load

25.0.0.0   (*2025 June*)  
- The 1st release of RED+, which is based on [Jonas John's](http://www.jonasjohn.de/) RED 2.3.0 beta
- Completely rewrote the directory and file name matching routines allowing the use of an extended syntax for more sophisticated matches
- Added a new directory filter to allowing specifying that a directory is **never** to be treated as empty but still check any sub-directories
- Added a dedicated grid for the display & editing of filter rules
- Tweaked the UI layout and some of the icons
- Settings load/save replaced with custom routine allowing truly portable use
	- The settings file has the same name as the executable but with an extension of '.cfg'
	- If you want the settings saved to the %appdata% folder then delete the .cfg file and RED+ will prompt for the location next time it is run.
- Added the foundations for translations using industry standard .po files

Original RED  
- See [original RED](https://github.com/hxseven/Remove-Empty-Directories/issues) for earlier releases

## Credits

Third-party components  
- File system calls are powered by the [AlphaFS library](https://github.com/alphaleonis/AlphaFS)

Icon sources
- Nuvola icons (GNU LGPL 2.1. license)
- NuoveXT icons (GPL license)
- [famfamfam silk icons](https://github.com/legacy-icons/famfamfam-silk) (Creative Commons Attribution 2.5 license) 
- [FatCow free-icons] (https://github.com/gammasoft/fatcow) (Creative Commons Attribution 3.0 license)

## License

RED is free software; you can redistribute it and/or modify it under the terms of the
[GNU Lesser General Public License](http://www.gnu.org/licenses/lgpl.html) as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.