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

namespace tictactoe_1._0
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        public Pole pl = new Pole(64, 36);
        public string xo = "X";
        public MainWindow()
        {
            InitializeComponent();
           
            
            for (int i = 0; i < 64; i++)
            {
                for (int a = 0; a < 36; a++)
                {
                    Button btn = new Button();
                    btn.Height = 29;
                    btn.Width = 29;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.Click += new RoutedEventHandler(Button_Click);
                    btn.Content = "  ";
                    btn.Tag = $"{i}_{a}";
                    btn.SetValue(Grid.RowProperty, a);
                    btn.SetValue(Grid.ColumnProperty, i);
                    grid.Children.Add(btn);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Button btn = sender as Button;
            String[] spltag =  btn.Tag.ToString().Split('_');
            int x = Convert.ToInt32(spltag[0]);
            int y = Convert.ToInt32(spltag[1]);
            if (btn.Name != "end")
            {
            btn.Content = xo;
            btn.Name = "end";
                if (pl.Move( x, y, xo))
                {
                    string ox = "O";
                    foreach (Button item in grid.Children)
                    {
                        item.Content = " ";
                        item.Name = "btn";
                        xo = ox;
                        if (ox == "X") { ox = "O"; }
                        else { ox = "X"; }
                    }
                }
                if (xo == "X") { xo = "O"; }
                else { xo = "X"; }                
            }
            if (btn.Content.ToString()=="X")
            {
                btn.Foreground = Brushes.Tomato;
            }
            if (btn.Content.ToString() == "O")
            {
                btn.Foreground = Brushes.Blue;
            }
            
        }
    }

   public class Pole
    {
        string[,] pole;
            
       public Pole(int xWidth,int yHeight)
        {
            pole = new string[xWidth, yHeight];
        }

       public bool Move(int x, int y, string cont)
       {
            pole[x, y] = cont;
            return WinChecker(x, y, cont);
       }

        private bool WinChecker(int x, int y, string cont)
        {
            if (pole[x-1,y-1]==cont||pole[x+1,y+1]==cont)
            {
                return LenghtChecker(x,y,-1,-1,cont);
            }

            if (pole[x, y - 1] == cont || pole[x, y + 1] == cont)
            {
                return LenghtChecker(x, y, 0, -1, cont);
            }

            if (pole[x + 1, y - 1] == cont || pole[x - 1, y + 1] == cont)
            {
                return LenghtChecker(x, y, 1, -1, cont);
            }

            if (pole[x - 1, y] == cont || pole[x + 1, y] == cont)
            {
                return LenghtChecker(x, y, -1,0, cont);
            }
            return false;
        }

        private bool LenghtChecker(int x, int y, int xc, int yc,string cont)
        {
            int xs = x;
            int ys = y;
            int contLenght = 1;
           

             while (pole[xs + xc, ys + yc] == cont)
             {
                contLenght++;
                xs += xc;
                ys += yc;
             }

            xs = x;
            ys = y;

            while (pole[xs - xc, ys - yc] == cont)
            {
                contLenght++;
                xs -= xc;
                ys -= yc;
            }

            if (contLenght == 5)
            {
                MessageBox.Show($"Hráč ktarý hrál s {cont} vyhrál :D");
                Array.Clear(pole, 0, pole.Length);
                return true;
               
            }
            else { return false; }
        }
    }





}
