using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db;

public class DbContexto : DbContext
{

    private readonly IConfiguration _configuracaoAppSettings;
    public DbContexto(IConfiguration configuraçaoAppSettings)
    {
        _configuracaoAppSettings = configuraçaoAppSettings;
    }
    public DbSet<Administrador> Administradors { get; set; } = default!;

    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador 
            {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "1234560",
                Perfil = "Adm"
            }
        );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConexao = _configuracaoAppSettings.GetConnectionString("SqlServer")?.ToString();
            if (!string.IsNullOrEmpty(stringConexao))
            {
                optionsBuilder.UseSqlServer(stringConexao);
                //ServerVersion.Autodetect("string de conexao") autodetecta a string de conexao  (mysql usado em aula)

            }
        }
        
       
    }
}