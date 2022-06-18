/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 3/13/2009
 * Time: 8:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ExpertMultimedia
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tbStatus = new System.Windows.Forms.TextBox();
			this.tbInput = new System.Windows.Forms.TextBox();
			this.ofiledlg = new System.Windows.Forms.OpenFileDialog();
			this.lbOut = new System.Windows.Forms.ListBox();
			this.cbAutoscroll = new System.Windows.Forms.CheckBox();
			this.comboInput = new System.Windows.Forms.ComboBox();
			this.panel1 = new FrameworkFastPanel();
			this.progressbarMain = new System.Windows.Forms.ProgressBar();
			this.trackbarFrame = new System.Windows.Forms.TrackBar();
			this.tbH = new System.Windows.Forms.TextBox();
			this.tbM = new System.Windows.Forms.TextBox();
			this.tbS = new System.Windows.Forms.TextBox();
			this.tbMs = new System.Windows.Forms.TextBox();
			this.lblHColon = new System.Windows.Forms.Label();
			this.lblMColon = new System.Windows.Forms.Label();
			this.lblSColon = new System.Windows.Forms.Label();
			this.lblMSColon = new System.Windows.Forms.Label();
			this.nudMinDigits = new System.Windows.Forms.NumericUpDown();
			this.lblMinDigits = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openImageSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.framesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maskToAlphaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.videoToSequenceMJPEGLosslessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tablelayoutpanelSidePanelOuter1Col = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tablelayoutpanelSideBarInner2Col = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.trackbarFrame)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMinDigits)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tablelayoutpanelSidePanelOuter1Col.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tablelayoutpanelSideBarInner2Col.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbStatus
			// 
			this.tbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbStatus.Location = new System.Drawing.Point(3, 578);
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.ReadOnly = true;
			this.tbStatus.Size = new System.Drawing.Size(291, 23);
			this.tbStatus.TabIndex = 0;
			// 
			// tbInput
			// 
			this.tbInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbInput.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbInput.Location = new System.Drawing.Point(3, 3);
			this.tbInput.Name = "tbInput";
			this.tbInput.Size = new System.Drawing.Size(291, 21);
			this.tbInput.TabIndex = 5;
			// 
			// ofiledlg
			// 
			this.ofiledlg.FileName = "openFileDialog1";
			this.ofiledlg.FileOk += new System.ComponentModel.CancelEventHandler(this.OfiledlgFileOk);
			// 
			// lbOut
			// 
			this.lbOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbOut.FormattingEnabled = true;
			this.lbOut.HorizontalScrollbar = true;
			this.lbOut.ItemHeight = 15;
			this.lbOut.Location = new System.Drawing.Point(3, 221);
			this.lbOut.Name = "lbOut";
			this.lbOut.Size = new System.Drawing.Size(291, 304);
			this.lbOut.TabIndex = 12;
			// 
			// cbAutoscroll
			// 
			this.cbAutoscroll.AutoSize = true;
			this.cbAutoscroll.Checked = true;
			this.cbAutoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAutoscroll.Location = new System.Drawing.Point(3, 196);
			this.cbAutoscroll.Name = "cbAutoscroll";
			this.cbAutoscroll.Size = new System.Drawing.Size(156, 19);
			this.cbAutoscroll.TabIndex = 13;
			this.cbAutoscroll.Text = "Scroll Output Messages";
			this.cbAutoscroll.UseVisualStyleBackColor = true;
			// 
			// comboInput
			// 
			this.comboInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.comboInput.FormattingEnabled = true;
			this.comboInput.Location = new System.Drawing.Point(152, 72);
			this.comboInput.Name = "comboInput";
			this.comboInput.Size = new System.Drawing.Size(140, 23);
			this.comboInput.TabIndex = 14;
			this.comboInput.Visible = false;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(205, 153);
			this.panel1.TabIndex = 16;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1Paint);
			// 
			// progressbarMain
			// 
			this.progressbarMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progressbarMain.Location = new System.Drawing.Point(3, 559);
			this.progressbarMain.Name = "progressbarMain";
			this.progressbarMain.Size = new System.Drawing.Size(291, 13);
			this.progressbarMain.TabIndex = 17;
			// 
			// trackbarFrame
			// 
			this.trackbarFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackbarFrame.LargeChange = 1;
			this.trackbarFrame.Location = new System.Drawing.Point(3, 540);
			this.trackbarFrame.Maximum = 0;
			this.trackbarFrame.Name = "trackbarFrame";
			this.trackbarFrame.Size = new System.Drawing.Size(291, 13);
			this.trackbarFrame.TabIndex = 18;
			this.trackbarFrame.ValueChanged += new System.EventHandler(this.TrackbarFrameValueChanged);
			this.trackbarFrame.Scroll += new System.EventHandler(this.TrackbarFrameScroll);
			// 
			// tbH
			// 
			this.tbH.Location = new System.Drawing.Point(3, 18);
			this.tbH.Name = "tbH";
			this.tbH.Size = new System.Drawing.Size(33, 23);
			this.tbH.TabIndex = 20;
			// 
			// tbM
			// 
			this.tbM.Location = new System.Drawing.Point(42, 18);
			this.tbM.Name = "tbM";
			this.tbM.Size = new System.Drawing.Size(33, 23);
			this.tbM.TabIndex = 21;
			// 
			// tbS
			// 
			this.tbS.Location = new System.Drawing.Point(81, 18);
			this.tbS.Name = "tbS";
			this.tbS.Size = new System.Drawing.Size(33, 23);
			this.tbS.TabIndex = 22;
			// 
			// tbMs
			// 
			this.tbMs.Location = new System.Drawing.Point(120, 18);
			this.tbMs.Name = "tbMs";
			this.tbMs.Size = new System.Drawing.Size(45, 23);
			this.tbMs.TabIndex = 23;
			// 
			// lblHColon
			// 
			this.lblHColon.AutoSize = true;
			this.lblHColon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHColon.Location = new System.Drawing.Point(3, 0);
			this.lblHColon.Name = "lblHColon";
			this.lblHColon.Size = new System.Drawing.Size(17, 15);
			this.lblHColon.TabIndex = 24;
			this.lblHColon.Text = "h:";
			// 
			// lblMColon
			// 
			this.lblMColon.AutoSize = true;
			this.lblMColon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMColon.Location = new System.Drawing.Point(42, 0);
			this.lblMColon.Name = "lblMColon";
			this.lblMColon.Size = new System.Drawing.Size(21, 15);
			this.lblMColon.TabIndex = 25;
			this.lblMColon.Text = "m:";
			// 
			// lblSColon
			// 
			this.lblSColon.AutoSize = true;
			this.lblSColon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSColon.Location = new System.Drawing.Point(81, 0);
			this.lblSColon.Name = "lblSColon";
			this.lblSColon.Size = new System.Drawing.Size(17, 15);
			this.lblSColon.TabIndex = 26;
			this.lblSColon.Text = "s:";
			// 
			// lblMSColon
			// 
			this.lblMSColon.AutoSize = true;
			this.lblMSColon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMSColon.Location = new System.Drawing.Point(120, 0);
			this.lblMSColon.Name = "lblMSColon";
			this.lblMSColon.Size = new System.Drawing.Size(28, 15);
			this.lblMSColon.TabIndex = 27;
			this.lblMSColon.Text = "ms:";
			// 
			// nudMinDigits
			// 
			this.nudMinDigits.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.nudMinDigits.Location = new System.Drawing.Point(152, 3);
			this.nudMinDigits.Maximum = new decimal(new int[] {
									20,
									0,
									0,
									0});
			this.nudMinDigits.Name = "nudMinDigits";
			this.nudMinDigits.Size = new System.Drawing.Size(48, 23);
			this.nudMinDigits.TabIndex = 28;
			this.nudMinDigits.Value = new decimal(new int[] {
									4,
									0,
									0,
									0});
			this.nudMinDigits.ValueChanged += new System.EventHandler(this.NudMinDigitsValueChanged);
			// 
			// lblMinDigits
			// 
			this.lblMinDigits.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblMinDigits.AutoSize = true;
			this.lblMinDigits.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMinDigits.Location = new System.Drawing.Point(3, 5);
			this.lblMinDigits.Name = "lblMinDigits";
			this.lblMinDigits.Size = new System.Drawing.Size(143, 15);
			this.lblMinDigits.TabIndex = 29;
			this.lblMinDigits.Text = "Sequence Digits *0000.*";
			this.lblMinDigits.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.framesToolStripMenuItem,
									this.controlToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(866, 24);
			this.menuStrip1.TabIndex = 30;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.openVideoToolStripMenuItem,
									this.openImageSequenceToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openVideoToolStripMenuItem
			// 
			this.openVideoToolStripMenuItem.Name = "openVideoToolStripMenuItem";
			this.openVideoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.openVideoToolStripMenuItem.Text = "Open Video...";
			this.openVideoToolStripMenuItem.Click += new System.EventHandler(this.OpenVideoToolStripMenuItemClick);
			// 
			// openImageSequenceToolStripMenuItem
			// 
			this.openImageSequenceToolStripMenuItem.Name = "openImageSequenceToolStripMenuItem";
			this.openImageSequenceToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.openImageSequenceToolStripMenuItem.Text = "Open Image Sequence...";
			this.openImageSequenceToolStripMenuItem.Click += new System.EventHandler(this.OpenImageSequenceToolStripMenuItemClick);
			// 
			// framesToolStripMenuItem
			// 
			this.framesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.maskToAlphaToolStripMenuItem,
									this.videoToSequenceMJPEGLosslessToolStripMenuItem});
			this.framesToolStripMenuItem.Name = "framesToolStripMenuItem";
			this.framesToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.framesToolStripMenuItem.Text = "Frame&s";
			// 
			// maskToAlphaToolStripMenuItem
			// 
			this.maskToAlphaToolStripMenuItem.Name = "maskToAlphaToolStripMenuItem";
			this.maskToAlphaToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
			this.maskToAlphaToolStripMenuItem.Text = "Mask to Alpha";
			this.maskToAlphaToolStripMenuItem.Click += new System.EventHandler(this.MaskToAlphaToolStripMenuItemClick);
			// 
			// videoToSequenceMJPEGLosslessToolStripMenuItem
			// 
			this.videoToSequenceMJPEGLosslessToolStripMenuItem.Name = "videoToSequenceMJPEGLosslessToolStripMenuItem";
			this.videoToSequenceMJPEGLosslessToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
			this.videoToSequenceMJPEGLosslessToolStripMenuItem.Text = "Video to Sequence (MJPEG lossless)";
			this.videoToSequenceMJPEGLosslessToolStripMenuItem.Click += new System.EventHandler(this.VideoToSequenceMJPEGLosslessToolStripMenuItemClick);
			// 
			// controlToolStripMenuItem
			// 
			this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.cancelToolStripMenuItem});
			this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
			this.controlToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.controlToolStripMenuItem.Text = "&Control";
			// 
			// cancelToolStripMenuItem
			// 
			this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
			this.cancelToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
			this.cancelToolStripMenuItem.Text = "Cance&l";
			this.cancelToolStripMenuItem.Click += new System.EventHandler(this.CancelToolStripMenuItemClick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tablelayoutpanelSidePanelOuter1Col);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer1.Panel2.BackgroundImage")));
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(866, 596);
			this.splitContainer1.SplitterDistance = 297;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 19;
			// 
			// tablelayoutpanelSidePanelOuter1Col
			// 
			this.tablelayoutpanelSidePanelOuter1Col.ColumnCount = 1;
			this.tablelayoutpanelSidePanelOuter1Col.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.tbInput, 0, 0);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.progressbarMain, 0, 6);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.trackbarFrame, 0, 5);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.tableLayoutPanel1, 0, 2);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.tbStatus, 0, 7);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.lbOut, 0, 4);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.tablelayoutpanelSideBarInner2Col, 0, 1);
			this.tablelayoutpanelSidePanelOuter1Col.Controls.Add(this.cbAutoscroll, 0, 3);
			this.tablelayoutpanelSidePanelOuter1Col.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablelayoutpanelSidePanelOuter1Col.Location = new System.Drawing.Point(0, 0);
			this.tablelayoutpanelSidePanelOuter1Col.Name = "tablelayoutpanelSidePanelOuter1Col";
			this.tablelayoutpanelSidePanelOuter1Col.RowCount = 8;
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.660377F));
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.94883F));
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.79693F));
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.79693F));
			this.tablelayoutpanelSidePanelOuter1Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.79693F));
			this.tablelayoutpanelSidePanelOuter1Col.Size = new System.Drawing.Size(297, 596);
			this.tablelayoutpanelSidePanelOuter1Col.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.tbH, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbM, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblHColon, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblMSColon, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbMs, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblSColon, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbS, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblMColon, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 146);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(168, 44);
			this.tableLayoutPanel1.TabIndex = 31;
			// 
			// tablelayoutpanelSideBarInner2Col
			// 
			this.tablelayoutpanelSideBarInner2Col.ColumnCount = 2;
			this.tablelayoutpanelSideBarInner2Col.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tablelayoutpanelSideBarInner2Col.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tablelayoutpanelSideBarInner2Col.Controls.Add(this.nudMinDigits, 1, 0);
			this.tablelayoutpanelSideBarInner2Col.Controls.Add(this.lblMinDigits, 0, 0);
			this.tablelayoutpanelSideBarInner2Col.Controls.Add(this.comboInput, 1, 2);
			this.tablelayoutpanelSideBarInner2Col.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablelayoutpanelSideBarInner2Col.Location = new System.Drawing.Point(3, 25);
			this.tablelayoutpanelSideBarInner2Col.Name = "tablelayoutpanelSideBarInner2Col";
			this.tablelayoutpanelSideBarInner2Col.RowCount = 3;
			this.tablelayoutpanelSideBarInner2Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tablelayoutpanelSideBarInner2Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tablelayoutpanelSideBarInner2Col.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tablelayoutpanelSideBarInner2Col.Size = new System.Drawing.Size(291, 115);
			this.tablelayoutpanelSideBarInner2Col.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(866, 620);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainForm";
			this.Text = "RotoCanvas";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Resize += new System.EventHandler(this.MainFormResize);
			((System.ComponentModel.ISupportInitialize)(this.trackbarFrame)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMinDigits)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tablelayoutpanelSidePanelOuter1Col.ResumeLayout(false);
			this.tablelayoutpanelSidePanelOuter1Col.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tablelayoutpanelSideBarInner2Col.ResumeLayout(false);
			this.tablelayoutpanelSideBarInner2Col.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lblMSColon;
		private System.Windows.Forms.TableLayoutPanel tablelayoutpanelSideBarInner2Col;
		private System.Windows.Forms.TableLayoutPanel tablelayoutpanelSidePanelOuter1Col;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem videoToSequenceMJPEGLosslessToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem maskToAlphaToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem framesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openImageSequenceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openVideoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.Label lblMColon;
		private System.Windows.Forms.Label lblSColon;
		private System.Windows.Forms.Label lblMinDigits;
		private System.Windows.Forms.NumericUpDown nudMinDigits;
		private System.Windows.Forms.Label lblHColon;
		private System.Windows.Forms.TextBox tbM;
		private System.Windows.Forms.TextBox tbS;
		private System.Windows.Forms.TextBox tbMs;
		private System.Windows.Forms.TextBox tbH;
		private System.Windows.Forms.TrackBar trackbarFrame;
		private System.Windows.Forms.ProgressBar progressbarMain;
		private FrameworkFastPanel panel1;
		private System.Windows.Forms.ComboBox comboInput;
		private System.Windows.Forms.CheckBox cbAutoscroll;
		private System.Windows.Forms.ListBox lbOut;
		private System.Windows.Forms.OpenFileDialog ofiledlg;
		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.TextBox tbStatus;
	}
}
