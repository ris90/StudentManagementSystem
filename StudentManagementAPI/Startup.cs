using Newtonsoft.Json.Serialization;
using StudentManagementAPI.Database;
using StudentManagementAPI.Services;

namespace StudentManagementAPI
{
    public class Startup
    {
        public static IConfiguration? Configuration { get; set; }
        public static IWebHostEnvironment? Environment { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbClient, MongoDbClient>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddCors(Options =>
            {
                Options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
                });
                
            });

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
