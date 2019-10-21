FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["WishList/WishList.csproj", "WishList/"]
COPY ["WishList.Shared/WishList.Shared.csproj", "WishList.Shared/"]
COPY ["WishList.Repositories.IoC/WishList.Repositories.IoC.csproj", "WishList.Repositories.IoC/"]
COPY ["WishList.Repositories.ReadOnly/WishList.Repositories.ReadOnly.csproj", "WishList.Repositories.ReadOnly/"]
COPY ["WishList.Repositories/WishList.Repositories.csproj", "WishList.Repositories/"]
COPY ["WishList.Domain/WishList.Domain.csproj", "WishList.Domain/"]
COPY ["WishList.Models/WishList.Models.csproj", "WishList.Models/"]
COPY ["WishList.Repositories.Indexer/WishList.Repositories.Index.csproj", "WishList.Repositories.Indexer/"]
COPY ["WishList.Repositories.Seed/WishList.Repositories.Seed.csproj", "WishList.Repositories.Seed/"]
COPY ["WishList.Services.IoC/WishList.Services.IoC.csproj", "WishList.Services.IoC/"]
COPY ["WishList.Services/WishList.Services.csproj", "WishList.Services/"]
RUN dotnet restore "WishList/WishList.csproj"
COPY . .
WORKDIR "/src/WishList"
RUN dotnet build "WishList.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WishList.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WishList.dll"]