namespace DataStore
#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Swashbuckle
open Swashbuckle.Application

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        let app = builder.Build()
        
        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()
        app.UseSwaggerUI( fun options -> options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1") )
        app.Run()

        exitCode
