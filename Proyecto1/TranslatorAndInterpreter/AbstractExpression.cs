// ------------------------------------------ Librerias E Imports --------------------------------------------------
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Abastracta     
    abstract class AbstractExpression
    {

        // Creación De Metodos Abstractos 

        // Método Traducir 
        public abstract Retorno Translate(EnviromentTable Env); 

        // Método Ejecutar 
        public abstract Retorno Execute(EnviromentTable Env);       

    }
}