import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  @Input() Movie: any;
  @Output() rateMovie: EventEmitter<any> = new EventEmitter();


  constructor() { }

  ngOnInit(): void {
  }


  Vote(rating: any) {
    this.rateMovie.emit({MovieId: this.Movie.movieID, Rating: rating})
  }
}
