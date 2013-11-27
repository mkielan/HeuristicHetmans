using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using TheHeuristicHetmans.Core;

namespace TheHeuristicHetmans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProblemOfHetmans _problem;
        private delegate void UpdateBorder(Board board);

        public MainWindow()
        {
            InitializeComponent();

            _problem = new ProblemOfHetmans();
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            int value;

            if (int.TryParse(txbHetmansCount.Text, out value))
            {
                if (value == 2)
                {
                    MessageBox.Show("Dla liczby 2 nie można znaleźć poprawnego rozwiązania!");
                    return;
                }
                btnSolve.IsEnabled = false;
                txbHetmansCount.IsEnabled = false;

                var thread = new Thread(t =>
                {
                    var result = _problem.Solve(value);

                    if (result != null)
                    {
                        SetBorder(result.Board);
                        MessageBox.Show(string.Format("Rozwiązanie znaleziono w {0} krokach, z {1} powrotami!", result.StepCount, result.BackCount));
                    }
                    else
                    {
                        MessageBox.Show("Nieudało się znaleźć rozwiązania!");
                    }
                });
                thread.Start();
            }
            else {
                MessageBox.Show("Podaj poprawną liczbę całkowitą!");
            }
        }

        private void SetBorder(Board board)
        {
            if (Dispatcher.CheckAccess())
            {
                #region set border
                var n = board.Length;

                if (n != dgdChessboard.Columns.Count)
                {
                    dgdChessboard.Columns.Clear();

                    for (var i = 0; i < n; i++)
                    {
                        var column = new DataGridTextColumn();
                        column.Header = (i + 1).ToString();
                        column.Binding = new Binding("Cells[" + (i).ToString() + "]");
                        dgdChessboard.Columns.Add(column);
                    }
                }

                dgdChessboard.Items.Clear();
                for (var i = n - 1; i >= 0; i--)
                {
                    var cells = new string[n];

                    for (var j = 0; j < n; j++)
                    {
                        //cells[j] = (board.Spacing[i, j]) ? "+" : "";
                        cells[j] = (board.Spacing[i] == j) ? "+" : "";
                    }

                    var row = new GridItem { Cells = cells };
                    dgdChessboard.Items.Add(row);
                }

                #endregion

                btnSolve.IsEnabled = true;
                txbHetmansCount.IsEnabled = true;
            }
            else
            {
                Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal
                    , new UpdateBorder(SetBorder)
                    , board
                    );
            }
        }

        private void dgdChessboard_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (dgdChessboard.Items.Count - e.Row.GetIndex()).ToString(); 
        }
    }
}
