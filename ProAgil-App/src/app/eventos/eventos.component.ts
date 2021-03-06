import { Evento } from './../_models/Evento';
import { EventoService } from './../_services/Evento.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  //variaveis
  eventosFiltrados: Evento[] = [];
  eventos: Evento[] = [];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  modalRef: BsModalRef = new BsModalRef;
  registerForm: FormGroup = new FormGroup({});


   //propriedade
 private _filtroLista: string = "";




 public get filtroLista(): string{
   return this._filtroLista;
 }

 public set filtroLista(value : string) {
   this._filtroLista = value;
   this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
 }

  constructor(
     private eventoService: EventoService
    ,private modalService: BsModalService
    ) { }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }



  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string) : Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
                    || evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
                    || evento.dataEvento.toLocaleLowerCase().indexOf(filtrarPor) !== -1
                    || evento.qtdPessoas.toLocaleLowerCase().indexOf(filtrarPor) !== -1
                    || evento.lote.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation(){
    this.registerForm = new FormGroup({
        tema: new FormControl('',
         [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
        local: new FormControl,
        imagemURL: new FormControl,
        dataEvento: new FormControl,
        qtdPessoas: new FormControl('',
          [Validators.required, Validators.max(120000)]),
        telefone: new FormControl,
        email: new FormControl('',
          [Validators.required, Validators.email])
    });
  }

  salvarAlteracao(){

  }

  getEventos(){
     this.eventoService.getAllEvento().subscribe(
       (_eventos : Evento[]) => {
       this.eventos = _eventos;
       this.eventosFiltrados = this.eventos;
       console.log(_eventos);
     }, error =>{
       console.log(error);
     });
  }

}
