export interface Lote {
   //Essa interface aqui não é uma interface visual
  //É uma interface de referencia, onde vou trabalhar com um tipo especifico, é a interface de um tipo
  //Voce cria esse arquivo com botão diretiro -> Generate Interface
  
  id: number;
  nome: string;
  preco: number;
  dataInicio?: Date;
  dataFim?: Date;
  quantidade: number;
  eventoId: number;
}
