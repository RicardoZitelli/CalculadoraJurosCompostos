using CalculadoraJurosCompostos.Dominio;
using Microsoft.Extensions.DependencyInjection;

namespace CalculadoraJurosCompostos.Aplicacao
{
    /// <summary>
    /// Registro dos serviços desta camada e das suas dependências de domínio. Fica aqui,
    /// e não no composition root, para que a apresentação não precise conhecer o domínio.
    /// </summary>
    public static class ServicosDaAplicacao
    {
        public static IServiceCollection AdicionarAplicacao(this IServiceCollection servicos)
        {
            ArgumentNullException.ThrowIfNull(servicos);

            return servicos
                .AddSingleton<SimuladorDeJurosCompostos>()
                .AddSingleton<ISimularInvestimento, SimularInvestimento>();
        }
    }
}
