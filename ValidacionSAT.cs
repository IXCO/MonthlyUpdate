using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MonthlyUpdate
{
    class ValidacionSAT
    {
        public String status;
        public String code;
        public ValidacionSAT(String chain)
        {
            //Specifies channel of connection
            ConsultaCFDIService.ConsultaCFDIServiceClient client = new ConsultaCFDIService.ConsultaCFDIServiceClient("BasicHttpBinding_IConsultaCFDIService");
            //Opens channel
            client.Open();
            ConsultaCFDIService.Acuse operation = new ConsultaCFDIService.Acuse();
            //If the connection was successful then continue
            if (client.State == CommunicationState.Opened)
            {
                try
                {
                    //Sends request with information obtain from invoice
                    //Specific format is described on SAT webpage
                    operation = client.Consulta(chain);
                    //Get just the significant characters
                    //for status and the complete code.
                    status = operation.CodigoEstatus.Substring(0, 7);
                    code = operation.Estado;
                }
                catch (Exception)
                {
                    status = "Error";
                }
            }
            client.Close();

        }
    }
}
