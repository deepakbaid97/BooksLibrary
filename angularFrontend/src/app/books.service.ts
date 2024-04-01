import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  private _url = 'http://localhost:5207';
  constructor(private http: HttpClient, private _userService: UserService) { }

  getBooks(): Observable<any> {
    const headers = new HttpHeaders ({
      "Authorization": this._userService.getUserDetails().sessionToken
    });

    return this.http.get(this._url + '/books', {headers});
  };

  lendBook(id: string): Observable<any> {
    console.log(this._userService.getUserDetails().sessionToken, 'user');
    const headers = new HttpHeaders ({
      'Authorization': this._userService.getUserDetails().sessionToken
    });

    return this.http.put(this._url + `/books/takebook/${id}`,{} ,{headers})
  }

  returnBook(id: string): Observable<any> {
    const headers = new HttpHeaders ({
      "Authorization": this._userService.getUserDetails().sessionToken
    });

    return this.http.put(this._url + `/books/returnBook/${id}`,{} ,{headers});
  }

  createBook (Name: string, Genre: string, AuthorName: string): Observable<any> {
    const data = {Name, Genre, AuthorName};

    const headers = new HttpHeaders ({
      "Authorization": this._userService.getUserDetails().sessionToken
    });

    return this.http.post(this._url + '/books/create', data, {headers});
  }
  
}
