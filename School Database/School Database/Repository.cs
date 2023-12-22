using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Database
{
    public partial class Repository : Form
    {
        private string dbCommand;

        public string username { get; set; } // this gets the passes value of username from Form1
        public bool rights { get; set; }
        
        public Repository()
        {
            InitializeComponent();
        }

        private void Repository_Load(object sender, EventArgs e)
        {
            string query = "SELECT file_name, file_type, file_size, upload_date, posted_by FROM RepositoryInfoTable";
            SqlCommand command = new SqlCommand(query, DB.connection);
            if (rights == true)
            {
                toolStripButton4.Enabled = true;
            }
            else
            {
                toolStripButton4.Enabled = false;
            }
            //MessageBox.Show(rights.ToString());

            //here we again open the connection
            DB.openConnection();
            updateDataBinding();
            

        }
        private void Repository_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rights)
            {
                // user is a teacher with rights
                this.Dispose();
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                // user is not a teacher, show confirmation dialog when closing the form
                DialogResult result = MessageBox.Show("Сигурни ли сте, че искате да затворите приложението?", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DB.closeConnection();
                    Application.Exit();
                }
                else
                {
                    // cancel form closing if the user clicks "No"
                    e.Cancel = true;
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

                    // Create an OpenFileDialog object to let the user select a file
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png, *.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif|" +
                        "Word Files (*.doc, *.docx)|*.doc;*.docx|" +
                        "PowerPoint Files (*.ppt, *.pptx)|*.ppt;*.pptx|" +
                        "PDF Files (*.pdf)|*.pdf|" +
                        "Text Files (*.txt)|*.txt|" +
                        "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";

                    // If the user selects a file
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        // Get the file name, type, size, and data
                        FileInfo file = new FileInfo();
                        file.FileName = openFileDialog1.SafeFileName;
                        file.FileLocation = openFileDialog1.FileName;

                        // Set the file binary data property
                        file.FileBinaryData = File.ReadAllBytes(file.FileLocation);
                        long fileSize = file.FileBinaryData.Length;

                        // Set the upload date property
                        file.UploadDate = DateTime.Now.ToString("yyyy-MM-dd");

                        // Set the posted by property
                        string postedBy = username; // Example username
                        file.PostedBy = postedBy;

                        //Set the file type property
                        int index = file.FileName.LastIndexOf('.');
                        if (index >= 0 && index < file.FileName.Length - 1)
                        {
                            string extension = file.FileName.Substring(index); // includes the dot
                            file.FileType = extension;
                        }

                        // Set the file name property
                        int dotIndex = file.FileName.IndexOf('.');
                        if (dotIndex != -1) // check if dot exists in the string
                        {
                            string beforeDot = file.FileName.Substring(0, dotIndex);
                            file.FileName = beforeDot;
                        }

                        //!!!this is a test to check if the copied bytes are the ones needed
                        //string testPath = @"C:\Users\merti\OneDrive\Работен плот\problem.png";
                        //byte[] testData = File.ReadAllBytes(testPath);

                        //if (testData.Length == file.FileBinaryData.Length)
                        //{
                        //    MessageBox.Show("Bravo vee stana");
                        //}

                        // Set the file size property and check what unit to use
                        long fileSizeInBytes = file.FileBinaryData.Length;
                        double fileSizeInGB;
                        string unit;
                        const double gigabyte = 1024 * 1024 * 1024; // 1 GB = 1024 MB * 1024 KB * 1024 bytes

                        if (fileSizeInBytes >= gigabyte)
                        {
                            fileSizeInGB = (double)fileSizeInBytes / gigabyte;
                            unit = "GB";
                        }
                        else if (fileSizeInBytes >= 1024 * 1024)
                        {
                            fileSizeInGB = (double)fileSizeInBytes / (1024 * 1024);
                            unit = "MB";
                        }
                        else if (fileSizeInBytes >= 1024)
                        {
                            fileSizeInGB = (double)fileSizeInBytes / 1024;
                            unit = "KB";
                        }
                        else
                        {
                            fileSizeInGB = (double)fileSizeInBytes;
                            unit = "bytes";
                        }
                        string sizeString = fileSizeInGB.ToString("0.00");
                        file.FileSize = sizeString + unit;
                        //add the size limit and if the user exceedes it show a messageBox and return 


                        DialogResult result;
                        result = MessageBox.Show($"Сигурни ли сте, че искате да запишете този файл ({file.FileName + file.FileType})?", "Upload File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            string query1 = "SELECT COUNT(*) FROM RepositoryInfoTable WHERE file_name = @file_name";
                            SqlCommand command1 = new SqlCommand(query1, DB.connection);
                            command1.Parameters.AddWithValue("@file_name", file.FileName);
                            int count = (int)command1.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Вече съществува файл с това име.", "Проблем", MessageBoxButtons.OK ,MessageBoxIcon.Warning);
                                bindingNavigatorAddNewItem.Text = "Add new";
                                bindingNavigatorAddNewItem.ToolTipText = "Add new";

                                //these are the small blue arrows (top left on the form)
                                bindingNavigatorMoveFirstItem.Enabled = true;
                                bindingNavigatorMovePreviousItem.Enabled = true;
                                bindingNavigatorPositionItem.Enabled = true;
                                bindingNavigatorMoveNextItem.Enabled = true;
                                bindingNavigatorMoveLastItem.Enabled = true;

                                updateDataBinding();
                                return;
                            }
                            else
                            {
                                // insert query here
                                // Initialize the SQL query string
                                string query = "INSERT INTO RepositoryInfoTable(file_name,file_type,file_size,file_binaryData,upload_date,posted_by) VALUES (@file_name,@file_type,@file_size,@file_binaryData,@upload_date,@posted_by)";

                                // Create a new SqlCommand object
                                SqlCommand command = new SqlCommand(query, DB.connection);

                                // Add parameter values to the SqlCommand object
                                command.Parameters.AddWithValue("@file_name", file.FileName);
                                command.Parameters.AddWithValue("@file_type", file.FileType);
                                command.Parameters.AddWithValue("@file_size", file.FileSize);
                                command.Parameters.AddWithValue("@file_binaryData", file.FileBinaryData);
                                command.Parameters.AddWithValue("@upload_date", file.UploadDate);
                                command.Parameters.AddWithValue("@posted_by", file.PostedBy);

                                // Open the database connection
                                DB.openConnection();

                                // Execute the SQL query and get the number of rows affected
                                int rowsAffected = command.ExecuteNonQuery();

                                // Close the database connection
                                DB.closeConnection();
                                updateDataBinding();
                            }
                            bindingNavigatorAddNewItem.Text = "Add new";
                            bindingNavigatorAddNewItem.ToolTipText = "Add new";
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
                            return;
                        }
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
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void updateDataBinding(SqlCommand command = null, Button btn = null)
        {
            try
            {
                if (command == null)
                {// МОЖЕ ДА ТРЯБВАТ ПРОМЕНИ
                    DB.cmd.CommandText = "SELECT file_name, file_type, file_size, upload_date, posted_by FROM RepositoryInfoTable ORDER BY upload_date";
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

                DB.dataAdapter.Fill(DB.dataSet, "Repository"); // при него е написано TeacherList!!!!
                                                                //In simple terms, Fill method is used to fetch data from a database table and store it in a DataSet or DataTable object for further processing. 
                                                                //Once the data is in the DataSet or DataTable, it can be used for data binding, manipulation, or other operations.
                DB.bindingSrc = new BindingSource(DB.dataSet, "Repository");

                bindingNavigator1.BindingSource = DB.bindingSrc; // (обекта, който ще работи с данните за да ги визуализира).свойство = (нещото, с което го свързваме а именно нашата връзка с базата данни)

                dataGridView1.Enabled = true; // so we can Interact and change things in the grid
                dataGridView1.DataSource = DB.bindingSrc;
                //Select the full row
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); //Overall, the code ensures that the columns in the DataGridView are automatically resized to fit their contents 
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //This means that when the user clicks on a cell in the DataGridView, the entire row of the clicked cell will be selected.
                                                                                       //If FullRowSelect is not set, only the clicked cell would be selected instead of the entire row.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            updateDataBinding();
        }

        private void Repository_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (IsAddingNewRecord())
            {
                return;
            }

            DB.openConnection();

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string selectedFileName = selectedRow.Cells["file_name"].Value.ToString();
                string selectedFileType = selectedRow.Cells["file_type"].Value.ToString();

                DialogResult dialogResult = MessageBox.Show($"Файл: {selectedFileName}{selectedFileType}", " Сигурни ли сте, че искате да изтриете записа?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    dbCommand = "DELETE";
                    DB.sql = "DELETE FROM RepositoryInfoTable WHERE file_name = @file_name AND file_type = @file_type";

                    DB.cmd.Parameters.Clear();
                    DB.cmd.CommandText = DB.sql;
                    DB.cmd.Parameters.AddWithValue("@file_name", selectedFileName);
                    DB.cmd.Parameters.AddWithValue("@file_type", selectedFileType);

                    int execute = DB.cmd.ExecuteNonQuery();
                    if (execute != -1)
                    {
                        MessageBox.Show("Записът беше изтрит. " + dbCommand);
                        dataGridView1.Rows.Remove(selectedRow);
                        updateDataBinding();
                    }
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
    }
}
