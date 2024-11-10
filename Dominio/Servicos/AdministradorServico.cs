
using minimal_api.Dominio.Interfaces;
using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;
    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;

    }

    public Administrador? BuscaPorId(int id)
    {
        return _contexto.Administradors.Where(a => a.Id == id).FirstOrDefault();
    }

    public Administrador Incluir(Administrador administrador)
    {
        _contexto.Administradors.Add(administrador);
        _contexto.SaveChanges();

        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _contexto.Administradors.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
        return adm;
    }

    public List<Administrador> Todos(int? pagina)
    {
        var query = _contexto.Administradors.AsQueryable();

        int itemsPorPagina = 10;

        if(pagina != null)
        {
            query = query.Skip(((int)pagina - 1) * itemsPorPagina).Take(itemsPorPagina);
        }
    
        return query.ToList();
    }
}
