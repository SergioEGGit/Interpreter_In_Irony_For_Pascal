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
        public abstract ObjectReturn Translate(EnviromentTable Env); 

        // Método Ejecutar 
        public abstract ObjectReturn Execute(EnviromentTable Env);       

    }
}