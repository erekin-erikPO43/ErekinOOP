using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
public partial class Form1 : Form
{
    private ReportService _reportService = new ReportService(); private int _currentPage = 1; private int _pageSize = 5;
    private ComboBox cmbCategoryFilter = null;
    private DataGridView dgvTab1 = null;
    private DataGridView dgvTab2 = null;
    private TextBox txtGlobalAudit = null;
    private DataGridView dgvTab3 = null;
    private DataGridView dgvTab4 = null;
    private Button btnPrev = null;
    private Button btnNext = null;
    private NumericUpDown numPageSize = null;
    private Label lblPageInfo = null;

    public Form1()
    {
        this.Text = "LINQ Анализ";
        this.Size = new Size(900, 600);
        this.StartPosition = FormStartPosition.CenterScreen;

        BuildUI();
        DatabaseInitializer.Initialize();
        _reportService.LoadDataFromDatabase();

        InitFilters();
        LoadTab1();
        LoadTab2();
        LoadTab4();
    }

    private void BuildUI()
    {
        var tabs = new TabControl { Dock = DockStyle.Fill };

        // Таб 1
        var t1 = new TabPage("Реестр");
        cmbCategoryFilter = new ComboBox { Location = new Point(10, 10), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
        var btnFilt = new Button { Text = "Фильтр", Location = new Point(170, 8), Width = 90 };
        btnFilt.Click += delegate (object sender, EventArgs e) {
            string cat = cmbCategoryFilter.SelectedItem != null ? cmbCategoryFilter.SelectedItem.ToString() : "Все";
            dgvTab1.DataSource = cat == "Все" ? (object)_reportService.GetSaleReports() : _reportService.GetSaleReports().Where(x => x.CategoryName == cat).ToList();
        };
        dgvTab1 = new DataGridView { Location = new Point(10, 45), Size = new Size(850, 480), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, ReadOnly = true };
        t1.Controls.Add(cmbCategoryFilter);
        t1.Controls.Add(btnFilt);
        t1.Controls.Add(dgvTab1);

        // Таб 2
        var t2 = new TabPage("Группировка");
        dgvTab2 = new DataGridView { Location = new Point(10, 10), Size = new Size(850, 250), ReadOnly = true };
        txtGlobalAudit = new TextBox { Location = new Point(10, 270), Size = new Size(850, 230), Multiline = true, ReadOnly = true };
        t2.Controls.Add(dgvTab2);
        t2.Controls.Add(txtGlobalAudit);

        // Таб 3
        var t3 = new TabPage("Множества");
        var b1 = new Button { Text = "Online Only", Location = new Point(10, 10), Width = 130 };
        var b2 = new Button { Text = "Intersect", Location = new Point(150, 10), Width = 130 };
        var b3 = new Button { Text = "Union", Location = new Point(290, 10), Width = 130 };
        dgvTab3 = new DataGridView { Location = new Point(10, 50), Size = new Size(850, 470), ReadOnly = true };
        b1.Click += delegate (object sender, EventArgs e) { dgvTab3.DataSource = _reportService.GetOnlineOnlyProducts(); };
        b2.Click += delegate (object sender, EventArgs e) { dgvTab3.DataSource = _reportService.GetPopularBothChannelsProducts(); };
        b3.Click += delegate (object sender, EventArgs e) { dgvTab3.DataSource = _reportService.GetAllSoldProductsUnique(); };
        t3.Controls.Add(b1);
        t3.Controls.Add(b2);
        t3.Controls.Add(b3);
        t3.Controls.Add(dgvTab3);

        // Таб 4
        var t4 = new TabPage("Пагинация");
        dgvTab4 = new DataGridView { Location = new Point(10, 10), Size = new Size(850, 460), ReadOnly = true };
        btnPrev = new Button { Text = "Назад", Location = new Point(10, 480), Width = 80 };
        lblPageInfo = new Label { Text = "Страница 1", Location = new Point(100, 485), AutoSize = true };
        btnNext = new Button { Text = "Вперед", Location = new Point(200, 480), Width = 80 };
        numPageSize = new NumericUpDown { Location = new Point(300, 482), Minimum = 1, Maximum = 20, Value = 5, Width = 60 };

        btnPrev.Click += delegate (object sender, EventArgs e) { _currentPage--; LoadTab4(); };
        btnNext.Click += delegate (object sender, EventArgs e) { _currentPage++; LoadTab4(); };
        numPageSize.ValueChanged += delegate (object sender, EventArgs e) { _pageSize = (int)numPageSize.
            Value; _currentPage = 1; LoadTab4();
        };

        t4.Controls.Add(dgvTab4);
        t4.Controls.Add(btnPrev);
        t4.Controls.Add(lblPageInfo);
        t4.Controls.Add(btnNext);
        t4.Controls.Add(numPageSize);

        tabs.TabPages.Add(t1);
        tabs.TabPages.Add(t2);
        tabs.TabPages.Add(t3);
        tabs.TabPages.Add(t4);
        this.Controls.Add(tabs);
    }

    private void InitFilters()
    {
        cmbCategoryFilter.Items.Add("Все");
        foreach (var c in _reportService.Categories) cmbCategoryFilter.Items.Add(c.Name);
        cmbCategoryFilter.SelectedIndex = 0;
    }

    private void LoadTab1() { dgvTab1.DataSource = _reportService.GetSaleReports(); }
    private void LoadTab2() { dgvTab2.DataSource = _reportService.GetCategorySalesSummaries(); txtGlobalAudit.Text = _reportService.GetGlobalAuditSummary(); }
    private void LoadTab4()
    {
        string cat = cmbCategoryFilter.SelectedItem != null ? cmbCategoryFilter.SelectedItem.ToString() : "Все";
        int total = 0;
        var data = _reportService.GetFilteredSales(cat, _currentPage, _pageSize, out total);
        dgvTab4.DataSource = data;
        int max = (int)Math.Ceiling((double)total / _pageSize);
        if (max == 0) max = 1;
        lblPageInfo.Text = "Страница " + _currentPage + " из " + max;
        btnPrev.Enabled = _currentPage > 1;
        btnNext.Enabled = _currentPage < max;
    }
}   