using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mohall
{
    public class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeMainMenu();
        }
        //public partial class GameMode : Form
        //{
        //}

        //private GameMode? gameMode;

        private readonly MenuButton exit_button = new();
        private readonly MenuButton statistics_button = new();
        private readonly MenuButton simulate_button = new();
        private readonly MenuButton play_button = new();


        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_button_Click(object? sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Opens the Mohall game window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_button_Click(object? sender, EventArgs e)
        {
            GameMode gameMode = new();
            Hide();
            gameMode.ShowDialog();
            Show();
        }

        private class MenuButton : Button
        {
            public MenuButton()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right;
                Margin = new Padding(4);
                Size = new Size(490, 50);
                UseVisualStyleBackColor = true;
            }
        }

        private void InitializeMainMenu()
        {
            //
            // exit_button
            //
            exit_button.Anchor |= AnchorStyles.Bottom;
            exit_button.Location = new Point(13, 187);
            exit_button.Name = "exit_button";
            exit_button.TabIndex = 4;
            exit_button.Text = "Exit";
            exit_button.Click += exit_button_Click;
            //
            // statistics_button
            //
            statistics_button.Anchor |= AnchorStyles.Top;
            statistics_button.Location = new Point(13, 129);
            statistics_button.Name = "statistics_button";
            statistics_button.TabIndex = 3;
            statistics_button.Text = "Statistics";
            //statistics_button.Click += statistics_button_Click;
            statistics_button.Enabled = false;
            //
            // simulate_button
            //
            simulate_button.Anchor |= AnchorStyles.Top;
            simulate_button.Location = new Point(13, 71);
            simulate_button.Name = "simulate_button";
            simulate_button.TabIndex = 2;
            simulate_button.Text = "Simulate";
            //simulate_button.Click += simulate_button_Click;
            simulate_button.Enabled = false;
            //
            // play_button
            //
            play_button.Anchor |= AnchorStyles.Top;
            play_button.Location = new Point(13, 13);
            play_button.Name = "play_button";
            play_button.TabIndex = 1;
            play_button.Text = "Play Mohall";
            play_button.Click += play_button_Click;
            //
            // MainMenu
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 250);
            MinimumSize = new Size(250, 289);
            Controls.Add(play_button);
            Controls.Add(simulate_button);
            Controls.Add(statistics_button);
            Controls.Add(exit_button);
            Name = "MainMenu";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Main Menu";
            ResumeLayout(false);

        }
    }
}
