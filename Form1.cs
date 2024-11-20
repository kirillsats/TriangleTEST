using System;
using System.Drawing;
using System.Xml.Linq; //для работы с LINQ to XML
using System.Windows.Forms;

namespace Triangle1
{
    public partial class Form1 : Form
    {
        // Элементы интерфейса: кнопка, текстовые поля, ListView, PictureBox
        Button btn;
        TextBox txtA, txtB, txtC;
        ListView listView1;
        PictureBox trianglePicture;

        public Form1()
        {
            // Настройка формы
            this.Height = 800;
            this.Width = 900;
            this.Text = "Töö kolmnurgaga";
            this.BackColor = Color.LightBlue; // голубой фон 

            // Создание кнопки "Käivitamine" (Запуск)
            btn = new Button();
            btn.Text = "Käivitamine";
            btn.Font = new Font("Arial", 28);
            btn.AutoSize = true;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Location = new Point(320, 100);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.FlatAppearance.BorderSize = 0; // Убираем границу кнопки
            Controls.Add(btn);
            btn.Click += Btn_Click; // Привязываем обработчик события клика

            // Создание метки для txtA (Külg 1)
            Label lblA = new Label();
            lblA.Text = "Alus:";
            lblA.Location = new Point(220, 200);
            lblA.Font = new Font("Arial", 10);
            Controls.Add(lblA);

            // Текстовое поле txtA
            txtA = new TextBox();
            txtA.Location = new Point(320, 200);
            txtA.Font = new Font("Arial", 10);
            txtA.Width = 200;
            txtA.BackColor = Color.MistyRose; // цвет
            Controls.Add(txtA);

            // Создание метки для txtB (Külg 2)
            Label lblB = new Label();
            lblB.Text = "Külg 1:";
            lblB.Location = new Point(220, 240);
            lblB.Font = new Font("Arial", 10);
            Controls.Add(lblB);

            // Текстовое поле txtB
            txtB = new TextBox();
            txtB.Location = new Point(320, 240);
            txtB.Font = new Font("Arial", 10);
            txtB.Width = 200;
            txtB.BackColor = Color.MistyRose; // цвет
            Controls.Add(txtB);

            // Создание метки для txtC (Külg 3)
            Label lblC = new Label();
            lblC.Text = "Külg 2:";
            lblC.Location = new Point(220, 280);
            lblC.Font = new Font("Arial", 10);
            Controls.Add(lblC);

            // Текстовое поле txtC
            txtC = new TextBox();
            txtC.Location = new Point(320, 280);
            txtC.Font = new Font("Arial", 10);
            txtC.Width = 200;
            txtC.BackColor = Color.MistyRose;
            Controls.Add(txtC);

            // Настройка ListView
            listView1 = new ListView();
            listView1.Location = new Point(100, 400);
            listView1.Font = new Font("Arial", 10);
            listView1.Width = 400;
            listView1.Height = 200;
            listView1.View = View.Details;
            listView1.Columns.Add("Nimi", 150);
            listView1.Columns.Add("Väärtus", 150);
            Controls.Add(listView1);

            // PictureBox для отображения изображений треугольников
            trianglePicture = new PictureBox();
            trianglePicture.Location = new Point(600, 200);
            trianglePicture.Size = new Size(200, 200);
            trianglePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(trianglePicture);

            // Создание кнопки для открытия другой формы (Form2)
            Button btnOpenForm = new Button();
            btnOpenForm.Text = "Arvutada poole aluse"; // Текст кнопки
            btnOpenForm.Location = new Point(600, 100);
            btnOpenForm.Font = new Font("Arial", 12);
            btnOpenForm.AutoSize = true;
            btnOpenForm.FlatStyle = FlatStyle.Flat;
            Controls.Add(btnOpenForm);
            btnOpenForm.Click += BtnOpenForm_Click; // Привязываем обработчик события клика
        }

