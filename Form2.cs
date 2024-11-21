using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Xml;


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
            this.Height = 900;
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


        private void SaveTriangleDataToXml(double a, double b, double c, double perimeter, double area, string triangleType)
        {
            string filePath = @"C:\Users\opilane\Source\Repos\TriangleTEST1\kolmnurgad.xml";  // Путь к XML-файлу

            // Создаём элемент для нового треугольника
            XElement triangleElement = new XElement("Triangle",
                new XElement("Base", a),
                new XElement("Side1", b),
                new XElement("Side2", c),
                new XElement("Perimeter", perimeter),
                new XElement("Area", area),
                new XElement("Type", triangleType)
            );

            // Настройки для XML-формата
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    ",
                NewLineChars = Environment.NewLine + Environment.NewLine,  // Добавляем пустую строку между треугольниками
                NewLineHandling = NewLineHandling.Replace
            };

            if (File.Exists(filePath))
            {
                // Загружаем существующий XML
                XDocument doc = XDocument.Load(filePath);

                // Добавляем новый элемент в XML
                doc.Element("Triangles").Add(triangleElement);

                // Сохраняем файл с новыми данными
                using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                {
                    doc.Save(writer);
                }
            }
            else
            {
                // Создаём новый XML-документ
                XDocument newDoc = new XDocument(
                    new XElement("Triangles", triangleElement)
                );

                // Сохраняем новый файл
                using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                {
                    newDoc.Save(writer);
                }
            }
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

                // Проверяем существование треугольника по сторонам, если все стороны введены
                if (a > 0 && b > 0 && c > 0)
                {
                    Triangle1 triangle = new Triangle1(a, b, c);

                    if (!triangle.ExistTriangle)
                    {
                        MessageBox.Show("Sellist kolmnurka ei ole! (Triipuse poolte reeglid on rikutud)");
                        return;
                    }

                    // Очищаем и заполняем ListView
                    listView1.Items.Clear();
                    listView1.View = View.Details;
                    listView1.Columns.Clear();
                    listView1.Columns.Add("Nimi", 150);
                    listView1.Columns.Add("Väärtus", 150);
                    listView1.Items.Add(new ListViewItem(new[] { "külg a", a.ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "külg b", b.ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "külg c", c.ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "Perimetr", triangle.Perimeter().ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "Pindala", triangle.Area().ToString() }));

                    // Определяем тип треугольника
                    string triangleType = triangle.GetTriangleType();
                    listView1.Items.Add(new ListViewItem(new[] { "Tüüp", triangleType }));

                    // Обновляем изображение в зависимости от типа треугольника
                    UpdateTriangleImage(triangleType);

                    // Сохраняем данные треугольника в XML
                    SaveTriangleDataToXml(a, b, c, triangle.Perimeter(), triangle.Area(), triangleType);
                }
                else if (a > 0 && h > 0) // Если введены только основание и высота
                {
                    Triangle1 triangle = new Triangle1(a, h);

                    listView1.Items.Clear();
                    listView1.View = View.Details;
                    listView1.Columns.Clear();
                    listView1.Columns.Add("Nimi", 150);
                    listView1.Columns.Add("Väärtus", 150);
                    listView1.Items.Add(new ListViewItem(new[] { "alus", a.ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "Kõrgus", h.ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "Pindala", triangle.Area1().ToString() }));

                    // Здесь тип треугольника можно не определять, так как доступно только основание и высота
                    trianglePicture.Image = null;

                    // Сохраняем данные о треугольнике с основанием и высотой в XML
                    SaveTriangleDataToXml(a, 0, 0, 0, triangle.Area1(), "Tüüp ei leitud");
                }
                else
                {
                    MessageBox.Show("Palun täitke kõik vajalikud väljad (alus, külg, kõrgus).");
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
                string imagePath = "";
                switch (triangleType)
                {
                    case "Võrdkülgne":
                        imagePath = @"C:\Users\opilane\Source\Repos\TriangleTEST1\ravnostoron.png";
                        break;
                    case "Võrdhaarsed":
                        imagePath = @"C:\Users\opilane\Source\Repos\TriangleTEST\ravnobed.png";
                        break;
                    case "Ristkülikukujuline":
                        imagePath = @"C:\Users\opilane\Source\Repos\TriangleTEST\prjamugol.png";
                        break;
                    case "nüri":
                        imagePath = @"C:\Users\opilane\Source\Repos\TriangleTEST\tipougol.png";
                        break;
                    case "Teravnurkne":
                        imagePath = @"C:\Users\opilane\Source\Repos\TriangleTEST\ostrougol.jpg";
                        break;
                    case "Mitmekülgne":
                        imagePath = @"C:\Users\opilane\Source\Repos\TriangleTEST\raznostoron.png";
                        break;
                    default:
                        MessageBox.Show("Tundmatu kolmnurga tüüp.");
                        trianglePicture.Image = null;
                        return;
                }

                // Проверка на существование файла
                if (File.Exists(imagePath))
                {
                    trianglePicture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    MessageBox.Show("Pilti ei leitud: " + imagePath);
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
