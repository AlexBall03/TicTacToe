﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;
        /// <summary>
        /// True if it is player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;
        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        /// <summary>
        /// Default Costructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure player 1 starts the game
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change backgroung, foreground and content to default
                button.Content = string.Empty;
                button.Background = Brushes.LightGray;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game whe use click a btn when game finishes
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the button's position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which player's turn it 
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set btn txt to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the players turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a three line straight
        /// </summary
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for horizontal wins
            //
            // - Row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Cyan;
            }

            //
            // - Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Cyan;
            }

            //
            // - Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Cyan;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical wins
            //
            // - Column 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Cyan;
            }

            //
            // - Column 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Cyan;
            }

            //
            // - Column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Cyan;
            }
            #endregion

            #region Diagonal Wins
            // Che for diagonal wins
            //
            // - Top Left to Bottom Right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Cyan;
            }

            //
            // - Top Right to Bottom Left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells Cyan
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Cyan;
            }
            #endregion

            #region No Winners
            // Check for now winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game ends
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}