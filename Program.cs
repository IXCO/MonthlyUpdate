using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdate
{
    /*
    *Update on status on SAT database for received invoices.
    */
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize classes
            ControladorBD accessDB = new ControladorBD();
            Console.WriteLine("Revisión de estatus de facturas del mes");
            Console.WriteLine("Buscando pendientes...");
            //Gets last 3000 invoices
            Factura[] invoices = accessDB.getPendings();
            Console.WriteLine("Iniciando revisión");
            //Checks status for all 3000 invoices
            foreach (Factura invoice in invoices)
            {
                Console.Write(".");
                //If it has content
                if (invoice != null)
                {
                    
                    switch (invoice.statusOnSAT())
                    {
                        case 0://Error at connection
                        case 1://All good
                        case 2://Pending
                            break;
                        case 3://Canceled
                        case 4://Incorrect information
                            Console.WriteLine(" ");
                            Console.WriteLine("Se encontro cancelada.");
                            accessDB.insertCanceled(invoice.uuid, invoice.recepientRFC, invoice.senderRFC, invoice.uuid);
                            break;
                        default:
                            break;

                    }
                }
                else
                {//In case there are less than 3000 invoice on the last 20 days
                    Console.WriteLine("Sin más facturas por revisar");
                    break;
                }
            }
            Console.WriteLine("Termina proceso");
        }
    }
}
