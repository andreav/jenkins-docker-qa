# Getting swagger.json at runtime

Go to URL for you swagger endpoint:

    http://localhost:5000/swagger/v1/swagger.json

# Getting swagger.json at compile time

* Install Swashbuckle.AspNetCore.Cli locally to you solution (or globally, but locally can be restored and is under source control)

    * Create a tool-manifest  
    This file is needed when installing tools for local access, for the current directory and subdirectories)  
    This will create a `dotnet-tools.json` file in te directory of the command
    
            dotnet new tool-manifest
            dotnet tool install Swashbuckle.AspNetCore.Cli --version 6.0.7
    
    * Now build the applciation

            dotnet build

    
    * Generate the spec.  
    *Note: "v1" is the name of the "Version" parameter inside services.AddSwaggerGen*
    
            dotnet tool run swagger tofile --output swagger.json <path-to-dll> v1


# Getting swagger.json from command line (no CLI but with code)

In some situations CLI cannot build the file (looking for other dependencies)

Following [this comment on github](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/541#issuecomment-1006420895) - generate file with command line directive:

        public static class Program
        {
            public static void Main(string[] args)
            {
                var webHost = CreateWebHostBuilder().Build();
                
                // If args, then it's for generating swagger and exit
                // otherwise run web server
                if (args.Length > 0)
                {
                    Console.WriteLine(GenerateSwagger(webHost, args[0]));
                    Environment.Exit(0);
                }
                
                webHost.Run();
            }
            
            private static IWebHostBuilder CreateWebHostBuilder()
            {
                IWebHostBuilder builder = WebHost
                    .CreateDefaultBuilder()
                    .UseConfiguration(...)
                    .UseUrls(...)
                    .UseStartup<Startup>()
                    ....;
                
                return builder;
            }
            
            private static string GenerateSwagger(IWebHost host, string docName)
            {
                ISwaggerProvider sw = (ISwaggerProvider)host.Services.GetService(typeof(ISwaggerProvider));
                OpenApiDocument doc = sw.GetSwagger(docName);
                return doc.SerializeAsJson(OpenApiSpecVersion.OpenApi3_0);
            }
        }

Then you can run the webapi for generating spec like this:

        dotnet run --project <project-path> v1 > spec.json


# Generating k6 tests from swagger spec.json

Generates file `k6\swagger\generation\k6-test\script.js` for using with k6 load test

        cd k6\swagger
        docker run --rm -v ${PWD}/generation:/local openapitools/openapi-generator-cli generate -i /local/spec-swagger.json -g k6 -o /local/k6-test/ --skip-validate-spec

