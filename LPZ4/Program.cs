using System;
using System.Windows.Forms;
using WinFormsLinqApp;

namespace LPZ4
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Настройка параметров отображения интерфейса
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск главной формы приложения
            Application.Run(new FormMain());
        }
    }
}