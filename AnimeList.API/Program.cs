var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


// app.Run();
// delete everything below from here 
app.MapGet("/", () => Results.Content(
    @"
    <html>
    <head>
        <style>
            body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 30px; background: #1a1a1a; color: #e0e0e0; }
        </style>
    </head>
    <body>
        <div style='max-width: 800px; margin: 0 auto;'>
            <h1 style='color: #6a0dad; border-bottom: 2px solid #6a0dad; padding-bottom: 10px;'>
                🎌 Anime List API - Development Dashboard
            </h1>
            
            <div style='background: #2d2d2d; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                <h3>System Info</h3>
                <p><strong>Version:</strong> 1.0.0</p>
                <p><strong>Status:</strong> <span style='color: #4caf50;'>● Running</span></p>
                <p><strong>Server Time:</strong> " + DateTime.Now + @"</p>
                <p><strong>Environment:</strong> Development</p>
            </div>

            <div style='background: #2d2d2d; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                <h3>Available Endpoints</h3>
                <ul style='list-style: none; padding: 0;'>
                    <li style='margin: 10px 0; padding: 10px; background: #3d3d3d; border-radius: 4px;'>
                        <span style='color: #ffd700; font-weight: bold;'>GET</span> 
                        <code style='background: #4d4d4d; padding: 3px 8px; border-radius: 3px;'>/</code>
                        <span style='color: #aaa; margin-left: 10px;'>- This dashboard</span>
                    </li>
                    <li style='margin: 10px 0; padding: 10px; background: #3d3d3d; border-radius: 4px;'>
                        <span style='color: #ffd700; font-weight: bold;'>GET</span> 
                        <code style='background: #4d4d4d; padding: 3px 8px; border-radius: 3px;'>/swagger</code>
                        <span style='color: #aaa; margin-left: 10px;'>- API documentation</span>
                    </li>
                </ul>
            </div>

            <div style='background: #2d2d2d; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                <h3>Quick Test</h3>
                <p>Try these commands in your terminal:</p>
                <pre style='background: #1e1e1e; padding: 15px; border-radius: 4px; overflow-x: auto;'>
# Test the API with curl
curl http://localhost:5000/health

# Check JSON response format
curl -H ""Accept: application/json"" http://localhost:5000/api/status
                </pre>
            </div>
        </div>
    </body>
    </html>", 
    "text/html"
));

// Also add a JSON endpoint for actual API usage
app.MapGet("/api/status", () =>
{
    return new
    {
        version = "1.0.0",
        status = "running",
        timestamp = DateTime.UtcNow,
        endpoints = new[]
        {
            new { path = "/", method = "GET", description = "Development dashboard" },
            new { path = "/api/status", method = "GET", description = "API status in JSON" }
        }
    };
});

app.Run();

