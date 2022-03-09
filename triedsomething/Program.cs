using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using System.Reflection;

namespace DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            IEnumerable<ICustomer> customers = kernel.GetAll<ICustomer>();
            foreach (ICustomer customer in customers)
            {
                var admin = new Admin(customer);
                admin.GetCustomers();
            }
            Console.ReadLine();


        }
    }

    public interface ICustomer
    {
        void GetCustomerDetails();
    }

    public class RetailCustomer : ICustomer
    {
        public void GetCustomerDetails()
        {
            Console.WriteLine("Retail customer");
        }
    }

    public class WholeSaleCustomer : ICustomer
    {
        public void GetCustomerDetails()
        {
            Console.WriteLine("WholeSale customer");
        }
    }
    public class Customer3 : ICustomer
    {
        public void GetCustomerDetails()
        {
            Console.WriteLine("3rd customer");
        }
    }

    public class Admin
    {


        private readonly ICustomer customer;
        public Admin(ICustomer cust)
        {
            customer = cust;
        }
        public void GetCustomers()
        {
            customer.GetCustomerDetails();

        }
    }

    public class DependencyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomer>().To<WholeSaleCustomer>();
            Bind<ICustomer>().To<RetailCustomer>();
        }
    }
}