// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
    
    // Clase Principal 
    class PrimitiveValue : AbstractExpression
    {

        // Valor 
        private object Value;

        // Constructor 
        public PrimitiveValue(object Value) {

            // Inicicalizar Valores  
            this.Value = Value;
        
        }

        // Métodod Ejecutar 
        public override ObjectReturn Execute(EnviromentTable Env)
        {

            // Objecto A Retornar
            ObjectReturn AuxiliaryReturn;

            // Verificar Que Tipo De Valor Primtivo ES 
            if(int.TryParse(this.Value.ToString(), out int AuxiliaryValueI))
            {

                // Agreagr A Objecto Valor 
                AuxiliaryReturn = new ObjectReturn(AuxiliaryValueI, "integer");

            }
            else if(Decimal.TryParse(this.Value.ToString(), out Decimal AuxiliaryValueD))
            {

                // Agregar A Objecto Valor 
                AuxiliaryReturn = new ObjectReturn(AuxiliaryValueD, "real");

            }
            else if(this.Value.ToString() == "true")
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(true, "boolean");

            }
            else if(this.Value.ToString() == "false")
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(false, "boolean");

            }
            else
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(this.Value.ToString(), "string");

            }

            // Retornar 
            return AuxiliaryReturn;
        }

        // Método Traducir
        public override ObjectReturn Translate(EnviromentTable Env)
        {

            // Verificar Si Es Integer, Real O Boolean
            if (int.TryParse(this.Value.ToString(), out int Value) || Decimal.TryParse(this.Value.ToString(), out Decimal Value_) || this.Value.ToString().ToLower().Equals("true") || this.Value.ToString().ToLower().Equals("false"))
            {

                // Agregar Traduccion 
                Variables.TranslateString += this.Value.ToString();

            }
            else 
            {

                // Agregar Traduccion 
                Variables.TranslateString += "'" + this.Value.ToString() + "'";

            }

            // Retornar Null
            return null;

        }
    
    }

}