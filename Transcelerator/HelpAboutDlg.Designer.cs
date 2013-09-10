// ---------------------------------------------------------------------------------------------
#region // Copyright (c) 2012, SIL International. All Rights Reserved.
// <copyright from='2012' to='2012' company='SIL International'>
//		Copyright (c) 2012, SIL International. All Rights Reserved.
//
//		Distributable under the terms of the MIT License (http://sil.mit-license.org/)
// </copyright>
#endregion
//
// File: Form1.cs
// Responsibility: bogle
// ---------------------------------------------------------------------------------------------
namespace SIL.Transcelerator
{
	partial class HelpAboutDlg
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
			System.Diagnostics.Debug.WriteLineIf(!disposing, "****** Missing Dispose() call for " + GetType() + ". ****** ");
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
			this.m_txlInfo = new SIL.Transcelerator.TxlInfo();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_txlInfo
			// 
			this.m_txlInfo.AutoSize = true;
			this.m_txlInfo.BackColor = System.Drawing.Color.Transparent;
			this.m_txlInfo.Location = new System.Drawing.Point(0, 0);
			this.m_txlInfo.Name = "m_txlInfo";
			this.m_txlInfo.Size = new System.Drawing.Size(666, 368);
			this.m_txlInfo.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(300, 392);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// HelpAboutDlg
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(674, 427);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.m_txlInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HelpAboutDlg";
			this.ShowIcon = false;
			this.Text = "About Transcelerator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TxlInfo m_txlInfo;
		private System.Windows.Forms.Button btnOk;
	}
}