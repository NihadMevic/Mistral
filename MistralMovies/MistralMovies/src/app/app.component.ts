import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public MovieData?: Array<Movie> = [];
  public loading?: boolean;
  private _http?: HttpClient;
  private CurrentTab?: string = "1";
  headerDict = {
    'Authorization': 'Basic ' + btoa(unescape(encodeURIComponent("BasicUser:BasicPw")))
  }
  requestOptions = {
    headers: new HttpHeaders(this.headerDict),
  };
  
  constructor(http: HttpClient) {
    this._http = http;
    this.loading = true;
    http.get<Movie[]>('/movie', this.requestOptions).subscribe(result => {
      this.loading = false;
      this.MovieData = result;
    }, error => {
      this.loading = false;
      alert("Something went wrong.");
      console.error(error)
    });
  }

  title = 'Mistral Movies';

  TabSwitch(tab: any) {
    if (this._http) {
      this.loading = true;
      this._http.get<Movie[]>('/movie/' + tab, this.requestOptions).subscribe(result => {
        if (tab == "1") {
          this.title = 'Mistral Movies';
        }
        else {
          this.title = 'Mistral TV Shows';
        }
        this.MovieData = result;
        this.CurrentTab = tab;
        this.loading = false;
      }, error => {
        this.loading = false;
        alert("Something went wrong.");
        console.error(error)
      });
    }
  }
  Search(query: any) {
    if (this._http) {
      this.loading = true;
      this._http.get<Movie[]>('/movie/' + this.CurrentTab + '/10/' + query, this.requestOptions).subscribe(result => {
        this.loading = false;
        this.MovieData = result;
      }, error => {
        this.loading = false;
        alert("Something went wrong.");
        console.error(error)
      });
    }
  }
  _RateMovie(rating: any) {
    if (this._http) {
      this.loading = true;
      const body = { rating: rating.Rating, MovieId: rating.MovieId };
      this._http.put<MovieRatingRequest>('/movie', body, this.requestOptions).subscribe(result => {
        this.loading = false;
      }, error => {
        this.loading = false;
        alert("Something went wrong.");
        console.error(error)
      });
    }
  }
  _LoadMore() {
    if (this._http) {
      this.loading = true;
      var count = 10;
      if (this.MovieData?.length) {
        count = this.MovieData?.length + 10;
      }
      this._http.get<Movie[]>('/movie/' + this.CurrentTab + "/" + count, this.requestOptions).subscribe(result => {
        this.MovieData = result;
        this.loading = false;
      }, error => {
        this.loading = false;
        alert("Something went wrong.");
        console.error(error)
      });
    }
  }
}

interface Movie {
  Title: string,
  ImgUrl: string,
  Description: string,
  ReleaseDate: Date,
  Cast: Actor[],
  Rating: number
}
interface Actor {
  Name: string
}

interface MovieRatingRequest {
  Rating: number,
  MovieId: number
}
