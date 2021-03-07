// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
    class PrimitiveValue : AbstractExpression
    {

        private object value;
        public PrimitiveValue(object value) {
            this.value = value;
        }

        public override Retorno Execute(EnviromentTable ambiente)
        {
            Retorno ret = null;

            int val = 0;
            double val2 = 0;
            if (int.TryParse(this.value.ToString(), out val))
            {
                ret = new Retorno(val, "integer");
            }
            else if (double.TryParse(this.value.ToString(), out val2))
            {
                ret = new Retorno(val2, "real");

            }
            else if (this.value.ToString() == "true")
            {
                ret = new Retorno(true, "boolean");
            }
            else if (this.value.ToString() == "false")
            {
                ret = new Retorno(false, "boolean");
            }
            else { //string
                ret = new Retorno(this.value.ToString(), "string");
            }
            
            return ret;
        }

        public override Retorno Translate(EnviromentTable ambiente)
        {
            throw new NotImplementedException();
        }
    }
}
