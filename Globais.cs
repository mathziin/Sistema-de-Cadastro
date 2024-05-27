using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plenitude
{
    internal class Globais
    {
        public static string versao = "1.0";
        public static Boolean logado = false;
        public static int nivel = 0; // Explicação níveis: 0 = básico, 1 = visualizar, 2 = Cargo master.
        public static string caminho = System.Environment.CurrentDirectory;
        public static string nomeBanco = "banco_plenitude00.db";
        public static string caminhoBanco = caminho+@"\banco\";
        public static string caminhoFotos = caminho + @"\fotos\";

    }
}
