using System;
using System.Collections.Generic;
using System.Text;
using Proyecto1.Misc;
using Proyecto1.TranslatorAndInterpreter;

namespace Proyecto1.TranslatorAndInterpreter
{
    class SUM : AbstractExpression
    {
        private AbstractExpression ValueIzquierda;
        private AbstractExpression ValueDerecha;

        public SUM(AbstractExpression ValueIzquierda,AbstractExpression ValueDerecha) {
            this.ValueIzquierda = ValueIzquierda;
            this.ValueDerecha = ValueDerecha;
        }

        public override Retorno Execute(EnviromentTable ambiente)
        {
            Retorno izq = this.ValueIzquierda.Execute(ambiente);
            Retorno der = this.ValueDerecha.Execute(ambiente);

            String tipo = TipoDominante.TipoDominanteVal(izq.Tipo.ToString(),der.Tipo.ToString());//psh XD
            Retorno Ret= null;
            if (tipo == "string")
            {
                Ret = new Retorno(izq.Value.ToString() + der.Value.ToString(), tipo);
            }
            else if (tipo == "integer")
            {
                Ret = new Retorno(int.Parse(izq.Value.ToString()) + int.Parse(der.Value.ToString()), tipo);
            }
            else if (tipo == "real")
            {
                Ret = new Retorno(double.Parse(izq.Value.ToString()) + double.Parse(der.Value.ToString()), tipo);
            }
            else { 
                //error
            }
            //nitido jaja buena prro XD jaja mañana ve

            return Ret;
        }

        public override Retorno Translate(EnviromentTable ambiente)
        {
            throw new NotImplementedException();
        }
    }
}
