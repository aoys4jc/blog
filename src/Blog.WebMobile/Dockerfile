FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/Blog.WebMobile/Blog.WebMobile.csproj src/Blog.WebMobile/
COPY D:\github\git\blog\src\Blog.WebMobile\nuget.config src/Blog.WebMobile/
COPY src/Blog.BLL/Blog.BLL.csproj src/Blog.BLL/
COPY src/Blog.Mvc/Blog.Mvc.csproj src/Blog.Mvc/
COPY src/Blog.Common/Blog.Common.csproj src/Blog.Common/
RUN dotnet restore
COPY . .
WORKDIR /src/src/Blog.WebMobile
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Blog.WebMobile.dll"]
