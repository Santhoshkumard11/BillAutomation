using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BillAutomation      //DO NOT change the namespace name
{
    public class ElectricityBoard  //DO NOT change the class name
    {
        public MySqlConnection SqlCon { get; set; }

        public ElectricityBoard()
        {
            SqlCon = DBHandler.GetConnection();
        }

        public void AddBill(ElectricityBill ebill)
        {
            using (MySqlConnection conn = DBHandler.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO electricitybill(consumer_number,consumer_name,units_consumed,bill_amount) VALUES(@consumer_number,@consumer_name,@units_consumed,@bill_amount)", conn))
                {
                    command.Parameters.AddWithValue("consumer_number", ebill.ConsumerNumber);
                    command.Parameters.AddWithValue("consumer_name", ebill.ConsumerName);
                    command.Parameters.AddWithValue("units_consumed", ebill.UnitsConsumed);
                    command.Parameters.AddWithValue("bill_amount", ebill.BillAmount);

                    try
                    {
                        conn.Open();

                        int rowsInserted = command.ExecuteNonQuery();

                    }
                    catch (MySqlException ex)

                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public void CalculateBill(ElectricityBill ebill)
        {
            int tempUnits = 0, billTotal = 0;


            if (ebill.UnitsConsumed > 1000)
            {
                tempUnits = ebill.UnitsConsumed - 1000;

                billTotal = Convert.ToInt32(tempUnits * 7.5);

                billTotal += Convert.ToInt32(200 * 1.50);
                billTotal += Convert.ToInt32(300 * 3.50);
                billTotal += Convert.ToInt32(400 * 5.50);
                ebill.BillAmount = billTotal;
            }
            if (ebill.UnitsConsumed > 600 && ebill.UnitsConsumed <= 1000)
            {
                tempUnits = ebill.UnitsConsumed - 600;

                billTotal += Convert.ToInt32(tempUnits * 5.5);
                billTotal += Convert.ToInt32(300 * 3.5);
                billTotal += Convert.ToInt32(200 * 1.5);

                ebill.BillAmount = billTotal;

            }
            if (ebill.UnitsConsumed > 300 && ebill.UnitsConsumed <= 600)
            {
                tempUnits = ebill.UnitsConsumed - 300;

                billTotal += Convert.ToInt32(tempUnits * 3.5);
                billTotal += Convert.ToInt32(200 * 1.5);

                ebill.BillAmount = billTotal;

            }
            if (ebill.UnitsConsumed > 100 && ebill.UnitsConsumed <= 300)
            {
                tempUnits = ebill.UnitsConsumed - 100;

                ebill.BillAmount += Convert.ToInt32(tempUnits * 1.5);

            }

            if (ebill.UnitsConsumed <= 100)
                ebill.BillAmount = 0;


        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {

            List<ElectricityBill> electricityBills = new List<ElectricityBill>();


            using (MySqlConnection conn = DBHandler.GetConnection())
            {
                string query = string.Format("SELECT * FROM ElectricityBill  LIMIT {0}", num);
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    electricityBills.Add(new ElectricityBill { ConsumerNumber = reader.GetString(0), ConsumerName = reader.GetString(1), UnitsConsumed = reader.GetInt32(2), BillAmount = reader.GetDouble(3) });
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                }
            }
            return electricityBills;
        }


    }
}
