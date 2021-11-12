
//Para colocar esse import... digite import e ja vai aparecer para selecionar o import statement.. ele ja montara uma estrutura para
//voce  basta colocar no lugar do "Module" o seu módulo e depois coloque o nome dentro das chaves.
import { Lote  } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {
  //Essa interface aqui não é uma interface visual
  //É uma interface de referencia, onde vou trabalhar com um tipo especifico, é a interface de um tipo
  //Voce cria esse arquivo com botão diretiro -> Generate Interface

  id: number;
  local: string;
  dataEvento: Date;
  tema: string;
  qtdPessoas: number;
  imagemURL: string;
  telefone: string;
  email: string;
  lotes: Lote[]; //coloco colchetes.. por que também irá criar outra interface PARA lote, RedeSocial  e Palestrante
  redesSociais: RedeSocial[]; //coloco colchetes.. por que também irá criar outra interface PARA lote, RedeSocial  e Palestrante
  palestrantesEventos: Palestrante[]; //coloco colchetes.. por que também irá criar outra interface PARA lote, RedeSocial  e Palestrante
}
