import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from 'src/app/util/Constants';

//Interessante.. quando voce for usar essa classe nova que CRIOU.. NÃO USE O NOME DA CLASSE!!!
//****VAI USAR O NOME DO @PIPE.. ->  name:DateTimeFormatPipe
@Pipe({
  name: 'DateTimeFormatPipe'
})
export class DateTimeFormatPipePipe extends DatePipe implements PipeTransform {

//Para gerar essa classe DateTimeFormatPipe... criei uma pasta "helps" depois cliquei com botao direito e "Generate Pipe"
//alterei da forma que já foi criado o return.. colocado o super.transform... e extendido o DatePipe.. dessa forma o super importa
// o DatePipe do angular/common
  transform(value: any, args?: any): any {
    return super.transform(value, Constants.DATE_TIME_FMT);
  }

}
