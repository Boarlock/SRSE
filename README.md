# Sins of a Solar Empire: Rebellion - Script Extender
SRSE is an early stage C# script extender for Sins of a Solar Empire: Rebellion. The project aims to provide a managed interface for interacting with the game's internal state and functions, making it easier to develop extensions and experimental modifications using C#.

Rather than requiring every project to independently locate and interact with game memory, RSSE centralizes discovered addresses and exposes higher level methods for common operations.

Status: Early development / experimental

This project is not yet a stable release. The API, memory mappings, and internal implementations may change as development and reverse engineering continue. Use at your own risk.

Features

Current and developing functionality includes:

Game state access — Read game time, focused time, frame counters, and game state flags.
Pause control — Read the game's pause state and request pause or resume operations.
Game speed control — Read and change the simulation speed through the game's internal functions.
Camera access — Read and modify discovered camera properties, including rotation, pitch, zoom, and movement related values.
Mouse input — Read left and right mouse-button states and retrieve cursor coordinates.
Window information — Read the game's window dimensions and display mode.
Managed API — Provide reusable C# methods and types so future extensions can build on the same foundation.

The game is a 32-bit process, so the extender must use compatible x86 interop and process injection configurations.

Project structure

The codebase separates address definitions, memory access, and higher level functionality.

Component	Purpose
SinsAddresses	Centralized game addresses and object offsets
SinsGetters	Reads global game values and flags
SinsGame	Access to game instance state, pause, and speed
SinsCamera	Access to discovered camera properties
EntryPoint	Initialization and periodic update handling

Names and organization may change as the project develops.

Building
Clone or download this repository.
Open the solution in Visual Studio.
Configure the project for the .NET Framework 3.5 target and x86 architecture.
Configure references and build settings for the required EasyHook framework.
Build the project and resolve any environment specific reference paths.

The game and its dependencies are not included in this repository. You must own and install the game separately.

Using the API

The public SRSE class is intended to provide convenient methods for interacting with the game without requiring callers to directly manipulate memory addresses.

Example usage:

// Put this using statement at the top of your file or use SRSE.XMethod() on each call
Using SRSE;

// Read the current game speed.
GameSpeed speed = SSSE.GetSpeed();

// Pause or resume the game.
Pause();
Resume();
TogglePause(); //Toggles state no matter initial condition

// Set a specific simulation speed.
SetSpeed(GameSpeed.Fast);

// Adjust the camera.
RotateCamera(45f);
AngleCamera(30f);

// Read the game window dimensions.
Coordinate size = SSSE.GetWindowSize();
int width = size.X;
int height = size.Y;

These examples illustrate the intended API. They require SRSE to be loaded and initialized in a compatible environment, and the relevant features must be implemented and working in the version being used.

Technical approach

SRSE is being developed in C# and uses memory inspection, native function interop, and reverse engineering tools to identify relevant game state and behavior.

Development uses tools including Runtime Engine and Ghidra to investigate memory addresses, object layouts, and native functions. Discovered addresses and offsets are centralized to make them easier to maintain as the project evolves.

The goal is to build a reusable foundation for managed extensions, rather than having every extension independently implement the same low level interactions.

Compatibility

Compatibility has not yet been comprehensively established across game versions or system configurations.

Game updates, changes to executable layouts, and differences in runtime behavior may affect functionality. Treat the current implementation as experimental.

Contributing

Bug reports, reproducible findings, and technical contributions are welcome.

When reporting an issue, include:

The game version
The SRSE revision or commit
The feature being tested
The steps to reproduce the issue
Relevant logs, exceptions, or observed behavior

Please distinguish confirmed findings from suspected causes when reporting reverse engineering results.

Disclaimer

SRSE is an independent community project and is not affiliated with, endorsed by, or supported by Stardock Entertainment or Ironclad Games.

Use the software at your own risk. Back up relevant files and saves before experimenting with extensions.

License

See the repository's LICENSE file for the terms governing use, modification, and redistribution.
