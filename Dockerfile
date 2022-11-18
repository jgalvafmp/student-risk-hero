#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/student-risk-hero/student-risk-hero.csproj", "student-risk-hero/"]
RUN dotnet restore "student-risk-hero/student-risk-hero.csproj"
COPY . .
WORKDIR "/src/student-risk-hero"
RUN dotnet build "student-risk-hero.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "student-risk-hero.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "student-risk-hero.dll"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet WalletPlanifier.dll