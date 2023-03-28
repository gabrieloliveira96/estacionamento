using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Constants
{
    public class MensagemErrosContants
    {
        public static string MENSAGEM_GENERICA_ERRO = $"Erro ao realizar a operação. Por favor abra um chamado anexando um print com essa mensagem. - {DateTime.Now:dd/MM/yyyy hh:mm:ss}";
    }
}
