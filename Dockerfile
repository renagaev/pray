FROM mcr.microsoft.com/dotnet/sdk:8.0 as backend

WORKDIR app

COPY backend/WebApplication/WebApplication.csproj .
RUN dotnet restore WebApplication.csproj

COPY backend/WebApplication .
RUN dotnet publish WebApplication.csproj --output ./publish

FROM node:lts-alpine AS site_frontend

WORKDIR /app

COPY site_fronend/package.json .
RUN npm install 

COPY site_fronend . 
RUN npm run build 

FROM node:lts-alpine AS tg_frontend

WORKDIR /app

COPY tg_frontend/package.json .
RUN npm install 

COPY tg_frontend . 
RUN npm run build 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final

WORKDIR /app
COPY --from=backend app/publish .
COPY --from=site_frontend app/dist wwwroot
COPY --from=tg_frontend app/dist wwwroot/tg-app

EXPOSE 80
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENTRYPOINT ["dotnet", "WebApplication.dll"]