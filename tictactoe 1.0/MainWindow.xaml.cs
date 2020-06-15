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

        public bool AImove = true;
        public Pole pl = new Pole(3, 3);
        public string xo = "X";
        public int wincountpl = 0;
        public int wincountAI = 0;
        public MainWindow()
        {
            InitializeComponent();


            for (int i = 0; i < 3; i++)
            {
                for (int a = 0; a < 3; a++)
                {
                    Button btn = new Button();
                    btn.Height = 250;
                    btn.Width = 250;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.Click += new RoutedEventHandler(Button_Click);
                    btn.Content = " ";
                    btn.FontSize = 80;
                    btn.Tag = $"{i}_{a}";
                    btn.SetValue(Grid.RowProperty, a);
                    btn.SetValue(Grid.ColumnProperty, i);
                    grid.Children.Add(btn);
                }
            }
        }

        public void BClick(string t)
        {
            
            foreach (Button item in grid.Children)
            {
               
                if ((string)item.Tag==t)
                {
                    Button_Click(item, new RoutedEventArgs());
                }
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            bool winmovecheck = false;
            Button btn = sender as Button;
            String[] spltag =  btn.Tag.ToString().Split('_');
            int x = Convert.ToInt32(spltag[0]);
            int y = Convert.ToInt32(spltag[1]);
            if (btn.Name != "end")
            {
            btn.Content = xo;
            btn.Name = "end";
            pl.Move(x, y, xo);

                if (pl.WinChecker(x, y, xo,pl.pole)=="X"|| pl.WinChecker(x, y, xo,pl.pole) =="O")
                {

                    winmovecheck = true;
                    //if (AImove)
                    //{
                    //    wincountAI++;
                        
                    //}
                    //else
                    //{
                    //    wincountpl++;
                    //}
                    MessageBox.Show($"Hráč ktarý hrál s {pl.WinChecker(x, y, xo,pl.pole)} vyhrál :D");
                    Array.Clear(pl.pole, 0, pl.pole.Length);
                    foreach (Button item in grid.Children)
                    {
                        item.Content = " ";
                        item.Name = "btn";          
                    }
                }
                else if (pl.WinChecker(x, y, xo, pl.pole) == "  ")
                {

                            MessageBox.Show($"Remíza :D");
                    winmovecheck = true;
                    foreach (Button item in grid.Children)
                             {
                                 item.Content = " ";
                                 item.Name = "btn";
                             }
                    Array.Clear(pl.pole, 0, pl.pole.Length);
                }

                if (xo == "X") { xo = "O"; }
                else { xo = "X"; }
            
               if (AImove & !winmovecheck)
               {
                   AImove = false;
                   BClick(pl.AIMove(xo));
               }
               else
               {
                   AImove = true;
               }
               if (winmovecheck)
               {
                   AImove = true;
               }

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
       public string[,] pole;
            
       public Pole(int xWidth,int yHeight)
        {
            pole = new string[xWidth, yHeight];
        }

       public void Move(int x, int y, string cont)
       {
         
               pole[x, y] = cont;     
                                
       }

       public string AIMove (string cont)
       {
            int score = 0;
            Point p = new Point();
          string[,] tpole = pole;
            int Minv = int.MinValue;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (tpole[x, y] == null)
                    {
                        tpole[x, y] = "cont";
                        score = Minimax(tpole, true,cont);
                        tpole[x, y] = null;
                        if (score>Minv)
                        {
                            Minv = score;
                            p.X = x;
                            p.Y = y;
                        }
                    }
                }
            }
            return $"{p.X}_{p.Y}";

        }

        int Minimax(string[,] tpole,bool isMax,string cont)
        {
            int score = 0;
            int Minv = int.MinValue;
            int Maxv = int.MaxValue;
            string acont = "";
            if (cont == "O") { acont = "X"; }
            else { acont = "O"; }

            if (isMax)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (tpole[x, y] == null)
                        {
                            if (WinChecker(x, y, cont, tpole) == cont)
                            {
                                return +1;
                            }
                            if (WinChecker(x, y, acont, tpole) == acont)
                            {
                                return -1;
                            }
                            if (WinChecker(x, y, cont, tpole) == "  ")
                            {
                                return 0;
                            }
                            if (WinChecker(x, y, acont, tpole) == "  ")
                            {
                                return 0;
                            }

                        }
                    }
                }


                  for (int x = 0; x < 3; x++)
                  {
                      for (int y = 0; y < 3; y++)
                      {

                          if (tpole[x, y] == null)
                          {
                              tpole[x, y] = cont;
                              score = Minimax(tpole, false,cont);
                              tpole[x, y] = null;
                              if (score > Minv)
                              {
                                  Minv = score;
                              }
                          }
                      }
                  }
                  return Minv; 
            }
            else 
            {

                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (tpole[x, y] == null)
                        {
                            if (WinChecker(x, y, cont, tpole) == cont)
                            {
                                return +1;
                            }
                            if (WinChecker(x, y, acont, tpole) == acont)
                            {
                                return -1;
                            }
                            if (WinChecker(x, y, cont, tpole) == "  ")
                            {
                                return 0;
                            }
                            if (WinChecker(x, y, acont, tpole) == "  ")
                            {
                                return 0;
                            }

                        }
                    }
                }



                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (tpole[x, y] == null)
                        {
                            tpole[x, y] = acont;
                            score = Minimax(tpole, true,cont);
                            tpole[x, y] = null;
                            if (score < Maxv)
                            {
                                Maxv = score;
                            }
                        }
                    }
                }
                return Maxv;
            }

        }


        public string WinChecker(int x, int y, string cont,string[,] pole)
        {

            if (x - 1 >= 0 & y - 1 >= 0)
            {
                     if (pole[x - 1, y - 1] == cont )
                     {
                      return LenghtChecker(x, y, -1, -1, cont);
                     }
            }
             

            if (x + 1 < 3 & y + 1 < 3)
            {
                if (pole[x + 1, y + 1] == cont)
                {
                     return LenghtChecker(x, y, -1, -1, cont);
                }
            }


            if (y + 1 < 3)
            {
               if (pole[x, y + 1] == cont)
               {
                   return LenghtChecker(x, y, 0, -1, cont);
               }
            }


            if (y - 1 >= 0 )
            {
                if (pole[x, y - 1] == cont )
                {
                    return LenghtChecker(x, y, 0, -1, cont);
                }
            }

            if (x + 1 < 3 & y - 1 >= 0)
            {
                if (pole[x + 1, y - 1] == cont)
                {
                   return LenghtChecker(x, y, 1, -1, cont);
                }
            }

            if (x - 1 >= 0 & y + 1 < 3)
            {
               if (pole[x - 1, y + 1] == cont)
               {
                   return LenghtChecker(x, y, 1, -1, cont);
               }
            }


            if (x - 1 >= 0 )
            {
                   if (pole[x - 1, y] == cont )
                   {
                   return LenghtChecker(x, y, -1, 0, cont);
                   }
            }




            if (x + 1 < 3)
            {
               if ( pole[x + 1, y] == cont)
               {
                   return LenghtChecker(x, y, -1,0, cont);
               }
            }




           
      
            return " ";
        }

        private string LenghtChecker(int x, int y, int xc, int yc,string cont)
        {
            int xs = x;
            int ys = y;
            int contLenght = 1;
            bool bl = true;
           

             while (bl)
             {
                bl = false;
                if (xs + xc >= 0 & ys + yc >= 0 & xs + xc < 3 & ys + yc < 3)
                {
                    if (pole[xs + xc, ys + yc] == cont)
                    {
                        bl = true;
                        contLenght++;
                        xs += xc;
                        ys += yc;
                    }
                }

             }

            xs = x;
            ys = y;
            bl = true;

            while (bl)
            {
                bl = false;
                if (xs - xc >= 0 & ys - yc >= 0 & xs - xc < 3 & ys - yc < 3)
                {
                    if (pole[xs - xc, ys - yc] == cont)
                    {
                        bl = true;
                        contLenght++;
                        xs -= xc;
                        ys -= yc;
                    }
                }
                     
            }
            int count = 0;
            foreach (string item in pole)
            {
                if (item=="X"||item=="O")
                {
                    count++;
                }
            }
          
            if (contLenght == 3)
            {
               
                
                return cont;
               
            }
            if (count ==9)
            {
                return "  ";
            }
           return " ";
        }
    }





}
