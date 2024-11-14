# HTML TO PDF API
## Overview
This API is used to convert HTML to PDF. It is a simple API that takes HTML content and converts it to PDF. THe API is built using Minimal APIs and is intended to run on windows and docker container (linux).

## Libraries
The API uses the following libraries:
- [`DinkToPdf`](https://github.com/rdvojmoc/DinkToPdf) - This is a .NET Core library that uses the WebKit engine to convert HTML to PDF. It is a wrapper around the `wkhtmltopdf` library. - MIT
- [`NReco.PdfGenerator.LT`](https://www.nrecosite.com/pdf_generator_net.aspx) - HTML to PDF converter for .NET Framework / .NET Core (WkHtmlToPdf wrapper). - Commercial License
- [`PuppeteerSharp`](https://github.com/hardkoded/puppeteer-sharp) - Puppeteer Sharp is a .NET port of the official [Node.JS Puppeteer API](https://github.com/puppeteer/puppeteer). It uses a headless browser (chrome) to convert HTML to PDF. - MIT
- [`Gotenberg`](https://gotenberg.dev/) - A Docker-powered stateless API for PDF files. - MIT
- [`iTextSharp 4.1.6`](https://github.com/schourode/iTextSharp-LGPL) - iTextSharp is a port of the iText open source Java library for PDF generation written entirely in C# for the .NET platform.  - Mozilla Public License and the LGPL.

The following libraries was used but I was not able to get them to work in both environments (docker/windows)
- [`IronPdf`](https://ironpdf.com/) - IronPDF is a commercial library that can be used to convert HTML to PDF. - Commercial License
- [`PugPDF.Core`](https://github.com/rdvojmoc/DinkToPdf) - WkHtmlToPdf .net core wrapper
- [`SyncFusion`](https://www.syncfusion.com/) - The Syncfusion HTML to PDF converter is a .NET Standard library that converts URLs, HTML string, SVG, and MHTML to PDF in .NET Core applications. This converter uses the advanced Blink rendering engine, thus generating pixel-perfect PDF from HTML or URL.

## Prerequisites
- .NET 8.0 SDK
- Docker
- NReco/IronPDF/Syncfusion (trial) license key

## How to run the API
### Docker
- Set `docker-compose` project as startup project and run.
- The API will be available at `http://localhost:5001`
- Gotenberg API will be available at `http://gotenberg:3000` in the container but also in the host machine at `http://localhost:3000`
### Windows
- Stop the API image on container but let Gotenberg running.
- Set PocAPI as startup project, select `https profile` and run.

## Contributing
- Fork the repository and clone it to your local machine.
- Submit a pull request explaining the changes you have made.
