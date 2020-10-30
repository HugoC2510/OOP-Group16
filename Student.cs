using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class Student : Person
    {
        public YearGrade year;
        public List<WorkGroup> group;
        public string program;
        public List<int> marks;
        public int fees=0;
        public int paymentMode;
        private static int due_fees;
        public int absence;
        public string password;
        public Student(string _name, string _surname,int _age,char _sex,string _email, string _phoneNumber, string _ID,string _password)
            : base(_ID, _name, _surname, _age, _sex, _email, _phoneNumber)

        {
            this.status = "Student";
            this.password = _password;
            due_fees = 8000;
            absence = 0;
        }

        public override void Tostring() //we show on the console a description of the object
        {
            Console.WriteLine("Name : " + name
                + "; Surname : " + surname
                + "; Age : " + age
                + "; Sex : " + sex
                + "; Email : " + email
                + "; Phone Number : " + phoneNumber
                + "; ID : " + ID
                + "; Year grade : "+year.year
                + "; Program : "+program
                + "; Status : " + status);
        }
        public override bool ConnectionCheck()
        {
            bool res = false;
            Console.WriteLine("What is your password ? "); //we ask the administrator to write his password
            string password = Console.ReadLine();
            int tries = 2;
            while (password != this.password && tries > 0)
            {
                Console.WriteLine("The password is incorrect, try again (you still have " + tries + " tries)");
                password = Console.ReadLine();
                tries--;
            }
            if (password == this.password)
            {
                res = true;
            }
            return res;
        }
        public bool CheckPayment()//check if the sudent has paid all his fees, return true if it's the case
        {
            if(fees==due_fees)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AskPaymentMode()//the student chooses is payment mode
        {
            Console.WriteLine("If you want to pay all your fees with only one payment, press 1. If you want to pay with 8 mensuality, press 2 ");
            int choice = Convert.ToInt32(Console.ReadLine());
            this.paymentMode = choice;
        }
        public void FirstPayment()//this method manages the first payment depnding on the payment mode chosen
        {
            AskPaymentMode();
            if(this.paymentMode==1)
            {
                Console.WriteLine("You have to pay " + due_fees + " euros.");
                AddFees(due_fees);
            }
            else
            {
                Console.WriteLine("You have to pay " + (due_fees/8) + " euros for the moment");
                AddFees(due_fees / 8);
            }
        }
        public void AddFees(int amount)//this method adds a certain amount to the current fees of the student
        {
            this.fees = this.fees + amount;
        }
        public bool CheckMensualiteDue()//return true if the student have to pay a mensuality
        {
            bool res = false;
            if(this.paymentMode==2)
            {
                DateTime today = DateTime.Now;
                DateTime firstDay = new DateTime(2020, 9, 1);
                TimeSpan diff = today - firstDay;
                int days = Convert.ToInt32(diff.TotalDays);
                int mensualityDue = days / 30;
                int mensualityPaid = fees / (due_fees / 8);
                if (mensualityDue > mensualityPaid)
                {
                    res = true;
                }
            }
            
            return res;
        }
        public int NumberMensualityDue()//this method returns the number of mensuality the student has to pay
        {
            DateTime today = DateTime.Now;
            DateTime firstDay = new DateTime(2020, 9, 1);
            TimeSpan diff = today - firstDay;
            int days = Convert.ToInt32(diff.TotalDays);
            int mensualityDue = days / 30;
            int mensualityPaid = fees / (due_fees / 8);
            if(mensualityDue>mensualityPaid)
            {
                return mensualityDue - mensualityPaid;
            }
            else
            {
                return 0;
            }
        }
        public void ManagePayment()//this method lets the choice to the student if he wants to pay or not
        {
            if(this.paymentMode==2)
            {
                bool firstCheck = CheckPayment();
                if(firstCheck==false)
                {
                    bool check = CheckMensualiteDue();
                    if (check == true)
                    {
                        int men_due = NumberMensualityDue();
                        int due = (men_due * (due_fees / 8)) + Penalties();
                        Console.WriteLine("You have to pay " + due + " euros.");
                        Console.WriteLine("Do you want to pay it right now ?");
                        Console.WriteLine("(Any late with the payment will add financial penalties)");
                        Console.WriteLine("Press 1 if YES, press 2 if NO");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        if (choice == 1)
                        {
                            AddFees(due - Penalties());
                            //we consider the payment as done but we don't had the penalties to the fees
                            //it's to avoid any mistake for next month
                            Console.WriteLine("Your payment has been done");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't havy any mensuality to pay for the moment");
                    }
                }
                else
                {
                    Console.WriteLine("You have already paid all your fees");
                }

            }
        }
        public int Penalties()//this method returns the amount to pay if the student is late in his payment
        {
            int res = 0;
            int excessDays = 0;
            DateTime today = DateTime.Now;
            DateTime firstDay = new DateTime(2020, 9, 1);
            TimeSpan diff = today - firstDay;
            int days = Convert.ToInt32(diff.TotalDays);
            int mensualityDue = days / 30;
            int mensualityPaid = fees / (due_fees / 8);
            if (mensualityDue > mensualityPaid)
            {
                excessDays = days - mensualityPaid * 30;
                res = excessDays * 50; //50 euros by day late;
            }
            return res;
        }

    }
}
