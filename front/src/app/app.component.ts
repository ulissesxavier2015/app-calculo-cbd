import { Component } from '@angular/core';
import { CalcularCdbService } from './services/calcular-cdb.service';
import { ResultadoCalculoCdbDto } from './models/resultado-calculo-cdb-dto.model';
import { CalcularCdb } from './models/calcular-cdb.model'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  valorAporteInicial: number = 0.0;
  quantidadeMeses: number = 0;

  valorReal: string = '';
  prazoFormatado: string = '';
  erroPrazo: boolean = false;

  resultadoCalculo!: ResultadoCalculoCdbDto;
  formatador: Intl.NumberFormat;

  constructor(private servico: CalcularCdbService) {
    this.formatador = new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
      
    })
  }

  formataValorAporte(input: InputEvent) {
    if (input.data) {
      this.valorReal += input.data?.replace(/\D/g, '');
      const formattedAmount = this.formatador.format(Number(this.valorReal) / 100);
      (input.target as HTMLInputElement).value = formattedAmount;
      return;
    }

    this.valorReal = '';
    (input.target as HTMLInputElement).value = this.valorReal;
  }

  formataQuantidadeDeMeses(input: InputEvent) {
    if (input.data) {
      this.prazoFormatado += input.data?.replace(/\D/g, '');
    } else {
      this.prazoFormatado = '';
    }

    (input.target as HTMLInputElement).value = this.prazoFormatado;
  }

  enviaAporteInicial(valor: string) {
    const valorNumerico = valor
      .replace('R$', '')
      .replace('.', '')
      .replace(',', '.');
    this.valorAporteInicial = parseFloat(valorNumerico);
  }

  enviaPrazo(valor: string) {
    this.quantidadeMeses = parseInt(valor);
    this.erroPrazo = this.quantidadeMeses <= 1;
  }

  simular() {
    if (this.erroPrazo) {
      return;
    }

    const calcularCdb: CalcularCdb = {
      valorAporteInicial: this.valorAporteInicial,
      quantidadeMeses: this.quantidadeMeses
    };

    this.servico.calcularCdb(calcularCdb)
      .subscribe((resposta: ResultadoCalculoCdbDto) => this.resultadoCalculo = resposta);
  }
}
