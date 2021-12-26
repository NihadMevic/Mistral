import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from './toolbar.component';



@NgModule({
  exports: [
    ToolbarComponent
  ],
  declarations: [
    ToolbarComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ToolbarModule { }
