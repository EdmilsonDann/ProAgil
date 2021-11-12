using System;

namespace ProAgil.Domain
{
    public class Lote
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int Quantidade { get; set; }

        //Relação de 1 para 1 -> Lote para Evento
        public int EventoId { get; set; }

        //EXTREMAMENTE IMPORTANTE.. toda a entidade A que possui uma B e a B possui a A dentro dela também, 
        // deve ser apenas de leitura.. nunca pode ser "set", para evitar um looping sem fim
        //POR EXEMPLO..AQUI DENTRO DA CLASSE LOTE temos Evento.. e Dentro da Classe Evento temos
        //public List<Lote> Lotes { get; set; }.. logo se pudessemos adicionar Eventos dentro da classe Lote
        // Ficaria um looping sem fim.. POR ISSO EVENTO SÓ PODE SER ADICIONADO EM EVENTO.. POR ISSO SEU NOME
        //NADA DE ADICIONAR EVENTO.. DENTRO DE OUTRA CLASSE... QUE NAO SEJA ELA.. 
        //DEVEMOS USAR SIM.. MAS ALI É SOMENTE LEITURA.. 
        // ao usar o metodo GET .. COM UMA ENTIDADE A, B e B, A.. ele retornara o erro "Could not beautify"
        // colocando somente get, no retorno do json no metodo get.. vai voltar null.. o entity framework entende isso 
        public Evento Evento { get; }
    }
}