using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MonthlyUpdate
{
    class ControladorBD
    {

        public MySqlConnection connection;
        public ControladorBD()
        {
            connection = new MySqlConnection();
            connection.ConnectionString = "server =" + server
                + ";user id=" + user + ";password=" + password
                + ";database=" + database;
        }
        public Factura[] getPendings()
        {
            Factura[] pendingInvoices = new Factura[800];
            int counter = 0;
            String statement = "SELECT RECEPTOR.RFC, EMISOR.RFC, COMPROBANTE.TOTAL," +
            "TIMBRE.UUID FROM CFDI_COMPROBANTE COMPROBANTE " +
            "INNER JOIN CFDI_COMPLEMENTO COMPLEMENTO ON COMPROBANTE.CFDI_COMPROBANTE_PKEY = " +
            "COMPLEMENTO.CFDI_COMPROBANTE_FKEY "+
            "INNER JOIN TFD_TIMBREFISCALDIGITAL TIMBRE ON TIMBRE.CFDI_COMPLEMENTO_FKEY = "+
            "COMPLEMENTO.CFDI_COMPLEMENTO_PKEY "+
            "INNER JOIN CFDI_RECEPTOR RECEPTOR ON RECEPTOR.CFDI_COMPROBANTE_FKEY ="+
            "COMPROBANTE.CFDI_COMPROBANTE_PKEY "+
            "INNER JOIN CFDI_EMISOR EMISOR ON EMISOR.CFDI_COMPROBANTE_FKEY = "+
            "COMPROBANTE.CFDI_COMPROBANTE_PKEY "+
            "WHERE TIMBRE.added_at > DATE_SUB(NOW(), INTERVAL 15 DAY) " +
            "LIMIT 800 ;";
            connection.Open();
            try
            {
                MySqlCommand command = new MySqlCommand(statement, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pendingInvoices[counter] = new Factura(reader.GetString(0),reader.GetString(1),reader.GetString(3),
                        reader.GetString(2),"");
                    counter++;
                }

            }
            catch (MySqlException)
            {

            }
            finally
            {
                connection.Dispose();
            }
            connection.Close();
            return pendingInvoices;
        }
        public Boolean insertCanceled(String name, String receiverRFC, String senderRFC, String serial)
        {
            bool success = true;
            connection.Open();
            String statement = "INSERT INTO Canceladas (name,rfcR,rfcE,folio,time_added) VALUES('" + name + "','" + receiverRFC + "','" + senderRFC + "'," +
                "'" + serial + "','" + DateTime.Today.ToShortDateString() + "');";
            try
            {
                MySqlCommand command = new MySqlCommand(statement, connection);
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                success = false;
            }
            finally
            {
                connection.Dispose();
            }
            connection.Close();
            return success;
        }



    }
}
