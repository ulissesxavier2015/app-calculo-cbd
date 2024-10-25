import { Component, Input } from '@angular/core';

@Component({
  selector: 'cdb-button',
  templateUrl: './cdb-button.component.html'
})
export class CDBButtonComponent {

  @Input() texto!: string;
}
