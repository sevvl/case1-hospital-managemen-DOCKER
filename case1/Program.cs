    using case1.Data;
    using case1.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Text;

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    // Veritabanı (Npgsql)
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

    // JWT Authentication
    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];
    var jwtAudience = builder.Configuration["Jwt:Audience"];

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
            };
        });

    // Services
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IPatientService, PatientService>();
    builder.Services.AddSingleton<IAiPredictionService, MockAiPredictionService>();

    // CORS for Angular
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AngularApp", policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // HTTPS yönlendirmesini geliştirme ortamı için bırakın, Docker'da kaldırın
    if (!app.Environment.EnvironmentName.Equals("Docker"))
    {
        app.UseHttpsRedirection();
    }

    app.UseCors("AngularApp");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // Database initialization (Artık migrations servisi olduğu için burası teknik olarak gerekli değil, ama zararı da yok)
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Bu kısım migrations servisi tarafından zaten yapılacak, ancak uygulamanın içinde de tutulabilir.
        DbInitializer.Initialize(context);
    }

    app.Run();