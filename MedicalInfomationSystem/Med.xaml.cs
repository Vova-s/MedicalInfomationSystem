﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalInfomationSystem
{
    /// <summary>
    /// Interaction logic for Med.xaml
    /// </summary>
    public partial class Med : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Med()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetQuery();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment ap = new Appointment();
            this.Close();
            ap.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();   
            this.Close();
            mw.Show();
        }
        public void GetAndShowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;
            connection.Close();
        }
        private void GetQuery()
        {
            string SQlQuery = "SELECT AppID as [№]," +
                "CustSurname as [Прізвище пацієнта]," +
                "CustName as [Ім'я пацієнта]," +
                "FORMAT(TimeOfAppointment,'d') as [Дата прийому]," +
                "FamDocSurname as [Прізвище сімейного лікаря]," +
                "FamDocName as [Ім'я сімейного лікаря]," +
                "FamDocSpec as [Спеціалізація]," +
                "Symptom as [Симптоми]," +
                "DocSurname as [Прізвище спеціаліста]," +
                "DocName as [Ім'я спеціаліста]," +
                "DocSpec as [Спеціалізація]," +
                "MedicineName as [Назва лікарства]," +
                "UsageOfMedicine as [Спосіб прийняття лікарства]," +
                "BacksideEffect as [Побічний ефект] " +
                "FROM Apointment " +
                "ORDER by AppID;";
            try
            {
                GetAndShowData(SQlQuery, Query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
