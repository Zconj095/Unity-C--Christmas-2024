# ObjectPool

## Overview
The `ObjectPool` script is designed to manage a pool of reusable game objects in Unity. It creates a specified number of inactive instances of a prefab at the start and provides methods to retrieve and return objects to the pool. This approach improves performance by reducing the overhead of instantiating and destroying objects frequently during gameplay. The `ObjectPool` fits into the codebase as a utility for efficiently handling object management, particularly in scenarios where many similar objects are needed, such as bullets, enemies, or projectiles.

## Variables

- **prefab**: A `GameObject` that represents the template for the objects to be pooled. This prefab is instantiated to create new objects.
  
- **initialPoolSize**: An `int` that determines the initial number of objects to create and store in the pool when the script starts. The default value is set to 50.

- **pool**: A `Queue<GameObject>` that holds the inactive objects. This queue allows for efficient retrieval and storage of objects in the pool.

## Functions

- **Start()**: This method is called when the script instance is being loaded. It initializes the object pool by creating a number of inactive instances of the prefab specified by `initialPoolSize` and enqueues them into the `pool`.

- **GetObject()**: This public method retrieves an object from the pool. If there are available objects in the pool, it dequeues one, activates it, and returns it. If the pool is empty, it instantiates a new object from the prefab and returns it, effectively expanding the pool as needed.

- **ReturnObject(GameObject obj)**: This public method takes a `GameObject` as an argument, deactivates it, and returns it to the pool by enqueuing it. This allows the object to be reused in the future without the overhead of instantiation.