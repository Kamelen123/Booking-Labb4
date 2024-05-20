
using Booking_Labb4.Converter;
using Booking_Labb4.Data;
using Booking_Labb4.Repository;
using Booking_Labb4.Services;
using Microsoft.EntityFrameworkCore;

namespace Booking_Labb4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //My What ever we call this
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICompany, CompanyRepository>();
            builder.Services.AddScoped<ICustomer, CustomerRepository>();
            builder.Services.AddScoped<IAppointment, AppointmentRepository>();

            //EF till SQL
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
