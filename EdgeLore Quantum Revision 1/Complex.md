# Complex Struct

## Overview
The `Complex` struct represents a complex number, which consists of a real part and an imaginary part. This struct allows for mathematical operations on complex numbers, such as addition, subtraction, multiplication, and division. It also provides a way to calculate the magnitude of the complex number. This struct fits into a larger codebase where complex number operations are necessary, such as in fields like engineering, physics, or computer graphics.

## Variables

- **Real**: A `double` property that holds the real part of the complex number.
- **Imaginary**: A `double` property that holds the imaginary part of the complex number.

## Functions

- **Complex(double real, double imaginary)**: Constructor that initializes a new instance of the `Complex` struct with specified real and imaginary values.

- **Magnitude**: A read-only property that calculates the magnitude (or absolute value) of the complex number using the formula √(Real² + Imaginary²).

- **operator + (Complex c1, Complex c2)**: Defines the addition operation between two complex numbers. It returns a new `Complex` instance that represents the sum of `c1` and `c2`.

- **operator - (Complex c1, Complex c2)**: Defines the subtraction operation between two complex numbers. It returns a new `Complex` instance that represents the difference of `c1` and `c2`.

- **operator * (Complex c1, Complex c2)**: Defines the multiplication operation between two complex numbers. It returns a new `Complex` instance that represents the product of `c1` and `c2`.

- **operator / (Complex c1, Complex c2)**: Defines the division operation between two complex numbers. It returns a new `Complex` instance that represents the quotient of `c1` divided by `c2`.

- **ToString()**: Overrides the default `ToString` method to provide a string representation of the complex number in the format "Real + Imaginaryi".