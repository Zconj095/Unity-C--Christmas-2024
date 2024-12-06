# PhotonLightField

## Overview
The `PhotonLightField` script is designed to create and manage a visual light field using particles in Unity. It initializes a particle system that simulates a glowing, rotating prism effect, enhancing the visual experience in a 3D environment. This script fits within the larger codebase by providing a dynamic light source that can be easily manipulated and visualized, contributing to the overall aesthetic and interactive elements of the application.

## Variables

- `numParticles`: An integer that specifies the total number of particles to be generated in the light field.
- `fieldRadius`: A float that defines the radius of the spherical area in which the particles are emitted.
- `luminescenceIntensity`: A float that determines the intensity of the luminescence effect applied to the particles.
- `particleColorGradient`: A Gradient object that defines the color transition for the particles over their lifetime.
- `prismHeight`: A float that sets the height of the prism formation when activated.
- `prismBaseRadius`: A float that specifies the base radius of the prism shape.
- `prismRotationSpeed`: A float that controls the speed at which the prism rotates.
- `particleSystem`: A reference to the ParticleSystem component attached to the GameObject, used to manage particle behavior.

## Functions

- `Start()`: This Unity lifecycle method is called before the first frame update. It invokes the `InitializeLightField` function to set up the particle system based on the specified settings.

- `Update()`: Another Unity lifecycle method that is called once per frame. It calls the `RotatePrismFormation` function to continuously rotate the prism shape as specified.

- `InitializeLightField()`: This method configures the particle system settings, including the maximum number of particles, shape type, color over lifetime, and emission rate. It ensures that the particle system is ready and starts playing.

- `RotatePrismFormation()`: This method rotates the GameObject around the Y-axis based on the defined `prismRotationSpeed`, creating a dynamic visual effect.

- `FormPrism()`: This public method changes the shape of the particle system from a sphere to a cone, representing a prism formation. It also sets the starting speed of the particles to the specified `prismHeight` and logs a message indicating that the prism formation has been activated.