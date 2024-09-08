using System.Windows.Forms;

namespace EveOPreview.View.Implementation
{
	partial class MainForm
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

        /// <summary>s
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ToolStripMenuItem RestoreWindowMenuItem;
            ToolStripMenuItem ExitMenuItem;
            ToolStripMenuItem TitleMenuItem;
            ToolStripSeparator SeparatorMenuItem;
            TabControl ContentTabControl;
            TabPage GeneralTabPage;
            Panel GeneralSettingsPanel;
            TabPage ThumbnailTabPage;
            Panel ThumbnailSettingsPanel;
            Label HeigthLabel;
            Label WidthLabel;
            Label OpacityLabel;
            Panel ZoomSettingsPanel;
            Label ZoomFactorLabel;
            Label ZoomAnchorLabel;
            TabPage OverlayTabPage;
            Panel OverlaySettingsPanel;
            TabPage ClientsTabPage;
            Panel ClientsPanel;
            Label ThumbnailsListLabel;
            TabPage AboutTabPage;
            Panel AboutPanel;
            Label CreditMaintLabel;
            Label DocumentationLinkLabel;
            Label DescriptionLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Label NameLabel;
            Label FontSizeLabel;
            MinimizeInactiveClientsCheckBox = new CheckBox();
            EnableClientLayoutTrackingCheckBox = new CheckBox();
            HideActiveClientThumbnailCheckBox = new CheckBox();
            ShowThumbnailsAlwaysOnTopCheckBox = new CheckBox();
            HideThumbnailsOnLostFocusCheckBox = new CheckBox();
            EnablePerClientThumbnailsLayoutsCheckBox = new CheckBox();
            MinimizeToTrayCheckBox = new CheckBox();
            ThumbnailsWidthNumericEdit = new NumericUpDown();
            ThumbnailsHeightNumericEdit = new NumericUpDown();
            ThumbnailOpacityTrackBar = new TrackBar();
            ZoomTabPage = new TabPage();
            ZoomAnchorPanel = new Panel();
            ZoomAanchorNWRadioButton = new RadioButton();
            ZoomAanchorNRadioButton = new RadioButton();
            ZoomAanchorNERadioButton = new RadioButton();
            ZoomAanchorWRadioButton = new RadioButton();
            ZoomAanchorSERadioButton = new RadioButton();
            ZoomAanchorCRadioButton = new RadioButton();
            ZoomAanchorSRadioButton = new RadioButton();
            ZoomAanchorERadioButton = new RadioButton();
            ZoomAanchorSWRadioButton = new RadioButton();
            EnableThumbnailZoomCheckBox = new CheckBox();
            ThumbnailZoomFactorNumericEdit = new NumericUpDown();
            HighlightColorLabel = new Label();
            ActiveClientHighlightColorButton = new Panel();
            EnableActiveClientHighlightCheckBox = new CheckBox();
            ShowThumbnailOverlaysCheckBox = new CheckBox();
            ShowThumbnailFramesCheckBox = new CheckBox();
            ThumbnailsList = new CheckedListBox();
            VersionLabel = new Label();
            DocumentationLink = new LinkLabel();
            NotifyIcon = new NotifyIcon(components);
            TrayMenu = new ContextMenuStrip(components);
            ThumbnailsFontSizeNumericEdit = new NumericUpDown();
            RestoreWindowMenuItem = new ToolStripMenuItem();
            ExitMenuItem = new ToolStripMenuItem();
            TitleMenuItem = new ToolStripMenuItem();
            SeparatorMenuItem = new ToolStripSeparator();
            ContentTabControl = new TabControl();
            GeneralTabPage = new TabPage();
            GeneralSettingsPanel = new Panel();
            ThumbnailTabPage = new TabPage();
            ThumbnailSettingsPanel = new Panel();
            HeigthLabel = new Label();
            WidthLabel = new Label();
            OpacityLabel = new Label();
            ZoomSettingsPanel = new Panel();
            ZoomFactorLabel = new Label();
            ZoomAnchorLabel = new Label();
            OverlayTabPage = new TabPage();
            OverlaySettingsPanel = new Panel();
            ClientsTabPage = new TabPage();
            ClientsPanel = new Panel();
            ThumbnailsListLabel = new Label();
            AboutTabPage = new TabPage();
            AboutPanel = new Panel();
            CreditMaintLabel = new Label();
            DocumentationLinkLabel = new Label();
            DescriptionLabel = new Label();
            NameLabel = new Label();
            FontSizeLabel = new Label();
            ContentTabControl.SuspendLayout();
            GeneralTabPage.SuspendLayout();
            GeneralSettingsPanel.SuspendLayout();
            ThumbnailTabPage.SuspendLayout();
            ThumbnailSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThumbnailsWidthNumericEdit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThumbnailsHeightNumericEdit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThumbnailOpacityTrackBar).BeginInit();
            ZoomTabPage.SuspendLayout();
            ZoomSettingsPanel.SuspendLayout();
            ZoomAnchorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThumbnailZoomFactorNumericEdit).BeginInit();
            OverlayTabPage.SuspendLayout();
            OverlaySettingsPanel.SuspendLayout();
            ClientsTabPage.SuspendLayout();
            ClientsPanel.SuspendLayout();
            AboutTabPage.SuspendLayout();
            AboutPanel.SuspendLayout();
            TrayMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThumbnailsFontSizeNumericEdit).BeginInit();
            SuspendLayout();
            // 
            // RestoreWindowMenuItem
            // 
            RestoreWindowMenuItem.Name = "RestoreWindowMenuItem";
            RestoreWindowMenuItem.Size = new System.Drawing.Size(151, 22);
            RestoreWindowMenuItem.Text = "Restore";
            RestoreWindowMenuItem.Click += RestoreMainForm_Handler;
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new System.Drawing.Size(151, 22);
            ExitMenuItem.Text = "Exit";
            ExitMenuItem.Click += ExitMenuItemClick_Handler;
            // 
            // TitleMenuItem
            // 
            TitleMenuItem.Enabled = false;
            TitleMenuItem.Name = "TitleMenuItem";
            TitleMenuItem.Size = new System.Drawing.Size(151, 22);
            TitleMenuItem.Text = "EVE-O Preview";
            // 
            // SeparatorMenuItem
            // 
            SeparatorMenuItem.Name = "SeparatorMenuItem";
            SeparatorMenuItem.Size = new System.Drawing.Size(148, 6);
            // 
            // ContentTabControl
            // 
            ContentTabControl.Alignment = TabAlignment.Left;
            ContentTabControl.Controls.Add(GeneralTabPage);
            ContentTabControl.Controls.Add(ThumbnailTabPage);
            ContentTabControl.Controls.Add(ZoomTabPage);
            ContentTabControl.Controls.Add(OverlayTabPage);
            ContentTabControl.Controls.Add(ClientsTabPage);
            ContentTabControl.Controls.Add(AboutTabPage);
            ContentTabControl.Dock = DockStyle.Fill;
            ContentTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            ContentTabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ContentTabControl.ItemSize = new System.Drawing.Size(35, 120);
            ContentTabControl.Location = new System.Drawing.Point(0, 0);
            ContentTabControl.Margin = new Padding(4, 3, 4, 3);
            ContentTabControl.Multiline = true;
            ContentTabControl.Name = "ContentTabControl";
            ContentTabControl.SelectedIndex = 0;
            ContentTabControl.Size = new System.Drawing.Size(493, 289);
            ContentTabControl.SizeMode = TabSizeMode.Fixed;
            ContentTabControl.TabIndex = 6;
            ContentTabControl.DrawItem += ContentTabControl_DrawItem;
            // 
            // GeneralTabPage
            // 
            GeneralTabPage.BackColor = System.Drawing.SystemColors.Control;
            GeneralTabPage.Controls.Add(GeneralSettingsPanel);
            GeneralTabPage.Location = new System.Drawing.Point(124, 4);
            GeneralTabPage.Margin = new Padding(4, 3, 4, 3);
            GeneralTabPage.Name = "GeneralTabPage";
            GeneralTabPage.Padding = new Padding(4, 3, 4, 3);
            GeneralTabPage.Size = new System.Drawing.Size(365, 281);
            GeneralTabPage.TabIndex = 0;
            GeneralTabPage.Text = "General";
            // 
            // GeneralSettingsPanel
            // 
            GeneralSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
            GeneralSettingsPanel.Controls.Add(MinimizeInactiveClientsCheckBox);
            GeneralSettingsPanel.Controls.Add(EnableClientLayoutTrackingCheckBox);
            GeneralSettingsPanel.Controls.Add(HideActiveClientThumbnailCheckBox);
            GeneralSettingsPanel.Controls.Add(ShowThumbnailsAlwaysOnTopCheckBox);
            GeneralSettingsPanel.Controls.Add(HideThumbnailsOnLostFocusCheckBox);
            GeneralSettingsPanel.Controls.Add(EnablePerClientThumbnailsLayoutsCheckBox);
            GeneralSettingsPanel.Controls.Add(MinimizeToTrayCheckBox);
            GeneralSettingsPanel.Dock = DockStyle.Fill;
            GeneralSettingsPanel.Location = new System.Drawing.Point(4, 3);
            GeneralSettingsPanel.Margin = new Padding(4, 3, 4, 3);
            GeneralSettingsPanel.Name = "GeneralSettingsPanel";
            GeneralSettingsPanel.Size = new System.Drawing.Size(357, 275);
            GeneralSettingsPanel.TabIndex = 18;
            // 
            // MinimizeInactiveClientsCheckBox
            // 
            MinimizeInactiveClientsCheckBox.AutoSize = true;
            MinimizeInactiveClientsCheckBox.Location = new System.Drawing.Point(9, 91);
            MinimizeInactiveClientsCheckBox.Margin = new Padding(4, 3, 4, 3);
            MinimizeInactiveClientsCheckBox.Name = "MinimizeInactiveClientsCheckBox";
            MinimizeInactiveClientsCheckBox.Size = new System.Drawing.Size(178, 19);
            MinimizeInactiveClientsCheckBox.TabIndex = 24;
            MinimizeInactiveClientsCheckBox.Text = "Minimize inactive EVE clients";
            MinimizeInactiveClientsCheckBox.UseVisualStyleBackColor = true;
            MinimizeInactiveClientsCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // EnableClientLayoutTrackingCheckBox
            // 
            EnableClientLayoutTrackingCheckBox.AutoSize = true;
            EnableClientLayoutTrackingCheckBox.Location = new System.Drawing.Point(9, 36);
            EnableClientLayoutTrackingCheckBox.Margin = new Padding(4, 3, 4, 3);
            EnableClientLayoutTrackingCheckBox.Name = "EnableClientLayoutTrackingCheckBox";
            EnableClientLayoutTrackingCheckBox.Size = new System.Drawing.Size(136, 19);
            EnableClientLayoutTrackingCheckBox.TabIndex = 19;
            EnableClientLayoutTrackingCheckBox.Text = "Track client locations";
            EnableClientLayoutTrackingCheckBox.UseVisualStyleBackColor = true;
            EnableClientLayoutTrackingCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // HideActiveClientThumbnailCheckBox
            // 
            HideActiveClientThumbnailCheckBox.AutoSize = true;
            HideActiveClientThumbnailCheckBox.Checked = true;
            HideActiveClientThumbnailCheckBox.CheckState = CheckState.Checked;
            HideActiveClientThumbnailCheckBox.Location = new System.Drawing.Point(9, 63);
            HideActiveClientThumbnailCheckBox.Margin = new Padding(4, 3, 4, 3);
            HideActiveClientThumbnailCheckBox.Name = "HideActiveClientThumbnailCheckBox";
            HideActiveClientThumbnailCheckBox.Size = new System.Drawing.Size(197, 19);
            HideActiveClientThumbnailCheckBox.TabIndex = 20;
            HideActiveClientThumbnailCheckBox.Text = "Hide preview of active EVE client";
            HideActiveClientThumbnailCheckBox.UseVisualStyleBackColor = true;
            HideActiveClientThumbnailCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // ShowThumbnailsAlwaysOnTopCheckBox
            // 
            ShowThumbnailsAlwaysOnTopCheckBox.AutoSize = true;
            ShowThumbnailsAlwaysOnTopCheckBox.Checked = true;
            ShowThumbnailsAlwaysOnTopCheckBox.CheckState = CheckState.Checked;
            ShowThumbnailsAlwaysOnTopCheckBox.Location = new System.Drawing.Point(9, 119);
            ShowThumbnailsAlwaysOnTopCheckBox.Margin = new Padding(4, 3, 4, 3);
            ShowThumbnailsAlwaysOnTopCheckBox.Name = "ShowThumbnailsAlwaysOnTopCheckBox";
            ShowThumbnailsAlwaysOnTopCheckBox.RightToLeft = RightToLeft.No;
            ShowThumbnailsAlwaysOnTopCheckBox.Size = new System.Drawing.Size(148, 19);
            ShowThumbnailsAlwaysOnTopCheckBox.TabIndex = 21;
            ShowThumbnailsAlwaysOnTopCheckBox.Text = "Previews always on top";
            ShowThumbnailsAlwaysOnTopCheckBox.UseVisualStyleBackColor = true;
            ShowThumbnailsAlwaysOnTopCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // HideThumbnailsOnLostFocusCheckBox
            // 
            HideThumbnailsOnLostFocusCheckBox.AutoSize = true;
            HideThumbnailsOnLostFocusCheckBox.Checked = true;
            HideThumbnailsOnLostFocusCheckBox.CheckState = CheckState.Checked;
            HideThumbnailsOnLostFocusCheckBox.Location = new System.Drawing.Point(9, 147);
            HideThumbnailsOnLostFocusCheckBox.Margin = new Padding(4, 3, 4, 3);
            HideThumbnailsOnLostFocusCheckBox.Name = "HideThumbnailsOnLostFocusCheckBox";
            HideThumbnailsOnLostFocusCheckBox.Size = new System.Drawing.Size(252, 19);
            HideThumbnailsOnLostFocusCheckBox.TabIndex = 22;
            HideThumbnailsOnLostFocusCheckBox.Text = "Hide previews when EVE client is not active";
            HideThumbnailsOnLostFocusCheckBox.UseVisualStyleBackColor = true;
            HideThumbnailsOnLostFocusCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // EnablePerClientThumbnailsLayoutsCheckBox
            // 
            EnablePerClientThumbnailsLayoutsCheckBox.AutoSize = true;
            EnablePerClientThumbnailsLayoutsCheckBox.Checked = true;
            EnablePerClientThumbnailsLayoutsCheckBox.CheckState = CheckState.Checked;
            EnablePerClientThumbnailsLayoutsCheckBox.Location = new System.Drawing.Point(9, 174);
            EnablePerClientThumbnailsLayoutsCheckBox.Margin = new Padding(4, 3, 4, 3);
            EnablePerClientThumbnailsLayoutsCheckBox.Name = "EnablePerClientThumbnailsLayoutsCheckBox";
            EnablePerClientThumbnailsLayoutsCheckBox.Size = new System.Drawing.Size(200, 19);
            EnablePerClientThumbnailsLayoutsCheckBox.TabIndex = 23;
            EnablePerClientThumbnailsLayoutsCheckBox.Text = "Unique layout for each EVE client";
            EnablePerClientThumbnailsLayoutsCheckBox.UseVisualStyleBackColor = true;
            EnablePerClientThumbnailsLayoutsCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // MinimizeToTrayCheckBox
            // 
            MinimizeToTrayCheckBox.AutoSize = true;
            MinimizeToTrayCheckBox.Location = new System.Drawing.Point(9, 8);
            MinimizeToTrayCheckBox.Margin = new Padding(4, 3, 4, 3);
            MinimizeToTrayCheckBox.Name = "MinimizeToTrayCheckBox";
            MinimizeToTrayCheckBox.Size = new System.Drawing.Size(154, 19);
            MinimizeToTrayCheckBox.TabIndex = 18;
            MinimizeToTrayCheckBox.Text = "Minimize to System Tray";
            MinimizeToTrayCheckBox.UseVisualStyleBackColor = true;
            MinimizeToTrayCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // ThumbnailTabPage
            // 
            ThumbnailTabPage.BackColor = System.Drawing.SystemColors.Control;
            ThumbnailTabPage.Controls.Add(ThumbnailSettingsPanel);
            ThumbnailTabPage.Location = new System.Drawing.Point(124, 4);
            ThumbnailTabPage.Margin = new Padding(4, 3, 4, 3);
            ThumbnailTabPage.Name = "ThumbnailTabPage";
            ThumbnailTabPage.Padding = new Padding(4, 3, 4, 3);
            ThumbnailTabPage.Size = new System.Drawing.Size(365, 281);
            ThumbnailTabPage.TabIndex = 1;
            ThumbnailTabPage.Text = "Thumbnail";
            // 
            // ThumbnailSettingsPanel
            // 
            ThumbnailSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
            ThumbnailSettingsPanel.Controls.Add(FontSizeLabel);
            ThumbnailSettingsPanel.Controls.Add(ThumbnailsFontSizeNumericEdit);
            ThumbnailSettingsPanel.Controls.Add(HeigthLabel);
            ThumbnailSettingsPanel.Controls.Add(WidthLabel);
            ThumbnailSettingsPanel.Controls.Add(ThumbnailsWidthNumericEdit);
            ThumbnailSettingsPanel.Controls.Add(ThumbnailsHeightNumericEdit);
            ThumbnailSettingsPanel.Controls.Add(ThumbnailOpacityTrackBar);
            ThumbnailSettingsPanel.Controls.Add(OpacityLabel);
            ThumbnailSettingsPanel.Dock = DockStyle.Fill;
            ThumbnailSettingsPanel.Location = new System.Drawing.Point(4, 3);
            ThumbnailSettingsPanel.Margin = new Padding(4, 3, 4, 3);
            ThumbnailSettingsPanel.Name = "ThumbnailSettingsPanel";
            ThumbnailSettingsPanel.Size = new System.Drawing.Size(357, 275);
            ThumbnailSettingsPanel.TabIndex = 19;
            // 
            // HeigthLabel
            // 
            HeigthLabel.AutoSize = true;
            HeigthLabel.Location = new System.Drawing.Point(9, 66);
            HeigthLabel.Margin = new Padding(4, 0, 4, 0);
            HeigthLabel.Name = "HeigthLabel";
            HeigthLabel.Size = new System.Drawing.Size(103, 15);
            HeigthLabel.TabIndex = 24;
            HeigthLabel.Text = "Thumbnail Heigth";
            // 
            // WidthLabel
            // 
            WidthLabel.AutoSize = true;
            WidthLabel.Location = new System.Drawing.Point(9, 38);
            WidthLabel.Margin = new Padding(4, 0, 4, 0);
            WidthLabel.Name = "WidthLabel";
            WidthLabel.Size = new System.Drawing.Size(99, 15);
            WidthLabel.TabIndex = 23;
            WidthLabel.Text = "Thumbnail Width";
            // 
            // ThumbnailsWidthNumericEdit
            // 
            ThumbnailsWidthNumericEdit.BackColor = System.Drawing.SystemColors.Window;
            ThumbnailsWidthNumericEdit.BorderStyle = BorderStyle.FixedSingle;
            ThumbnailsWidthNumericEdit.CausesValidation = false;
            ThumbnailsWidthNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ThumbnailsWidthNumericEdit.Location = new System.Drawing.Point(137, 36);
            ThumbnailsWidthNumericEdit.Margin = new Padding(9, 3, 9, 3);
            ThumbnailsWidthNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            ThumbnailsWidthNumericEdit.Name = "ThumbnailsWidthNumericEdit";
            ThumbnailsWidthNumericEdit.Size = new System.Drawing.Size(121, 23);
            ThumbnailsWidthNumericEdit.TabIndex = 21;
            ThumbnailsWidthNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
            ThumbnailsWidthNumericEdit.ValueChanged += ThumbnailSizeChanged_Handler;
            // 
            // ThumbnailsHeightNumericEdit
            // 
            ThumbnailsHeightNumericEdit.BackColor = System.Drawing.SystemColors.Window;
            ThumbnailsHeightNumericEdit.BorderStyle = BorderStyle.FixedSingle;
            ThumbnailsHeightNumericEdit.CausesValidation = false;
            ThumbnailsHeightNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ThumbnailsHeightNumericEdit.Location = new System.Drawing.Point(137, 66);
            ThumbnailsHeightNumericEdit.Margin = new Padding(9, 3, 9, 3);
            ThumbnailsHeightNumericEdit.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            ThumbnailsHeightNumericEdit.Name = "ThumbnailsHeightNumericEdit";
            ThumbnailsHeightNumericEdit.Size = new System.Drawing.Size(121, 23);
            ThumbnailsHeightNumericEdit.TabIndex = 22;
            ThumbnailsHeightNumericEdit.Value = new decimal(new int[] { 70, 0, 0, 0 });
            ThumbnailsHeightNumericEdit.ValueChanged += ThumbnailSizeChanged_Handler;
            // 
            // ThumbnailOpacityTrackBar
            // 
            ThumbnailOpacityTrackBar.AutoSize = false;
            ThumbnailOpacityTrackBar.LargeChange = 10;
            ThumbnailOpacityTrackBar.Location = new System.Drawing.Point(71, 7);
            ThumbnailOpacityTrackBar.Margin = new Padding(4, 3, 4, 3);
            ThumbnailOpacityTrackBar.Maximum = 100;
            ThumbnailOpacityTrackBar.Minimum = 20;
            ThumbnailOpacityTrackBar.Name = "ThumbnailOpacityTrackBar";
            ThumbnailOpacityTrackBar.Size = new System.Drawing.Size(223, 25);
            ThumbnailOpacityTrackBar.TabIndex = 20;
            ThumbnailOpacityTrackBar.TickFrequency = 10;
            ThumbnailOpacityTrackBar.Value = 20;
            ThumbnailOpacityTrackBar.ValueChanged += OptionChanged_Handler;
            // 
            // OpacityLabel
            // 
            OpacityLabel.AutoSize = true;
            OpacityLabel.Location = new System.Drawing.Point(9, 10);
            OpacityLabel.Margin = new Padding(4, 0, 4, 0);
            OpacityLabel.Name = "OpacityLabel";
            OpacityLabel.Size = new System.Drawing.Size(48, 15);
            OpacityLabel.TabIndex = 19;
            OpacityLabel.Text = "Opacity";
            // 
            // ZoomTabPage
            // 
            ZoomTabPage.BackColor = System.Drawing.SystemColors.Control;
            ZoomTabPage.Controls.Add(ZoomSettingsPanel);
            ZoomTabPage.Location = new System.Drawing.Point(124, 4);
            ZoomTabPage.Margin = new Padding(4, 3, 4, 3);
            ZoomTabPage.Name = "ZoomTabPage";
            ZoomTabPage.Size = new System.Drawing.Size(365, 281);
            ZoomTabPage.TabIndex = 2;
            ZoomTabPage.Text = "Zoom";
            // 
            // ZoomSettingsPanel
            // 
            ZoomSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
            ZoomSettingsPanel.Controls.Add(ZoomFactorLabel);
            ZoomSettingsPanel.Controls.Add(ZoomAnchorPanel);
            ZoomSettingsPanel.Controls.Add(ZoomAnchorLabel);
            ZoomSettingsPanel.Controls.Add(EnableThumbnailZoomCheckBox);
            ZoomSettingsPanel.Controls.Add(ThumbnailZoomFactorNumericEdit);
            ZoomSettingsPanel.Dock = DockStyle.Fill;
            ZoomSettingsPanel.Location = new System.Drawing.Point(0, 0);
            ZoomSettingsPanel.Margin = new Padding(4, 3, 4, 3);
            ZoomSettingsPanel.Name = "ZoomSettingsPanel";
            ZoomSettingsPanel.Size = new System.Drawing.Size(365, 281);
            ZoomSettingsPanel.TabIndex = 36;
            // 
            // ZoomFactorLabel
            // 
            ZoomFactorLabel.AutoSize = true;
            ZoomFactorLabel.Location = new System.Drawing.Point(9, 38);
            ZoomFactorLabel.Margin = new Padding(4, 0, 4, 0);
            ZoomFactorLabel.Name = "ZoomFactorLabel";
            ZoomFactorLabel.Size = new System.Drawing.Size(75, 15);
            ZoomFactorLabel.TabIndex = 39;
            ZoomFactorLabel.Text = "Zoom Factor";
            // 
            // ZoomAnchorPanel
            // 
            ZoomAnchorPanel.BorderStyle = BorderStyle.FixedSingle;
            ZoomAnchorPanel.Controls.Add(ZoomAanchorNWRadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorNRadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorNERadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorWRadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorSERadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorCRadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorSRadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorERadioButton);
            ZoomAnchorPanel.Controls.Add(ZoomAanchorSWRadioButton);
            ZoomAnchorPanel.Location = new System.Drawing.Point(27, 89);
            ZoomAnchorPanel.Margin = new Padding(4, 3, 4, 3);
            ZoomAnchorPanel.Name = "ZoomAnchorPanel";
            ZoomAnchorPanel.Size = new System.Drawing.Size(90, 84);
            ZoomAnchorPanel.TabIndex = 38;
            // 
            // ZoomAanchorNWRadioButton
            // 
            ZoomAanchorNWRadioButton.AutoSize = true;
            ZoomAanchorNWRadioButton.Location = new System.Drawing.Point(4, 3);
            ZoomAanchorNWRadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorNWRadioButton.Name = "ZoomAanchorNWRadioButton";
            ZoomAanchorNWRadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorNWRadioButton.TabIndex = 0;
            ZoomAanchorNWRadioButton.TabStop = true;
            ZoomAanchorNWRadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorNWRadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorNRadioButton
            // 
            ZoomAanchorNRadioButton.AutoSize = true;
            ZoomAanchorNRadioButton.Location = new System.Drawing.Point(36, 3);
            ZoomAanchorNRadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorNRadioButton.Name = "ZoomAanchorNRadioButton";
            ZoomAanchorNRadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorNRadioButton.TabIndex = 1;
            ZoomAanchorNRadioButton.TabStop = true;
            ZoomAanchorNRadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorNRadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorNERadioButton
            // 
            ZoomAanchorNERadioButton.AutoSize = true;
            ZoomAanchorNERadioButton.Location = new System.Drawing.Point(69, 3);
            ZoomAanchorNERadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorNERadioButton.Name = "ZoomAanchorNERadioButton";
            ZoomAanchorNERadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorNERadioButton.TabIndex = 2;
            ZoomAanchorNERadioButton.TabStop = true;
            ZoomAanchorNERadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorNERadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorWRadioButton
            // 
            ZoomAanchorWRadioButton.AutoSize = true;
            ZoomAanchorWRadioButton.Location = new System.Drawing.Point(4, 33);
            ZoomAanchorWRadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorWRadioButton.Name = "ZoomAanchorWRadioButton";
            ZoomAanchorWRadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorWRadioButton.TabIndex = 3;
            ZoomAanchorWRadioButton.TabStop = true;
            ZoomAanchorWRadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorWRadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorSERadioButton
            // 
            ZoomAanchorSERadioButton.AutoSize = true;
            ZoomAanchorSERadioButton.Location = new System.Drawing.Point(69, 63);
            ZoomAanchorSERadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorSERadioButton.Name = "ZoomAanchorSERadioButton";
            ZoomAanchorSERadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorSERadioButton.TabIndex = 8;
            ZoomAanchorSERadioButton.TabStop = true;
            ZoomAanchorSERadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorSERadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorCRadioButton
            // 
            ZoomAanchorCRadioButton.AutoSize = true;
            ZoomAanchorCRadioButton.Location = new System.Drawing.Point(36, 33);
            ZoomAanchorCRadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorCRadioButton.Name = "ZoomAanchorCRadioButton";
            ZoomAanchorCRadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorCRadioButton.TabIndex = 4;
            ZoomAanchorCRadioButton.TabStop = true;
            ZoomAanchorCRadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorCRadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorSRadioButton
            // 
            ZoomAanchorSRadioButton.AutoSize = true;
            ZoomAanchorSRadioButton.Location = new System.Drawing.Point(36, 63);
            ZoomAanchorSRadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorSRadioButton.Name = "ZoomAanchorSRadioButton";
            ZoomAanchorSRadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorSRadioButton.TabIndex = 7;
            ZoomAanchorSRadioButton.TabStop = true;
            ZoomAanchorSRadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorSRadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorERadioButton
            // 
            ZoomAanchorERadioButton.AutoSize = true;
            ZoomAanchorERadioButton.Location = new System.Drawing.Point(69, 33);
            ZoomAanchorERadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorERadioButton.Name = "ZoomAanchorERadioButton";
            ZoomAanchorERadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorERadioButton.TabIndex = 5;
            ZoomAanchorERadioButton.TabStop = true;
            ZoomAanchorERadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorERadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAanchorSWRadioButton
            // 
            ZoomAanchorSWRadioButton.AutoSize = true;
            ZoomAanchorSWRadioButton.Location = new System.Drawing.Point(4, 63);
            ZoomAanchorSWRadioButton.Margin = new Padding(4, 3, 4, 3);
            ZoomAanchorSWRadioButton.Name = "ZoomAanchorSWRadioButton";
            ZoomAanchorSWRadioButton.Size = new System.Drawing.Size(14, 13);
            ZoomAanchorSWRadioButton.TabIndex = 6;
            ZoomAanchorSWRadioButton.TabStop = true;
            ZoomAanchorSWRadioButton.UseVisualStyleBackColor = true;
            ZoomAanchorSWRadioButton.CheckedChanged += OptionChanged_Handler;
            // 
            // ZoomAnchorLabel
            // 
            ZoomAnchorLabel.AutoSize = true;
            ZoomAnchorLabel.Location = new System.Drawing.Point(9, 66);
            ZoomAnchorLabel.Margin = new Padding(4, 0, 4, 0);
            ZoomAnchorLabel.Name = "ZoomAnchorLabel";
            ZoomAnchorLabel.Size = new System.Drawing.Size(46, 15);
            ZoomAnchorLabel.TabIndex = 40;
            ZoomAnchorLabel.Text = "ZoomAnchor";
            // 
            // EnableThumbnailZoomCheckBox
            // 
            EnableThumbnailZoomCheckBox.AutoSize = true;
            EnableThumbnailZoomCheckBox.Checked = true;
            EnableThumbnailZoomCheckBox.CheckState = CheckState.Checked;
            EnableThumbnailZoomCheckBox.Location = new System.Drawing.Point(9, 8);
            EnableThumbnailZoomCheckBox.Margin = new Padding(4, 3, 4, 3);
            EnableThumbnailZoomCheckBox.Name = "EnableThumbnailZoomCheckBox";
            EnableThumbnailZoomCheckBox.RightToLeft = RightToLeft.No;
            EnableThumbnailZoomCheckBox.Size = new System.Drawing.Size(108, 19);
            EnableThumbnailZoomCheckBox.TabIndex = 36;
            EnableThumbnailZoomCheckBox.Text = "Zoom on hover";
            EnableThumbnailZoomCheckBox.UseVisualStyleBackColor = true;
            EnableThumbnailZoomCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // ThumbnailZoomFactorNumericEdit
            // 
            ThumbnailZoomFactorNumericEdit.BackColor = System.Drawing.SystemColors.Window;
            ThumbnailZoomFactorNumericEdit.BorderStyle = BorderStyle.FixedSingle;
            ThumbnailZoomFactorNumericEdit.Location = new System.Drawing.Point(97, 36);
            ThumbnailZoomFactorNumericEdit.Margin = new Padding(9, 3, 9, 3);
            ThumbnailZoomFactorNumericEdit.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            ThumbnailZoomFactorNumericEdit.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            ThumbnailZoomFactorNumericEdit.Name = "ThumbnailZoomFactorNumericEdit";
            ThumbnailZoomFactorNumericEdit.Size = new System.Drawing.Size(93, 23);
            ThumbnailZoomFactorNumericEdit.TabIndex = 37;
            ThumbnailZoomFactorNumericEdit.Value = new decimal(new int[] { 2, 0, 0, 0 });
            ThumbnailZoomFactorNumericEdit.ValueChanged += OptionChanged_Handler;
            // 
            // OverlayTabPage
            // 
            OverlayTabPage.BackColor = System.Drawing.SystemColors.Control;
            OverlayTabPage.Controls.Add(OverlaySettingsPanel);
            OverlayTabPage.Location = new System.Drawing.Point(124, 4);
            OverlayTabPage.Margin = new Padding(4, 3, 4, 3);
            OverlayTabPage.Name = "OverlayTabPage";
            OverlayTabPage.Size = new System.Drawing.Size(365, 281);
            OverlayTabPage.TabIndex = 3;
            OverlayTabPage.Text = "Overlay";
            // 
            // OverlaySettingsPanel
            // 
            OverlaySettingsPanel.BorderStyle = BorderStyle.FixedSingle;
            OverlaySettingsPanel.Controls.Add(HighlightColorLabel);
            OverlaySettingsPanel.Controls.Add(ActiveClientHighlightColorButton);
            OverlaySettingsPanel.Controls.Add(EnableActiveClientHighlightCheckBox);
            OverlaySettingsPanel.Controls.Add(ShowThumbnailOverlaysCheckBox);
            OverlaySettingsPanel.Controls.Add(ShowThumbnailFramesCheckBox);
            OverlaySettingsPanel.Dock = DockStyle.Fill;
            OverlaySettingsPanel.Location = new System.Drawing.Point(0, 0);
            OverlaySettingsPanel.Margin = new Padding(4, 3, 4, 3);
            OverlaySettingsPanel.Name = "OverlaySettingsPanel";
            OverlaySettingsPanel.Size = new System.Drawing.Size(365, 281);
            OverlaySettingsPanel.TabIndex = 25;
            // 
            // HighlightColorLabel
            // 
            HighlightColorLabel.AutoSize = true;
            HighlightColorLabel.Location = new System.Drawing.Point(6, 90);
            HighlightColorLabel.Margin = new Padding(4, 0, 4, 0);
            HighlightColorLabel.Name = "HighlightColorLabel";
            HighlightColorLabel.Size = new System.Drawing.Size(36, 15);
            HighlightColorLabel.TabIndex = 29;
            HighlightColorLabel.Text = "Color";
            // 
            // ActiveClientHighlightColorButton
            // 
            ActiveClientHighlightColorButton.BorderStyle = BorderStyle.FixedSingle;
            ActiveClientHighlightColorButton.Location = new System.Drawing.Point(49, 89);
            ActiveClientHighlightColorButton.Margin = new Padding(4, 3, 4, 3);
            ActiveClientHighlightColorButton.Name = "ActiveClientHighlightColorButton";
            ActiveClientHighlightColorButton.Size = new System.Drawing.Size(108, 19);
            ActiveClientHighlightColorButton.TabIndex = 28;
            ActiveClientHighlightColorButton.Click += ActiveClientHighlightColorButton_Click;
            // 
            // EnableActiveClientHighlightCheckBox
            // 
            EnableActiveClientHighlightCheckBox.AutoSize = true;
            EnableActiveClientHighlightCheckBox.Checked = true;
            EnableActiveClientHighlightCheckBox.CheckState = CheckState.Checked;
            EnableActiveClientHighlightCheckBox.Location = new System.Drawing.Point(9, 63);
            EnableActiveClientHighlightCheckBox.Margin = new Padding(4, 3, 4, 3);
            EnableActiveClientHighlightCheckBox.Name = "EnableActiveClientHighlightCheckBox";
            EnableActiveClientHighlightCheckBox.RightToLeft = RightToLeft.No;
            EnableActiveClientHighlightCheckBox.Size = new System.Drawing.Size(142, 19);
            EnableActiveClientHighlightCheckBox.TabIndex = 27;
            EnableActiveClientHighlightCheckBox.Text = "Highlight active client";
            EnableActiveClientHighlightCheckBox.UseVisualStyleBackColor = true;
            EnableActiveClientHighlightCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // ShowThumbnailOverlaysCheckBox
            // 
            ShowThumbnailOverlaysCheckBox.AutoSize = true;
            ShowThumbnailOverlaysCheckBox.Checked = true;
            ShowThumbnailOverlaysCheckBox.CheckState = CheckState.Checked;
            ShowThumbnailOverlaysCheckBox.Location = new System.Drawing.Point(9, 8);
            ShowThumbnailOverlaysCheckBox.Margin = new Padding(4, 3, 4, 3);
            ShowThumbnailOverlaysCheckBox.Name = "ShowThumbnailOverlaysCheckBox";
            ShowThumbnailOverlaysCheckBox.RightToLeft = RightToLeft.No;
            ShowThumbnailOverlaysCheckBox.Size = new System.Drawing.Size(96, 19);
            ShowThumbnailOverlaysCheckBox.TabIndex = 25;
            ShowThumbnailOverlaysCheckBox.Text = "Show overlay";
            ShowThumbnailOverlaysCheckBox.UseVisualStyleBackColor = true;
            ShowThumbnailOverlaysCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // ShowThumbnailFramesCheckBox
            // 
            ShowThumbnailFramesCheckBox.AutoSize = true;
            ShowThumbnailFramesCheckBox.Checked = true;
            ShowThumbnailFramesCheckBox.CheckState = CheckState.Checked;
            ShowThumbnailFramesCheckBox.Location = new System.Drawing.Point(9, 36);
            ShowThumbnailFramesCheckBox.Margin = new Padding(4, 3, 4, 3);
            ShowThumbnailFramesCheckBox.Name = "ShowThumbnailFramesCheckBox";
            ShowThumbnailFramesCheckBox.RightToLeft = RightToLeft.No;
            ShowThumbnailFramesCheckBox.Size = new System.Drawing.Size(94, 19);
            ShowThumbnailFramesCheckBox.TabIndex = 26;
            ShowThumbnailFramesCheckBox.Text = "Show frames";
            ShowThumbnailFramesCheckBox.UseVisualStyleBackColor = true;
            ShowThumbnailFramesCheckBox.CheckedChanged += OptionChanged_Handler;
            // 
            // ClientsTabPage
            // 
            ClientsTabPage.BackColor = System.Drawing.SystemColors.Control;
            ClientsTabPage.Controls.Add(ClientsPanel);
            ClientsTabPage.Location = new System.Drawing.Point(124, 4);
            ClientsTabPage.Margin = new Padding(4, 3, 4, 3);
            ClientsTabPage.Name = "ClientsTabPage";
            ClientsTabPage.Size = new System.Drawing.Size(365, 281);
            ClientsTabPage.TabIndex = 4;
            ClientsTabPage.Text = "Active Clients";
            // 
            // ClientsPanel
            // 
            ClientsPanel.BorderStyle = BorderStyle.FixedSingle;
            ClientsPanel.Controls.Add(ThumbnailsList);
            ClientsPanel.Controls.Add(ThumbnailsListLabel);
            ClientsPanel.Dock = DockStyle.Fill;
            ClientsPanel.Location = new System.Drawing.Point(0, 0);
            ClientsPanel.Margin = new Padding(4, 3, 4, 3);
            ClientsPanel.Name = "ClientsPanel";
            ClientsPanel.Size = new System.Drawing.Size(365, 281);
            ClientsPanel.TabIndex = 32;
            // 
            // ThumbnailsList
            // 
            ThumbnailsList.BackColor = System.Drawing.SystemColors.Window;
            ThumbnailsList.BorderStyle = BorderStyle.FixedSingle;
            ThumbnailsList.CheckOnClick = true;
            ThumbnailsList.Dock = DockStyle.Bottom;
            ThumbnailsList.FormattingEnabled = true;
            ThumbnailsList.IntegralHeight = false;
            ThumbnailsList.Location = new System.Drawing.Point(0, 45);
            ThumbnailsList.Margin = new Padding(4, 3, 4, 3);
            ThumbnailsList.Name = "ThumbnailsList";
            ThumbnailsList.Size = new System.Drawing.Size(363, 234);
            ThumbnailsList.TabIndex = 34;
            ThumbnailsList.ItemCheck += ThumbnailsList_ItemCheck_Handler;
            // 
            // ThumbnailsListLabel
            // 
            ThumbnailsListLabel.AutoSize = true;
            ThumbnailsListLabel.Location = new System.Drawing.Point(9, 10);
            ThumbnailsListLabel.Margin = new Padding(4, 0, 4, 0);
            ThumbnailsListLabel.Name = "ThumbnailsListLabel";
            ThumbnailsListLabel.Size = new System.Drawing.Size(181, 15);
            ThumbnailsListLabel.TabIndex = 33;
            ThumbnailsListLabel.Text = "Thumbnails (check to force hide)";
            // 
            // AboutTabPage
            // 
            AboutTabPage.BackColor = System.Drawing.SystemColors.Control;
            AboutTabPage.Controls.Add(AboutPanel);
            AboutTabPage.Location = new System.Drawing.Point(124, 4);
            AboutTabPage.Margin = new Padding(4, 3, 4, 3);
            AboutTabPage.Name = "AboutTabPage";
            AboutTabPage.Size = new System.Drawing.Size(365, 281);
            AboutTabPage.TabIndex = 5;
            AboutTabPage.Text = "About";
            // 
            // AboutPanel
            // 
            AboutPanel.BackColor = System.Drawing.Color.Transparent;
            AboutPanel.BorderStyle = BorderStyle.FixedSingle;
            AboutPanel.Controls.Add(CreditMaintLabel);
            AboutPanel.Controls.Add(DocumentationLinkLabel);
            AboutPanel.Controls.Add(DescriptionLabel);
            AboutPanel.Controls.Add(VersionLabel);
            AboutPanel.Controls.Add(NameLabel);
            AboutPanel.Controls.Add(DocumentationLink);
            AboutPanel.Dock = DockStyle.Fill;
            AboutPanel.Location = new System.Drawing.Point(0, 0);
            AboutPanel.Margin = new Padding(4, 3, 4, 3);
            AboutPanel.Name = "AboutPanel";
            AboutPanel.Size = new System.Drawing.Size(365, 281);
            AboutPanel.TabIndex = 2;
            // 
            // CreditMaintLabel
            // 
            CreditMaintLabel.AutoSize = true;
            CreditMaintLabel.Location = new System.Drawing.Point(0, 165);
            CreditMaintLabel.Margin = new Padding(4, 0, 4, 0);
            CreditMaintLabel.Name = "CreditMaintLabel";
            CreditMaintLabel.Padding = new Padding(9, 3, 9, 3);
            CreditMaintLabel.Size = new System.Drawing.Size(291, 21);
            CreditMaintLabel.TabIndex = 7;
            CreditMaintLabel.Text = "Credit to previous maintainer: Phrynohyas Tig-Rah";
            // 
            // DocumentationLinkLabel
            // 
            DocumentationLinkLabel.AutoSize = true;
            DocumentationLinkLabel.Location = new System.Drawing.Point(0, 188);
            DocumentationLinkLabel.Margin = new Padding(4, 0, 4, 0);
            DocumentationLinkLabel.Name = "DocumentationLinkLabel";
            DocumentationLinkLabel.Padding = new Padding(9, 3, 9, 3);
            DocumentationLinkLabel.Size = new System.Drawing.Size(259, 21);
            DocumentationLinkLabel.TabIndex = 6;
            DocumentationLinkLabel.Text = "For more information visit the forum thread:";
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.BackColor = System.Drawing.Color.Transparent;
            DescriptionLabel.Location = new System.Drawing.Point(0, 33);
            DescriptionLabel.Margin = new Padding(4, 0, 4, 0);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Padding = new Padding(9, 3, 9, 3);
            DescriptionLabel.Size = new System.Drawing.Size(304, 176);
            DescriptionLabel.TabIndex = 5;
            DescriptionLabel.Text = resources.GetString("DescriptionLabel.Text");
            // 
            // VersionLabel
            // 
            VersionLabel.AutoSize = true;
            VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            VersionLabel.Location = new System.Drawing.Point(155, 10);
            VersionLabel.Margin = new Padding(4, 0, 4, 0);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new System.Drawing.Size(49, 20);
            VersionLabel.TabIndex = 4;
            VersionLabel.Text = "1.0.0";
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            NameLabel.Location = new System.Drawing.Point(5, 10);
            NameLabel.Margin = new Padding(4, 0, 4, 0);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new System.Drawing.Size(130, 20);
            NameLabel.TabIndex = 3;
            NameLabel.Text = "EVE-O Preview";
            // 
            // DocumentationLink
            // 
            DocumentationLink.Location = new System.Drawing.Point(0, 212);
            DocumentationLink.Margin = new Padding(35, 3, 4, 3);
            DocumentationLink.Name = "DocumentationLink";
            DocumentationLink.Padding = new Padding(9, 3, 9, 3);
            DocumentationLink.Size = new System.Drawing.Size(355, 60);
            DocumentationLink.TabIndex = 2;
            DocumentationLink.TabStop = true;
            DocumentationLink.Text = "to be set from prresenter to be set from prresenter to be set from prresenter to be set from prresenter";
            DocumentationLink.LinkClicked += DocumentationLinkClicked_Handler;
            // 
            // NotifyIcon
            // 
            NotifyIcon.ContextMenuStrip = TrayMenu;
            NotifyIcon.Icon = (System.Drawing.Icon)resources.GetObject("NotifyIcon.Icon");
            NotifyIcon.Text = "EVE-O Preview";
            NotifyIcon.Visible = true;
            NotifyIcon.MouseDoubleClick += RestoreMainForm_Handler;
            // 
            // TrayMenu
            // 
            TrayMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            TrayMenu.Items.AddRange(new ToolStripItem[] { TitleMenuItem, RestoreWindowMenuItem, SeparatorMenuItem, ExitMenuItem });
            TrayMenu.Name = "contextMenuStrip1";
            TrayMenu.Size = new System.Drawing.Size(152, 76);
            // 
            // FontSizeLabel
            // 
            FontSizeLabel.AutoSize = true;
            FontSizeLabel.Location = new System.Drawing.Point(9, 97);
            FontSizeLabel.Margin = new Padding(4, 0, 4, 0);
            FontSizeLabel.Name = "FontSizeLabel";
            FontSizeLabel.Size = new System.Drawing.Size(54, 15);
            FontSizeLabel.TabIndex = 26;
            FontSizeLabel.Text = "Font Size";
            // 
            // ThumbnailsFontSizeNumericEdit
            // 
            ThumbnailsFontSizeNumericEdit.BackColor = System.Drawing.SystemColors.Window;
            ThumbnailsFontSizeNumericEdit.BorderStyle = BorderStyle.FixedSingle;
            ThumbnailsFontSizeNumericEdit.CausesValidation = false;
            ThumbnailsFontSizeNumericEdit.Location = new System.Drawing.Point(137, 97);
            ThumbnailsFontSizeNumericEdit.Margin = new Padding(9, 3, 9, 3);
            ThumbnailsFontSizeNumericEdit.Maximum = new decimal(new int[] { 72, 0, 0, 0 });
            ThumbnailsFontSizeNumericEdit.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            ThumbnailsFontSizeNumericEdit.Name = "ThumbnailsFontSizeNumericEdit";
            ThumbnailsFontSizeNumericEdit.Size = new System.Drawing.Size(121, 23);
            ThumbnailsFontSizeNumericEdit.TabIndex = 25;
            ThumbnailsFontSizeNumericEdit.Value = new decimal(new int[] { 8, 0, 0, 0 });
            ThumbnailsFontSizeNumericEdit.ValueChanged += OptionChanged_Handler;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(493, 289);
            Controls.Add(ContentTabControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(0);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "EVE-O Preview";
            TopMost = true;
            FormClosing += MainFormClosing_Handler;
            Load += MainFormResize_Handler;
            Resize += MainFormResize_Handler;
            ContentTabControl.ResumeLayout(false);
            GeneralTabPage.ResumeLayout(false);
            GeneralSettingsPanel.ResumeLayout(false);
            GeneralSettingsPanel.PerformLayout();
            ThumbnailTabPage.ResumeLayout(false);
            ThumbnailSettingsPanel.ResumeLayout(false);
            ThumbnailSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThumbnailsWidthNumericEdit).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThumbnailsHeightNumericEdit).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThumbnailOpacityTrackBar).EndInit();
            ZoomTabPage.ResumeLayout(false);
            ZoomSettingsPanel.ResumeLayout(false);
            ZoomSettingsPanel.PerformLayout();
            ZoomAnchorPanel.ResumeLayout(false);
            ZoomAnchorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThumbnailZoomFactorNumericEdit).EndInit();
            OverlayTabPage.ResumeLayout(false);
            OverlaySettingsPanel.ResumeLayout(false);
            OverlaySettingsPanel.PerformLayout();
            ClientsTabPage.ResumeLayout(false);
            ClientsPanel.ResumeLayout(false);
            ClientsPanel.PerformLayout();
            AboutTabPage.ResumeLayout(false);
            AboutPanel.ResumeLayout(false);
            AboutPanel.PerformLayout();
            TrayMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ThumbnailsFontSizeNumericEdit).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private NotifyIcon NotifyIcon;
		private ContextMenuStrip TrayMenu;
		private TabPage ZoomTabPage;
		private CheckBox EnableClientLayoutTrackingCheckBox;
		private CheckBox HideActiveClientThumbnailCheckBox;
		private CheckBox ShowThumbnailsAlwaysOnTopCheckBox;
		private CheckBox HideThumbnailsOnLostFocusCheckBox;
		private CheckBox EnablePerClientThumbnailsLayoutsCheckBox;
		private CheckBox MinimizeToTrayCheckBox;
		private NumericUpDown ThumbnailsWidthNumericEdit;
		private NumericUpDown ThumbnailsHeightNumericEdit;
		private TrackBar ThumbnailOpacityTrackBar;
		private Panel ZoomAnchorPanel;
		private RadioButton ZoomAanchorNWRadioButton;
		private RadioButton ZoomAanchorNRadioButton;
		private RadioButton ZoomAanchorNERadioButton;
		private RadioButton ZoomAanchorWRadioButton;
		private RadioButton ZoomAanchorSERadioButton;
		private RadioButton ZoomAanchorCRadioButton;
		private RadioButton ZoomAanchorSRadioButton;
		private RadioButton ZoomAanchorERadioButton;
		private RadioButton ZoomAanchorSWRadioButton;
		private CheckBox EnableThumbnailZoomCheckBox;
		private NumericUpDown ThumbnailZoomFactorNumericEdit;
		private Label HighlightColorLabel;
		private Panel ActiveClientHighlightColorButton;
		private CheckBox EnableActiveClientHighlightCheckBox;
		private CheckBox ShowThumbnailOverlaysCheckBox;
		private CheckBox ShowThumbnailFramesCheckBox;
		private CheckedListBox ThumbnailsList;
		private LinkLabel DocumentationLink;
		private Label VersionLabel;
		private CheckBox MinimizeInactiveClientsCheckBox;
        private NumericUpDown ThumbnailsFontSizeNumericEdit;
    }
}