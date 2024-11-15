﻿FROM mcr.microsoft.com/dotnet/sdk:8.0 as backend

WORKDIR app

COPY WebApplication/WebApplication.csproj .
RUN dotnet restore WebApplication.csproj

COPY WebApplication .
RUN dotnet publish WebApplication.csproj --output ./publish

FROM node:lts-alpine AS frontend 

WORKDIR app 

COPY WebApplication/frontend/package.json .
RUN npm install 

COPY WebApplication/frontend . 
RUN npm run build 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final 
WORKDIR app 
COPY --from=backend app/publish .
COPY --from=frontend app/dist wwwroot

EXPOSE 80
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENTRYPOINT ["dotnet", "WebApplication.dll"]