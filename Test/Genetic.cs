using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Genetic : Form
    {
        public Genetic()
        {
            InitializeComponent();
        }
        int[] fit = new int[3000];
        string[] Chromosome = new string[3000];
        int R = 0;




        private void button1_Click(object sender, EventArgs e)
        {

            int[] array = new int[8];


            Random rand = new Random();

            string str = "";


            for (int j = 0; j < 3000; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    int help = rand.Next(1, 9);
                    array[i] = help;
                    str += array[i].ToString();
                }

                int h = Fitness(array);

                listBox1.Items.Add(str);
                listBox2.Items.Add(h.ToString());
                Chromosome[j] = str;


                fit[j] = h;
                str = "";

            }
            this.button2.PerformClick();

            if (R != 0)
                this.button3.PerformClick();

            R++;
            button1.Enabled = false;
        }



        //============================================
        private int Fitness(int[] arr)
        {

            int worth = 0;

            int[] NumberOfQueenInColumn = new int[8];
            int[] LeftToRight = new int[8];
            int[] RightToLeft = new int[8];
            int i = 0, j = 0;
            int ColumnGuard = 0;
            int LeftDiameterGuard = 0;
            int RightDiameterGuard = 0;
            int c = 0, r = 0;

            int temp = 0;
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                    if (arr[j] == i + 1)
                        temp++;
                NumberOfQueenInColumn[i] = temp;
                temp = 0;
            }



            for (i = 0; i < 8; i++)
                if (NumberOfQueenInColumn[i] > 1)
                    for (j = 1; j < NumberOfQueenInColumn[i]; j++)
                        ColumnGuard += j;


            for (i = 0; i < 8; i++)
            {
                r = i + 1;
                c = arr[i];
                LeftToRight[i] = c - r;
                RightToLeft[i] = c + r;
            }
            //===================================
            temp = 0;
            for (i = -6; i <= 6; i++)
            {
                for (j = 0; j < 8; j++)
                    if (LeftToRight[j] == i)
                        temp++;

                if (temp > 1)
                    for (int f = 1; f < temp; f++)
                        LeftDiameterGuard += f;

                temp = 0;

            }
            //===================================

            temp = 0;
            for (i = 3; i <= 15; i++)
            {
                for (j = 0; j < 8; j++)
                    if (RightToLeft[j] == i)
                        temp++;

                if (temp > 1)
                    for (int f = 1; f < temp; f++)
                        RightDiameterGuard += f;

                temp = 0;

            }


            worth = ColumnGuard + LeftDiameterGuard + RightDiameterGuard;
            return worth;
        }
        //==========================================================================
        private void button2_Click(object sender, EventArgs e)
        {
            Sort();
            listBox2.Items.Clear();
            listBox1.Items.Clear();

            for (int i = 0; i < 3000; i++)
            {
                listBox2.Items.Add(fit[i].ToString());
                listBox1.Items.Add(Chromosome[i]);
            }
        }
        //===============================================================================================
        private void button3_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            button3.Enabled = false;
            int Generation = 1;
            for (int p = 1; p < 11; p++)
            {
                int status = 0;
                int i, j = 0;
                int[] arry = new int[8];

                for (i = 1500; i < 3000; i++)
                    Chromosome[i] = "";



                string[] str = new string[1500];

                for (i = 0; i < 1500; i += 2)
                {
                    str[i] = Chromosome[i].Substring(0, 4) + Chromosome[i + 1].Substring(4, 4);
                    str[i + 1] = Chromosome[i + 1].Substring(0, 4) + Chromosome[i].Substring(4, 4);
                }


                for (i = 1500; i < 3000; i++)
                    Chromosome[i] = str[i - 1500];

                for (i = 0; i < 3000; i++)
                {
                    arry = new int[8];
                    for (j = 0; j < 8; j++)
                        arry[j] = int.Parse(Chromosome[i].Substring(j, 1));

                    int h = Fitness(arry);
                    fit[i] = h;
                    if (fit[i] == 0)
                        status++;
                }


                Sort();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                for (i = 0; i < 3000; i++)
                {
                    listBox3.Items.Add(Chromosome[i]);
                    listBox4.Items.Add(fit[i].ToString());
                }

                listBox3.Items.Add("============");
                listBox4.Items.Add("============");



                if (status != 0)
                {
                    Generation = p;
                    SettingQueens();
                    break;
                }
                Generation = p + 1;
            }

            if (Generation == 11)
            {
                fit = new int[3000];
                Chromosome = new string[3000];
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                button3.Enabled = true;
                button1.Enabled = true;
                this.button1.PerformClick();
            }

            Application.DoEvents();
        }
        //==========================================================================================
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //================================================================
        private void Sort()
        {
            for (int i = 0; i < 3000; i++)
                for (int j = i; j < 3000; j++)
                    if (fit[i] > fit[j])
                    {
                        int help = fit[i];
                        fit[i] = fit[j];
                        fit[j] = help;

                        string temp = Chromosome[i];
                        Chromosome[i] = Chromosome[j];
                        Chromosome[j] = temp;
                    }
        }
        //================================================================

        private void SettingQueens()
        {
            int[] array = new int[8];
            int gapEdge = pictureBox1.Left + 43;
            int beetweenSpace = 56;
            string str = "46831752";

            for (int i = 0; i < 8; i++)
                array[i] = int.Parse(str.Substring(i, 1)) - 1;

            Queen1.Left = gapEdge + array[0] * beetweenSpace;
            Queen2.Left = gapEdge + array[1] * beetweenSpace;
            Queen3.Left = gapEdge + array[2] * beetweenSpace;
            Queen4.Left = gapEdge + array[3] * beetweenSpace;
            Queen5.Left = gapEdge + array[4] * beetweenSpace;
            Queen6.Left = gapEdge + array[5] * beetweenSpace;
            Queen7.Left = gapEdge + array[6] * beetweenSpace;
            Queen8.Left = gapEdge + array[7] * beetweenSpace;

            if (array[0] % 2 != 0)
                Queen1.BackColor = Color.FromArgb(128, 64, 0);

            if (array[1] % 2 == 0)
                Queen2.BackColor = Color.FromArgb(128, 64, 0);

            if (array[2] % 2 != 0)
                Queen3.BackColor = Color.FromArgb(128, 64, 0);

            if (array[3] % 2 == 0)
                Queen4.BackColor = Color.FromArgb(128, 64, 0);

            if (array[4] % 2 != 0)
                Queen5.BackColor = Color.FromArgb(128, 64, 0);

            if (array[5] % 2 == 0)
                Queen6.BackColor = Color.FromArgb(128, 64, 0);

            if (array[6] % 2 != 0)
                Queen7.BackColor = Color.FromArgb(128, 64, 0);

            if (array[7] % 2 == 0)
                Queen8.BackColor = Color.FromArgb(128, 64, 0);


        }
        private void Genetic_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
