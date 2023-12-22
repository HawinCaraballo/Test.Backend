## Proyecto Aplicando `Clean Architecture`

## Tabla de contenido
1. [Informacion General](#informacion-general)
2. [Tecnologias](#tecnologias)
3. [Configuracion](#configuracion)

### Informacion General

***
Proyecto creado NET 8 NET CORE con arquitectura `Clean Architecture` y patrones CQRS y MEDIATOR.

### Screenshot
Se visualiza imagenes del proyecto N-capas aplicando la arquitectura:

![Image text](/doc/image/arquitecturaProyecto.png)
## Tecnologias
***
La lista de tecnologias usada con el proyecto:
* [C# Net](https://dotnet.microsoft.com/es-es/download/dotnet/8.0) Version 8 
* Base de datos SQL Server 2019 (Microsoft SQL Server 2019 15.0.2104.1)

### Test.Backend.Api
* [Serilog](https://www.nuget.org/packages/Serilog/3.1.2-dev-02097) Version 3.1.1
* [Serilog.AspNetCore](https://www.nuget.org/packages/Serilog.AspNetCore) Version 8
* [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console) Version 5.0.1
* [Serilog.Sinks.File](https://www.nuget.org/packages/Serilog.Sinks.File/5.0.1-dev-00968) Version 5.0.1

### Test.Backend.Application
* [AutoMapper](https://www.nuget.org/packages/AutoMapper) Version 12.0.1
* [AutoMapper.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection) Version 12.0.1
* [FluentValidation](https://www.nuget.org/packages/FluentValidation) Version 11.8.1
* [FluentValidation.DependencyInjectionExtensions](https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions) Version 11.8.1
* [LazyCache](https://www.nuget.org/packages/LazyCache) Version 2.4.0
* [LazyCache.AspNetCore](https://www.nuget.org/packages/LazyCache.AspNetCore) Version 2.4.0
* [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection) Version 11.0.0
* [Microsoft.Extensions.Logging.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions) Version 8
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) Version 13.0.3

### Test.Backend.Infraestructure
* [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design) Version 8
* [Microsoft.EntityFrameworkCore.SqlServer ](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer) Version 8
* [Microsoft.EntityFrameworkCore.Tools ](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools) Version 8

### Test.Backend.UnitTest
* [AutoFixture](https://www.nuget.org/packages/AutoFixture/4.18.1) Version 4.18.1
* [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) Version 8
* [Microsoft.EntityFrameworkCore.InMemory](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory) Version 8
* [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/17.8.0) Version 17.8.0
* [Moq](https://www.nuget.org/packages/Moq) Version 4.20.70
* [Shouldly](https://www.nuget.org/packages/Shouldly) Version 4.2.1
* [Xunit](https://www.nuget.org/packages/xunit) Version 2.6.3
* [Xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio) Version 2.5.5


## Configuracion
***
- Ejecutar script en el motor de base de datos.
[Descargar Scripts](/doc/Scripts_Create_Database.sql)
- Tener en cuenta que se usa [mockapi](https://mockapi.io/), [URL endpoint](https://658310b502f747c8367afe14.mockapi.io/api/discountProductById/discount) para generar consumo externo.
- En la raiz del proyecto `Test.Backend.API` se deja carpeta llamada LOGs para guardar las transacciones del endpoint.

- ![image](/doc/image/logs.png)

- Compilar y ejecutar proyecto.
