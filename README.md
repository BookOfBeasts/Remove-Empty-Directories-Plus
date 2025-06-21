RED+: Remove Empty Directories Plus
===================================

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


## License

RED is free software; you can redistribute it and/or modify it under the terms of the
[GNU Lesser General Public License](http://www.gnu.org/licenses/lgpl.html) as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.