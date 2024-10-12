using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboShop.helpers
{
    internal class MessagesHelper
    {
        public static void Messages(ref bool logged)
        {
         
            if (!logged)
            {
                Console.WriteLine("Qeydiyyatdan keçmək üçün - 1 \n" +
                           "Hesabınıza daxil olmaq üçün - 2 seçin");
            }
            else if (logged)
            {
                Console.Write("Hesabınıza baxmaq üçün - 5 seçin \n" +
                    "Elan yerləşdirmək üçün - 6 seçin \n" +
                    "Hesabdan çıxmaq üçün - 10 seçin \n");
            }
            Console.Write("Elanlara baxmaq üçün - 3 seçin, \n" +
                "Axtarış menyusu üçün - 22 daxil edin \n" +
                 "Proqramı bağlamaq üçün isə - 0 seçin \n");
        }
    }
}
