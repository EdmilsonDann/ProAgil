import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

//por aqui consigo injetar o serviço em qualquer lugar do app
@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'http://localhost:5000/api/evento';

  //para o httpclient apenas digitei ele ja indicou e fez o import na parte de cima.
constructor(private http: HttpClient) { }

//ESTE É UM OBSERVABLE
//Colocando a classe evento;.. eu ja informo que o meu observable terá um retorno baseado em Eventos
getAllEvento(): Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseURL);
}

getEventoByTema(tema: string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
}

getEventoById(id: number): Observable<Evento>{
  return this.http.get<Evento>(`${this.baseURL}/${id}`);
}

}
