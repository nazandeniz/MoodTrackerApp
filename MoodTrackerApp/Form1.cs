using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.EntityFrameworkCore; // EnsureCreated i�in
using MoodTrackerApp.DataAccess;
using MoodTrackerApp.DataAccess.Repositories;
using MoodTrackerApp.Entities;

namespace MoodTrackerApp.UI
{
    public partial class Form1 : Form
    {
        private readonly MoodEntryRepository repo = new MoodEntryRepository();

        // Grid + Chart kapsay�c�
        private Panel panelData;

        // Designer'da olmayabilir; bulursak tutar�z, yoksa biz ekleriz
        private Button btnUpdateRef;

        public Form1()
        {
            InitializeComponent();

            // --- Grid ayarlar� ---
            grid.AutoGenerateColumns = true;
            grid.DataBindingComplete += Grid_DataBindingComplete;
            grid.CellDoubleClick += grid_CellDoubleClick;

            // --- Update butonunu garanti et ---
            EnsureUpdateButton();   // varsa ba�lar, yoksa olu�turur

            // --- Yerle�im: Grid + Chart paneli ---
            EnsureDataPanel();      // grid'i panel i�ine al�r ve Dock=Fill yapar

            // --- Chart garanti ---
            EnsureChart();          // chart'� panel i�ine ekler ve Dock=Bottom yapar
            InitializeChartStyle(); // chart g�r�n�m/seri �ablonlar�
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var db = new AppDbContext())
                db.Database.EnsureCreated();

            comboTag.Items.Clear();
            comboTag.Items.AddRange(new[] { "Work", "Study", "Relax", "Social", "Health", "Other" });
            if (comboTag.Items.Count > 0)
                comboTag.SelectedIndex = 0;

            LoadData();
        }

        // =============================
        // Update butonunu garanti et
        // =============================
        private void EnsureUpdateButton()
        {
            // Designer'da var m�?
            btnUpdateRef = this.Controls
                .OfType<Button>()
                .FirstOrDefault(b => string.Equals(b.Name, "btnUpdate", StringComparison.OrdinalIgnoreCase));

            if (btnUpdateRef == null)
            {
                // Yoksa olu�tural�m
                btnUpdateRef = new Button
                {
                    Name = "btnUpdate",
                    Text = "Update",
                    AutoSize = true
                };

                // btnSave yan�na yerle�tirmeye �al��
                var btnSaveCtrl = this.Controls
                    .OfType<Button>()
                    .FirstOrDefault(b => string.Equals(b.Name, "btnSave", StringComparison.OrdinalIgnoreCase));

                if (btnSaveCtrl != null)
                {
                    btnUpdateRef.Left = btnSaveCtrl.Right + 8;
                    btnUpdateRef.Top = btnSaveCtrl.Top;
                    btnUpdateRef.Height = btnSaveCtrl.Height;
                }
                else
                {
                    // Konum bulamazsak sa� �stte dursun
                    btnUpdateRef.Left = 12;
                    btnUpdateRef.Top = 12;
                }

                this.Controls.Add(btnUpdateRef);
            }

            // Click event�ini tekille�tirip ba�la
            btnUpdateRef.Click -= btnUpdate_Click;
            btnUpdateRef.Click += btnUpdate_Click;
        }

