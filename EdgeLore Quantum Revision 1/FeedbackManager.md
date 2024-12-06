# FeedbackManager Script

## Overview
The `FeedbackManager` script is responsible for displaying temporary feedback messages to the user within a Unity application. It utilizes Unity's UI system to update a text element with a message and automatically clears this message after a set duration. This script is useful for providing users with notifications or alerts without requiring additional user interaction.

## Variables
- `FeedbackText`: A `Text` component from Unity's UI system that displays the feedback message to the user. This variable must be assigned to a UI Text element in the Unity editor for the script to function correctly.

## Functions
- `ShowMessage(string message)`: This public method takes a string parameter `message` and sets the `FeedbackText` to display this message. It also invokes the `ClearMessage` method after a delay of 3 seconds to clear the displayed message.
  
- `ClearMessage()`: This private method clears the text displayed in the `FeedbackText` by setting it to an empty string. It is called automatically after a 3-second delay when a message is shown.