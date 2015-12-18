using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf.Tenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool circleTurn;
        public char[,] board = new char[3,3];
        bool failedAttempt = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        //Lägger ut knapparna på spelbordet (värdet):
        void Btn1_OnClick(object sender, RoutedEventArgs e)
        {
            Button(2, 0, board);
        }

        private void Btn2_OnClick(object sender, RoutedEventArgs e)
        {
            Button(2, 1, board);
        }

        private void Btn3_OnClick(object sender, RoutedEventArgs e)
        {
            Button(2, 2, board);
        }

        private void Btn4_OnClick(object sender, RoutedEventArgs e)
        {
            Button(1, 0, board);
        }

        private void Btn5_OnClick(object sender, RoutedEventArgs e)
        {
            Button(1, 1, board);
        }

        private void Btn6_OnClick(object sender, RoutedEventArgs e)
        {
            Button(1, 2, board);
        }

        private void Btn7_OnClick(object sender, RoutedEventArgs e)
        {
            Button(0, 0, board);
        }

        private void Btn8_OnClick(object sender, RoutedEventArgs e)
        {
            Button(0, 1, board);
        }

        private void Btn9_OnClick(object sender, RoutedEventArgs e)
        {
            Button(0, 2, board);
        }

        //Kollar om det är X eller O som sätts ut och sätter vem som ska lägga härnäst:
        void Button(int p1, int p2, char[,] board)
        {
            if (InputToGrid(p1, p2, board, circleTurn))
            {
                circleTurn = !circleTurn;
            }

            SetTurnMsg(circleTurn);
            SetButtons();

            if (StateCheck() && !failedAttempt)
            {
                failedAttempt = true;
                return;
            }

            if (StateCheck() && failedAttempt)
            {
                Reset();
                return;
            }
        }



        void SetButtons()
        {
            //Topp
            Btn1.Content = board[2, 0];
            Btn2.Content = board[2, 1];
            Btn3.Content = board[2, 2];
            //Mitten
            Btn4.Content = board[1, 0];
            Btn5.Content = board[1, 1];
            Btn6.Content = board[1, 2];
            //Botten
            Btn7.Content = board[0, 0];
            Btn8.Content = board[0, 1];
            Btn9.Content = board[0, 2];
        }

        //Skriver ut vems tur det är
        void SetTurnMsg(bool noughtTurn)
        {
            WhosTurn.Content = "";
            if (noughtTurn)
                WhosTurn.Content += "Cirkel";
            else
                WhosTurn.Content += "Kryss";
        }

        //Resettar spelet (Nytt spel)
        void Reset()
        {
            board = new char[3, 3];
            SetTurnMsg(circleTurn);
            SetButtons();
            failedAttempt = false;
        }


        //Kollar vinnare eller om det blev lika (resultatet)
        bool StateCheck()
        {
            switch (CheckBoard(board))
            {
                case 'F':
                    MessageBox.Show("Lika!");
                    return true;
                case 'X':
                    MessageBox.Show("Kryss vann!");
                    return true;
                case 'O':
                    MessageBox.Show("Cirkel vann!");
                    return true;
            }
            return false;
        }

        static bool InputToGrid(int p1, int p2, char[,] grid, bool isCircelTurn)
        {
            if (grid[p1, p2] == '\0')
            {
                if (isCircelTurn)
                    grid[p1, p2] = 'O';
                else
                    grid[p1, p2] = 'X';

                return true;
            }
            return false;
        }

        static char OX(char c)
        {
            if (c.ToString().ToUpper() == "X")
            {
                return 'X';
            }
            else if (c.ToString().ToUpper() == "O")
            {
                return 'O';
            }
            return ' ';
        }

        //Kontrollerar hur spelplanen ser ut och utser vinnare enligt 3-i-rad reglerna
        
        static char CheckBoard(char[,] board)
        {
            if ((OX(board[1, 1]) == "O"[0] || OX(board[1, 1]) == "X"[0]) && (board[2, 2] == board[1, 1] && board[1, 1] == board[0, 0]))
            {
                return OX(board[1, 1]);
            }
            else if ((OX(board[1, 1]) == "O"[0] || OX(board[1, 1]) == "X"[0]) && (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2]))
            {
                return OX(board[1, 1]);
            }
            else if ((OX(board[2, 1]) == "O"[0] || OX(board[2, 1]) == "X"[0]) && (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2]))
            {
                return OX(board[2, 1]);
            }
            else if ((OX(board[1, 1]) == "O"[0] || OX(board[1, 1]) == "X"[0]) && (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2]))
            {
                return OX(board[1, 1]);
            }
            else if ((OX(board[0, 1]) == "O"[0] || OX(board[0, 1]) == "X"[0]) && (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2]))
            {
                return OX(board[0, 1]);
            }
            else if ((OX(board[1, 0]) == "O"[0] || OX(board[1, 0]) == "X"[0]) && (board[2, 0] == board[1, 0] && board[1, 0] == board[0, 0]))
            {
                return OX(board[1, 0]);
            }
            else if ((OX(board[1, 1]) == "O"[0] || OX(board[1, 1]) == "X"[0]) && (board[2, 1] == board[1, 1] && board[1, 1] == board[0, 1]))
            {
                return OX(board[1, 1]);
            }
            else if ((OX(board[1, 2]) == "O"[0] || OX(board[1, 2]) == "X"[0]) && (board[2, 2] == board[1, 2] && board[1, 2] == board[0, 2]))
            {
                return OX(board[1, 2]);
            }

            //Räknar antal satta spelknappar och returnerar F (fullt) om det är nio eller mer
            int filledTiles = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != '\0')
                    {
                        filledTiles += 1;
                    }
                }
            }
            
            if (filledTiles >= 9)
            {
                return 'F';
            }

            return " "[0];
        }

        //Nytt spel i menyn
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        //Avslutar programmet (här var jag osäker på om du ville avsluta omgången men antog att du ville ha en stänga ner spelet knapp -
        // eftersom man får en ny omgång om man använder metoden ovan)
        private void MenuItemAvsluta_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
