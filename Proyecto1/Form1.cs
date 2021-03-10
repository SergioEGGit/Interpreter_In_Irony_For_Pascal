// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1
{
    
    // Clase Principal Form
    public partial class Form1 : Form
    {
        // Instancia Clase Irony_Analyze
        readonly Irony_Resources.Irony_Parser ParserTranslate = new Irony_Resources.Irony_Parser();

        // Constructor Inicial 
        public Form1()
        {
            
            // Inicializar Componentes 
            InitializeComponent();

        }

        // Acción Click Botón Analizar 
        private void ButtonAnalyze_Click(object sender, EventArgs e)
        {
           
            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Analizar Texto 
            ParserTranslate.AnalyzeTranslate(EntranceString);

            // Limpiar Consola
            TextEntrance.Text = "";

            // Agregar Traduccion
            TextEntrance.Text = Variables.TranslateString;

            // Limpiar Consola 
            TextConsole.Text = "";

            // Agregar Ejecucion
            TextConsole.Text = Variables.ExecuteString;

        }

        // Acción Click Botón Ejecutar
        private void ButtonExecute_Click(object sender, EventArgs e)
        {

        }

        // Acción Click Botón Reportes 
        private void ButtonReports_Click(object sender, EventArgs e)
        {

            foreach(var Error in Variables.ErrorList) {

                MessageBox.Show(Error.ErrorDescripcion);
            
            }

        }

    }

}