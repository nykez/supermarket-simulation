# supermarket-simulation
A C# Application to simulate checkout lines at a Supermarket.

https://youtu.be/jZB6NsgCE5M

Purpose: Find the smallest amount of registers needed to serve 600 customers without a line being over 2 people at once. 

Customers arrived at the checkout lines in uniform fashion while their time to checkout was generated using negative exponential-distribution with no checkout time being less than 2:00 or greater than 5:45. 


Events were slowed to 100ms (each tick-loop) to demonstrate a real-time simulation.
