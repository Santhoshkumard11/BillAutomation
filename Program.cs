using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace BillAutomation         //DO NOT change the namespace name
{
    public class Program        //DO NOT change the class name
    {

        static void Main(string[] args)  //DO NOT change the 'Main' method signature
        {
            ElectricityBoard bill = new ElectricityBoard();
            BillValidator validator = new BillValidator();
            Console.WriteLine("Enter Number of Bills To Be Added : ");
            int noBills = int.Parse(Console.ReadLine());
            ElectricityBill[] customer = new ElectricityBill[noBills];

            for (int index = 0; index < noBills; index++)
            {
                Console.WriteLine("\nEnter Consumer Number:");
                string number = Console.ReadLine().ToString();
                Regex regex = new Regex(@"EB+[0-9]{5}");

                Match match = regex.Match(number);

                if (!(match.Success))
                {
                    throw new FormatException("Invalid Consumer Number");
                }

                Console.WriteLine("Enter Consumer Name:");
                string name = Console.ReadLine().ToString();

                int units = 0;

                while (true)
                {
                    Console.WriteLine("Enter Units Consumed:");
                    units = int.Parse(Console.ReadLine());
                    if (validator.ValidateUnitsConsumed(units) == "1")
                        break;

                }
                double amount = 0;

                customer[index] = new ElectricityBill(name, number, units,amount);                                

                bill.CalculateBill(customer[index]);

                bill.AddBill(customer[index]);

            }

            Console.WriteLine("Enter Last 'N' Number of Bills To Generate:");

            noBills = int.Parse(Console.ReadLine());

            for (int index = 0; index < noBills; index++)
            {
                Console.WriteLine("{0}", customer[index].ConsumerName);
                Console.WriteLine("{0}", customer[index].ConsumerNumber);
                Console.WriteLine("{0}", customer[index].UnitsConsumed);
                Console.WriteLine("{0}", customer[index].BillAmount);
            }

            Console.WriteLine("Details of last ‘N’ bills:");
            List<ElectricityBill> dbPrint = bill.Generate_N_BillDetails(noBills);
            foreach (ElectricityBill item in dbPrint)
            {
                Console.WriteLine("EB Bill for {0} is {1}", item.ConsumerName, item.BillAmount);

            }


        }
    }

    public class BillValidator
    {      //DO NOT change the class name

        public String ValidateUnitsConsumed(int UnitsConsumed)      //DO NOT change the method signature
        {
            //Implement code here
            if (UnitsConsumed < 0)
            {
                Console.WriteLine("Given units is invalid");
                return "0";
            }
            return ("1");

        }


    }
}
