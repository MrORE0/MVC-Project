using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Database
{
    public partial class Log_In : Form
    {
        public Log_In()
        {
            InitializeComponent();
        }

        private void Log_In_Load(object sender, EventArgs e)
        {
            DB.openConnection();
        }
        private void Log_In_FormClosing(object sender, FormClosingEventArgs e)
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
        private void customTxtBox_UserID__TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_LogIn_Click(object sender, EventArgs e)
        {
            try
            {// !!!! its .Texts with 's' because in the customTxtbox class it's called like that
                if (string.IsNullOrEmpty(customTxtBox_UserName.Texts.Trim()) || string.IsNullOrEmpty(customTxtBox_Password.Texts.Trim()))
                {
                    MessageBox.Show(" Моля попълнете полетата ");
                    return;
                }
                else
                {
                    //трябва да проверяваш дали имат права или не, ако нямат значи са ученици и отиват направо към repository, ако имат са учители и отиват към втората форма

                    if (DB.connection.State == ConnectionState.Closed)
                    {
                        DB.connection.Open();
                    }
                    else
                    {
                        // SQL query to check if user credentials match
                        string query2 = "SELECT COUNT(*) FROM UsersTable WHERE Username = @username AND Password = @password";
                        SqlCommand command2 = new SqlCommand(query2, DB.connection);
                        command2.Parameters.AddWithValue("@username", customTxtBox_UserName.Texts.Trim());
                        command2.Parameters.AddWithValue("@password", customTxtBox_Password.Texts.Trim());
                        int count = (int)command2.ExecuteScalar();

                        if (count > 0)
                        {
                            // user credentials are correct, now check the rights column
                            query2 = "SELECT rights FROM UsersTable WHERE Username = @username AND Password = @password";
                            command2.CommandText = query2;
                            bool hasRights = (bool)command2.ExecuteScalar();

                            // redirect to appropriate form based on rights
                            if (hasRights)
                            {
                                // user is a teacher with rights
                                this.Hide();
                                TeacherView f2 = new TeacherView();
                                f2.username = customTxtBox_UserName.Texts.Trim();
                                f2.ShowDialog();
                            }
                            else
                            {
                                // user is a student without rights, redirect to repository
                                this.Hide();
                                Repository f3 = new Repository();
                                f3.username = customTxtBox_UserName.Texts.Trim(); // this passes the value to the repository form
                                f3.rights = hasRights;
                                f3.ShowDialog();
                            }
                        }
                        else
                        {
                            // user credentials are incorrect
                            MessageBox.Show("Невалидно потребителско име или парола.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            // Show confirmation dialog when the user clicks the "Cancel" button
            DialogResult result = MessageBox.Show("Сигурни ли сте, че искате да затворите приложението?", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Close the database connection and exit the application
                DB.closeConnection();
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        public void EmailSent()
        {
            try
            {
                string toAddress = "pman4358@gmail.com";
                toAddress = toAddress.TrimEnd(';');
                string subject = "Смяна на парола";
                string body = $"Искам да сменя своята паролата. Потребителското ми име е (тук напишете вашето потребителско име)";

                string gmailUri = $"https://mail.google.com/mail/u/0/?view=cm&fs=1&to={toAddress}&su={subject}&body={body}";

                Process.Start(new ProcessStartInfo(gmailUri));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmailSent();
        }
    }
}

