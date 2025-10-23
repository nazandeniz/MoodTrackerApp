using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MoodTrackerApp.UI
{
    partial class Form1
    {
        /// <summary>Gerekli designer değişkeni.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Kullanılan tüm kaynakları temizleyin.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        private void InitializeComponent()
        {
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblMood = new Label();
            numericMood = new NumericUpDown();
            lblEnergy = new Label();
            numericEnergy = new NumericUpDown();
            lblTag = new Label();
            comboTag = new ComboBox();
            lblNote = new Label();
            txtNote = new TextBox();
            btnSave = new Button();
            btnDelete = new Button();
            grid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numericMood).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(20, 31);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(41, 20);
            lblDate.TabIndex = 0;
            lblDate.Text = "Date";
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(78, 28);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 27);
            dtpDate.TabIndex = 0;
            // 
            // lblMood
            // 
            lblMood.AutoSize = true;
            lblMood.Location = new Point(351, 28);
            lblMood.Name = "lblMood";
            lblMood.Size = new Size(95, 20);
            lblMood.TabIndex = 1;
            lblMood.Text = "Mood (1–10)";
            // 
            // numericMood
            // 
            numericMood.Location = new Point(452, 29);
            numericMood.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericMood.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericMood.Name = "numericMood";
            numericMood.Size = new Size(60, 27);
            numericMood.TabIndex = 1;
            numericMood.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblEnergy
            // 
            lblEnergy.AutoSize = true;
            lblEnergy.Location = new Point(568, 28);
            lblEnergy.Name = "lblEnergy";
            lblEnergy.Size = new Size(100, 20);
            lblEnergy.TabIndex = 2;
            lblEnergy.Text = "Energy (1–10)";
            lblEnergy.Click += lblEnergy_Click;
            // 
            // numericEnergy
            // 
            numericEnergy.Location = new Point(674, 29);
            numericEnergy.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericEnergy.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericEnergy.Name = "numericEnergy";
            numericEnergy.Size = new Size(60, 27);
            numericEnergy.TabIndex = 2;
            numericEnergy.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblTag
            // 
            lblTag.AutoSize = true;
            lblTag.Location = new Point(819, 28);
            lblTag.Name = "lblTag";
            lblTag.Size = new Size(32, 20);
            lblTag.TabIndex = 3;
            lblTag.Text = "Tag";
            // 
            // comboTag
            // 
            comboTag.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTag.Location = new Point(868, 31);
            comboTag.Name = "comboTag";
            comboTag.Size = new Size(180, 28);
            comboTag.TabIndex = 3;
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.Location = new Point(20, 76);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(42, 20);
            lblNote.TabIndex = 4;
            lblNote.Text = "Note";
            // 
            // txtNote
            // 
            txtNote.Location = new Point(12, 132);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.ScrollBars = ScrollBars.Vertical;
            txtNote.Size = new Size(1626, 247);
            txtNote.TabIndex = 4;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(1167, 16);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(183, 55);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1439, 17);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(183, 55);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeight = 29;
            grid.Location = new Point(12, 407);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersWidth = 51;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(1575, 300);
            grid.TabIndex = 7;
            grid.CellDoubleClick += grid_CellDoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1806, 812);
            Controls.Add(lblDate);
            Controls.Add(dtpDate);
            Controls.Add(lblMood);
            Controls.Add(numericMood);
            Controls.Add(lblEnergy);
            Controls.Add(numericEnergy);
            Controls.Add(lblTag);
            Controls.Add(comboTag);
            Controls.Add(lblNote);
            Controls.Add(txtNote);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(grid);
            Font = new Font("Segoe UI", 9F);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mood Tracker";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericMood).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblMood;
        private NumericUpDown numericMood;
        private Label lblEnergy;
        private NumericUpDown numericEnergy;
        private Label lblTag;
        private ComboBox comboTag;
        private Label lblNote;
        private TextBox txtNote;
        private Button btnSave;
        private Button btnDelete;
        private DataGridView grid;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}
