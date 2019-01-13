FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Blog.WebBack/Blog.WebBack.csproj", "src/Blog.WebBack/"]
RUN dotnet restore "src/Blog.WebBack/Blog.WebBack.csproj"
COPY . .
WORKDIR "/src/src/Blog.WebBack"
RUN dotnet build "Blog.WebBack.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Blog.WebBack.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Blog.WebBack.dll"]