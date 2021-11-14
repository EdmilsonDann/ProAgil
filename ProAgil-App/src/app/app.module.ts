import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { DateTimeFormatPipePipe } from './helps/DateTimeFormatPipe.pipe';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';

import { EventoService } from './_services/Evento.service';


//Nunca posso esquecer de colocar tudo que for usar .. nos meus modulos
//por exemplo usei o DateTimeFormatPipe.. que criei.. para ele ficar disponível dentro do projeto.. preciso
//colocar ele nos declarations e ele irá fazer o imports automatico.. se nao fizer.. precisa ter importado sempre confira!!

//Tambem usei o registerForm: FormGroup = new FormGroup({}); no eventos.component.ts, e tambem usei no eventos.html
//para ser um form reativo .. nao posso esquecer de colocaqr aqui no imports.. o ReactiveFormsModule ai funcionara o formgroup

@NgModule({
  declarations: [
    AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe
   ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    //forRoot.. significa que quero poder utilizar em toda a estrutura de nossa aplicação, usar desde a raiz
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    EventoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
