# Monitoring Suite

Monitoring Suite is simple event/changes tracking tool for Unity Engine.

# Installation

- Open ``Window/Package Manager``
- Click ``+``
- Click ``Add package from git URL`` or ``Add package by name``
- Add ``https://github.com/FurrField-Studio/MonitoringSuite`` in Package Manager

## Basic usage

``MonitoringSuiteManager.ObjectChanged(string objectName, T obj);`` records change that happened to specific entry, ``T obj`` is basically calling ToString() on a object, so there is also version that takes string instead of T.

``MonitoringSuiteManager.ObjectReset(string objectName);`` resets the current data of an entry.

To see the last 10 sessions captured by the tool go to ``Tools/Monitoring Suite``.

``Usage List`` shows grid of all entries with statistics about specific entry.

``Event Log`` shows log of all changes in entries, yellow ones are marked as redundant/repeated changes that doesnt change the value of an entry. On the left is time of an event relative to the time when session started, on the right there is button to toggle visibility of captured stacktrace (stacktrace is trimmed to not show internal Monitoring Suite code).
On the top left there is button ``Ignore repeated changes`` to disable visibility of redundant/repeated changes.
