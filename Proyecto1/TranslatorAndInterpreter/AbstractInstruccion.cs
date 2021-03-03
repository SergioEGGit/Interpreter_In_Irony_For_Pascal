// ------------------------------------------ Librerias E Imports -----------------------------------------------


// ------------------------------------------------ NameSpace ---------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
   
    // Clase Abastracta     
    abstract class AbstractInstruccion
    {

        // Creación De Metodos Abstractos 
        
        // Método Traducir 
        public abstract object Translate();

        // Método Ejecutar 
        public abstract object Execute();

    }

}