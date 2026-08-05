using System;
using System.Windows.Forms;
using CalculadoraJurosCompostos.Aplicacao;
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
        /// Composition root: conhece a camada de aplicação e o formulário, e delega a ela
        /// o registro das próprias dependências.
        /// </summary>
        private static ServiceProvider ConfigurarServicos()
        {
            return new ServiceCollection()
                .AdicionarAplicacao()
                .AddTransient<FrmCalculadoraJurosCompostos>()
                .BuildServiceProvider();
        }
    }
}
