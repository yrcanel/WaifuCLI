# WaifuCLI ![.NET](https://img.shields.io/badge/.NET-10-purple) ![License](https://img.shields.io/badge/license-MIT-green)
A simple CLI application for downloading waifu images with specified tags.
## Features
- Bulk download
- Tag-based images retrieval
- NSFW filter
- Asynchronous images retrieval via API
- Asynchronous tags retrieval via API
- Asychrounous file writing
- Network errors handling
- Spinner indicating that the application is working

## Stack
- .NET 10
- xUnit tests
- Microsoft.Extensions.DependencyInjection

## Prerequisites
- .NET 10 Runtime

## Installation and running
```bash
git clone https://github.com/yrcanel/WaifuCLI
cd WaifuCLI/WaifuCLI
dotnet run -- <args>
```
### Running from .exe
```bash
./WaifuCLI.exe <args>
```
## Arguments 
```
--help -h -?                Show help and usage info
get-tags                    list of available tags
--outputPath (REQUIRED)     Path where the image will be saved
--IsNsfw (bool)             Enable/disable 18+ content. Default: true
--tags                      Tag used to find images (can be used multiple times). Default: random image
--amount (int)             Number of images to download. Default: 1
```
## Usage examples 
This command downloads 5 images with the `waifu` tag into the specified folder:
```bash
dotnet run --  --tags waifu  --amount 5  --outputPath "Path\to\your\image"
```
Following linу lists available tags into your terminal
```bash
dotnet run --  get-tags
```
## Architecture
This project consists of four main parts:
- Core
- Infrastructure
- Utils
- Tests
### Core 
The core consists of 5 components: 
```
CLI                        Handles command-line interaction
Engine                     Orchestrates application logic
Interfaces                 Abstractions implemented by the infrastructure
Also there are Models and Exceptions.
```
### Infrastructure 
The infrastructure consists of 5 components:
```
ApiClient         Responsible for API calls
ImageClient       Responsible for getting image data from CDN of the API
ImageDownloader   Responsible for writing image data to a file
JsonDeserializer  Responsible for mapping JSON to an instance of a class
UrlBuilder        Responsible for building a URL for a query with specified params
```
### Utils
Contains helper methods used across the project.
### Tests 
Unit tests written using xUnit for the Infrastructure layer.
