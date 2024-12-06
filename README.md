# Asset Tracking Application

Simple C# console application for tracking IT assets, developed as part of Lexicon programming education.

## Features

- Track IT assets (Computers and Phones) with details:
  - Asset Type
  - Brand
  - Model
  - Price
  - Purchase Date
  - End of Life Date (automatically set to 3 years after purchase)

## Core functionality

- Add new assets to database
- Remove assets from database
- View all assets
- Sort assets by purchase date
- Visual indicator (red text) for assets past their end-of-life date

## Project Structure

AssetsDB/
- Dependencies/
- Migrations/
- .gitignore
- Asset.cs
- AssetDBContext.cs
- Program.cs
- README.md

## Technical Details

- Built with C# and .NET
- Uses Entity Framework for database management
- Console-based user interface
- Entity Framework Code First approach for database creation

## Notes

Assets are automatically marked in red text when they are more than 3 years old from their purchase date, helping to identify equipment that may need replacement.