using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Matrix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<List<int>> matrix = new List<List<int>>();

        // Преобразует массив строк в массив чисел
        private int[] arrayStringToInt(string[] arr)
        {
            try
            {
                return Array.ConvertAll(arr, s => int.Parse(s));
            } catch (FormatException e)
            {
                return null;
            }
        }

        private int getMaxLength()
        {
            int maxLength = 0;
            foreach (List<int> list in matrix)
            {
                if (list.Count > maxLength)
                {
                    maxLength = list.Count;
                }
            }
            return maxLength;
        }

        private string introduceMatrixInString()
        {
            string result = "";
            for (int i = 0; i < matrix.Count; i++)
            {
                result += String.Join(" ", matrix[i]) + (i < (matrix.Count - 1) ? "\n" : "");
            }
            return result;
        }

        private void fillDataGridView()
        {
            int width = matrix[0].Count;
            int height = matrix.Count;

            matrixView.ColumnCount = width;

            for (int i = 0; i < height; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(matrixView);

                for (int j = 0; j < width; j++)
                {
                    row.Cells[j].Value = matrix[i][j];
                }

                matrixView.Rows.Add(row);
            }
        }

        private void sortIncreaseMatrixRows()
        {
            foreach (List<int> line in matrix)
            {
                line.Sort();
            }
        }

        private void sortWaningMatrixRows()
        {
            foreach (List<int> line in matrix)
            {
                line.Sort((a, b) => b.CompareTo(a));
            }
        }

        // Добавление недоставющих элементов матрице
        // для выравнивании количества элементов матрицы 
        private void matrixAlignment()
        {
            int maxLength = getMaxLength();

            foreach (List<int> list in matrix)
            {
                while (list.Count != maxLength) {
                    list.Add(0);
                }
            }
        }
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                matrixView.Rows.Clear();
                matrix.Clear();
                openFileDialog.ShowDialog();
                string filename = openFileDialog.FileName;
                string readFile = File.ReadAllText(filename);
                char[] separators = new char[] { '\n', '\r' };
                string[] lines = readFile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    string[] row = line.Split(' ');
                    int[] intRow = arrayStringToInt(row);
                    List<int> newRow = intRow.OfType<int>().ToList();
                    matrix.Add(newRow); 
                }
                matrixAlignment();

                fillDataGridView();
            } catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        // События FormClosing для формы. Отвечает за вызов сообщения 
        // с подтверждением закрытия программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        // Кнопка "Возрастание"
        private void sortIncrease_Click(object sender, EventArgs e)
        {
            if (matrix.Count != 0)
            {
                matrixView.Rows.Clear();
                sortIncreaseMatrixRows();
                fillDataGridView();
            }
        }

        // Кнопка "Убывание"
        private void sortWaning_Click(object sender, EventArgs e)
        {
            if (matrix.Count != 0)
            {
                matrixView.Rows.Clear();
                sortWaningMatrixRows();
                fillDataGridView();
            }
        }

        private void saveMatrix_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllText(filename, introduceMatrixInString());
            MessageBox.Show("Файл сохранен");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пограмма предназначена для обработки входных матриц из файлов формата txt. В приложении можно отсортировать строки матрицы по возрастанию/убыванию, а затем записать данные в файл", "Справка");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                matrixView.Rows.Clear();
                matrix.Clear();
                openFileDialog.ShowDialog();
                string filename = openFileDialog.FileName;
                string readFile = File.ReadAllText(filename);
                char[] separators = new char[] { '\n', '\r' };
                string[] lines = readFile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    string[] row = line.Split(' ');
                    int[] intRow = arrayStringToInt(row);
                    List<int> newRow = intRow.OfType<int>().ToList();
                    matrix.Add(newRow);
                }
                matrixAlignment();

                fillDataGridView();
            }
            catch (Exception err)
            {
                MessageBox.Show("Входные данные неверные!");
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllText(filename, introduceMatrixInString());
            MessageBox.Show("Файл сохранен");
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пограмма предназначена для обработки входных матриц из файлов формата txt. В приложении можно отсортировать строки матрицы по возрастанию/убыванию, а затем записать данные в файл", "Справка");
        }
    }
}
