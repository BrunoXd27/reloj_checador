namespace reloj_checador
{
    partial class Checador
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer_lab = new System.Windows.Forms.Label();
            this.checar = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.TextBox();
            this.enter = new System.Windows.Forms.GroupBox();
            this.entradas = new System.Windows.Forms.ListBox();
            this.exit = new System.Windows.Forms.GroupBox();
            this.salidas = new System.Windows.Forms.ListBox();
            this.fechaCompleta = new System.Windows.Forms.Label();
            this.notificacion = new System.Windows.Forms.Label();
            this.enter.SuspendLayout();
            this.exit.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer_lab
            // 
            this.timer_lab.AutoSize = true;
            this.timer_lab.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer_lab.Location = new System.Drawing.Point(56, 130);
            this.timer_lab.Name = "timer_lab";
            this.timer_lab.Size = new System.Drawing.Size(184, 86);
            this.timer_lab.TabIndex = 0;
            this.timer_lab.Text = "Hora";
            // 
            // checar
            // 
            this.checar.BackColor = System.Drawing.Color.LightGreen;
            this.checar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.checar.Location = new System.Drawing.Point(490, 81);
            this.checar.Name = "checar";
            this.checar.Size = new System.Drawing.Size(136, 27);
            this.checar.TabIndex = 1;
            this.checar.Text = "Checar";
            this.checar.UseVisualStyleBackColor = false;
            this.checar.Click += new System.EventHandler(this.checar_Click);
            // 
            // Id
            // 
            this.Id.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Id.Location = new System.Drawing.Point(455, 40);
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(207, 25);
            this.Id.TabIndex = 3;
            this.Id.Text = "Ingrese ID";
            this.Id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // enter
            // 
            this.enter.Controls.Add(this.entradas);
            this.enter.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enter.Location = new System.Drawing.Point(416, 122);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(277, 145);
            this.enter.TabIndex = 6;
            this.enter.TabStop = false;
            this.enter.Text = "Entradas";
            // 
            // entradas
            // 
            this.entradas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entradas.FormattingEnabled = true;
            this.entradas.ItemHeight = 15;
            this.entradas.Location = new System.Drawing.Point(16, 20);
            this.entradas.Name = "entradas";
            this.entradas.Size = new System.Drawing.Size(246, 109);
            this.entradas.TabIndex = 0;
            // 
            // exit
            // 
            this.exit.Controls.Add(this.salidas);
            this.exit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.Location = new System.Drawing.Point(416, 280);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(277, 145);
            this.exit.TabIndex = 7;
            this.exit.TabStop = false;
            this.exit.Text = "Salidas";
            // 
            // salidas
            // 
            this.salidas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salidas.FormattingEnabled = true;
            this.salidas.ItemHeight = 15;
            this.salidas.Location = new System.Drawing.Point(16, 19);
            this.salidas.Name = "salidas";
            this.salidas.Size = new System.Drawing.Size(246, 109);
            this.salidas.TabIndex = 1;
            // 
            // fechaCompleta
            // 
            this.fechaCompleta.AutoSize = true;
            this.fechaCompleta.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fechaCompleta.Location = new System.Drawing.Point(65, 208);
            this.fechaCompleta.Name = "fechaCompleta";
            this.fechaCompleta.Size = new System.Drawing.Size(186, 32);
            this.fechaCompleta.TabIndex = 8;
            this.fechaCompleta.Text = "Fecha Completa";
            // 
            // notificacion
            // 
            this.notificacion.AutoSize = true;
            this.notificacion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificacion.Location = new System.Drawing.Point(48, 341);
            this.notificacion.Name = "notificacion";
            this.notificacion.Size = new System.Drawing.Size(93, 21);
            this.notificacion.TabIndex = 9;
            this.notificacion.Text = "Notificación";
            // 
            // Checador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 450);
            this.Controls.Add(this.notificacion);
            this.Controls.Add(this.fechaCompleta);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.Id);
            this.Controls.Add(this.checar);
            this.Controls.Add(this.timer_lab);
            this.Name = "Checador";
            this.Text = "Checador";
            this.enter.ResumeLayout(false);
            this.exit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label timer_lab;
        private System.Windows.Forms.Button checar;
        private System.Windows.Forms.TextBox Id;
        private System.Windows.Forms.GroupBox enter;
        private System.Windows.Forms.ListBox entradas;
        private System.Windows.Forms.GroupBox exit;
        private System.Windows.Forms.ListBox salidas;
        private System.Windows.Forms.Label fechaCompleta;
        private System.Windows.Forms.Label notificacion;
    }
}

