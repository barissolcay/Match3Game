using System;
using System.Windows.Forms;

namespace Match3Game
{
    internal static class Program
    {
        /// <summary>
        /// Programýn ana giriþi
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}
