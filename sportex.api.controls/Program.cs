using sportex.api.logic;
using System;

namespace sportex.api.controls
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("Se ejecutan los controles.");
                EventManager em = new EventManager();
                //em.LogTest(DateTime.Now);
                em.CheckCompletedEvents(DateTime.Now);
                //Console.WriteLine("Controles ejecutados con éxito.");
                //Console.Read();
            }
            catch(Exception ex)
            {
                //Console.WriteLine("Ha ocurrido un error. Operaciones canceladas.");
               // Console.WriteLine("Detalles del error: " + ex.Message);
                //Console.Read();
            }
        }
    }
}
