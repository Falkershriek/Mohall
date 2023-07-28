namespace Mohall
{
    public class GameMode : Form
    {
        public GameMode()
        {
            InitializeGame();
        }

        /// <summary>
        /// Contains values for the possible game stages.
        /// </summary>
        enum GameStage
        {
            Stage0,
            Stage1,
            Stage2,
            Stage3,
            Stage4,
            Stage5,
        }

        /// <summary>
        /// Contains value of the current game stage.
        /// </summary>
        private GameStage CurrentStage = GameStage.Stage0;

        private readonly Button exit_button = new();
        private readonly Button continue_button = new();
        private readonly Label directions_label = new();
        private readonly Panel doors_panel = new();
        private readonly DoorBtn door1 = new();
        private readonly DoorBtn door2 = new();
        private readonly DoorBtn door3 = new();

        /// <summary>
        /// Contains the doors to be chosen from in the game.
        /// </summary>
        private readonly List<DoorBtn> doorBtns = new();
        int numberOfDoors = 0;

        /// <summary>
        /// Advances the game to the next stage. Once the final stage is reached, wraps back to stage zero.
        /// </summary>
        private void AdvanceGameStage()
        {
            CurrentStage = (CurrentStage < GameStage.Stage5) ? CurrentStage + 1 : GameStage.Stage0;
        }

        /// <summary>
        /// Updates the game's elements in accordance with the current game stage.
        /// </summary>
        private void UpdateGame()
        {
            UpdateDirectionsLabel();

            switch (CurrentStage)
            {
                case GameStage.Stage0:
                    ResetAllControls();
                    RandomlyAssignReward();
                    break;
                case GameStage.Stage1:
                    break;
                case GameStage.Stage2:
                    EnableAllDoors(false);
                    break;
                case GameStage.Stage3:
                    RandomlyOpenEmptyUnselectedDoor();
                    EnableAllDoors(true);
                    break;
                case GameStage.Stage4:
                    EnableAllDoors(false);
                    break;
                case GameStage.Stage5:
                    OpenAllDoors();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates the label with game directions.
        /// </summary>
        private void UpdateDirectionsLabel()
        {
            directions_label.Text = GetCurrentGameDirections();
        }

        /// <summary>
        /// Contains the game directions specific to the current game stage.
        /// </summary>
        /// <returns>String with game directions for the current stage.</returns>
        private string GetCurrentGameDirections()
        {
            return CurrentStage switch
            {
                GameStage.Stage0 => "Pick a door. Behind one of the doors is a reward. The other two doors are empty.",
                GameStage.Stage1 => "You can change your choice if you wish. Press \"Continue\" when you're ready.",
                GameStage.Stage2 => "From the remaining doors, I will now open one that contains no reward.",
                GameStage.Stage3 => "Next, the remaining doors will be opened. However, before that happens, I will allow you to change your choice. If you want to, you can pick another door. Once you continue, your choice will become final.",
                GameStage.Stage4 => "Your choice is now set in stone. Let's open the remaining doors!",
                GameStage.Stage5 => (DidPlayerSelectReward()) ? "Congratulations, you've won the reward!" : "You've lost. Better luck next time!",
                _ => "Something went wrong",
            };
        }

        /// <summary>
        /// Checks whether the player selected a door with a reward
        /// </summary>
        /// <returns>True if player selected a door with a reward, false if player selected an empty door</returns>
        private bool DidPlayerSelectReward()
        {
            DoorBtn? door = doorBtns.Find(door => door.IsSelected);
            return door != null && door.HasReward;
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
        /// Randomly assigns reward to one of the doors.
        /// </summary>
        private void RandomlyAssignReward()
        {
            Random rnd = new();
            DoorBtn randomDoor = doorBtns[rnd.Next(0, numberOfDoors)];

            randomDoor.HasReward = true;
        }

        /// <summary>
        /// Enables all doors if true, disables all closed doors if false. Only works on closed doors (see DoorBtn.IsEnabled).
        /// </summary>
        /// <param name="enableValue">Value to set each door's "Enabled" parameter to.</param>
        private void EnableAllDoors(bool enableValue = true)
        {
            foreach (DoorBtn doorBtn in doorBtns) doorBtn.IsEnabled = enableValue;
        }

        /// <summary>
        /// Randomly opens one empty, unselected door.
        /// </summary>
        private void RandomlyOpenEmptyUnselectedDoor()
        {
            Random rnd = new();
            DoorBtn currDoor = doorBtns[rnd.Next(0, numberOfDoors)];

            while (currDoor.IsSelected || currDoor.IsOpen || currDoor.HasReward)
            {
                currDoor = doorBtns[rnd.Next(0, numberOfDoors)];
            }

            currDoor.IsOpen = true;
        }

        /// <summary>
        /// Opens all doors.
        /// </summary>
        private void OpenAllDoors()
        {
            foreach (DoorBtn doorBtn in doorBtns) doorBtn.IsOpen = true;
        }

        /// <summary>
        /// Selects the given door and deselects all other doors.
        /// </summary>
        /// <param name="doorBtn">The door to select.</param>
        private void SelectDoor(DoorBtn doorBtn)
        {
            DeselectAllDoors();
            doorBtn.IsSelected = true;
        }

        /// <summary>
        /// Deselects all doors.
        /// </summary>
        private void DeselectAllDoors()
        {
            foreach (DoorBtn doorBtn in doorBtns) doorBtn.IsSelected = false;
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
            AdvanceGameStage();
            UpdateGame();
        }

        /// <summary>
        /// Selects the clicked door. If the game is in stage zero, advances to the next game stage and updates the game's elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void door_Click(object? sender, EventArgs e)
        {
            if (sender is DoorBtn clickedDoor)
            {
                SelectDoor(clickedDoor);

                if (CurrentStage == GameStage.Stage0)
                {
                    continue_button.Enabled = true;
                    AdvanceGameStage();
                    UpdateGame();
                }
            }
        }

        /// <summary>
        /// Contains the values used for the creation of door buttons.
        /// </summary>
        private class DoorBtn : Button
        {
            private readonly Color defaultDoorColor = Color.RoyalBlue;
            private readonly Color selectedDoorColor = Color.LightSteelBlue;
            private readonly Color emptyDoorColor = Color.Silver;
            private readonly Color missedDoorColor = Color.Wheat;
            private readonly Color wrongDoorColor = Color.Salmon;
            private readonly Color correctDoorColor = Color.PaleGreen;
            private bool isSelected = false;
            public bool IsSelected
            {
                get { return isSelected; }
                set
                {
                    if (!isOpen)
                    {
                        isSelected = value;
                        BackColor = (value) ? selectedDoorColor : defaultDoorColor;
                    }
                }
            }
            public bool HasReward = false;
            private bool isOpen = false;
            public bool IsOpen
            {
                get { return isOpen; }
                set
                {
                    isOpen = value;
                    if (isOpen)
                    {
                        if (HasReward)
                        {
                            BackColor = (isSelected) ? correctDoorColor : missedDoorColor;
                        }
                        else
                        {
                            BackColor = (isSelected) ? wrongDoorColor : emptyDoorColor;
                        }
                    }
                    else
                    {
                        BackColor = defaultDoorColor;
                    }
                }
            }
            public bool IsEnabled
            {
                get { return Enabled; }
                set
                {
                    if (!isOpen)
                    {
                        Enabled = value;
                    }
                }
            }


            public DoorBtn()
            {
                Font = new Font("Segoe UI", 32F, FontStyle.Bold, GraphicsUnit.Point);
                Margin = new Padding(0, 10, 0, 10);
                Size = new Size(100, 130);
                UseVisualStyleBackColor = false;
                BackColor = defaultDoorColor;
                ForeColor = Color.White;
            }

            public void ToggleSelection()
            {
                IsSelected = !IsSelected;
            }

            public void Reset()
            {
                IsOpen = false;
                IsEnabled = true;
                IsSelected = false;
                HasReward = false;
            }
        }

        /// <summary>
        ///  Initialization method, contains the window's layout
        /// </summary>
        private void InitializeGame()
        {
            doorBtns.Add(door1);
            doorBtns.Add(door2);
            doorBtns.Add(door3);
            numberOfDoors = doorBtns.Count;

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
            doors_panel.Controls.Add(door3);
            doors_panel.Controls.Add(door2);
            doors_panel.Controls.Add(door1);
            doors_panel.Location = new Point(13, 13);
            doors_panel.Margin = new Padding(4);
            doors_panel.Name = "doors_panel";
            doors_panel.Size = new Size(490, 150);
            doors_panel.TabIndex = 4;
            // 
            // door3
            // 
            door3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            door3.Location = new Point(315, 10);
            door3.Name = "door3";
            door3.TabIndex = 3;
            door3.Text = "3";
            door3.Click += door_Click;
            // 
            // door2
            // 
            door2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            door2.Location = new Point(195, 10);
            door2.Name = "door2";
            door2.TabIndex = 2;
            door2.Text = "2";
            door2.Click += door_Click;
            // 
            // door1
            // 
            door1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            door1.Location = new Point(75, 10);
            door1.Name = "door1";
            door1.TabIndex = 1;
            door1.Text = "1";
            door1.Click += door_Click;
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

            UpdateGame();
        }
    }
}