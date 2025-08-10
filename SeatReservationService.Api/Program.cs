using SeatReservation.Infrastructure.Postgres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddScoped<ReservationServiceDbContext>(_ 
    => new ReservationServiceDbContext(builder.Configuration.GetConnectionString("ReservationServiceDb")!)); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();
