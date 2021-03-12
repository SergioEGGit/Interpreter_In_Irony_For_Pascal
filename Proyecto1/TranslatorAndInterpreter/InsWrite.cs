// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
    class InsWrite : AbstractInstruccion
    {

        // Atributos 

        // Tipo De Write
        private readonly String WriteType;

        // Lista De Expressiones
        private readonly LinkedList<AbstractExpression> ExpressionList = new LinkedList<AbstractExpression>();

        // Tipo Traduccion
        private readonly String TranslateType;

        // Constructor 
        public InsWrite(String WriteType, LinkedList<AbstractExpression> ExpressionList, String TranslateType) 
        {

            // Incializar Valores
            this.WriteType = WriteType;
            this.ExpressionList = ExpressionList;
            this.TranslateType = TranslateType;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Objecto Auxiliar 
            ObjectReturn AuxiliaryObject;

            // Verificar Tipo De Write 
            if(ExpressionList != null) 
            {

                // Recorrer Lista De Expressiones 
                foreach(AbstractExpression Expression in ExpressionList) 
                {

                    // Obtener Valor
                    AuxiliaryObject = Expression.Execute(Env);

                    // Verificar Que No SEa Nullo
                    if(Expression != null) 
                    {

                        // Obtener Valor Y Agregarlo A Consola
                        VariablesMethods.ExecuteString += AuxiliaryObject.Value.ToString();

                    }
                
                }

                // Verificar Si Hay Que Agregar Salto De Linea
                if(this.WriteType.Equals("WriteLine")) 
                {

                    // Agregar Salto 
                    VariablesMethods.ExecuteString += "\n";
                
                }
            
            } 
            else
            {

                // Verificar Si Hay Que Agregar Salto De Linea
                if (this.WriteType.Equals("WriteLine"))
                {

                    // Agregar Salto 
                    VariablesMethods.ExecuteString += "\n";

                }

            }

            // Retornar 
            return null;

        }

        public override object Translate(EnviromentTable Env)
        {
            
            // Objecto Auxiliar 
            ObjectReturn AuxiliaryObject;

            // Contador Auxiliar 
            int AuxiliaryCounter = 1;

            // Verificar Tipo De Write 
            if (ExpressionList != null)
            {

                // Verificar Si Hay Que Agregar Salto De Linea
                if (this.WriteType.Equals("WriteLine"))
                {

                    // Agregar Salto 
                    VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "writeln(";

                }
                else 
                {

                    // Agregar Salto 
                    VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "write(";

                }

                // Recorrer Lista De Expressiones 
                foreach (AbstractExpression Expression in ExpressionList)
                {

                    // Verificar Que no SEa Nullo
                    if(Expression != null) 
                    {

                        // Obtener Valor
                        AuxiliaryObject = Expression.Translate(Env);

                    }

                    if (AuxiliaryCounter < ExpressionList.Count) 
                    {

                        // Agregar Coma 
                        VariablesMethods.TranslateString += ", ";

                    }

                    // Aumentar Contador
                    AuxiliaryCounter += 1;

                }

                // Agregar Otro Parentesis
                VariablesMethods.TranslateString += ");\n";

            }
            else
            {

                // Verificar Si Es Write Line
                if(this.WriteType.Equals("WriteLine")) 
                {

                    // Verficar Tipo Traduccion
                    if (this.TranslateType.Equals("2"))
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "writeln();\n";

                    }
                    else 
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "writeln;\n";

                    }

                }
                else
                {

                    // Verficar Tipo Traduccion
                    if (this.TranslateType.Equals("2"))
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "write();\n";

                    }
                    else
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "write;\n";

                    }

                }

            }

            // Retornar 
            return null;
        
        }

    }

}