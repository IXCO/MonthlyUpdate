# MonthlyUpdate

## Desclaimer
This solution is for specific needs on internal procedure of *Mundo Inmobiliario S.A*
functionality may be limited in other scenarios. 
This was created according to the specifications on the legal regulations of Mexico.

## Description
*MonthlyUpdate* is part of the digital invoice validator for all the provideers that give service

to any of the industries inside __Mundo Inmobiliario S.A & Industrias__ . 

Check the monthly amount of invoices that were received against the online service for verification 

on SAT.

## Requirements

  1. Windows OS (7 or higher)
  2. MySql Instance


## How to use

- Edit __ControladorDB__ class 
```C#
private static string server = "https://www.myserver.com";
private static string username = "myuser";
private static string password = "SomePasword";
private static string database = "mydatabase";
```
- Compile and create

## Things to notice

- Structure of the database would most likely be different, therefore the queries will need to be updated.
- Policies for reception may vary, according to specification.

*Mundo Inmobiliario S.A*
