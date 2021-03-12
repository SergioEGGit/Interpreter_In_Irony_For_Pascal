// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Principal
    class InsFor : AbstractInstruccion
    {

        // Atributos

        // Identifier
        public readonly String Identifier;

        // Expresion 1
        public readonly AbstractExpression Expression_;

        // Expresion 2
        public readonly AbstractExpression Expression__;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Token Line
        public readonly int TokenLine;

        // Token COlumn 
        public readonly int TokenColumn;

        // Constructor 
        public InsFor(String Identifier, AbstractExpression Expression_, AbstractExpression Expression__, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.Identifier = Identifier;
            this.Expression_ = Expression_;
            this.Expression__ = Expression__;
            this.InstruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Crear Nuevo Entorno
            EnviromentTable ForEnv = new EnviromentTable(Env, "Env_For");

            // Verificar La Expression 1
            ObjectReturn AsgExp = this.Expression_.Execute(ForEnv);

            // Verificar La Expression 2
            ObjectReturn LmExp = this.Expression__.Execute(ForEnv);

            // Simbolo
            SymbolTable ActualVar = ForEnv.GetVariable(this.Identifier);

            // Value Auxiliar 
            ObjectReturn ActualValue = new ObjectReturn("", "");

            // Buscar Variable 
            if (ActualVar != null)
            {

                // Verificar Si Ambas Condiciones Son Integers
                if(AsgExp.Type.Equals("integer") && LmExp.Type.Equals("integer") && ActualVar.Type.Equals("integer"))
                {

                    // For 
                    for(int Count = int.Parse(AsgExp.Value.ToString()); Count <= int.Parse(LmExp.Value.ToString()); Count++)
                    {
                        
                        // Setear Valores 
                        ActualValue.Type = "integer";
                        ActualValue.Value = Count;

                        // Agregar A Tabla De Simbolos 
                        ActualVar.Value = ActualValue;

                        // Setear Nueva Variable 
                        ForEnv.SetVariable(this.Identifier, ActualVar);

                        // Verificar Si La Lista De Instrucciones ESta Nulla
                        if (this.InstruccionsList != null) 
                        {

                            // Recorrer Lista 
                            foreach (AbstractInstruccion Instruccion in this.InstruccionsList) 
                            {

                                // Verificar Si NO Es Null
                                if (Instruccion != null) 
                                {

                                    Instruccion.Execute(ForEnv);
                                
                                }
                            
                            }
                        
                        }

                    }

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable De Asignacion, Comienzo Y Limite De Un For Tienen Que Ser De Tipo Integer", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }
                
            }
            else
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable Indica En El Ciclo For No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                // Aumentar Contador
                VariablesMethods.AuxiliaryCounter += 1;

            }                          

            // Retornar
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Crear Nuevo Entorno 
            EnviromentTable ForEnv = new EnviromentTable(Env, "Env_For");

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "for " + this.Identifier + " := ";
                       
            // Obtener Traduccion De Expressiones 
            this.Expression_.Translate(ForEnv);

            // Agregar Traduccion
            VariablesMethods.TranslateString += " to ";

            // Obtener Traduccion De Expressiones 
            this.Expression__.Translate(ForEnv);

            // Agregar Traduccion
            VariablesMethods.TranslateString += " do";

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "begin\n";

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            // Recorrer Instrucciones 
            if (this.InstruccionsList != null)
            {

                // Recorrer Lista De Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Verificar Si Esta Vaica 
                    if(Instruccion != null) 
                    {

                        // Ejecutar Instruccion 
                        Instruccion.Translate(ForEnv);

                    }

                }

            }
            else
            {

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n \n";

            }

            // Pop A Pila 
            VariablesMethods.AuxiliaryPile.Pop();

            // Agregar A Traduccion
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            // Retornar 
            return null;

        }

    }

}