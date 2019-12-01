using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members
        /// <summary>
        /// Holds the curremnt results of cells in the active game
        /// </summary>
        private MarkType[] results;

        /// <summary>
        /// True if it is player 1's turn (X) or players 2's turn (O)
        /// </summary>
        private bool player1Turn;

        /// <summary>
        /// True if games has ended
        /// </summary>
        private bool gameEnded;

        #endregion
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        private void NewGame()
        {
            results = new MarkType[9];
            Array.ForEach(results, r => r = MarkType.Free);
            player1Turn = true;
            Container.Children.Cast<Button>().ToList().ForEach(b =>
            {
                b.Content = "";
                b.Background = Brushes.White;
                b.Foreground = Brushes.Blue;
            });
            gameEnded = false;
        }
        /// <summary>
        ///  Handels a button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(gameEnded)
            {
                NewGame();
                return;
            }
            Button button = (Button)sender;
            int columnNumber = Grid.GetColumn(button);
            int rowNumber = Grid.GetRow(button);

            int index = columnNumber + (rowNumber * 3);

            if (results[index] != MarkType.Free)
                return;

            results[index] = player1Turn ? MarkType.Cross : MarkType.Nought;
            button.Content = player1Turn ? "X" : "O";

            if (!player1Turn)
                button.Foreground = Brushes.Red;

            player1Turn ^= true;
            CheckForAWinner();

        }

        public void CheckForAWinner()
        {
            if (results[0] != MarkType.Free && (results[0] & results[1] & results[2]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            if (results[3] != MarkType.Free && (results[3] & results[4] & results[5]) == results[3])
            {
                gameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            if (results[6] != MarkType.Free && (results[6] & results[7] & results[8]) == results[6])
            {
                gameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }


            if (results[0] != MarkType.Free && (results[0] & results[3] & results[6]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            if (results[1] != MarkType.Free && (results[1] & results[4] & results[7]) == results[1])
            {
                gameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            if (results[2] != MarkType.Free && (results[2] & results[5] & results[8]) == results[2])
            {
                gameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (results[0] != MarkType.Free && (results[0] & results[4] & results[8]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (results[2] != MarkType.Free && (results[2] & results[4] & results[6]) == results[2])
            {
                gameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }





            if (!results.Any(r => r == MarkType.Free))
            {
                gameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(b =>
                {
                    b.Background = Brushes.Orange;
                });
            }
        }
    }
}
