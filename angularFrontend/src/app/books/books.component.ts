import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BooksService } from '../books.service';
import { BookDetail } from '../Books';
import { CommonModule } from '@angular/common';
import { UserService } from '../user.service';
import { UserDetails } from '../User';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './books.component.html',
  styleUrl: './books.component.scss'
})
export class BooksComponent {
  error: string = '';
  books: BookDetail[] = [];
  userDetail: UserDetails;
  constructor (private router: Router, private _booksService: BooksService, private _userService: UserService) { 
    this.userDetail = this._userService.getUserDetails();
  }
  
  ngOnInit() {
    this._booksService.getBooks()
      .subscribe(
        (response) => {
          console.log(response, "BOOKS RESPO");
          this.books = response;
        },
        (error) => {
          this.error = JSON.stringify(error.error);
        }
      )
  }

  lendBook(id: string) {
    this._booksService.lendBook(id)
      .subscribe(
        (response) => {
          console.log(response);
          let resBook = this.books.find(item => item.id == id);
          if (resBook)
            resBook.userID = this._userService.getUserDetails().email;
        },
        (error)=> {
          console.log(error);
          this.error = error.error;
        }
      );
  }

  returnBook(id: string) {
    this._booksService.returnBook(id)
      .subscribe(
        (response) => {
          console.log(response);
          let resBook = this.books.find(item => item.id == id);
          if (resBook)
            resBook.userID = '';
        },
        (error) => {
          this.error = error.error;
        }
      );
  }
}
