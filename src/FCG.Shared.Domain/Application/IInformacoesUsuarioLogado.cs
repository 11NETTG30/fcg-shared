namespace FCG.Shared.Application;

public interface IInformacoesUsuarioLogado
{
    public Guid Id { get; }
    public string Email { get; }
    public bool Administrador { get; set; }
}
