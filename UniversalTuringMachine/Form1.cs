using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Hokanson.UniversalTuringMachine;

namespace UniversalTuringMachine
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TextBox utmFileTxt;
		private TextBox inputFileTxt;
		private Button pauseResumeBtn;
		private CheckBox showStatesChk;
		private CheckBox showTapeChk;
		private TextBox outputTxt;
		private Label label1;
		private Button showMachineBtn;
		private Panel panel1;
		private Button openUTMFile;
		private Button runBtn;
		private Button openInputBtn;

		private Thread _turingRunnerThread;
		private string _utmFile = string.Empty;
		private string _inputFile = string.Empty;
		private bool _running = false;
		private bool _paused = false;
		private ManualResetEvent _manualEvent;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			this.openUTMFile = new System.Windows.Forms.Button();
			this.runBtn = new System.Windows.Forms.Button();
			this.openInputBtn = new System.Windows.Forms.Button();
			this.utmFileTxt = new System.Windows.Forms.TextBox();
			this.inputFileTxt = new System.Windows.Forms.TextBox();
			this.pauseResumeBtn = new System.Windows.Forms.Button();
			this.showStatesChk = new System.Windows.Forms.CheckBox();
			this.showTapeChk = new System.Windows.Forms.CheckBox();
			this.outputTxt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.showMachineBtn = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// openUTMFile
			// 
			this.openUTMFile.Location = new System.Drawing.Point(8, 8);
			this.openUTMFile.Name = "openUTMFile";
			this.openUTMFile.Size = new System.Drawing.Size(88, 23);
			this.openUTMFile.TabIndex = 0;
			this.openUTMFile.Text = "Open UTM";
			this.openUTMFile.Click += new System.EventHandler(this.openUTMFile_Click);
			// 
			// runBtn
			// 
			this.runBtn.Location = new System.Drawing.Point(104, 104);
			this.runBtn.Name = "runBtn";
			this.runBtn.Size = new System.Drawing.Size(88, 23);
			this.runBtn.TabIndex = 6;
			this.runBtn.Text = "Run";
			this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
			// 
			// openInputBtn
			// 
			this.openInputBtn.Location = new System.Drawing.Point(8, 40);
			this.openInputBtn.Name = "openInputBtn";
			this.openInputBtn.Size = new System.Drawing.Size(88, 23);
			this.openInputBtn.TabIndex = 2;
			this.openInputBtn.Text = "Open Input";
			this.openInputBtn.Click += new System.EventHandler(this.openInputBtn_Click);
			// 
			// utmFileTxt
			// 
			this.utmFileTxt.Location = new System.Drawing.Point(104, 8);
			this.utmFileTxt.Name = "utmFileTxt";
			this.utmFileTxt.ReadOnly = true;
			this.utmFileTxt.Size = new System.Drawing.Size(344, 20);
			this.utmFileTxt.TabIndex = 1;
			this.utmFileTxt.Text = "";
			// 
			// inputFileTxt
			// 
			this.inputFileTxt.Location = new System.Drawing.Point(104, 40);
			this.inputFileTxt.Name = "inputFileTxt";
			this.inputFileTxt.ReadOnly = true;
			this.inputFileTxt.Size = new System.Drawing.Size(344, 20);
			this.inputFileTxt.TabIndex = 3;
			this.inputFileTxt.Text = "";
			// 
			// pauseResumeBtn
			// 
			this.pauseResumeBtn.Enabled = false;
			this.pauseResumeBtn.Location = new System.Drawing.Point(200, 104);
			this.pauseResumeBtn.Name = "pauseResumeBtn";
			this.pauseResumeBtn.Size = new System.Drawing.Size(88, 23);
			this.pauseResumeBtn.TabIndex = 7;
			this.pauseResumeBtn.Text = "Pause";
			this.pauseResumeBtn.Click += new System.EventHandler(this.pauseResumeBtn_Click);
			// 
			// showStatesChk
			// 
			this.showStatesChk.Location = new System.Drawing.Point(104, 72);
			this.showStatesChk.Name = "showStatesChk";
			this.showStatesChk.TabIndex = 4;
			this.showStatesChk.Text = "Show states";
			// 
			// showTapeChk
			// 
			this.showTapeChk.Location = new System.Drawing.Point(200, 72);
			this.showTapeChk.Name = "showTapeChk";
			this.showTapeChk.TabIndex = 5;
			this.showTapeChk.Text = "Show \'tape\'";
			// 
			// outputTxt
			// 
			this.outputTxt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputTxt.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.outputTxt.Location = new System.Drawing.Point(0, 0);
			this.outputTxt.Multiline = true;
			this.outputTxt.Name = "outputTxt";
			this.outputTxt.ReadOnly = true;
			this.outputTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.outputTxt.Size = new System.Drawing.Size(456, 544);
			this.outputTxt.TabIndex = 9;
			this.outputTxt.Text = "";
			this.outputTxt.WordWrap = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Output";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.outputTxt);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 166);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 544);
			this.panel1.TabIndex = 10;
			// 
			// showMachineBtn
			// 
			this.showMachineBtn.Location = new System.Drawing.Point(8, 104);
			this.showMachineBtn.Name = "showMachineBtn";
			this.showMachineBtn.Size = new System.Drawing.Size(88, 23);
			this.showMachineBtn.TabIndex = 11;
			this.showMachineBtn.Text = "Show Machine";
			this.showMachineBtn.Click += new System.EventHandler(this.showMachineBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(456, 710);
			this.Controls.Add(this.showMachineBtn);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.showTapeChk);
			this.Controls.Add(this.showStatesChk);
			this.Controls.Add(this.pauseResumeBtn);
			this.Controls.Add(this.inputFileTxt);
			this.Controls.Add(this.utmFileTxt);
			this.Controls.Add(this.openInputBtn);
			this.Controls.Add(this.runBtn);
			this.Controls.Add(this.openUTMFile);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() => Application.Run(new Form1());

		private void openUTMFile_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				utmFileTxt.Text = _utmFile = dlg.FileName;
			}
		}

		private void runBtn_Click(object sender, EventArgs e)
		{
			if (_running)
			{
				// stop the thread
				if (_paused)
				{
					_manualEvent.Set();
				}
				_turingRunnerThread.Abort();
			}
			else
			{
				// get the machine thread running
				if (_utmFile.Length == 0 || _inputFile.Length == 0)
				{
					MessageBox.Show("Please specify a turing machine spec file and an input file.");
					return;
				}

				try
				{
					var machine = new TuringMachine();
					_manualEvent = new ManualResetEvent(false);

					machine.LoadSpec(new StreamReader(_utmFile).ReadToEnd());

					var runner = new TuringRunner(machine, new StreamReader(_inputFile).ReadToEnd(), _manualEvent);
					runner.RunEvent += (snder, args) => BeginInvoke(new EventHandler<TuringRunEventArgs>(OnRunEvent), new[] { snder, args });
					_turingRunnerThread = new Thread(runner.Run)
					{
						Priority = ThreadPriority.BelowNormal,
						Name = "Turing Machine Runner"
					};
					_turingRunnerThread.Start();  // starts in waiting mode

					_manualEvent.Set();  // release the thread
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void SetButtonState()
		{
			openUTMFile.Enabled = !_running;
			openInputBtn.Enabled = !_running;
			showMachineBtn.Enabled = !_running;

			pauseResumeBtn.Text = (_paused ? "Resume" : "Pause");
			pauseResumeBtn.Enabled = _running;

			runBtn.Text = (_running ? "Stop" : "Run");
		}

		private void openInputBtn_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				inputFileTxt.Text = _inputFile = dlg.FileName;
			}
		}

		private void OnRunEvent(object sender, TuringRunEventArgs e)
		{
			switch (e.TuringEvt)
			{
				case TuringEvent.Error:
					WriteOutput(e.Error);
					_running = false;
					SetButtonState();
					break;
				case TuringEvent.Step:
					if (showStatesChk.Checked)
						WriteOutput($"{e.State.Num}:{e.State.Input}--->{e.Action.ChangeStateTo}:{e.Action.ChangeTapeTo}:{e.Action.Dir}");
					if (showTapeChk.Checked)
						WriteOutput(e.Input);
					break;
				case TuringEvent.Done:
					if (!showTapeChk.Checked)
						WriteOutput(e.Input);
					WriteOutput("Done.");
					_running = false;
					SetButtonState();
					break;
				case TuringEvent.Run:
					ClearOutput();
					WriteOutput("Running...");
					WriteOutput(e.Input);
					_running = true;
					_paused = false;
					SetButtonState();
					break;
			}
		}

		private void ClearOutput() => outputTxt.Clear();

		private void WriteOutput(string s)
		{
			outputTxt.AppendText(s ?? string.Empty);
			outputTxt.AppendText("\r\n");
			outputTxt.SelectionStart = outputTxt.Text.Length;
			outputTxt.ScrollToCaret();
		}

		private void pauseResumeBtn_Click(object sender, EventArgs e)
		{
			if (_paused)
			{
				// resume thread
				_manualEvent.Set();
				_paused = false;
			}
			else
			{
				_manualEvent.Reset();
				_paused = true;
			}
			SetButtonState();
		}

		private void showMachineBtn_Click(object sender, EventArgs e)
		{
			if (_utmFile.Length == 0)
			{
				MessageBox.Show("Please specify a turing machine spec file.");
				return;
			}

			try
			{
				var machine = new TuringMachine();
				machine.LoadSpec(new StreamReader(_utmFile).ReadToEnd());
				ClearOutput();
				WriteOutput(machine.ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}