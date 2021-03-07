using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1.TranslatorAndInterpreter
{
    class TipoDominante
    {
        enum Tipo:int
        {
            Tstring = 0,
            Treal = 1,
            Tboolean =2,
            Tinteger = 3,
            Terror = 4,
        }

        private static Tipo[,] Matriz_Dominante = new Tipo[5, 5] { {Tipo.Tstring, Tipo.Tstring , Tipo.Tstring , Tipo.Tstring ,Tipo.Terror},
                                                         { Tipo.Tstring, Tipo.Treal, Tipo.Terror, Tipo.Treal, Tipo.Terror},
                                                         { Tipo.Tstring, Tipo.Terror, Tipo.Tboolean, Tipo.Terror, Tipo.Terror},
                                                         { Tipo.Tstring, Tipo.Treal, Tipo.Terror, Tipo.Tinteger, Tipo.Terror},
                                                         { Tipo.Terror, Tipo.Terror, Tipo.Terror, Tipo.Terror, Tipo.Terror}
                                                        };
        
        public static String TipoDominanteVal(String Izq,String Der) {
            int fila=0, columna=0;
            fila = RetEnum(Izq);
            columna = RetEnum(Der);
            String ret = RetVal(Matriz_Dominante[fila, columna].ToString());
            return ret;
        }

        private static int RetEnum(String val) {
            if (val=="string")
            {
                return 0;
            }else if (val == "real")
            {
                return 1;
            }
            else if (val == "boolean")
            {
                return 2;
            }
            else if (val == "integer")
            {
                return 3;
            }
            else 
            {
                return 4;
            }
        }


        private static String RetVal(String value) {
            if ("Tstring" == value) {
                return "string";
            } else if ("Treal" == value)  {
                return "real";
            }
            else if ("Tboolean" == value)
            {
                return "boolean";
            }
            else if ("Tinteger" == value)
            {
                return "integer";
            }
            else {
                return "error";
            }
        }
    }
}
