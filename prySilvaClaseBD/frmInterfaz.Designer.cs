namespace prySilvaClaseBD
{
    partial class frmInterfaz
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.cboTables = new System.Windows.Forms.ComboBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.lblTables = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDatabases
            // 
            this.cboDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(12, 29);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(460, 21);
            this.cboDatabases.TabIndex = 0;
            this.cboDatabases.SelectedIndexChanged += new System.EventHandler(this.cboDatabases_SelectedIndexChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(478, 27);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar...";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(559, 27);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 2;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // cboTables
            // 
            this.cboTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(12, 74);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(460, 21);
            this.cboTables.TabIndex = 3;
            this.cboTables.SelectedIndexChanged += new System.EventHandler(this.cboTables_SelectedIndexChanged);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 111);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(776, 327);
            this.dgvData.TabIndex = 4;
            // 
            // lblDatabases
            // 
            this.lblDatabases.AutoSize = true;
            this.lblDatabases.Location = new System.Drawing.Point(12, 9);
            this.lblDatabases.Name = "lblDatabases";
            this.lblDatabases.Size = new System.Drawing.Size(136, 13);
            this.lblDatabases.TabIndex = 5;
            this.lblDatabases.Text = "Archivos de base de datos:";
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(12, 58);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(42, 13);
            this.lblTables.TabIndex = 6;
            this.lblTables.Text = "Tablas:";
            // 
            // frmInterfaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.lblDatabases);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.cboTables);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cboDatabases);
            this.Name = "frmInterfaz";
            this.Text = "Visor de Bases de Datos";
            this.Load += new System.EventHandler(this.frmInterfaz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.Label lblTables;
    }
}