        // =============================
        // Yerle�im: kapsay�c� panel
        // =============================
        private void EnsureDataPanel()
        {
            if (panelData != null && Controls.Contains(panelData)) return;

            var origParent = grid.Parent ?? this;
            var bounds = grid.Bounds;

            panelData = new Panel
            {
                Name = "panelData",
                Bounds = bounds,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            origParent.Controls.Remove(grid);
            panelData.Controls.Add(grid);
            grid.Dock = DockStyle.Fill;

            Controls.Add(panelData);
            panelData.BringToFront();
        }

        // =============================
        // Veri: ba�la + grafi�i g�ncelle
        // =============================
        private void LoadData()
        {
            var data = repo.GetAll()
                           .OrderByDescending(x => x.Date)
                           .ToList();

            grid.DataSource = data; // kolon i�leri DataBindingComplete'ta
            LoadChartData(data);
        }

        // =============================
        // Chart g�venli olu�turma
        // =============================
        private void EnsureChart()
        {
            if (chart == null)
                chart = new Chart();

            if (chart.Parent != panelData)
            {
                chart.Parent?.Controls.Remove(chart);
                panelData.Controls.Add(chart);
            }

            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();

            chart.Name = "chart";
            chart.Dock = DockStyle.Bottom;   // altta sabit �erit
            chart.Height = 260;              // iste�in �zerine b�y�t�ld�
            chart.Visible = true;

            if (chart.ChartAreas.Count == 0)
            {
                var area = new ChartArea("MainArea")
                {
                    BackColor = Color.White,
                    BorderColor = Color.Gainsboro
                };

                area.AxisX.MajorGrid.Enabled = false;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8f);
                area.AxisX.IsMarginVisible = true;
                area.AxisX.Title = "Date";

                area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8f);
                area.AxisY.Title = "Score";
                area.AxisY.Minimum = 0;
                area.AxisY.Maximum = 10;
                area.AxisY.Interval = 1;

                chart.ChartAreas.Add(area);
            }

            if (chart.Legends.Count == 0)
            {
                chart.Legends.Add(new Legend("MainLegend")
                {
                    Docking = Docking.Bottom,
                    Alignment = StringAlignment.Center,
                    Font = new Font("Segoe UI", 9f)
                });
            }

