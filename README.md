## Info ##

The source code of SteamGrouper, licensed under the GPL, is provided here for public perusal.

Further modifications and redistributions must follow the license provided in this repository, namely the [LICENSE](https://github.com/waylaidwanderer/SteamGrouper/blob/master/LICENSE) file.

**BEFORE YOU GET STARTED:** This program requires you use git to download the program.  **Downloading the zip will not work!**  It doesn't work because the program uses git submodules, which are not included with the zip download.

### Configuration Instructions ###

If you've just recently cloned this repository, there are a few things you need to do in order to build the source code.

1. Run `git submodule init` to initalize the submodule configuration file.
2. Run `git submodule update` to pull the latest version of the submodules that are included (namely, SteamKit2).
 - Since SteamKit2 is licensed under the LGPL, and SteamGrouper should be released under the MIT license, SteamKit2's code cannot be included in SteamGrouper.  This includes executables.  We'll probably make downloads available on GitHub.
3. Open `SteamGrouper.sln` in your C# development environment; either MonoDevelop or Visual Studio should work.
4. Build the program.