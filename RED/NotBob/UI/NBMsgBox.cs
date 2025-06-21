/***************************************************************************
 * This customizable message box is based on original code by Max Kleyzit. *
 * https://github.com/mkleyzit/A-customizable-.NET-WinForms-Message-Box    *
 * It had no license. I emailed the author and they were happy for the     *
 * code to be freely used and modified.                                    *
 * ----------------------------------------------------------------------- *
 * I've modified it over the years to suit my own needs and preferences.   *
 * (so many changes now that it barely compares to the original)           *
 * ----------------------------------------------------------------------- *
 * You can define the buttons in any order, but generation stops at the    *
 * first disabled button. The rest are ignored. Button1 is always enabled. *
 *                                                                         *
 * If you want to resuse a previously defined NBMsgBox then you MUST call  *
 * .Reset() before changing any other settings.                            *
 ***************************************************************************/

/* 
 * Example usage:
 * 
 * using (NBMsgBox mbox = new NBMsgBox("Title Text", MessageBoxIcon.Information))
 * {
 *    mbox.SetMessage("This is an example of usage");
 *    mbox.SetButton(1, "Anne", DialogResult.Yes);
 *    mbox.SetButton(2, "Bobby", DialogResult.No, isDefault:true);
 *    mbox.SetButton(3, "Charlotte", DialogResult.Ignore);
 *    mbox.SetButton(4, "Dennis", DialogResult.Retry);
 *    mbox.ShowDialog(this);
 *    Label1.Text = string.Format("NBMsgBox returned ExitButton={0}, DialogResult={1}", mbox.DialogExitButton, mbox.DialogResult);
 *    ...
 *    mbox.Reset();
 *    mbox.SetMessage("This is a new message");
 *    etc...
 * }
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NotBob.UI
{
	/// <summary>
	/// A customizable Dialog box with 4 buttons, custom icon
	/// </summary>
	partial class NBMsgBox : Form
	{
		/// <summary>
		/// Create a new instance of the dialog box with title and standard windows messagebox icon.
		/// </summary>
		/// <param name="title">Dialog Box title.</param>
		/// <param name="icon">Standard system messagebox icon.</param>
		public NBMsgBox(string title, MessageBoxIcon icon) : this(string.Empty, title, icon) { }

		/// <summary>
		/// Create a new instance of the dialog box with a message and title.
		/// </summary>
		/// <param name="message">Message text.</param>
		/// <param name="title">Dialog Box title.</param>
		public NBMsgBox(string message, string title) : this(message, title, MessageBoxIcon.None) { }

		/// <summary>
		/// Create a new instance of the dialog box with a message and title and a standard windows messagebox icon.
		/// </summary>
		/// <param name="message">Message text.</param>
		/// <param name="title">Dialog Box title.</param>
		/// <param name="icon">Standard system messagebox icon.</param>
		public NBMsgBox(string message, string title, MessageBoxIcon icon) : this(message, title, GetSystemIcon(icon)) { }

		/// <summary>
		/// Create a new instance of the dialog box with a message and title and a custom windows icon.
		/// </summary>
		/// <param name="message">Message text.</param>
		/// <param name="title">Dialog Box title.</param>
		/// <param name="icon">Custom icon.</param>
		public NBMsgBox(string message, string title, Icon icon)
		{
			InitializeComponent();

			this.UxButton1.Visible = false;
			this.UxButton2.Visible = false;
			this.UxButton3.Visible = false;
			this.UxButton4.Visible = false;

			this.Text = title;
			this.UxMessageText.Text = message;
			this.m_DialogIcon = icon;

			if (this.m_DialogIcon == null)
			{
				this.UxMessageText.Location = new System.Drawing.Point(FORM_X_MARGIN, FORM_Y_MARGIN);
			}
		}


		#region Public API

		/// <summary>
		/// Reset the dialog box to the default state.
		/// </summary>
		public void Reset()
		{
			m_buttonList.Reset();
			m_buttonList.Add(1, "&OK", DialogResult.OK, isEnabled: true);
			this.DialogExitButton = NBMsgBoxExitButton.Button1;
			this.UxButton1.Visible = true;
			this.UxButton2.Visible = false;
			this.UxButton3.Visible = false;
			this.UxButton4.Visible = false;
			this.m_minButtonRowWidth = 0;
		}

		/// <summary>
		/// Set the button text and dialog result for the button.
		/// </summary>
		/// <param name="buttonIndex">Index of the button to set</param>
		/// <param name="buttonText">Text for the button</param>
		/// <param name="dialogResult">DialogResult for the button</param>	
		public void SetButton(int buttonIndex, string buttonText, DialogResult dialogResult)
		{
			SetButton(buttonIndex, buttonText, dialogResult, isDefault: false);
		}

		/// <summary>
		/// Set the button text, dialog result, and default for the button.
		/// </summary>
		/// <param name="buttonIndex">Index of the button to set</param>
		/// <param name="buttonText">Text for the button</param>
		/// <param name="dialogResult"></param>
		/// <param name="isDefault">Is the the default button for the dialog</param>
		public void SetButton(int buttonIndex, string buttonText, DialogResult dialogResult, bool isDefault = false)
		{
			m_buttonList.Add(buttonIndex, buttonText, dialogResult, isEnabled: true);
			if (isDefault)
			{
				m_buttonList.DefaultButton = buttonIndex;
			}
		}

		/// <summary>
		/// Sets the default button for the dialog
		/// </summary>
		/// <param name="buttonIndex">Index of the button to be used as the default</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public void SetButtonDefault(int buttonIndex)
		{
			if (buttonIndex < 1 || buttonIndex > BUTTON_MAX)
			{
				throw new ArgumentOutOfRangeException("buttonIndex", "Button index must be between 1 and " + BUTTON_MAX.ToString());
			}
			m_buttonList.DefaultButton = buttonIndex;
		}

		/// <summary>
		/// Get system icon from a MessageBoxIcon.
		/// </summary>
		/// <param name="icon">The MessageBoxIcon value.</param>
		/// <returns>SystemIcon type Icon.</returns>
		public static Icon GetSystemIcon(MessageBoxIcon icon)
		{
			switch (icon)
			{
				case MessageBoxIcon.None:
					return null;
				case MessageBoxIcon.Error:
					return SystemIcons.Error;
				case MessageBoxIcon.Question:
					return SystemIcons.Question;
				case MessageBoxIcon.Exclamation:
					return SystemIcons.Exclamation;
				case MessageBoxIcon.Information:
					return SystemIcons.Information;
				default:
					return null;
			}
		}

		/// <summary>
		/// Sets the min size of the dialog box. 
		/// If the text or button row needs more size then the dialog box will size to fit the text.
		/// </summary>
		/// <param name="width">Min width value.</param>
		/// <param name="height">Min height value.</param>
		public void SetMinSize(int width, int height)
		{
			m_minWidth = width;
			m_minHeight = height;
		}

		/// <summary>
		/// Sets the message text for the dialog box.
		/// </summary>
		/// <param name="text"></param>
		public void SetMessage(string text)
		{
			UxMessageText.Text = text;
		}

		/// <summary>
		/// Gets the button that was pressed.
		/// This is NOT the same as the DialogResult
		/// </summary>
		public NBMsgBoxExitButton DialogExitButton { get; private set; }

		#endregion Public API


		#region Events for Form, Controls etc

		/// <summary>
		/// Override the CreateParams to disable the close button on the form.
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				const int CP_DISABLE_CLOSE_BUTTON = 0x200;
				CreateParams cp = base.CreateParams;
				cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
				return cp;
			}
		}

		/// <summary>
		/// Build the form when it is shown for the first time
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NBMsgBox_Shown(object sender, EventArgs e)
		{
			BuildForm();
		}

		/// <summary>
		/// Paint the Dialog Icon in the top left corner.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			if (m_DialogIcon != null)
			{
				Graphics g = e.Graphics;
				g.DrawIconUnstretched(m_DialogIcon, new Rectangle(FORM_X_MARGIN, FORM_Y_MARGIN, m_DialogIcon.Width, m_DialogIcon.Height));
			}

			base.OnPaint(e);
		}

		/// <summary>
		/// Action the button to exit the dialog with required result
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UxButton_Click(object sender, EventArgs e)
		{
			if (sender == UxButton1)
			{
				DialogExitButton = NBMsgBoxExitButton.Button1;
			}
			else if (sender == UxButton2)
			{
				DialogExitButton = NBMsgBoxExitButton.Button2;
			}
			else if (sender == UxButton3)
			{
				DialogExitButton = NBMsgBoxExitButton.Button3;
			}
			else if (sender == UxButton4)
			{
				DialogExitButton = NBMsgBoxExitButton.Button4;
			}

			// If we haven't got a DialogResult set then close the form.
			// NOTE - This will automatically set the DialogResult to Cancel
			if (((Button)sender).DialogResult == DialogResult.None)
			{
				this.Close();
			}
		}

		#endregion Events for Form, Controls etc


		#region Private Variables etc

		// / <summary>
		/// The list of buttons and their configuration.
		/// index 0 is unused so that the button numbers 
		/// (1 to BUTTON_MAX) match the list index.
		/// </summary>
		private NMsgBoxButtonInfoCollection m_buttonList = new NMsgBoxButtonInfoCollection();

		/// <summary>
		/// Min set width.
		/// </summary>
		private int m_minWidth;

		/// <summary>
		/// Min set height.
		/// </summary>
		private int m_minHeight;

		/// <summary>
		/// The min required width of the button row. Sum of button widths + margins.
		/// </summary>
		private int m_minButtonRowWidth;

		/// <summary>
		/// The icon to paint.
		/// </summary>
		private Icon m_DialogIcon;

		/// <summary>
		/// Margins and spacing for form elements
		/// </summary>
		const int FORM_Y_MARGIN = 10;
		const int FORM_X_MARGIN = 16;
		const int TEXT_Y_MARGIN = 30;
		const int BUTTON_SPACE = 5;

		/// <summary>
		/// Max number of buttons on the form.
		/// </summary>
		const int BUTTON_MAX = 4;

		#endregion Private Variables etc


		#region Private Helper Routines

		/// <summary>
		/// Builds the buttons to be displayed on the form.
		/// </summary>
		private void BuildButtons()
		{
			// Set Button 1
			m_minButtonRowWidth += BuildButton(UxButton1, m_buttonList[1].Text, m_buttonList[1].DialogResultValue);
			// Set Button 2
			if (m_buttonList[2].Enabled)
			{
				m_minButtonRowWidth += BuildButton(UxButton2, m_buttonList[2].Text, m_buttonList[2].DialogResultValue);
				m_minButtonRowWidth += BUTTON_SPACE;

				// Set Button 3
				if (m_buttonList[3].Enabled)
				{
					m_minButtonRowWidth += BuildButton(UxButton3, m_buttonList[3].Text, m_buttonList[3].DialogResultValue);
					m_minButtonRowWidth += BUTTON_SPACE;

					// Set Button 4
					if (m_buttonList[4].Enabled)
					{
						m_minButtonRowWidth += BuildButton(UxButton4, m_buttonList[4].Text, m_buttonList[4].DialogResultValue);
						m_minButtonRowWidth += BUTTON_SPACE;
					}
				}
			}
		}

		/// <summary>
		/// Sets button text and returns the width.
		/// </summary>
		/// <param name="button">Button object.</param>
		/// <param name="text">Text of the button.</param>
		/// <param name="tab">TabIndex of the button.</param>
		/// <param name="mbResult">DialogResult of the button.</param>
		/// <returns>Width of the button.</returns>
		private int BuildButton(Button button, string text, DialogResult dialogResult)
		{
			button.Text = text;
			button.Visible = true;
			button.DialogResult = dialogResult;
			return button.Size.Width;
		}

		/// <summary>
		/// Sets the buttons location.
		/// </summary>
		private void SetButtonRowLocations()
		{
			int formWidth = this.ClientRectangle.Width;

			int x = formWidth - FORM_X_MARGIN;
			int y = UxButton1.Location.Y;

			if (UxButton4.Visible)
			{
				x -= UxButton4.Size.Width;
				UxButton4.Location = new Point(x, y);
				x -= BUTTON_SPACE;
			}

			if (UxButton3.Visible)
			{
				x -= UxButton3.Size.Width;
				UxButton3.Location = new Point(x, y);
				x -= BUTTON_SPACE;
			}

			if (UxButton2.Visible)
			{
				x -= UxButton2.Size.Width;
				UxButton2.Location = new Point(x, y);
				x -= BUTTON_SPACE;
			}

			x -= UxButton1.Size.Width;
			UxButton1.Location = new Point(x, y);
		}

		private void BuildForm()
		{
			if (!m_buttonList[1].Enabled)
			{
				Reset();
			}
			BuildButtons();
			SetDefaultButton();

			m_minButtonRowWidth += 2 * FORM_X_MARGIN; //add margin to the ends

			SetDialogSize();
			SetButtonRowLocations();
		}

		private void SetDefaultButton()
		{
			switch (m_buttonList.DefaultButton)
			{
				case 1:
					AcceptButton = UxButton1;
					UxButton1.Select();
					break;
				case 2:
					AcceptButton = UxButton2;
					UxButton2.Select();
					break;
				case 3:
					AcceptButton = UxButton3;
					UxButton3.Select();
					break;
				case 4:
					AcceptButton = UxButton4;
					UxButton4.Select();
					break;
				default:
					AcceptButton = null;
					UxButton1.Select();
					break;
			}
		}

		/// <summary>
		/// Auto fits the dialog box to fit the text and the buttons.
		/// </summary>
		private void SetDialogSize()
		{
			int requiredWidth = this.UxMessageText.Location.X + this.UxMessageText.Size.Width + FORM_X_MARGIN;
			requiredWidth = requiredWidth > m_minButtonRowWidth ? requiredWidth : m_minButtonRowWidth;

			int requiredHeight = this.UxMessageText.Location.Y + this.UxMessageText.Size.Height - this.UxButton2.Location.Y + this.ClientSize.Height + TEXT_Y_MARGIN;

			int minSetWidth = this.ClientSize.Width > this.m_minWidth ? this.ClientSize.Width : this.m_minWidth;
			int minSetHeight = this.ClientSize.Height > this.m_minHeight ? this.ClientSize.Height : this.m_minHeight;

			Size s = new Size();
			s.Width = requiredWidth > minSetWidth ? requiredWidth : minSetWidth;
			s.Height = requiredHeight > minSetHeight ? requiredHeight : minSetHeight;
			this.ClientSize = s;
		}

		#endregion Private Helper Routines

		/// <summary>
		/// Definition of a single button
		/// </summary>
		class NBMsgBoxButtonInfo
		{
			public NBMsgBoxButtonInfo() : this(false, string.Empty, DialogResult.None) { }
			public NBMsgBoxButtonInfo(bool isEnabled, string text, DialogResult dialogResult)
			{
				this.Enabled = isEnabled;
				this.Text = text;
				this.DialogResultValue = dialogResult;
			}
			public NBMsgBoxButtonInfo(string text, DialogResult result) : this(false, text, result) { }
			public NBMsgBoxButtonInfo(string text) : this(false, text, DialogResult.None) { }

			public bool Enabled { get; set; }
			public string Text { get; set; }
			public DialogResult DialogResultValue { get; set; }
		}

		/// <summary>
		/// Collection of button definitions
		/// </summary>
		class NMsgBoxButtonInfoCollection : List<NBMsgBoxButtonInfo>
		{
			public NMsgBoxButtonInfoCollection()
			{
				Reset();
			}

			private int m_DefaultButton = 1;

			public int DefaultButton
			{
				get { return m_DefaultButton; }
				set { m_DefaultButton = (value > 0 && value < BUTTON_MAX + 1) ? value : 1; }
			}

			public void Reset()
			{
				this.Clear();
				for (int i = 0; i <= BUTTON_MAX; i++)
				{
					this.Add(new NBMsgBoxButtonInfo(isEnabled: false, text: string.Empty, dialogResult: DialogResult.None));
				}
			}

			public void Add(int buttonIndex, string buttonText, DialogResult buttonResult, bool isEnabled)
			{
				if (buttonIndex < 1 || buttonIndex > BUTTON_MAX)
				{
					throw new ArgumentOutOfRangeException("buttonIndex", "Button index must be between 1 and " + BUTTON_MAX.ToString());
				}
				if (string.IsNullOrEmpty(buttonText) && buttonText.Length > 20)
				{
					throw new ArgumentOutOfRangeException("buttonText", "Button text must be less than 20 characters.");
				}

				// Button 1 is always enabled
				this[buttonIndex].Enabled = buttonIndex == 1 ? true : isEnabled;
				this[buttonIndex].Text = buttonText;
				this[buttonIndex].DialogResultValue = buttonResult;
			}
		}
	}

	public enum NBMsgBoxExitButton
	{
		Unknown = 0,
		Button1 = 1,
		Button2 = 2,
		Button3 = 3,
		Button4 = 4
	}
}