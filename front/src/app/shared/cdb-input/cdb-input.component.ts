import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'cdb-input',
  templateUrl: './cdb-input.component.html'
})
export class CDBInputComponent {
  
  @Input() id!: string;
  @Input() placeholder!: string;
  @Input() label!: string;

  @Output() valorFormatado = new EventEmitter();
  @Output() valor = new EventEmitter<string>();

  formatarValorReal(event: Event) {
    this.valorFormatado.emit(event);
  }

  enviaAporte(event: EventTarget | null) {
    const input = event as HTMLInputElement;
    this.valor.emit(input.value);
  }
}
