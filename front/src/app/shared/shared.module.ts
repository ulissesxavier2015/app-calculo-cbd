import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CDBInputComponent } from './cdb-input/cdb-input.component';
import { CDBButtonComponent } from './cdb-button/cdb-button.component';

@NgModule({
  declarations: [
    CDBInputComponent,
    CDBButtonComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    CDBInputComponent,
    CDBButtonComponent
  ]
})
export class SharedModule { }
