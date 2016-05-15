using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace IoCLabs
{

    public sealed class MyLogger
    {
        private string _name = string.Empty;
        public MyLogger() { }
        public MyLogger(string name) { _name = name; }

        public void Log(string message, params object[] parms) { Console.WriteLine(_name + ": " + message, parms); }
    }

    public class MyDatabase
    {
        MyLogger _logger = new MyLogger();

        public IDisposable Open()
        {
            _logger.Log("Db open");
            return Disposable.Create(() => _logger.Log("Db close"));
        }

        public void Query(string s)
        {
            _logger.Log("Query {0}", s);
        }
    }

    public class MyValidator
    {
        protected MyValidator() { }

        MyLogger _logger = new MyLogger();

        public bool HasPermissions(string user)
        {
            _logger.Log("Checking if user {0} has permission", user);
            return user.Contains("a");
        }


        public bool IsDataValid(string query)
        {
            _logger.Log("Checking if query {0} is valid", query);
            return query.Length > 10;
        }


        public static MyValidator Create() { return new MyValidator(); }

    }


    public class MyService
    {
        MyLogger _l;
        MyDatabase _d;
        MyValidator _v;

        public MyService()
        {
            _l = new MyLogger();
            _d = new MyDatabase();
            _v = MyValidator.Create();
        }


        public void Put(string user, string data)
        {
            _l.Log("Get request {0} from {1}", data, user);

            if (!_v.HasPermissions(user))
            {
                _l.Log("User {0} doesn't have permissions", user);
                return;
            }

            if (!_v.IsDataValid(data))
            {
                _l.Log("Data {0} is not valid", data);
                return;
            }

            using (_d.Open())
            {
                _d.Query(data);
            }

        }

        public void Dispose()
        {
            _l.Log("Disposing!");
        }

    }



    class Program
    {
        WindsorContainer _container;

        public void Run()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<MyService>());

            var service = _container.Resolve<MyService>();


            RunTest(1, "qwe", "xxxx");
            RunTest(2, "asd", "yyyy");
            RunTest(3, "asd", "jeden dwa trzy cztery");

            Console.ReadKey();
        }

        private void RunTest(int i, string u, string q)
        {
            var service = _container.Resolve<MyService>();

            Console.WriteLine("Test {0}", i);
            service.Put(u, q);

        }



        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
