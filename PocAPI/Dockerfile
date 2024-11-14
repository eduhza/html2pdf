# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#IronPDF
# install necessary packages
USER root
RUN apt update \
    && apt install -y sudo libxkbcommon-x11-0 libc6 libc6-dev libgtk2.0-0 libnss3 libatk-bridge2.0-0 libx11-xcb1 libxcb-dri3-0 libdrm-common libgbm1 libasound2 libxrender1 libfontconfig1 libxshmfence1 libgdiplus libva-dev

#NReco.PdfGenerator.LT
RUN apt-get update && apt-get install -y --no-install-recommends wget ca-certificates fontconfig libc6 libfreetype6 libjpeg62-turbo libpng16-16 libssl3 libstdc++6 libx11-6 libxcb1 libxext6 libxrender1 xfonts-75dpi xfonts-base zlib1g
RUN wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6.1-3/wkhtmltox_0.12.6.1-3.bookworm_amd64.deb
RUN dpkg -i wkhtmltox_0.12.6.1-3.bookworm_amd64.deb

#NReco.PdfGenerator.LT Windows Fonts
RUN echo "deb http://deb.debian.org/debian bookworm contrib non-free" > /etc/apt/sources.list.d/contrib.list
RUN echo "ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true" | debconf-set-selections
RUN apt-get update -y && apt-get install -y --no-install-recommends ttf-mscorefonts-installer
RUN fc-cache -f -v
USER app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PocAPI/PocAPI.csproj", "PocAPI/"]
RUN dotnet restore "./PocAPI/PocAPI.csproj"
COPY . .
WORKDIR "/src/PocAPI"
RUN dotnet build "./PocAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PocAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PocAPI.dll"]