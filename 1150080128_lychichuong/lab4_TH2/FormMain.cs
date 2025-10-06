using System.Windows.Forms;

namespace lab4_TH2
{
    public class FormMain : Form
    {
        public FormMain()
        {
            Text = "Thực hành 2 - Tabs";
            Width = 1000; Height = 650; StartPosition = FormStartPosition.CenterScreen;

            var tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(new TabPage("1) ExecuteScalar") { Controls = { new UCScalar { Dock = DockStyle.Fill } } });
            tabs.TabPages.Add(new TabPage("2) 1 dòng (Reader)") { Controls = { new UCOneRow { Dock = DockStyle.Fill } } });
            tabs.TabPages.Add(new TabPage("3) Nhiều dòng (Reader)") { Controls = { new UCMany { Dock = DockStyle.Fill } } });
            tabs.TabPages.Add(new TabPage("4) Parameter") { Controls = { new UCParam { Dock = DockStyle.Fill } } });
            tabs.TabPages.Add(new TabPage("Áp dụng") { Controls = { new UCApply { Dock = DockStyle.Fill } } });
            Controls.Add(tabs);
        }
    }
}
