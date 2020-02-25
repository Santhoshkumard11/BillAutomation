using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace BillAutomation    //DO NOT change the namespace name
{
    public class ElectricityBill         //DO NOT change the class name
    {
        private string consumerNumber;

        public string ConsumerNumber
        {
            get { return consumerNumber; }
            set
            {
               
                    consumerNumber = value;
                
            }
        }
        private string consumerName;

        public string ConsumerName
        {
            get { return consumerName; }
            set { consumerName = value; }
        }
        private int unitsConsumed;

        public int UnitsConsumed
        {
            get { return unitsConsumed; }
            set { unitsConsumed = value; }
        }

        private double billAmount;

        public double BillAmount
        {
            get { return billAmount; }
            set { billAmount = value; }
        }

        public ElectricityBill()
        {

        }

        public ElectricityBill(string consumerNumber, string consumerName, int unitsConsumed, double billAmount)
        {
            ConsumerNumber = consumerNumber;
            ConsumerName = consumerName;
            UnitsConsumed = unitsConsumed;
            BillAmount = billAmount;
        } 
        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}", ConsumerNumber, ConsumerName, UnitsConsumed, BillAmount);
        }
    }
}
