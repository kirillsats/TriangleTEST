using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Triangle1
{
    public partial class Form2 : Form
    {
        Button btn;
        TextBox txtA, txtH, txtB, txtC; // Добавлены текстовые поля для сторон
        ListView listView1;
        PictureBox trianglePicture; // Сделано полем класса

        public Form2()
        {
            // Свойства формы
            this.Height = 800;
            this.Width = 900;
            this.Text = "Töö kolmnurgaga";
            this.BackColor = Color.LightBlue; // Устанавливаем голубой фон

            // Кнопка
            btn = new Button();
            btn.Text = "Käivitamine";
            btn.Font = new Font("Arial", 28);
            btn.AutoSize = true;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Location = new Point(320, 100);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            Controls.Add(btn);
            btn.Click += Btn_Click;

            // Метка для txtA
            Label lblA = new Label();
            lblA.Text = "alus:";
            lblA.Location = new Point(220, 200);
            lblA.Font = new Font("Arial", 10);
            Controls.Add(lblA);

            // TextBox - txtA
            txtA = new TextBox();
            txtA.Location = new Point(320, 200);
            txtA.Font = new Font("Arial", 10);
            txtA.Width = 200;
            txtA.BackColor = Color.MistyRose; // цвет
            Controls.Add(txtA);

            // TextBox - txtH
            txtH = new TextBox();
            txtH.Location = new Point(320, 320);
            txtH.Font = new Font("Arial", 10);
            txtH.Width = 200;
            txtH.BackColor = Color.MistyRose; // цвет
            Controls.Add(txtH);

            // Метка для txtH
            Label lblH = new Label();
            lblH.Text = "Kõrgus:";
            lblH.Location = new Point(220, 320);
            lblH.Font = new Font("Arial", 10);
            Controls.Add(lblH);

            // TextBox - txtB (сторона B)
            txtB = new TextBox();
            txtB.Location = new Point(320, 440);
            txtB.Font = new Font("Arial", 10);
            txtB.Width = 200;
            txtB.BackColor = Color.MistyRose; // цвет
            Controls.Add(txtB);

            // Метка для txtB
            Label lblB = new Label();
            lblB.Text = "külg B:";
            lblB.Location = new Point(220, 440);
            lblB.Font = new Font("Arial", 10);
            Controls.Add(lblB);

            // TextBox - txtC (сторона C)
            txtC = new TextBox();
            txtC.Location = new Point(320, 560);
            txtC.Font = new Font("Arial", 10);
            txtC.Width = 200;
            txtC.BackColor = Color.MistyRose; // цвет
            Controls.Add(txtC);

            // Метка для txtC
            Label lblC = new Label();
            lblC.Text = "külg C:";
            lblC.Location = new Point(220, 560);
            lblC.Font = new Font("Arial", 10);
            Controls.Add(lblC);

            // ListView
            listView1 = new ListView();
            listView1.Location = new Point(100, 650);
            listView1.Font = new Font("Arial", 10);
            listView1.Width = 400;
            listView1.Height = 200;
            Controls.Add(listView1);

            // PictureBox для отображения треугольника
            trianglePicture = new PictureBox();
            trianglePicture.Location = new Point(600, 200); // Позиция картинки на форме
            trianglePicture.Size = new Size(200, 200); // Размер картинки
            Controls.Add(trianglePicture);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Читаем значения из полей ввода
                double a = Convert.ToDouble(txtA.Text);
                double h = Convert.ToDouble(txtH.Text);
                double b = string.IsNullOrWhiteSpace(txtB.Text) ? 0 : Convert.ToDouble(txtB.Text);
                double c = string.IsNullOrWhiteSpace(txtC.Text) ? 0 : Convert.ToDouble(txtC.Text);

                // Проверяем существование треугольника по основанию и высоте
                if (a <= 0 || h <= 0)
                {
                    MessageBox.Show("Sellist kolmnurka ei ole! (Osad väärtused on negatiivsed või null)");
                    return;
                }

                Triangle1 triangle = new Triangle1(a, h);

                // Если введены только основание и высота — выводим площадь
                listView1.Items.Clear();
                listView1.View = View.Details;
                listView1.Columns.Clear();
                listView1.Columns.Add("Nimi", 150);
                listView1.Columns.Add("Väärtus", 150);
                listView1.Items.Add(new ListViewItem(new[] { "alus", a.ToString() }));
                listView1.Items.Add(new ListViewItem(new[] { "Kõrgus", h.ToString() }));
                listView1.Items.Add(new ListViewItem(new[] { "Pindala", triangle.Area().ToString() }));

                // Если все три стороны введены, определяем тип треугольника
                if (b > 0 && c > 0)
                {
                    // Используем метод для определения типа по трём сторонам
                    string triangleType = triangle.GetTriangleType();
                    listView1.Items.Add(new ListViewItem(new[] { "Tüüp", triangleType }));

                    // Обновляем изображение
                    UpdateTriangleImage(triangleType);
                }
                else
                {
                    // Если введены только основание и высота — определяем тип по этим данным
                    string triangleType = triangle.GetTriangleTypeFromBaseAndHeight();
                    listView1.Items.Add(new ListViewItem(new[] { "Tüüp", triangleType }));

                    // Обновляем изображение
                    UpdateTriangleImage(triangleType);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Palun sisestage andmed õigesti!");
            }
        }




        private void UpdateTriangleImage(string triangleType)
        {
            try
            {
                // Словарь для сопоставления типов треугольников с полными путями файлов
                var imagePaths = new Dictionary<string, string>
        {
            { "Võrdkülgne", @"C:\Images\equilateral_triangle.png" },
            { "Võrdhaarsed", @"C:\Images\isosceles_triangle.png" },
            { "Ristkülikukujuline", @"C:\Images\right_triangle.png" },
            { "nüri", @"C:\Images\obtuse_triangle.png" },
            { "Teravnurkne", @"C:\Images\acute_triangle.png" },
            { "Mitmekülgne", @"C:\Images\scalene_triangle.png" }
        };

                // Проверка на наличие изображения
                if (imagePaths.TryGetValue(triangleType, out string imagePath))
                {
                    trianglePicture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    MessageBox.Show("Pilti ei leitud.");
                    trianglePicture.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                trianglePicture.Image = null;
            }
        }

    }
}
