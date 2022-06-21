# Setup
- .NET 7 preview
- Entity Framework
- Console Logger
- MS.Extensions.Hosting
- Domain Driven Design

# How the programm works

1. Hosted service will be started and wait for Esc key to exit
1. if new files, with extension defined in settings file, has been created, process the file
    1. Cloud scenario => blob storage trigger
1. To process FLF, marshalling is used, for better performance
1. Data are stored in the DB (inmemory)
1. print data

No implementation for JSON, XML, CSV: structure has to be defined correctly, especially for JSON and XML.

No Streaming implementation: no given stream provider(s)

# Data structure

I call first row: shipment details and second shipment details extension

# Shipment details

1. Id => looks like an integer, but can also contains alphabetics
1. All Numbers properties except those with 0 inside are numbers with leading zeroes.
    1. 0 Numbers seems to be booleans or may be short types
1. Found full address with first, last name and street, city, zip
1. Number starts with 2022 seems to be an datetime with femtoseconds (?) or other format that could not be identified
1. Found 2 currencies. May be price and tax

# Shipment details extension

1. contains id of the shipment details
1. and some Integer value with some kind of acronym?

# Logging

1. Console logger is installed. 
1. For cloud application, Application Insights is preferable.
    1. Logging can be customized by appsettings.json
1. Edge cases will be logged as unsupported format
    1. Wrong (too long) data size 
    1. Data cannot be interpreted
    1. Empty relationships, data not complete

# Problems

1. Fixed Length file has no documentation
1. Document was not provided as file, just as text via mail


# Documentation for data

See naming convetion for properties and classes
	