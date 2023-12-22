using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Database
{
    public partial class TeacherView : Form
    {
        public string username;
        public TeacherView()
        {
            InitializeComponent();
        }

        private string dbCommand = "";// really important

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TeacherView_Load(object sender, EventArgs e)
        {
            DB.openConnection();

            updateDataBinding();

        }
        private void TeacherView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Сигурни ли сте, че искате да затворите приложението?", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DB.closeConnection();
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        private void updateDataBinding(SqlCommand command = null, Button btn = null) //The default value of null for both parameters indicates that these parameters are optional, so you don't have to provide values for them when calling the method. If values are not provided for these parameters, the method will use the default value of null for them.
        {//The purpose of this method is to update the data binding in the form. The SqlCommand object is used to execute a SQL command to retrieve data from a database, and the Button object is used to disable the button while the data is being retrieved.
            try
            {
                TextBox txt;
                RadioButton radbutton;

                foreach (Control c in groupBox1.Controls)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        txt = (TextBox)c; // this is called type casting
                                          //The line of code "txt = (TextBox)c;" is a type casting expression. In C#, type casting is a way to convert an object from one type to another
                                          //Here, the expression is trying to convert the object "c", which is of type Control, to an object of type TextBox. The "c" object is being passed into the foreach loop as each control in the groupBox1.Controls collection. The purpose of this type casting is to access the properties and methods specific to the TextBox control, which are not available in the base Control class
                                          //The "(TextBox)" part of the expression specifies the target type for the cast.If the type of "c" is not a TextBox, an InvalidCastException will be thrown at runtime.To avoid this, it is common to check the type of the object before attempting to cast it, as is done in the code with the if statement "if (c.GetType() == typeof(TextBox))".
                                          //This ensures that only TextBox controls in the groupBox1.Controls collection will be cast to the TextBox type.
                        txt.DataBindings.Clear();
                        txt.Text = "";
                    }
                }

                foreach (Control c in groupBox3.Controls)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        txt = (TextBox)c; // this is called type casting
                                          //The line of code "txt = (TextBox)c;" is a type casting expression. In C#, type casting is a way to convert an object from one type to another
                                          //Here, the expression is trying to convert the object "c", which is of type Control, to an object of type TextBox. The "c" object is being passed into the foreach loop as each control in the groupBox1.Controls collection. The purpose of this type casting is to access the properties and methods specific to the TextBox control, which are not available in the base Control class
                                          //The "(TextBox)" part of the expression specifies the target type for the cast.If the type of "c" is not a TextBox, an InvalidCastException will be thrown at runtime.To avoid this, it is common to check the type of the object before attempting to cast it, as is done in the code with the if statement "if (c.GetType() == typeof(TextBox))".
                                          //This ensures that only TextBox controls in the groupBox1.Controls collection will be cast to the TextBox type.
                        txt.DataBindings.Clear();
                        txt.Text = "";
                    }
                    else if (c.GetType() == typeof(RadioButton))
                    {
                        radbutton = (RadioButton)c;
                        if (btn == null)
                        {
                            radbutton.Checked = false;
                        }
                    }
                }

                if (command == null)
                {// МОЖЕ ДА ТРЯБВАТ ПРОМЕНИ
                    DB.cmd.CommandText = "SELECT * FROM StudentsInfoTable ORDER BY StudentID";
                }
                else
                {
                    DB.cmd = command;
                }

                DB.dataAdapter = new SqlDataAdapter(DB.cmd);

                DB.dataSet = new DataSet(); //is an in-memory representation of a relational database that is used to store and manipulate data.
                                            //It can contain one or more DataTable objects, which in turn represent a table of data,
                                            //as well as relationships between tables, constraints, and other metadata.

                // !!! A DB.DataTable is an in-memory representation of a database table. You can use it to store, manipulate,
                // and manage data in your application and only when you press SAVE/UPDATE
                // the changes will be pushed to the original sql DB

                DB.dataAdapter.Fill(DB.dataSet, "StudentList"); // при него е написано TeacherList!!!!
                                                                //In simple terms, Fill method is used to fetch data from a database table and store it in a DataSet or DataTable object for further processing. 
                                                                //Once the data is in the DataSet or DataTable, it can be used for data binding, manipulation, or other operations.
                DB.bindingSrc = new BindingSource(DB.dataSet, "StudentList");

                bindingNavigator1.BindingSource = DB.bindingSrc; // (обекта, който ще работи с данните за да ги визуализира).свойство = (нещото, с което го свързваме а именно нашата връзка с базата данни)


                //Here we connect the data in the textboxes to match a certain field(column) in the DB
                txtBox_EGN.DataBindings.Add("Text", DB.bindingSrc, "EGN");
                txtBox_Name.DataBindings.Add("Text", DB.bindingSrc, "Firstname");
                txtBox_SecondName.DataBindings.Add("Text", DB.bindingSrc, "Secondname");
                txtBox_ThirdName.DataBindings.Add("Text", DB.bindingSrc, "Lastname");
                txtBox_phoneNumber.DataBindings.Add("Text", DB.bindingSrc, "Phone_number");
                txtBox_Grade.DataBindings.Add("Text", DB.bindingSrc, "Grade");
                txtBox_Year.DataBindings.Add("Text", DB.bindingSrc, "Year");

                /*In the example code, "Text" is a string value that specifies the name of the property of the TextBox control that the data binding is being set up for. 
                 In this case, it's the Text property of the TextBox control.
                "Text" is just one example of a property name that can be used in the Add() method. 
                Other examples of property names that can be used include "Enabled", "Visible", "BackColor", "ForeColor", "Value", "SelectedValue", and so on. 
                The property name that you use will depend on the control that you are binding to and the specific property that you want to bind to.
                It's important to note that the property that you are binding to must support the INotifyPropertyChanged interface in order for data binding to work correctly. 
                This interface provides notifications when the value of the property changes, allowing the UI to update automatically.
                */

                dataGridView1.Enabled = true; // so we can Interact and change things in the grid
                dataGridView1.DataSource = DB.bindingSrc;
                //Select the full row
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); //Overall, the code ensures that the columns in the DataGridView are automatically resized to fit their contents 
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //This means that when the user clicks on a cell in the DataGridView, the entire row of the clicked cell will be selected.
                                                                                       //If FullRowSelect is not set, only the clicked cell would be selected instead of the entire row.

                //!!If you want to add faculties (паралелки, специалности) use the first clip time- 1:40:00

            }


            catch (Exception ex)
            {
                MessageBox.Show("Update Data Bindings Error: " + ex.Message);
            }
            finally
            {
                if (txtBox_Search.CanSelect)
                {
                    txtBox_Search.Select();

                }
            }

        }


        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (bindingNavigatorAddNewItem.Text == "Add new") // in order to change the text property to Cancel if it is clicked
                {
                    //on default it clears all the text boxes and makes a new row on the grid 
                    //here we make it imposible to make a empty row by making the user to NOT be able to
                    //interact with somethings on the form and changing what the button does in the "else" statement

                    bindingNavigatorAddNewItem.Text = "Cancel";
                    bindingNavigatorAddNewItem.ToolTipText = "Cancel";

                    //these are the small blue arrows (top left on the form)
                    bindingNavigatorMoveFirstItem.Enabled = false;
                    bindingNavigatorMovePreviousItem.Enabled = false;
                    bindingNavigatorPositionItem.Enabled = false;
                    bindingNavigatorMoveNextItem.Enabled = false;
                    bindingNavigatorMoveLastItem.Enabled = false;

                    //clear the grid
                    dataGridView1.ClearSelection();
                    dataGridView1.Enabled = false;

                }
                else
                {
                    bindingNavigatorAddNewItem.Text = "Add new";
                    bindingNavigatorAddNewItem.ToolTipText = "Add new";

                    //these are the small blue arrows (top left on the form)
                    bindingNavigatorMoveFirstItem.Enabled = true;
                    bindingNavigatorMovePreviousItem.Enabled = true;
                    bindingNavigatorPositionItem.Enabled = true;
                    bindingNavigatorMoveNextItem.Enabled = true;
                    bindingNavigatorMoveLastItem.Enabled = true;

                    updateDataBinding();
                    return; //The return statement is used to exit a method and return control to the calling code (this one).
                }

                // if it doens't clean it automaticly use this code 
                TextBox txt;
                foreach (Control c in groupBox1.Controls)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        txt = (TextBox)c;
                        txt.Text = "";

                        if (txt.Name.Equals("txtBox_EGN")) //The statement is checking whether the current TextBox control being looped over, has a name of "txtBox_EGN".
                                                           // If it does, and it can be selected, the text inside the control will be selected.
                                                           // it's like if we said "txtbox_EGN.Focus = true;"
                        {
                            if (txt.CanSelect)
                            {
                                txt.Select();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void addCommandParameters()
        {//this is for the password and the username
            Random random = new Random();
            int first = random.Next(0, 10);
            int second = random.Next(0, 10);
            int third = random.Next(0, 10);
            string combined = $"{first}{second}{third}";

            DB.cmd.Parameters.Clear();
            DB.cmd.CommandText = DB.sql;

            //TO AVOID SQL INJECTION!!!
            DB.cmd.Parameters.AddWithValue("EGN", txtBox_EGN.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Firstname", txtBox_Name.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Secondname", txtBox_SecondName.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Lastname", txtBox_ThirdName.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Grade", txtBox_Grade.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Phone_number", txtBox_phoneNumber.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Year", txtBox_Year.Text.Trim());
            DB.cmd.Parameters.AddWithValue("Username", txtBox_Name.Text.Trim() + txtBox_ThirdName.Text.Trim() + combined);
            DB.cmd.Parameters.AddWithValue("Password", txtBox_Name.Text.Trim() + txtBox_ThirdName.Text.Trim() + combined);
            DB.cmd.Parameters.AddWithValue("Rights", 0);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TextBox txt;
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    txt = (TextBox)c;

                    //find a better way to check every textbox
                    if (txt.Name.Equals("txtBox_EGN") || txt.Name.Equals("txtBox_Name") || txt.Name.Equals("txtBox_Secondname") || txt.Name.Equals("txtBox_SecondName") || txt.Name.Equals("txtBox_Grade") || txt.Name.Equals("txtBox_phoneNumber") || txt.Name.Equals("txtBox_Year"))
                    {


                        //The Text.Trim() method returns a new string with any leading and trailing white space characters removed.
                        //For example, if you have a string " Hello, World! ", calling the Trim() method on it will return a new string
                        //"Hello, World!" with the leading and trailing spaces removed.
                        if (string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            MessageBox.Show("Моля попълнете всички полета");
                            return;
                        }
                        else
                        {
                            if (IsEGNorPhoneNumber(txtBox_EGN.Text.Trim()) == false)
                            {
                                MessageBox.Show("Моля попълнете полето ЕГН правилно.");
                                txtBox_EGN.Focus();
                                return;
                            }
                            if (IsEGNorPhoneNumber(txtBox_phoneNumber.Text.Trim()) == false)
                            {
                                MessageBox.Show("Моля попълнете полето Тел. номер правилно.");
                                txtBox_phoneNumber.Focus();
                                return;
                            }
                            if (IsYear(txtBox_Year.Text.Trim()) == false)
                            {
                                return;
                            }
                            if (IsName(txtBox_Name.Text.Trim()) == false || IsName(txtBox_SecondName.Text.Trim()) == false || IsName(txtBox_ThirdName.Text.Trim()) == false)
                            {
                                return;
                            }
                            if (!(int.TryParse(txtBox_Grade.Text, out int number) && number >= 8 && number <= 12))
                            {
                                // The textbox value grade is not a number or is not between 8 and 12 (inclusive)
                                MessageBox.Show("Попълнете полето клас правилно. (от 8 до 12 клас)");
                                return;
                            }
                        }
                    }
                }
            }

            //INSERT
            DB.openConnection();
            try
            {
                if (bindingNavigatorAddNewItem.Text.Equals("Add new")) // it is almost the same as saying: bindingNavigatorAddNewItem.Text == "Add new"
                {
                    //UPDATE
                    if (txtBox_EGN.Text.Trim() == "" || string.IsNullOrEmpty(txtBox_EGN.Text.Trim()))
                    {
                        MessageBox.Show("Моля изберете нещо от таблицата с данни");
                        return;
                    }
                    if (MessageBox.Show("ЕГН: " + txtBox_EGN.Text.Trim(), "Сигурни ли сте, че искате да запазите промените?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        updateDataBinding();
                        return;
                    }
                    dbCommand = "UPDATE";
                    //Where clause
                    DB.sql = "UPDATE StudentsInfoTable SET Firstname = @Firstname,Secondname = @Secondname,Lastname = @Lastname,Grade = @Grade,Phone_number = @Phone_number,Year = @Year WHERE EGN = @EGN";
                    //Command Parameters
                    addCommandParameters();

                }
                else if (bindingNavigatorAddNewItem.Text == "Cancel")
                {
                    DialogResult result;
                    result = MessageBox.Show("Сигурни ли сте, че искате да запишете промените?", "Data has been changed!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // INSERT into StudentsInfoTable
                        DB.sql = "INSERT INTO StudentsInfoTable(EGN, Firstname, Secondname, Lastname, Grade, Phone_number, Year) " +
                                 "VALUES (@EGN, @Firstname, @Secondname, @Lastname, @Grade, @Phone_number, @Year);";
                        // Command Parameters for StudentsInfoTable INSERT
                        addCommandParameters();

                    }
                    else
                    {
                        return;
                    }

                }

                int execute = DB.cmd.ExecuteNonQuery();
                if (execute != -1)
                {
                    // INSERT into UsersTable
                    DB.sql = "INSERT INTO UsersTable(Username, Password, Rights) VALUES (@Username, @Password, @Rights);";
                    // Command Parameters for UsersTable INSERT
                    addCommandParameters();

                   int execute1 = DB.cmd.ExecuteNonQuery();
                    if (execute1 != -1)
                    {
                        MessageBox.Show("Промените бяха запазени. " + dbCommand);
                        updateDataBinding();
                    }

                    bindingNavigatorAddNewItem.Text = "Add new";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Промените бяха запазени. " + ex.Message); // тук понякога дава проблем
                updateDataBinding();

            }
            finally
            {
                dbCommand = "";
                DB.closeConnection();
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            refresh();
        }
        public void refresh()
        {
            //refreshing the DB connection in case something happens
            txtBox_Search.Enabled = false;
            if (IsAddingNewRecord() == true)
            {
                return;
            }
            updateDataBinding();
            txtBox_Search.Clear();
        }

        private bool IsAddingNewRecord()
        {
            if (bindingNavigatorAddNewItem.Text == "Cancel")
            {
                MessageBox.Show("Моля първо да отмените добавянето на нов запис");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
        }



        //RADIO BUTTONS
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radButton_EGN.Checked == true)
            {//changing appearence when checked
                Font myFont = new Font("Arial Rounded MT", 10, FontStyle.Bold);
                radButton_EGN.Font = myFont;
                txtBox_Search.Enabled = true;
            }
            else
            {
                radButton_EGN.ForeColor = Color.Black;
                Font myFont = new Font("Arial Rounded MT", 10, FontStyle.Regular);
                radButton_EGN.Font = myFont;
            }
        }

        private void radButton_Name_CheckedChanged(object sender, EventArgs e)
        {
            if (radButton_Name.Checked == true)
            {//changing appearence when checked
                Font myFont = new Font("Arial Rounded MT", 10, FontStyle.Bold);

                radButton_Name.Font = myFont;
                txtBox_Search.Enabled = true;
            }
            else
            {
                radButton_Name.ForeColor = Color.Black;
                Font myFont = new Font("Arial Rounded MT", 10, FontStyle.Regular);
                radButton_Name.Font = myFont;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // the proper name is btn_Search
        {
            //I should try to make to update when the value of the textbox_search is changed so I can remove the button and it will feel more smooth!!!!!
            if (IsAddingNewRecord() == true)
            {
                return;
            }
            DB.openConnection();
            DB.sql = "SELECT * FROM StudentsInfoTable ";
            try
            {
                if (radButton_EGN.Checked == false && radButton_Name.Checked == false)
                {
                    MessageBox.Show("Моля изберете критерии за търсене.");
                    if (string.IsNullOrEmpty(txtBox_Search.Text.Trim()))
                    {
                        updateDataBinding(null, btn_Search);
                        return;
                    }
                }

                if (radButton_EGN.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtBox_Search.Text.Trim()))
                    {
                        MessageBox.Show("Моля попълнете полето за търсене.");
                        return;
                    }
                    else if (!IsEGNorPhoneNumber(txtBox_Search.Text.Trim()))
                    {
                        MessageBox.Show("Моля попълнете полето за търсене правилно, според начинът на търсене, който сте избрали.");
                        return;
                    }
                    else
                    {
                        DB.sql += "WHERE StudentsInfoTable.EGN = @Search ";
                        DB.sql += "ORDER BY StudentID;";
                    }
                }

                if (radButton_Name.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtBox_Search.Text.Trim()))
                    {
                        MessageBox.Show("Моля попълнете полето за търсене.");
                        return;
                    }
                    else if (!IsName(txtBox_Search.Text.Trim()))
                    {
                        return;
                    }
                    else
                    {
                        DB.sql += "WHERE (StudentsInfoTable.Firstname LIKE @Search ";
                        DB.sql += "OR StudentsInfoTable.Lastname LIKE @Search) ";
                        DB.sql += "ORDER BY StudentID";
                        //DB.sql = "SELECT * FROM StudentsInfoTable WHERE (Firstname LIKE @Search  ORDER BY ID;";
                    }
                }



                DB.cmd.CommandType = CommandType.Text;
                DB.cmd.CommandText = DB.sql;

                DB.cmd.Parameters.Clear();

                DB.cmd.Parameters.AddWithValue("@Search", txtBox_Search.Text.Trim());

                updateDataBinding(DB.cmd, btn_Search);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при търсенето: " + ex.Message);
            }
            finally
            {
                DB.closeConnection();
                if (txtBox_Search.CanFocus)
                {
                    txtBox_Search.Focus();
                }
            }
        }

        private void txtBox_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (IsAddingNewRecord() == true)
                {
                    return;
                }
                DB.openConnection();
                DB.sql = "SELECT * FROM StudentsInfoTable ";
                try
                {
                    if (radButton_EGN.Checked == false && radButton_Name.Checked == false)
                    {
                        MessageBox.Show("Моля изберете критерии за търсене.");
                        if (string.IsNullOrEmpty(txtBox_Search.Text.Trim()))
                        {
                            updateDataBinding(null, btn_Search);
                            return;
                        }
                    }

                    if (radButton_EGN.Checked == true)
                    {
                        if (string.IsNullOrEmpty(txtBox_Search.Text.Trim()))
                        {
                            MessageBox.Show("Моля попълнете полето за търсене.");
                            return;
                        }
                        else
                        {
                            DB.sql += "WHERE StudentsInfoTable.EGN = @Search ";
                            DB.sql += "ORDER BY ID;";
                        }
                    }

                    if (radButton_Name.Checked == true)
                    {
                        if (string.IsNullOrEmpty(txtBox_Search.Text.Trim()))
                        {
                            MessageBox.Show("Моля попълнете полето за търсене.");
                            return;
                        }
                        else
                        {
                            DB.sql += "WHERE (StudentsInfoTable.Firstname LIKE @Search ";
                            DB.sql += "OR StudentsInfoTable.Lastname LIKE @Search) ";
                            DB.sql += "ORDER BY ID";
                        }
                    }

                    DB.cmd.CommandType = CommandType.Text;
                    DB.cmd.CommandText = DB.sql;

                    DB.cmd.Parameters.Clear();

                    DB.cmd.Parameters.AddWithValue("Search", txtBox_Search.Text.Trim());

                    updateDataBinding(DB.cmd, btn_Search);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при търсенето: " + ex.Message);
                }
                finally
                {
                    DB.closeConnection();
                    if (txtBox_Search.CanFocus)
                    {
                        txtBox_Search.Focus();
                    }
                }//имаш да оправяш, когато се напише име а то е натиснато егн да казва нещо и също да оправиш колко и какво може да се пише в кутиите(всички)
            }
        }

        private bool IsEGNorPhoneNumber(string text)
        {
            // Check if the input string is exactly 10 characters long
            if (text.Length != 10)
            {
                return false;
            }

            // Check if the input string contains only numbers
            if (!int.TryParse(text, out int result))
            {
                return false;
            }

            // Check if the input string contains any special symbols
            if (!Regex.IsMatch(text, @"^[0-9]+$"))
            {
                return false;
            }

            return true;
        }

        private bool IsYear(string year)
        { //it checks for the proper format of the Year textbox
            string pattern = @"^\d{4}-\d{4}$";
            bool isMatch;
            if (isMatch = Regex.IsMatch(year, pattern))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Година трябва да се изпише с тире: 2020-2021.");
                txtBox_Year.Focus();
                return false;
            }


        }

        private bool IsName(string name)
        {
            // Check each character in the string
            foreach (char c in name)
            {
                string cirryllicOrHyphen = @"^[а-яА-ЯёЁ]+(-[а-яА-ЯёЁ]+)*$";
                // Check if the character is not a letter or hyphen
                if (!char.IsLetter(c) && c != '-')
                {
                    MessageBox.Show("Попълнете полетата с имена правилно.");
                    return false;
                }

                //check if the string contains only Cyrillic characters
                else if (!Regex.IsMatch(name, cirryllicOrHyphen))
                {// s[s.Length - 1] is used to access the last character of the string
                    MessageBox.Show("Попълнете полетата с имена, само с букви от кирилицата.");
                    return false;
                }

                //check if all the names start with a uppercase character
                else if (!char.IsUpper(name[0]))
                {
                    MessageBox.Show("Имената трябва да започват с главна буква.");
                    return false;
                }
            }
            // All characters are valid
            return true;
        }

        private void txtBox_Year_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Repository f3 = new Repository();
            f3.username = username;
            f3.rights = true;
            f3.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            if (IsAddingNewRecord())
            {
                updateDataBinding();
                bindingNavigatorAddNewItem.Text = "Add new";
                return;
            }

            DB.openConnection();

            if (string.IsNullOrEmpty(txtBox_EGN.Text.Trim()))
            {
                MessageBox.Show("Моля изберете нещо от таблицата с данни");
                updateDataBinding();
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;

                if (selectedRow == null)
                {
                    MessageBox.Show("Моля изберете запис, който да изтриете.");
                    updateDataBinding();
                    return;
                }

                string selectedEGN = selectedRow.Cells["EGN"].Value.ToString();

                if (selectedEGN != txtBox_EGN.Text.Trim())
                {
                    MessageBox.Show("Избраният запис не съответства на ЕГН в полето.");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show($"ЕГН: {txtBox_EGN.Text.Trim()}", " Сигурни ли сте, че искате да изтриете записа?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    dbCommand = "DELETE";
                    DB.sql = "DELETE FROM StudentsInfoTable WHERE EGN = @EGN";

                    DB.cmd.Parameters.Clear();
                    DB.cmd.CommandText = DB.sql;
                    DB.cmd.Parameters.AddWithValue("@EGN", txtBox_EGN.Text.Trim());

                    int execute = DB.cmd.ExecuteNonQuery();
                    if (execute != -1)
                    {
                        MessageBox.Show("Записът беше изтрит. " + dbCommand);
                        dataGridView1.Rows.Remove(selectedRow);
                        updateDataBinding();
                    }
                }
                else
                {
                    updateDataBinding();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблем при изтриването на запис." + ex.Message);
            }
            finally
            {
                dbCommand = "";
                DB.closeConnection();
            }
        }
    }
}
