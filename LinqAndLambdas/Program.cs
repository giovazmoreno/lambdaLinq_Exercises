using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            //QuerySyntax();
            //MethodSyntax();
            //Grouping();
            //Joining();
            Filtering();
        }

        #region WHERE,SELECT,SORTING,MULTIPLE CONDITIONS,QUERY ON OBJECTS
        private static void QuerySyntax()
        {
            string sentence = "I love the cats";
            string[] catNames = { "Pelusa", "Luna", "Bella", "Lucky", "Chispita", "Leon", "Tobi", "Simba", "Grumpy", "Gato", "Lana", "Mickey" };
            int[] numbers = { 5, 6, 34, 23, 456, 7, 85, 4, 56, 12, 24, 92, 70, 1 };

            //---------------------------------------------------------------------------------------------------
            var getTheNumbers = from number in numbers
                                where number < 5
                                orderby number descending
                                select number;
            //--ESTE QUERY NO ES EJECUTADO SI NO HASTA QUE TRATAMOS DE ACCESAR A SOBRE ELLA--------

            Console.WriteLine(string.Join(",", getTheNumbers));//EN ESTE MOMENTO ES DONDE EL QUERY ES EJECUTADO

            var CatsWithA = from cat in catNames
                            where cat.Contains("a") && (cat.Length < 5)
                            orderby cat
                            select cat;
            Console.WriteLine(string.Join(",", CatsWithA));

            var CatsWithO = from cat in catNames
                            where cat.Contains("o")
                            where cat.Length < 5
                            orderby cat
                            select cat;
            Console.WriteLine(string.Join(",", CatsWithO));

            List<Person> people = new List<Person>()
            {
                new Person("Giovanna",27,155,Gender.Female),
                new Person("John",34,190,Gender.Male),
                new Person("Sara",21,167,Gender.Female),
                new Person("Patrick",29,168,Gender.Male),
                new Person("Iñaki",27,198,Gender.Male),
                new Person("Tairon",12,140,Gender.Male),
                new Person("Clavel",19,160,Gender.Female)
            };

            var fourCharPeople = from person in people
                                 where person.Name.Length == 4
                                 orderby person.Name ascending, person.Gender descending
                                 select person;
            //select person.Name;

            //Console.WriteLine(string.Join("\n",fourCharPeople));

            foreach (var item in fourCharPeople)
            {
                Console.WriteLine(string.Format("Name: {0}, Age:{1}, Height:{2}, Gender:{3}", item.Name, item.Age, item.Height, item.Gender));
            }
        }

        #endregion

        #region LAMBDA OPERATIONS AND SHORTS FOREACH
        private static void MethodSyntax()
        {
            int[] numbers = { 5, 6, 34, 23, 456, 7, 85, 4, 56, 12, 24, 92, 70, 1, 60, 80, 67, 3, 54 };

            var oddNumbers = from n in numbers
                             where n % 2 == 1
                             orderby n
                             select n;
            Console.WriteLine(string.Join(",", oddNumbers));

            var oddNumbersLambda = numbers.Where(n => n % 2 == 1).OrderByDescending(n => n);
            Console.WriteLine(string.Join(",", oddNumbersLambda));



            List<Person> people = new List<Person>()
            {
                new Person("Giovanna",27,155,Gender.Female),
                new Person("John",34,190,Gender.Male),
                new Person("Sara",21,167,Gender.Female),
                new Person("Patrick",29,168,Gender.Male),
                new Person("Iñaki",27,198,Gender.Male),
                new Person("Tairon",12,140,Gender.Male),
                new Person("Clavel",19,160,Gender.Female)
            };

            var shortpeople = people.Where(p => p.Height < 150);
            foreach (var item in shortpeople)
            {
                Console.WriteLine(string.Format("Name: {0}, Height:{1}", item.Name, item.Height));
            }

            var heightShortpeople = people.Where(p => p.Height < 150).Select(p => p.Name);

            Console.WriteLine(string.Join(",", heightShortpeople));

            List<int> lstHeightShortpeople = people.Where(p => p.Height < 170)
                                            .Select(p => p.Height)
                                            .OrderBy(p => p)
                                            .ToList();
            lstHeightShortpeople.ForEach(sp => Console.WriteLine(sp));

            people.ForEach(p => Console.WriteLine(p.Height));
        }

        #endregion

        #region GROUPING AND GROUPING BY MULTIKEY METHOD
        private static void Grouping()
        {
            List<Person> people = new List<Person>()
            {
                new Person("Giovanna",27,155,Gender.Female),
                new Person("John",34,190,Gender.Male),
                new Person("Sara",21,167,Gender.Female),
                new Person("Patrick",29,168,Gender.Male),
                new Person("Iñaki",27,198,Gender.Male),
                new Person("Tairon",12,140,Gender.Male),
                new Person("Clavel",19,160,Gender.Female),
                new Person("Sandra",21,160,Gender.Female),
                new Person("Joel",38,180,Gender.Male),
                new Person("Gabriela",35,159,Gender.Female),
                new Person("Saul",27,189,Gender.Male),
                new Person("Nathan",12,150,Gender.Male),
                new Person("Karol",19,178,Gender.Female),
                new Person("Brina",21,148,Gender.Female),
            };

            var genderGroup = from p in people
                              group p by p.Gender;
            foreach (var person in genderGroup)
            {
                Console.WriteLine(person.Key);
                foreach (var p in person)
                {
                    Console.WriteLine($"{p.Name} {p.Gender}");
                }
            }


            var alphabeticalGroup = from p in people
                                    orderby p.Name
                                    group p by p.Name[0];

            foreach (var firstletter in alphabeticalGroup)
            {
                Console.WriteLine(firstletter.Key);
                foreach (var item in firstletter)
                {
                    Console.WriteLine($"  {item.Name}");
                }
            }

            var simpleGrouping = people.Where(p => p.Age > 20)
                                        .OrderBy(p => p.Age)
                                        .GroupBy(p => p.Gender);

            foreach (var item in simpleGrouping)
            {
                Console.WriteLine(item.Key);
                foreach (var p in item)
                {
                    Console.WriteLine($"  {p.Name}  {p.Age}");
                }
            }

            var multikey = people.GroupBy(p => new { p.Gender, p.Age }).OrderBy(p => p.Count());

            foreach (var group in multikey)
            {
                Console.WriteLine(group.Key);
                foreach (var p in group)
                {
                    Console.WriteLine($" {p.Name}");
                }

            }
        }


        #endregion

        #region JOINING , INNER JOIN AND COMPOSITE JOIN METHOD

        private static void Joining()
        {
            List<Alumnos> alumnos = new List<Alumnos>()
            {
                new Alumnos(1,"Joshua", "Cabrera", 20, 10),
                new Alumnos(10,"Alan", "Hernandez", 18, 30),
                new Alumnos(11,"Alma", "Flores", 20, 10),
                new Alumnos(12,"Ernesto", "Fernandez", 19, 20),
                new Alumnos(2,"Carolina", "Cristobal", 30, 10),
                new Alumnos(20,"Sandra", "Mendez", 22, 20),
                new Alumnos(21,"Cesar", "Chavez", 20, 10),
                new Alumnos(22,"Hugo", "Zamora", 20, 30),
                new Alumnos(3,"Gabriel", "Yukon", 19, 20),
                new Alumnos(4,"Cristian", "Alvarez", 19, 10),
                new Alumnos(5,"Susan", "Fuentes", 20, 10),
                new Alumnos(6,"Hilda", "Agustin", 20, 30)
            };

            List<Carreras> carreras = new List<Carreras>()
            {
                new Carreras(10, "Computacion", 9),
                new Carreras(20, "Diseño Grafico", 6),
                new Carreras(30, "Medicina", 10)
            };

            var joinAlumnoSemestres = alumnos.Join(carreras, a => a.ClaveCarrera, b => b.ClaveCarrera,
                (a, c) => new
                {
                    Carrera = c.Carrera,
                    Alumno = a.Nombre,
                    Semestres = c.Semestres
                }
                    );

            foreach (var  item in joinAlumnoSemestres)
            {
                Console.WriteLine($"Carrera: {item.Carrera} Alumno:{item.Alumno} Semestres:{item.Semestres}");
            }


            var innerJoin = from a in alumnos
                            join c in carreras on a.ClaveCarrera equals c.ClaveCarrera
                            orderby c.Carrera, a.Nombre
                            select new
                            {
                                a.Id,
                                a.Nombre,
                                c.Carrera,
                                c.Semestres
                            };
            foreach (var item in innerJoin)
            {
                Console.WriteLine($" Alumno: {item.Nombre} Matricula: {item.Id} Carrera: {item.Carrera} Semestres: {item.Semestres}");
            }

            Console.WriteLine("-----  -group a join--------------");
            var groupInnerJoin = from a in alumnos
                                 group a by a.ClaveCarrera into ag                       
                            join c in carreras on ag.FirstOrDefault().ClaveCarrera equals c.ClaveCarrera
                            select new
                            {               
                                IdAlumno = ag.FirstOrDefault().Id,
                                Alumno = ag.FirstOrDefault().Nombre,
                                c.Carrera,
                                c.Semestres
                            };

            foreach (var item in groupInnerJoin)
            {
                Console.WriteLine($" Carrera: {item.Carrera} ({item.Semestres} semestres) Matricula: {item.IdAlumno} Alumno: {item.Alumno} ");
            }
            
        }


        #endregion


        #region FILTERING COLLECTION BY THE TYPE OF THE ITEMS

        private static void Filtering()
        {
            List<Person> people = new List<Person>()
            {
                new Buyer(){ Age = 21 },
                new Supplier(){ Age = 48 },
                new Buyer(){ Age = 34 },
                new Buyer(){ Age = 29 },
                new Supplier(){ Age = 78 },
                new Supplier(){ Age = 90 },
                new Buyer(){ Age = 14 },
                new Supplier(){ Age = 37 }
            };

            /*var suppliers = from p in people
                            where p is Supplier
                            where (p as Supplier).Age > 30 && (p as Supplier).Age < 40
                            select p;*/

            var suppliers = people.OfType<Supplier>().Where(b => (b.Age > 30) && (b.Age < 40));
            foreach (var item in suppliers)
            {
                Console.WriteLine($"Tipo: {item.GetType().ToString()}");
            }
        }
        #endregion
    }

    class Person
    {
        private string _name;
        private int _age;
        private int _height;
        private Gender _gender;
        public string Name { get { return this._name; } set { this._name = value; } }
        public int Age { get { return this._age; } set { this._age = value; } }
        public int Height { get { return this._height; } set { this._height = value; } }
       
        public Gender Gender { get; set; }

        public Person() { }
        public Person(int age)
        {
            this.Age = age;
        }
        public Person(string name, int age, int height, Gender gender)
        {
            this.Name = name;
            this.Age = age < 1 ? 1: age;
            this.Height = height < 30 ? 1 : height;
            this.Gender = gender;
        }

        
    }
    
    internal enum Gender
    {
        Male,
        Female
    }


    public class Alumnos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public int ClaveCarrera { get; set; }

        public Alumnos(int id, string nombre, string apellido, int edad, int clavecarrera)
        {
            this.Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            ClaveCarrera = clavecarrera;
        }
    }

    public class Carreras
    {
        public int ClaveCarrera { get; set; }
        public string Carrera { get; set; }
        public int Semestres { get; set; }

        public Carreras(int clavecarrera, string carrera, int semestres)
        {
            ClaveCarrera = clavecarrera;
            Carrera = carrera;
            Semestres = semestres;
        }
    }

    internal class Buyer : Person
    {
       public int Age { get; set; }

        //public Buyer(int age) : base(age)
        //{
        //    this.Age = age;
        //}
    }

    internal class Supplier : Person
    {
        public int Age { get; set; }

        //public Supplier(int age) : base(age)
        //{
        //    this.Age = age;
        //}
    }
}