            if (chart.Titles.Count == 0)
            {
                chart.Titles.Add(new Title("Mood & Energy Trends",
                    Docking.Top, new Font("Segoe UI", 11f, FontStyle.Bold), Color.DimGray));
            }

            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
        }

        // =============================
        // Chart stil/seri �ablonu
        // =============================
        private void InitializeChartStyle()
        {
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            if (chart.Series.IndexOf("Mood") < 0)
            {
                var mood = new Series("Mood")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = Color.DeepSkyBlue,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6,
                    XValueType = ChartValueType.String,
                    IsXValueIndexed = true,
                    ToolTip = "Mood #VALY on #VALX",
                    ChartArea = chart.ChartAreas[0].Name
                };
                chart.Series.Add(mood);
            }

            if (chart.Series.IndexOf("Energy") < 0)
            {
                var energy = new Series("Energy")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = Color.LimeGreen,
                    MarkerStyle = MarkerStyle.Diamond,
                    MarkerSize = 6,
                    XValueType = ChartValueType.String,
                    IsXValueIndexed = true,
                    ToolTip = "Energy #VALY on #VALX",
                    ChartArea = chart.ChartAreas[0].Name
                };
                chart.Series.Add(energy);
            }
        }

        // =============================
        // Chart verisini doldurma
        // =============================
        private void LoadChartData(IEnumerable<MoodEntry> data)
        {
            EnsureChart();
            InitializeChartStyle();

            var sMood = chart.Series["Mood"];
            var sEnergy = chart.Series["Energy"];
            sMood.Points.Clear();
            sEnergy.Points.Clear();

            if (data == null || !data.Any())
            {
                chart.Visible = false;
                return;
            }

            chart.Visible = true;

            var ordered = data.OrderBy(x => x.Date).ToList();

            // Y ekseni (0�10 i�inde, en az 3 aral�k)
            int minVal = ordered.Min(x => Math.Min(x.MoodScore, x.EnergyScore));
            int maxVal = ordered.Max(x => Math.Max(x.MoodScore, x.EnergyScore));
            double pad = 1;
            double ymin = Math.Max(0, minVal - pad);
            double ymax = Math.Min(10, maxVal + pad);
            if (ymax - ymin < 3) ymax = Math.Min(10, ymin + 3);

            var area = chart.ChartAreas["MainArea"];
            area.AxisY.Minimum = ymin;
            area.AxisY.Maximum = ymax;

            // X etiket yo�unlu�u
            int n = ordered.Count;
            area.AxisX.Interval = n > 10 ? Math.Ceiling(n / 10.0) : 1;

            foreach (var it in ordered)
            {
                string dx = it.Date.ToString("dd.MM");
                sMood.Points.AddXY(dx, it.MoodScore);
                sEnergy.Points.AddXY(dx, it.EnergyScore);
            }
        }

        // =============================
        // Grid kolon/numara
        // =============================
        private void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (grid.Columns.Contains("Id"))
                grid.Columns["Id"].Visible = false;

            EnsureRowNumberColumn();
            RenumberRows();
        }

        private void EnsureRowNumberColumn()
        {
            if (grid.Columns.Contains("No")) return;

            var oldMode = grid.AutoSizeColumnsMode;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            var col = new DataGridViewTextBoxColumn
            {
                Name = "No",
                HeaderText = "#",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };

            grid.Columns.Insert(0, col);

            col.DefaultCellStyle.BackColor = Color.Gainsboro;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grid.AutoSizeColumnsMode = oldMode;
        }

        private void RenumberRows()
        {
            if (!grid.Columns.Contains("No"))
                EnsureRowNumberColumn();

            int noIndex = grid.Columns["No"].Index;

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                var row = grid.Rows[i];
                if (row.IsNewRow) continue;
                row.Cells[noIndex].Value = i + 1;
            }
        }

        // =============================
        // CRUD
        // =============================
        private void btnSave_Click(object sender, EventArgs e)
        {
            var entry = new MoodEntry
            {
                Date = dtpDate.Value.Date,
                MoodScore = (int)numericMood.Value,
                EnergyScore = (int)numericEnergy.Value,
                Tag = comboTag.Text,
                Note = txtNote.Text
            };

            // Upsert: yeni tarih ise insert, ayn� tarih varsa update
            repo.Upsert(entry);
            LoadData();
            SelectRowByDate(entry.Date);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not MoodEntry selected)
            {
                MessageBox.Show("G�ncellemek i�in listeden bir kay�t se�.", "Update",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var updated = new MoodEntry
            {
                Id = selected.Id,              // ayn� kay�t
                Date = dtpDate.Value.Date,
                MoodScore = (int)numericMood.Value,
                EnergyScore = (int)numericEnergy.Value,
                Tag = comboTag.Text,
                Note = txtNote.Text
            };

            try
            {
                repo.Upsert(updated); // Id dolu -> Update
                LoadData();
                SelectRowById(updated.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kay�t g�ncellenemedi.\n\n" + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not MoodEntry selected) return;

            repo.Delete(selected.Id);
            LoadData();
        }

        // =============================
        // Grid etkile�imleri
        // =============================
        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (grid.Rows[e.RowIndex].DataBoundItem is not MoodEntry selected) return;

            dtpDate.Value = selected.Date;
            numericMood.Value = selected.MoodScore;
            numericEnergy.Value = selected.EnergyScore;
            comboTag.Text = selected.Tag;
            txtNote.Text = selected.Note;
        }

        // =============================
        // Yard�mc�: sat�r se�
        // =============================
        private void SelectRowById(int id)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.DataBoundItem is MoodEntry me && me.Id == id)
                {
                    row.Selected = true;
                    grid.CurrentCell = row.Cells[Math.Max(1, row.Cells.Count - 1)];
                    grid.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void SelectRowByDate(DateTime date)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.DataBoundItem is MoodEntry me && me.Date.Date == date.Date)
                {
                    row.Selected = true;
                    grid.CurrentCell = row.Cells[Math.Max(1, row.Cells.Count - 1)];
                    grid.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void lblEnergy_Click(object sender, EventArgs e) { }
    }
}
