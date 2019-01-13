FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Blog.WebMobile/Blog.WebMobile.csproj", "Blog.WebMobile/"]
COPY ["Blog.WebMobile/nuget.config", "Blog.WebMobile/"]
COPY ["src/Blog.BLL/Blog.BLL.csproj", "src/Blog.BLL/"]
COPY ["src/Blog.Mvc/Blog.Mvc.csproj", "src/Blog.Mvc/"]
COPY ["src/Blog.Common/Blog.Common.csproj", "src/Blog.Common/"]
RUN dotnet restore "Blog.WebMobile/Blog.WebMobile.csproj"
COPY . .
WORKDIR "/src/Blog.WebMobile"
RUN dotnet build "Blog.WebMobile.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Blog.WebMobile.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Blog.WebMobile.dll"]