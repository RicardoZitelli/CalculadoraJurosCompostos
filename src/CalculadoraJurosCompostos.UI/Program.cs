using System;
using System.Windows.Forms;
using CalculadoraJurosCompostos.Aplicacao;
using CalculadoraJurosCompostos.Dominio;
using Microsoft.Extensions.DependencyInjection;

namespace CalculadoraJurosCompostos
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using ServiceProvider provedor = ConfigurarServicos();

            Application.Run(provedor.GetRequiredService<FrmCalculadoraJurosCompostos>());
        }

        /// <summary>
        /// Composition root: é o único lugar do aplicativo que conhece as implementações
        /// concretas e as amarra às abstrações.
        /// </summary>
        private static ServiceProvider ConfigurarServicos()
        {
            return new ServiceCollection()
                .AddSingleton<SimuladorDeJurosCompostos>()
                .AddSingleton<ISimularInvestimento, SimularInvestimento>()
                .AddTransient<FrmCalculadoraJurosCompostos>()
                .BuildServiceProvider();
        }
    }
}
