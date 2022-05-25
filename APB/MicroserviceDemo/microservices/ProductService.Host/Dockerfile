#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# tên image của .net core và asp.net core, dùng để tạo ra môi trường runtime đóng vai trò như một image base
# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# chỉ ra thư mục làm việc bên trong image là thư mục app
WORKDIR /app
# Cổng mà khi container chạy image này sẽ mở cho bên ngoài chọc vào
EXPOSE 80

# Chỉ ra image sdk được dùng để build code ứng dụng
# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# Chỉ ra thư mục làm việc để build là thư mục src của image
WORKDIR /src

# các dòng từ 16 đến 23 để copy file .csproj của các thư mục tương ứng trong thư mục app của image
COPY ["microservices/ProductService.Host/ProductService.Host.csproj", "microservices/ProductService.Host/"]
COPY ["modules/product/src/ProductManagement.EntityFrameworkCore/ProductManagement.EntityFrameworkCore.csproj", "modules/product/src/ProductManagement.EntityFrameworkCore/"]
COPY ["modules/product/src/ProductManagement.Domain/ProductManagement.Domain.csproj", "modules/product/src/ProductManagement.Domain/"]
COPY ["modules/product/src/ProductManagement.Domain.Shared/ProductManagement.Domain.Shared.csproj", "modules/product/src/ProductManagement.Domain.Shared/"]
COPY ["modules/product/src/ProductManagement.Application/ProductManagement.Application.csproj", "modules/product/src/ProductManagement.Application/"]
COPY ["modules/product/src/ProductManagement.Application.Contracts/ProductManagement.Application.Contracts.csproj", "modules/product/src/ProductManagement.Application.Contracts/"]
COPY ["modules/product/src/ProductManagement.HttpApi/ProductManagement.HttpApi.csproj", "modules/product/src/ProductManagement.HttpApi/"]
COPY ["shared/MsDemo.Shared/MsDemo.Shared.csproj", "shared/MsDemo.Shared/"]

# sau khi copy xong, run dotnet restore để download các package từ nuget về phục vụ cho việc build
RUN dotnet restore "microservices/ProductService.Host/ProductService.Host.csproj" -nowarn:msb3202,msb3277,nu1503
# copy toàn bộ file ở thư mục hiện tại sang thư mục src
COPY . .
# chuyển thư mục làm việc vào trong src/microservices/ProductService.Host
WORKDIR "/src/microservices/ProductService.Host"

# build project với mode release và output ra thư mục app của image
RUN dotnet build "ProductService.Host.csproj" --no-restore -nowarn:msb3202,msb3277,nu1503 -c Release -o /app/build

# khai báo image alias publish từ image build lúc trước
FROM build AS publish
# Publish project và output ra thư mục app
RUN dotnet publish "ProductService.Host.csproj" --no-restore -nowarn:msb3202,msb3277,nu1503 -c Release -o /app/publish

# chuyển thư mục làm việc về thư mục app
FROM base AS final
WORKDIR /app 
# copy toàn bộ image publish sang thư mục app
COPY --from=publish /app/publish .

# tạo entry point truy cập ứng dụng từ ProductService.Host.dll
ENTRYPOINT ["dotnet", "ProductService.Host.dll"]