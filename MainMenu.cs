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

        private readonly Statistics gameStatistics = new();

        private readonly MenuButton exit_button = new();
        private readonly MenuButton statistics_button = new();
        private readonly MenuButton simulate_button = new();
        private readonly MenuButton play_button = new();
        private readonly Panel menu_buttons_panel = new();
        private readonly Label statistics_label = new();
        private readonly MenuButton back_to_menu_button = new();
        private readonly Panel statistics_panel = new();


        /// <summary>
        /// Closes the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_button_Click(object? sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Closes the statistics panel and re-opens the main menu panel inside the main menu window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_to_menu_button_Click(object? sender, EventArgs e)
        {
            menu_buttons_panel.Show();
            statistics_panel.Hide();
        }

        /// <summary>
        /// Hides the main menu panel and opens the game statistics panel inside the main menu window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statistics_button_Click(object? sender, EventArgs e)
        {
            menu_buttons_panel.Hide();
            statistics_panel.Show();

            int totplayed = gameStatistics.TotalGamesPlayed();
            int totwon = gameStatistics.TotalGamesWon();
            int totwonswap = gameStatistics.TotalGamesWonAfterSwap();

            statistics_label.Text = "Total games played: " + totplayed.ToString();
            statistics_label.Text += "\n" + "Total games won: " + totwon.ToString();
            statistics_label.Text += "\n" + "Total games won after swap: " + totwonswap.ToString();

            double swap_win_ratio = (double)gameStatistics.TotalGamesWonAfterSwap() / (double)gameStatistics.TotalGamesWon();

            statistics_label.Text += "\n" + "Swap win ratio: " + swap_win_ratio.ToString("0.##");
            statistics_label.Text += "\n" + "Rewards behind door 1/2/3: " + gameStatistics.RewardsBehindDoor1().ToString() + "/" + gameStatistics.RewardsBehindDoor2().ToString() + "/" + gameStatistics.RewardsBehindDoor3().ToString();
        }

        /// <summary>
        /// Opens the Mohall game window.
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

        /// <summary>
        /// Contains the values used for the creation of menu buttons.
        /// </summary>
        private class MenuButton : Button
        {
            public MenuButton()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //Margin = new Padding(4);
                Size = new Size(490, 50);
                UseVisualStyleBackColor = true;
            }
        }

        private void InitializeMainMenu()
        {
            menu_buttons_panel.SuspendLayout();
            statistics_panel.SuspendLayout();
            SuspendLayout();
            //
            // exit_button
            //
            exit_button.Anchor |= AnchorStyles.Bottom;
            exit_button.Location = new Point(13, 187);
            exit_button.Name = "exit_button";
            exit_button.TabIndex = 8;
            exit_button.Text = "Exit";
            exit_button.Click += exit_button_Click;
            //
            // statistics_panel
            //
            statistics_panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statistics_panel.Controls.Add(statistics_label);
            statistics_panel.Controls.Add(back_to_menu_button);
            statistics_panel.Location = new Point(13, 13);
            statistics_panel.Name = "menu_buttons_panel";
            statistics_panel.Size = new Size(490, 182);
            statistics_panel.TabIndex = 7;
            //
            // back_to_menu_button
            //
            back_to_menu_button.Location = new Point(0, 116);
            back_to_menu_button.Name = "back_to_menu_button";
            back_to_menu_button.TabIndex = 6;
            back_to_menu_button.Text = "Back to menu";
            back_to_menu_button.Click += back_to_menu_button_Click;
            // 
            // statistics_label
            // 
            statistics_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            statistics_label.Location = new Point(0, 0);
            statistics_label.Margin = new Padding(4);
            statistics_label.Name = "statistics_label";
            statistics_label.Size = new Size(490, 108);
            statistics_label.TabIndex = 5;
            statistics_label.Text = "empty";
            statistics_label.TextAlign = ContentAlignment.MiddleCenter;
            //
            // menu_buttons_panel
            //
            menu_buttons_panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            menu_buttons_panel.Controls.Add(statistics_button);
            menu_buttons_panel.Controls.Add(simulate_button);
            menu_buttons_panel.Controls.Add(play_button);
            menu_buttons_panel.Location = new Point(13, 13);
            menu_buttons_panel.Name = "menu_buttons_panel";
            menu_buttons_panel.Size = new Size(490, 182);
            menu_buttons_panel.TabIndex = 4;
            //
            // statistics_button
            //
            statistics_button.Location = new Point(0, 116);
            statistics_button.Name = "statistics_button";
            statistics_button.TabIndex = 3;
            statistics_button.Text = "Statistics";
            //statistics_button.Enabled = false;
            statistics_button.Click += statistics_button_Click;
            //
            // simulate_button
            //
            simulate_button.Location = new Point(0, 58);
            simulate_button.Name = "simulate_button";
            simulate_button.TabIndex = 2;
            simulate_button.Text = "Simulate";
            simulate_button.Enabled = false;
            //simulate_button.Click += simulate_button_Click;
            //
            // play_button
            //
            play_button.Location = new Point(0, 0);
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
            MinimumSize = new Size(300, 289);
            Controls.Add(exit_button);
            Controls.Add(menu_buttons_panel);
            Controls.Add(statistics_panel);
            Name = "MainMenu";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Main Menu";

            menu_buttons_panel.ResumeLayout(false);
            statistics_panel.ResumeLayout(false);
            ResumeLayout(false);
            statistics_panel.Hide();

        }
    }
}
