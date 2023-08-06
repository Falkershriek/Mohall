using static Mohall.Game;

namespace Mohall
{
    public class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeGame();
        }

        //
        // Interface elements
        //
        private readonly Button exit_button = new();
        private readonly Button continue_button = new();
        private readonly Label directions_label = new();
        private readonly Panel doors_panel = new();
        private readonly DoorBtn doorBtn1 = new();
        private readonly DoorBtn doorBtn2 = new();
        private readonly DoorBtn doorBtn3 = new();

        /// <summary>
        /// Contains the door buttons to be used in the game.
        /// </summary>
        private readonly List<DoorBtn> doorBtns = new();

        private readonly Game game = new();

        /// <summary>
        /// Updates the game's elements in accordance with the current game stage.
        /// </summary>
        private void UpdateGameWindow()
        {
            UpdateDirectionsLabel();
            UpdateDoorBtns();

            switch (game.CurrentGameStage)
            {
                case Game.GameStage.Stage0:
                    ResetAllControls();
                    break;
                case Game.GameStage.Stage1:
                    continue_button.Enabled = true;
                    break;
                case Game.GameStage.Stage2:
                    break;
                case Game.GameStage.Stage3:
                    break;
                case Game.GameStage.Stage4:
                    break;
                case Game.GameStage.Stage5:
                    AddGameToDatabase();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adds the current game to the statistics database.
        /// </summary>
        private void AddGameToDatabase()
        {
            MenuWindow menuWindow = (MenuWindow)(this.Owner);
            game.GameEntry.SimulatedGame = false;
            menuWindow.AddGameToDatabase(game.GameEntry);
        }

        /// <summary>
        /// Updates the label with game directions.
        /// </summary>
        private void UpdateDirectionsLabel()
        {
            directions_label.Text = game.GetCurrentGameDirections();
        }

        /// <summary>
        /// Resets the settings of all game interface controls to the initial state.
        /// </summary>
        private void ResetAllControls()
        {
            foreach (DoorBtn doorBtn in doorBtns) doorBtn.Reset();
            continue_button.Enabled = false;
        }

        /// <summary>
        /// Finds the index of the given door button.
        /// </summary>
        /// <param name="door">Door button which's index is to be found.</param>
        /// <returns>Door button's index.</returns>
        private int DoorBtnIndex(DoorBtn door)
        {
            return doorBtns.IndexOf(door);
        }

        /// <summary>
        /// Updates the appearance of each door button depending on the door's properties.
        /// </summary>
        private void UpdateDoorBtns()
        {
            for (int i = 0; i < doorBtns.Count; i++)
            {
                doorBtns[i].IsEnabled = (game.Doors[i].IsEnabled) ? true : false;
                doorBtns[i].State = GetDoorState(game.Doors[i]);
            }
        }

        /// <summary>
        /// Establish the state of the given door button using the game data.
        /// </summary>
        /// <param name="door">The door which's state is to be established.</param>
        /// <returns>State of the given door button.</returns>
        private static DoorBtn.DoorBtnState GetDoorState(Game.Door door)
        {
            if (!door.IsOpen)
            {
                return (door.IsSelected) ? DoorBtn.DoorBtnState.Selected : DoorBtn.DoorBtnState.Default;
            }
            if (door.IsSelected)
            {
                return (door.HasReward) ? DoorBtn.DoorBtnState.Correct : DoorBtn.DoorBtnState.Wrong;
            }
            else
            {
                return (door.HasReward) ? DoorBtn.DoorBtnState.Missed : DoorBtn.DoorBtnState.Empty;
            }
        }

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
        /// Continues the game by advancing to the next game stage and updating the game's elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continue_button_Click(object? sender, EventArgs e)
        {
            game.AdvanceGameStage();
            UpdateGameWindow();
        }

        /// <summary>
        /// Selects the clicked door. If the game is in stage zero, advances to the next game stage and updates the game's elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doorBtn_Click(object? sender, EventArgs e)
        {
            if (sender is DoorBtn clickedDoor)
            {
                game.SelectDoor(DoorBtnIndex(clickedDoor));
                if (game.CurrentGameStage == GameStage.Stage0) game.AdvanceGameStage();
                UpdateGameWindow();
            }
        }

        /// <summary>
        /// Contains the values used for the creation of door buttons.
        /// </summary>
        private class DoorBtn : Button
        {
            public DoorBtn()
            {
                Font = new Font("Segoe UI", 32F, FontStyle.Bold, GraphicsUnit.Point);
                Margin = new Padding(0, 10, 0, 10);
                Size = new Size(100, 130);
                UseVisualStyleBackColor = false;
                BackColor = defaultDoorColor;
                ForeColor = Color.White;
                State = DoorBtnState.Default;
            }

            private readonly Color defaultDoorColor = Color.RoyalBlue;
            private readonly Color selectedDoorColor = Color.LightSteelBlue;
            private readonly Color emptyDoorColor = Color.Silver;
            private readonly Color missedDoorColor = Color.Wheat;
            private readonly Color wrongDoorColor = Color.Salmon;
            private readonly Color correctDoorColor = Color.PaleGreen;

            /// <summary>
            /// State of the door button.
            /// </summary>
            public enum DoorBtnState
            {
                Selected,
                Empty,
                Wrong,
                Missed,
                Correct,
                Default
            }
            private DoorBtnState state;
            /// <summary>
            /// State of this door button.
            /// </summary>
            public DoorBtnState State
            {
                get { return state; }
                set
                {
                    switch (value)
                    {
                        case DoorBtnState.Selected:
                            state = DoorBtnState.Selected;
                            BackColor = selectedDoorColor;
                            break;
                        case DoorBtnState.Empty:
                            state = DoorBtnState.Empty;
                            BackColor = emptyDoorColor;
                            break;
                        case DoorBtnState.Wrong:
                            state = DoorBtnState.Wrong;
                            BackColor = wrongDoorColor;
                            break;
                        case DoorBtnState.Missed:
                            state = DoorBtnState.Missed;
                            BackColor = missedDoorColor;
                            break;
                        case DoorBtnState.Correct:
                            state = DoorBtnState.Correct;
                            BackColor = correctDoorColor;
                            break;
                        default:
                            state = DoorBtnState.Default;
                            BackColor = defaultDoorColor;
                            break;
                    }
                }
            }

            /// <summary>
            /// True if the door is enabled, false otherwise.
            /// </summary>
            public bool IsEnabled
            {
                get { return Enabled; }
                set
                {
                    if (state == DoorBtnState.Default)
                    {
                        Enabled = value;
                    }
                }
            }

            /// <summary>
            /// Reset the door button.
            /// </summary>
            public void Reset()
            {
                State = DoorBtnState.Default;
                IsEnabled = true;
            }
        }

        /// <summary>
        ///  Initialization method, contains the window's layout
        /// </summary>
        private void InitializeGame()
        {
            doorBtns.Add(doorBtn1);
            doorBtns.Add(doorBtn2);
            doorBtns.Add(doorBtn3);

            doors_panel.SuspendLayout();
            SuspendLayout();
            // 
            // exit_button
            // 
            exit_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            exit_button.Location = new Point(13, 317);
            exit_button.Margin = new Padding(4);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(490, 50);
            exit_button.TabIndex = 7;
            exit_button.Text = "Exit";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // continue_button
            // 
            continue_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            continue_button.Location = new Point(13, 259);
            continue_button.Margin = new Padding(4);
            continue_button.Name = "continue_button";
            continue_button.Size = new Size(490, 50);
            continue_button.TabIndex = 6;
            continue_button.Text = "Continue";
            continue_button.UseVisualStyleBackColor = true;
            continue_button.Enabled = false;
            continue_button.Click += continue_button_Click;
            // 
            // directions_label
            // 
            directions_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            directions_label.Location = new Point(13, 171);
            directions_label.Margin = new Padding(4);
            directions_label.Name = "directions_label";
            directions_label.Size = new Size(490, 80);
            directions_label.TabIndex = 5;
            directions_label.Text = "empty";
            directions_label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // doors_panel
            // 
            doors_panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            doors_panel.Controls.Add(doorBtn3);
            doors_panel.Controls.Add(doorBtn2);
            doors_panel.Controls.Add(doorBtn1);
            doors_panel.Location = new Point(13, 13);
            doors_panel.Margin = new Padding(4);
            doors_panel.Name = "doors_panel";
            doors_panel.Size = new Size(490, 150);
            doors_panel.TabIndex = 4;
            // 
            // doorBtn3
            // 
            doorBtn3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            doorBtn3.Location = new Point(315, 10);
            doorBtn3.Name = "doorBtn3";
            doorBtn3.TabIndex = 3;
            doorBtn3.Text = "3";
            doorBtn3.Click += doorBtn_Click;
            // 
            // doorBtn2
            // 
            doorBtn2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            doorBtn2.Location = new Point(195, 10);
            doorBtn2.Name = "doorBtn2";
            doorBtn2.TabIndex = 2;
            doorBtn2.Text = "2";
            doorBtn2.Click += doorBtn_Click;
            // 
            // doorBtn1
            // 
            doorBtn1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            doorBtn1.Location = new Point(75, 10);
            doorBtn1.Name = "doorBtn1";
            doorBtn1.TabIndex = 1;
            doorBtn1.Text = "1";
            doorBtn1.Click += doorBtn_Click;
            // 
            // GameMode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 380);
            MinimumSize = new Size(516, 411);
            Controls.Add(doors_panel);
            Controls.Add(directions_label);
            Controls.Add(continue_button);
            Controls.Add(exit_button);
            Name = "GameMode";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Game Mode";
            doors_panel.ResumeLayout(false);
            ResumeLayout(false);

            UpdateGameWindow();
        }
    }
}