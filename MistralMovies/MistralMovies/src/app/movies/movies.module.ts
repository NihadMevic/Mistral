import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MoviesComponent } from './movies.component';
import { MovieModule } from '../movie/movie.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    MoviesComponent
  ],
  imports: [
    CommonModule,
    MovieModule,
    FormsModule
  ],
  exports: [
    MoviesComponent
  ]
})
export class MoviesModule { }
