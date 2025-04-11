namespace Sudoku_Game
{
    partial class frmAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            this.btnToFromLogin = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnOut = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteUser = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddAdmin = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportPdf = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dgvLogin = new System.Windows.Forms.DataGridView();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.btnToFromLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnToFromLogin
            // 
            this.btnToFromLogin.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(20, 21, 20, 21);
            this.btnToFromLogin.ExpandCollapseItem.Id = 0;
            this.btnToFromLogin.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnToFromLogin.ExpandCollapseItem,
            this.btnToFromLogin.SearchEditItem,
            this.btnOut,
            this.btnDelete,
            this.barButtonItem1,
            this.barButtonItem2,
            this.btnDeleteUser,
            this.btnAddAdmin,
            this.btnExportPdf});
            this.btnToFromLogin.Location = new System.Drawing.Point(0, 0);
            this.btnToFromLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToFromLogin.MaxItemId = 8;
            this.btnToFromLogin.Name = "btnToFromLogin";
            this.btnToFromLogin.OptionsMenuMinWidth = 220;
            this.btnToFromLogin.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.btnToFromLogin.Size = new System.Drawing.Size(683, 158);
            this.btnToFromLogin.StatusBar = this.ribbonStatusBar;
            this.btnToFromLogin.Click += new System.EventHandler(this.btnToFromLogin_Click);
            // 
            // btnOut
            // 
            this.btnOut.Caption = "Thoát";
            this.btnOut.Id = 1;
            this.btnOut.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnOut.ImageOptions.SvgImage")));
            this.btnOut.Name = "btnOut";
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Xóa";
            this.btnDelete.Id = 2;
            this.btnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDelete.ImageOptions.SvgImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Trở về đăng nhập";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Bảng Xếp Hạng";
            this.barButtonItem2.Id = 4;
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Caption = "Xóa tài khoản";
            this.btnDeleteUser.Id = 5;
            this.btnDeleteUser.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteUser.ImageOptions.SvgImage")));
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteUser_ItemClick);
            // 
            // btnAddAdmin
            // 
            this.btnAddAdmin.Caption = "Thêm Admin";
            this.btnAddAdmin.Id = 6;
            this.btnAddAdmin.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddAdmin.ImageOptions.SvgImage")));
            this.btnAddAdmin.Name = "btnAddAdmin";
            this.btnAddAdmin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddAdmin_ItemClick);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Caption = "Xuất file";
            this.btnExportPdf.Id = 7;
            this.btnExportPdf.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExportPdf.ImageOptions.SvgImage")));
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportPdf_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Admin";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnOut);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExportPdf);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Chức năng";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnDeleteUser);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnAddAdmin);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Thông tin";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 283);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.btnToFromLogin;
            this.ribbonStatusBar.Size = new System.Drawing.Size(683, 24);
            // 
            // dgvLogin
            // 
            this.dgvLogin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUserName,
            this.colPass,
            this.colEmail,
            this.colQuyen,
            this.colScore});
            this.dgvLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogin.Location = new System.Drawing.Point(0, 158);
            this.dgvLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvLogin.Name = "dgvLogin";
            this.dgvLogin.RowHeadersWidth = 62;
            this.dgvLogin.RowTemplate.Height = 28;
            this.dgvLogin.Size = new System.Drawing.Size(683, 125);
            this.dgvLogin.TabIndex = 2;
            this.dgvLogin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogin_CellContentClick);
            // 
            // colUserName
            // 
            this.colUserName.HeaderText = "TK";
            this.colUserName.MinimumWidth = 8;
            this.colUserName.Name = "colUserName";
            this.colUserName.Width = 150;
            // 
            // colPass
            // 
            this.colPass.HeaderText = "MK";
            this.colPass.MinimumWidth = 8;
            this.colPass.Name = "colPass";
            this.colPass.Width = 150;
            // 
            // colEmail
            // 
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 8;
            this.colEmail.Name = "colEmail";
            this.colEmail.Width = 150;
            // 
            // colQuyen
            // 
            this.colQuyen.HeaderText = "Quyen";
            this.colQuyen.MinimumWidth = 8;
            this.colQuyen.Name = "colQuyen";
            this.colQuyen.Width = 150;
            // 
            // colScore
            // 
            this.colScore.HeaderText = "Điểm";
            this.colScore.MinimumWidth = 8;
            this.colScore.Name = "colScore";
            this.colScore.Width = 150;
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 307);
            this.Controls.Add(this.dgvLogin);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.btnToFromLogin);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmAdmin";
            this.Ribbon = this.btnToFromLogin;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.frmAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnToFromLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl btnToFromLogin;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnOut;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem btnDeleteUser;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private System.Windows.Forms.DataGridView dgvLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private DevExpress.XtraBars.BarButtonItem btnAddAdmin;
        private DevExpress.XtraBars.BarButtonItem btnExportPdf;
    }
}