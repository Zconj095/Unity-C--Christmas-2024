# LazyUpdateManager

## Overview
The `LazyUpdateManager` is a Unity MonoBehaviour that efficiently schedules updates to be executed after a short delay. This is particularly useful in scenarios where multiple updates need to be batched together to optimize performance. Instead of executing updates immediately, this script allows for a slight delay, helping to minimize the frequency of updates and potentially improving the overall performance of the game or application.

## Variables

- **updatePending**: A boolean variable that tracks whether an update is currently pending. It ensures that only one update is scheduled at a time, preventing multiple concurrent updates from being initiated.

## Functions

- **ScheduleUpdate(System.Action updateAction)**: This public method is used to schedule an update. It checks if an update is already pending; if not, it sets `updatePending` to true and starts a coroutine to handle the delayed update. The `updateAction` parameter is an action delegate that represents the method to be executed after the delay.

- **DelayedUpdate(System.Action updateAction)**: This private coroutine method handles the actual delay and execution of the update. It waits for 0.1 seconds (to allow for batching) and then invokes the provided `updateAction`. After executing the action, it resets `updatePending` to false, allowing for future updates to be scheduled.