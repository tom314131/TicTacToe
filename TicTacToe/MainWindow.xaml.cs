using System;
using System.Linq;
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
        #region Private Members

        /// <summary>
        /// Holds the current result of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player1's turn (X) or player2's  turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mgameEnded;


        #endregion

        #region Constructor
        /// <summary>
        /// Default Ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // create new blank array of free cells
            mResults = new MarkType[9];
            for(var i = 0; i<mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            // make sure player 1 start the game
            mPlayer1Turn = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            //make sure the game hasn't finished
            mgameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game on the click after it finished
            if(mgameEnded)
            {
                NewGame();
                return;
            }

            //cast a sender to button
            var button = (Button)sender;

            //fiind the position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell has a value in it
            if(mResults[index] != MarkType.Free)
            {
                return;
            }

            //Set the cell value based on which player turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Set the button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            //change noght to green
            if(!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Toggle the player turn
            mPlayer1Turn ^= true;

            CheckForWin();
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWin()
        {
            //Check for horizontal wins

            //
            // Row 0
            //
            if (mResults[0]!= MarkType.Free && (mResults[0]==mResults[1] && mResults[0]==mResults[2]))
            {
                mgameEnded = true;
                
                //Highlight winning cell in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //
            // Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] == mResults[4] && mResults[3] == mResults[5]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //
            // Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] == mResults[7] && mResults[6] == mResults[8]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //Check for vertical wins

            //
            // Col 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] == mResults[3] && mResults[0] == mResults[6]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //
            // Col 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] == mResults[4] && mResults[1] == mResults[7]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //
            // Col 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] == mResults[5] && mResults[2] == mResults[8]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }


            //Check diagonal win
            if (mResults[0] != MarkType.Free && (mResults[0] == mResults[4] && mResults[0] == mResults[8]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            if (mResults[2] != MarkType.Free && (mResults[2] == mResults[4] && mResults[2] == mResults[6]))
            {
                mgameEnded = true;

                //Highlight winning cell in green
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
            }

            //Check for no winner
            if (!mResults.Any(result => result == MarkType.Free))
            {
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            } 

        }
    }
}