        // Обработчик события для открытия другой формы
        private void BtnOpenForm_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show(); // Открываем новую форму Form2
        }
        private void SaveTriangleData(double a, double b, double c, double perimeter, double area, string type)
        {
            string filePath = @"C:\Users\kiril\Source\Repos\Triangle1\kolmnurgad.xml";

            // Создаём структуру элемента "Triangle"
            XElement triangleElement = new XElement("Triangle",
                new XElement("Base", a),
                new XElement("Side1", b),
                new XElement("Side2", c),
                new XElement("Perimeter", perimeter),
                new XElement("Area", area),
                new XElement("Type", type)
            );

            // Если файл существует, добавляем данные
            if (System.IO.File.Exists(filePath))
            {
                XDocument doc = XDocument.Load(filePath);
                doc.Element("Triangles").Add(triangleElement); // Добавляем новый треугольник
                doc.Save(filePath); // Сохраняем файл
            }
            else
            {
                // Если файла нет, создаём новый
                XDocument newDoc = new XDocument(
                    new XElement("Triangles", triangleElement)
                );
                newDoc.Save(filePath);
            }
        }

        // Обработчик события клика по кнопке "Käivitamine"
        private void Btn_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear(); // Очищаем элементы в ListView
            double a, b, c;

            try
            {
                // Преобразуем введённые данные в числа
                a = Convert.ToDouble(txtA.Text);
                b = Convert.ToDouble(txtB.Text);
                c = Convert.ToDouble(txtC.Text);

                // Создаём объект треугольника
                Triangle1 triangle = new Triangle1(a, b, c);

                // Проверяем, существует ли треугольник
                if (!triangle.ExistTriangle)
                {
                    MessageBox.Show("Sellist kolmnurka ei ole!"); // Сообщение об ошибке
                    trianglePicture.Image = null; // Удаляем изображение
                    return;
                }

                // Метод для добавления элементов в ListView
                void AddListViewItem(string name, string value)
                {
                    listView1.Items.Add(new ListViewItem(new[] { name, value }));
                }

                // Добавляем данные в ListView
                AddListViewItem("külg a", a.ToString());
                AddListViewItem("külg b", b.ToString());
                AddListViewItem("külg c", c.ToString());
                AddListViewItem("Perimetr", triangle.Perimeter().ToString());
                AddListViewItem("Pindala", triangle.Area().ToString());
                AddListViewItem("Kas on olemas?", triangle.ExistTriangle.ToString());

                // Определяем тип треугольника
                string triangleType = triangle.GetTriangleType();
                AddListViewItem("Tüüp", triangleType);

                // Обновляем изображение в зависимости от типа треугольника
                string imagePath = "";

                // Выбираем путь к изображению в зависимости от типа треугольника
                switch (triangleType)
                {
                    case "Võrdkülgne":
                        imagePath = @"C:\Users\kiril\Source\Repos\Triangle1\ravnostoron.png";
                        break;
                    case "Võrdhaarsed":
                        imagePath = @"C:\Users\kiril\Source\Repos\Triangle1\ravnobed.png";
                        break;
                    case "Ristkülikukujuline":
                        imagePath = @"C:\Users\opilane\Source\Repos\Triangle1_\prjamugol.png";
                        break;
                    case "nüri":
                        imagePath = @"C:\Users\opilane\Source\Repos\Triangle1_\tipougol.png";
                        break;
                    case "Teravnurkne":
                        imagePath = @"C:\Users\opilane\Source\Repos\Triangle1_\ostrougol.jpg";
                        break;
                    case "Mitmekülgne":
                        imagePath = @"C:\Users\opilane\Source\Repos\Triangle1_\raznostoron.png";
                        break;
                }

                // Загружаем изображение, если путь указан и файл существует
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    trianglePicture.Image = Image.FromFile(imagePath);
                }

                // Сохраняем данные о треугольнике в XML
                SaveTriangleData(a, b, c, triangle.Perimeter(), triangle.Area(), triangleType);
            }
            catch (FormatException)
            {
                // Выводим сообщение об ошибке, если введены некорректные данные
                MessageBox.Show("Palun sisestage andmed õigesti!");
            }
        }




    }
}

