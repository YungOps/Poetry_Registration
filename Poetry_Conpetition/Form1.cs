using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poetry_Conpetition
{
    public partial class Form1 : Form
    {
        public delegate string EquelsEventHandlerShow(string Title);
        public static event EquelsEventHandlerShow EquelsEventShow;

        public delegate string EquelsEventHandlerSearch(string FIOP, string FIOC);
        public static event EquelsEventHandlerSearch EquelsEventSearch;

        public Form1()
        {
            InitializeComponent();
            Event e = new Event();
            Poets.ConvertToPoets(Application.StartupPath + "\\data.txt");
            
            GetCId();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) 
            {
                label6.Text = "ФИО родителя";
                label7.Text = "Место учебы";
                comboBox4.Visible = false;
                label8.Visible = false;
                comboBox3.Visible = false;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label6.Text = "Место работы";
                label7.Text = "Адресс организации";
                label8.Visible = true;
                label8.Text = "Семейное положение";
                comboBox4.Visible = false;
                comboBox3.Visible = true;
            }
            if (comboBox1.SelectedIndex == 2) 
            {
                label6.Text = "№ пенсионного";
                label7.Text = "Наличие наград СССР";
                comboBox4.Visible = true;
                comboBox3.Visible = false;
                label8.Visible = false;
            }
        }

        private void textBoxF_TextChanged(object sender, EventArgs e)
        {
            if (Poets.TestString(textBoxF.Text) == true)
            {
                textBoxF.BackColor = Color.White;
            }
            else 
            {
                textBoxF.BackColor = Color.Red;
                buttonSave.Enabled = false;
            }
        }

        private void textBoxN_TextChanged(object sender, EventArgs e)
        {
            if (Poets.TestString(textBoxN.Text) == true)
            {
                textBoxN.BackColor = Color.White;
                buttonSave.Enabled = true;
            }
            else
            {
                textBoxN.BackColor = Color.Red;
                buttonSave.Enabled = true;
            }
        }

        private void textBoxO_TextChanged(object sender, EventArgs e)
        {
            if (Poets.TestString(textBoxO.Text) == true)
            {
                textBoxO.BackColor = Color.White;
                buttonSave.Enabled = true;
            }
            else
            {
                textBoxO.BackColor = Color.Red;
                buttonSave.Enabled = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2) 
            {
                if (Poets.TestInt(textBox4.Text) == true)
                {
                    textBox4.BackColor = Color.White;
                    buttonSave.Enabled = true;
                }
                else
                {
                    textBox4.BackColor = Color.Red;
                    buttonSave.Enabled = false;
                }
            }
            if((comboBox1.SelectedIndex==1)||(comboBox1.SelectedIndex == 2))
            {
                if (Poets.TestString(textBox4.Text) == true)
                {
                    textBox4.BackColor = Color.White;
                    buttonSave.Enabled = true;
                }
                else
                {
                    textBox4.BackColor = Color.Red;
                    buttonSave.Enabled = false;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (Poets.TestString(textBox5.Text) == true)
            {
                textBox5.BackColor = Color.White;
                buttonSave.Enabled = true;
            }
            else
            {
                textBox5.BackColor = Color.Red;
                buttonSave.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if (comboBox1.SelectedIndex == 2)
                {
                    WorkWithException.TestNumber(textBox4.Text);
                }
                if (Save() == true)
                {
                    MessageBox.Show("Сохранение данных выполнено успешно");
                    Clear(); 
                }
                else
                {
                    MessageBox.Show("Проверьие корректность введенных данных");
                }
            }
            catch(MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool Save() 
        {
            bool test = false;
            if (comboBox1.SelectedIndex == 0) 
            {
                if ((Poets.TestString(textBoxF.Text) == true)
                    && (Poets.TestString(textBoxN.Text) == true)
                    && (Poets.TestString(textBoxO.Text) == true)
                    && (Poets.TestString(textBox4.Text) == true)
                    && (Poets.TestString(textBox5.Text) == true)
                    && (comboBoxCat.SelectedIndex>=0))
                {
                    ChildPoets temp = new ChildPoets(textBoxF.Text + " " + textBoxN.Text + " " + textBoxO.Text, comboBoxCat.SelectedItem.ToString(),
                        textBox4.Text, textBox5.Text);                    
                    Poets.MyPoets.Add(temp);
                    test = true;
                    comboBoxCat.SelectedIndex = -1;
                    comboBox1.SelectedIndex = -1;
                    GetCId();
                    comboBox7.SelectedIndex = 0;
                    string t = EquelsEventSearch(temp.FIOParent, temp.FIO);
                    if (t != string.Empty)
                    {
                        MessageBox.Show(t);
                    }
                }
            }
            if(comboBox1.SelectedIndex == 1)
            {
                if((Poets.TestString(textBoxF.Text) == true)
                    && (Poets.TestString(textBoxN.Text) == true)
                    && (Poets.TestString(textBoxO.Text) == true)
                    && (Poets.TestString(textBox4.Text) == true)
                    && (Poets.TestString(textBox5.Text) == true)
                    && (comboBox3.SelectedIndex >= 0))
                {
                    WorkersPoets temp = new WorkersPoets(textBoxF.Text + " " + textBoxN.Text + " " + textBoxO.Text, comboBoxCat.SelectedItem.ToString(),
                         textBox4.Text, textBox5.Text, comboBox3.SelectedItem.ToString());
                    Poets.MyPoets.Add(temp);
                    test = true;
                    comboBoxCat.SelectedIndex = -1;
                    GetCId();
                    comboBox7.SelectedIndex = 1;
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if((Poets.TestString(textBoxF.Text) == true)
                    && (Poets.TestString(textBoxN.Text) == true)
                    && (Poets.TestString(textBoxO.Text) == true)
                    && (Poets.TestInt(textBox4.Text) == true)
                    && (comboBox4.SelectedIndex >= 0))
                {
                    bool n = false;
                    if (comboBox4.SelectedIndex == 0)
                    {
                        n = true;
                    }
                    else 
                    {
                        n = false;
                    }
                    PensionersPoets temp = new PensionersPoets(textBoxF.Text + " " + textBoxN.Text + " " + textBoxO.Text, comboBoxCat.SelectedItem.ToString(),
                         Convert.ToInt32(textBox4.Text),n);
                    Poets.MyPoets.Add(temp);
                    test = true;
                    comboBox1.SelectedIndex = -1;
                    comboBox4.SelectedIndex = -1;
                    GetCId();
                    comboBox7.SelectedIndex = 2;
                }
            }
            return test;
        }

        private void GetCId()
        {
            comboBoxF.Items.Clear();
            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            for(int i = 0; i < Poets.MyPoets.Count; i++)
            {
                comboBoxF.Items.Add(Poets.MyPoets[i].ID);
                comboBox2.Items.Add(Poets.MyPoets[i].ID);
                comboBox5.Items.Add(Poets.MyPoets[i].ID);
            }
            for (int i = 2010; i <= DateTime.Now.Year; i++) 
            {
                comboBox6.Items.Add(i);
            }
        }

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            if (Poets.TestString(textBoxTitle.Text) == true)
            {
                textBoxTitle.BackColor = Color.White;
                buttonSaveWork.Enabled = true;
            }
            else
            {
                textBoxTitle.BackColor = Color.Red;
                buttonSaveWork.Enabled = false;
            }
        }

        private void textBoxYear_TextChanged(object sender, EventArgs e)
        {
            if (Poets.TestInt(textBoxYear.Text) == true)
            {
                textBoxYear.BackColor = Color.White;
                buttonSaveWork.Enabled = true;
            }
            else
            {
                textBoxYear.BackColor = Color.Red;
                buttonSaveWork.Enabled = false;
            }
        }

        private void buttonSaveWork_Click(object sender, EventArgs e)
        {
            try
            {
                WorkWithException.TestYear(textBoxYear.Text);
                bool test = Poets.AddWorks(comboBoxF.SelectedItem.ToString(), textBoxTitle.Text, textBoxYear.Text);
                if (test == true)
                {
                    MessageBox.Show("Сохранение прошло успешно");
                    string n = Poets.SearchType(comboBoxF.SelectedItem.ToString());
                    if(n == "Дети")
                    {
                        comboBox7.SelectedIndex = 0;
                    }
                    if(n == "Рабочие")
                    {
                        comboBox7.SelectedIndex = 1;
                    }
                    if(n == "Пенсионеры")
                    {
                        comboBox7.SelectedIndex = 2;
                    }
                    string temp = EquelsEventShow(textBoxTitle.Text);
                    if(temp != string.Empty)
                    {
                        MessageBox.Show($"Произведение с названием {textBoxTitle.Text} было уже у {temp}");
                    }
                    Clear();
                }
                else
                {
                    MessageBox.Show("Проверьте корректность ввода данных");
                }
            }
            catch(MyException2 ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Poets.ConvertToFile(Application.StartupPath + "\\data.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                
                MessageBox.Show(Poets.Remove(comboBox2.SelectedItem.ToString()));
                GetCId();
                UpdateData();
                Clear();
            }
            else
            {
                MessageBox.Show("Необходимо сначала выбрать № участника!!!");
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex >= 0)
            {
                button2.Enabled = true;
                textBox2.Text =  Poets.Seach(comboBox5.SelectedItem.ToString());                
            }
            else
            {
                MessageBox.Show("Необходимо сначала выбрать № участника");
                button2.Enabled = false;
            }
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex >= 0)
            {
                textBox3.Text = Poets.SearchYear(comboBox6.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Необходимо сначала выбрать год!!!");
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            int k = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < Poets.MyPoets.Count; i++)
            {
                if ((comboBox7.SelectedIndex == 0) && (Poets.MyPoets[i].Type == "Дети"))
                {
                    ChildPoets temp = (ChildPoets)Poets.MyPoets[i];
                    dataGridView1.Rows.Insert(k); // добавление строки в таблицу
                    dataGridView1.Columns[4].HeaderText = "ФИО родителя";
                    dataGridView1.Columns[5].HeaderText = "Место учебы";
                    dataGridView1.Rows[k].Cells["ID"].Value = temp.ID;
                    dataGridView1.Rows[k].Cells["FIO"].Value = temp.FIO;
                    for (int j = 0; j < temp.Works.Count; j++)
                    {
                        dataGridView1.Rows[k].Cells["Works"].Value += temp.Works[j] + "\n";
                    }
                    dataGridView1.Rows[k].Cells["Category"].Value = temp.Category;
                    dataGridView1.Rows[k].Cells["Column1"].Value = temp.FIOParent;
                    dataGridView1.Rows[k].Cells["Column2"].Value = temp.Education;
                    dataGridView1.Columns[6].Visible = false;
                    k++;
                }
                if ((comboBox7.SelectedIndex == 1) && (Poets.MyPoets[i].Type == "Рабочие"))
                {
                    WorkersPoets temp = (WorkersPoets)Poets.MyPoets[i];
                    dataGridView1.Rows.Insert(k);
                    dataGridView1.Columns[4].HeaderText = "Место работы";
                    dataGridView1.Columns[5].HeaderText = "Адресс организации";
                    dataGridView1.Columns[6].Visible = true;
                    for (int j = 0; j < temp.Works.Count; j++)
                    {
                        dataGridView1.Rows[k].Cells["Works"].Value += temp.Works[j] + "\r\n";
                    }
                    dataGridView1.Columns[6].HeaderText = "Семейное положение";
                    dataGridView1.Rows[k].Cells["ID"].Value = temp.ID;
                    dataGridView1.Rows[k].Cells["FIO"].Value = temp.FIO;
                    dataGridView1.Rows[k].Cells["Category"].Value = temp.Category;
                    dataGridView1.Rows[k].Cells["Column1"].Value = temp.WorkName;
                    dataGridView1.Rows[k].Cells["Column2"].Value = temp.AddressWork;
                    dataGridView1.Rows[k].Cells["Column3"].Value = temp.SP;
                    k++;
                }
                if ((comboBox7.SelectedIndex == 2) && (Poets.MyPoets[i].Type == "Пенсионеры"))
                {
                    PensionersPoets temp = (PensionersPoets)Poets.MyPoets[i];
                    dataGridView1.Rows.Insert(k);
                    dataGridView1.Columns[4].HeaderText = "Наличие наград СССР";
                    dataGridView1.Columns[5].HeaderText = "№пенсионного";
                    dataGridView1.Columns[6].Visible = false;
                    for (int j = 0; j < temp.Works.Count; j++)
                    {
                        dataGridView1.Rows[k].Cells["Works"].Value += temp.Works[j] + "\r\n";
                    }
                    dataGridView1.Rows[k].Cells["ID"].Value = temp.ID;
                    dataGridView1.Rows[k].Cells["FIO"].Value = temp.FIO;
                    dataGridView1.Rows[k].Cells["Category"].Value = temp.Category;
                    dataGridView1.Rows[k].Cells["Column1"].Value = temp.Reward;
                    dataGridView1.Rows[k].Cells["Column2"].Value = temp.Number;
                    k++;
                }
            }
        }

        private void Clear()
        {
            comboBox1.SelectedIndex = -1;
            textBoxF.Clear();
            textBoxF.BackColor = Color.White;
            textBoxO.Clear();
            textBoxO.BackColor = Color.White;
            textBoxN.Clear();
            textBoxN.BackColor = Color.White;
            comboBoxCat.SelectedIndex = -1;
            textBox4.Clear();
            textBox4.BackColor = Color.White;
            textBox5.Clear();
            textBox5.BackColor = Color.White;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBoxF.SelectedIndex = -1;
            textBoxTitle.Clear();
            textBoxTitle.BackColor = Color.White;
            textBoxYear.Clear();
            textBoxYear.BackColor = Color.White;
            comboBox2.SelectedIndex = -1;                        
            textBox2.BackColor = Color.White;

        }
    }
}
