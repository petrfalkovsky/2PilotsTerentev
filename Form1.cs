using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PiliotsSafe
{
    public partial class Form1 : Form
    {
        int n;
        string s = "Внимание!";  // строка для заголовка диалога 

        public Form1()
        {
            InitializeComponent();

            label1.Font = new Font("Times New Roman", 10, FontStyle.Bold); // вынесенный стиль, можно убрать            
            label1.ForeColor = System.Drawing.Color.Transparent;
            // выбор уровней, можно придумать усложнение, только спрайты уменьшить не забыть, чтобы входили на экран
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("5");
            comboBox1.Items.Add("6");
        }

        private PictureBox[,] pb = null;
        private void button1_Click(object sender, EventArgs e) // кнопка запуска
        {
            if (pb != null)
                foreach (var pictureBox in pb)
                    Controls.Remove(pictureBox);

            if (int.TryParse(comboBox1.Text, out n))
            {
                n = Convert.ToInt32(comboBox1.Text);
            }
            // проверка на уровень сложности
            if (n > 6 || n <= 2)
            {
                MessageBox.Show("Выберите 3, 4, 5 или 6 уровнь сложности.", s, MessageBoxButtons.OK);
            }
            // иначе загружаем бокс
            else
            {
                pb = new PictureBox[n, n];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        pb[i, j] = new PictureBox();
                        pb[i, j].Location = new System.Drawing.Point(20 + i * 100, 50 + j * 100);
                        pb[i, j].Size = new System.Drawing.Size(63, 63);
                        pb[i, j].TabIndex = i;
                        pb[i, j].Load("../../Resources/sprite3.png");
                        pb[i, j].Tag = 0;
                        Controls.Add(pb[i, j]);
                        int row = i;
                        int column = j;
                        pb[i, j].Click += (x, y) => { pbClick(row, column); };
                    }
                Rotation();
            }
        }
       

        private void Rotation()
        {
            var rnd = new Random();
            for (int i = 0; i < 50; i++)
                pbClick(rnd.Next(n), rnd.Next(n), true);
        }

        private bool pbClick(int row, int column, bool init = false) // цикл с проверкой на совпадение
        {
            bool win = !init;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == row || j == column)
                    {
                        Image image = pb[i, j].Image;
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        pb[i, j].Image = image;

                        pb[i, j].Tag = ((int)pb[i, j].Tag + 1) % 2;
                    }
                    win &= (int)pb[i, j].Tag == (int)pb[0, 0].Tag;
                }

            if (win)
                MessageBox.Show("Ура! У вас получилось!!!", s, MessageBoxButtons.OK);
            return win;
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            this.BackColor = Color.Silver;
            this.Left = 350;
            this.Top = 40;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) // todo: форма перед закрытием (доделать)
        {
            DialogResult dr = MessageBox.Show("Точно закрываем?\nМы пока не научились сохранять ваши достижения =(", s, MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      
    }
}
