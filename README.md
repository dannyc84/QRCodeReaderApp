# QRCodeReaderApp
QRCodeReaderApp is a simple web application to read QR Codes

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 3.1 or later](https://dotnet.microsoft.com/download/dotnet-core/3.1)

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running:
```csharp
dotnet restore
```
3. Next, build the solution by running:
```csharp
dotnet build
```
4. Next, within the Cubes3DIntersection.Api directory, launch the back end by running:
```csharp
dotnet run --project QRCodeReaderApp.Web
```
5. Launch http://localhost:5001/ in your browser

## Using QRCodeReaderApp

To use QRCodeReaderApp, follow these steps:

* Click on Choose File button and upload a QR Code image (only PNG, GIF or JPEG format are supported)
* Once you'll see the name of the uploaded file (next to the "Choose File" button), click on the "Read QR code" button
* See the results in the new HTML table printed to the screen

### Structure of Project
QRCodeReaderApp include layers divided by **2 project**;
* Application
    * Helpers
    * Interfaces    
    * Services
* Web
    * Controllers
    * Models
    * Views
    
### Application Layer
Development of **Domain Logic with implementation**. Interfaces drives business requirements and implementations in this layer.
This layer contains the application services injected into the HomeController (Web Layer) and some helper classes for images handling.

### Web Layer
Development of Web Logic with implementation.
This layer contains the MVC classes to process a request and generate a response (consuming the Application services), visible in HTML format.
The application's main **starting point** is the ASP.NET Core web project. This is a classical console application, with a public static void Main method in Program.cs. It currently uses the default **ASP.NET Core project template** which based on **Razor Pages** templates. This includes appsettings.json file plus environment variables in order to stored configuration parameters, and is configured in Startup.cs.

## Technologies
* .NET Core 3.1
* ASP.NET Core 3.1 
* .NET Core Native DI

## Contributors

Thanks to the following people who have contributed to this project:

* [@dannyc84](https://github.com/dannyc84)

## Contact

If you want to contact me you can reach me at daniele.crivello84@libero.it.

## License

This project uses the following license: [MIT](LICENSE.md).

