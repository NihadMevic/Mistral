import { Component, Input, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  @Input() Movies: any;
  @Output() Movie: any;
  @Output() SearchQuery: EventEmitter<any> = new EventEmitter();
  @Output() RateMovie: EventEmitter<any> = new EventEmitter();
  @Output() LoadMore: EventEmitter<any> = new EventEmitter();
  txtQueryChanged: Subject<string> = new Subject<string>();
  query = "";

  constructor() {
    this.txtQueryChanged.pipe(
      debounceTime(1000)
    ).subscribe(searchTextValue => {
      this.Search(searchTextValue);
    });
  }
  onKeyUp(searchTextValue: any) {
    this.txtQueryChanged.next(searchTextValue);
  }
  ngOnInit(): void {
  }
  Search(_query: any) {
    if ((_query && _query.trim() != "" && _query.trim().length > 2)) {
      this.SearchQuery.emit(_query);
    }
    else {
      this.SearchQuery.emit("");
    }
  }
  _rateMovie(rating: any) {
    this.RateMovie.emit(rating);
  }
  LoadMoreClick() {
    this.LoadMore.emit();
  }
}
