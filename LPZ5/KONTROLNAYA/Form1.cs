using System;
using System.Drawing;
using System.Windows.Forms;
namespace KONTROLNAYA
{
    public partial class Form1 : Form
    {
        private TabControl tabControlMain; private TabPage tabRegistry; private TabPage tabLinq; private TabPage tabAggregates; private TabPage tabPaging;
        public Form1()
        {
            // Настройки окна
            this.Text = "CorpFleet Analytics";
            this.Width = 900;
            this.Height = 600;

            // Создаем TabControl
            tabControlMain = new TabControl();
            tabControlMain.Dock = DockStyle.Fill;

            // Создаем 4 вкладки согласно техзаданию
            tabRegistry = new TabPage("1. Реестр ТС и ООП");
            tabLinq = new TabPage("2. LINQ-Аналитика");
            tabAggregates = new TabPage("3. Группировка и Агрегаты");
            tabPaging = new TabPage("4. Постраничный просмотр");

            // Добавляем вкладки в TabControl
            tabControlMain.TabPages.Add(tabRegistry);
            tabControlMain.TabPages.Add(tabLinq);
            tabControlMain.TabPages.Add(tabAggregates);
            tabControlMain.TabPages.Add(tabPaging);

            // Добавляем TabControl на форму
            this.Controls.Add(tabControlMain);
        }
    }
}