#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Vrnz2.Template.5_0.WebApi/Vrnz2.Template.5_0.WebApi.csproj", "Vrnz2.Template.5_0.WebApi/"]
RUN dotnet restore "Vrnz2.Template.5_0.WebApi/Vrnz2.Template.5_0.WebApi.csproj"
COPY . .
WORKDIR "/src/Vrnz2.Template.5_0.WebApi"
RUN dotnet build "Vrnz2.Template.5_0.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vrnz2.Template.5_0.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vrnz2.Template.5_0.WebApi.dll"]