/*log del error al momendo de hacer build del proyecto:

crit: Microsoft.AspNetCore.Hosting.Diagnostics[6]
      Application startup exception
      System.InvalidOperationException: A suitable constructor for type 'LibraryFinal.Middlewares.AddCookieHeaderMiddleware' could not be located. Ensure the type is concrete and services are registered for all parameters of a public constructor.
         at Microsoft.Extensions.Internal.ActivatorUtilities.CreateInstance(IServiceProvider provider, Type instanceType, Object[] parameters)
         at Microsoft.AspNetCore.Builder.UseMiddlewareExtensions.ReflectionMiddlewareBinder.CreateMiddleware(RequestDelegate next)
         at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
         at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
         at Microsoft.AspNetCore.Hosting.GenericWebHostService.StartAsync(CancellationToken cancellationToken)
fail: Microsoft.Extensions.Hosting.Internal.Host[11]
      Hosting failed to start
      System.InvalidOperationException: A suitable constructor for type 'LibraryFinal.Middlewares.AddCookieHeaderMiddleware' could not be located. Ensure the type is concrete and services are registered for all parameters of a public constructor.
         at Microsoft.Extensions.Internal.ActivatorUtilities.CreateInstance(IServiceProvider provider, Type instanceType, Object[] parameters)
         at Microsoft.AspNetCore.Builder.UseMiddlewareExtensions.ReflectionMiddlewareBinder.CreateMiddleware(RequestDelegate next)
         at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
         at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
         at Microsoft.AspNetCore.Hosting.GenericWebHostService.StartAsync(CancellationToken cancellationToken)
         at Microsoft.Extensions.Hosting.Internal.Host.<StartAsync>b__15_1(IHostedService service, CancellationToken token)
         at Microsoft.Extensions.Hosting.Internal.Host.ForeachService[T](IEnumerable`1 services, CancellationToken token, Boolean concurrent, Boolean abortOnFirstException, List`1 exceptions, Func`3 operation)
Unhandled exception. System.InvalidOperationException: A suitable constructor for type 'LibraryFinal.Middlewares.AddCookieHeaderMiddleware' could not be located. Ensure the type is concrete and services are registered for all parameters of a public constructor.
   at Microsoft.Extensions.Internal.ActivatorUtilities.CreateInstance(IServiceProvider provider, Type instanceType, Object[] parameters)
   at Microsoft.AspNetCore.Builder.UseMiddlewareExtensions.ReflectionMiddlewareBinder.CreateMiddleware(RequestDelegate next)
   at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
   at Microsoft.AspNetCore.Builder.ApplicationBuilder.Build()
   at Microsoft.AspNetCore.Hosting.GenericWebHostService.StartAsync(CancellationToken cancellationToken)
   at Microsoft.Extensions.Hosting.Internal.Host.<StartAsync>b__15_1(IHostedService service, CancellationToken token)
   at Microsoft.Extensions.Hosting.Internal.Host.ForeachService[T](IEnumerable`1 services, CancellationToken token, Boolean concurrent, Boolean abortOnFirstException, List`1 exceptions, Func`3 operation)
   at Microsoft.Extensions.Hosting.Internal.Host.StartAsync(CancellationToken cancellationToken)
   at Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions.RunAsync(IHost host, CancellationToken token)
   at Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions.RunAsync(IHost host, CancellationToken token)
   at Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions.Run(IHost host)
   at Program.<Main>$(String[] args) in C:\Users\Bizz\Desktop\aspnetapp\LibraryFinal\Program.cs:line 80 */
