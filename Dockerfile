# Stage 1: Build React frontend
FROM node:22-alpine AS frontend-build
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable

WORKDIR /app/frontend
COPY Logistica.Client/package.json Logistica.Client/pnpm-lock.yaml ./
RUN pnpm install --no-frozen-lockfile

COPY Logistica.Client/ ./
RUN pnpm build

# Stage 2: Build .NET API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build
WORKDIR /app/backend

COPY Logistica.API/Logistica.API.csproj ./
RUN dotnet restore

COPY Logistica.API/ ./
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080

COPY --from=backend-build /app/publish .
COPY --from=frontend-build /app/frontend/dist ./wwwroot

ENTRYPOINT ["dotnet", "Logistica.API.dll"]
