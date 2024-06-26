import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import { BooksService } from '../books.service';

@Component({
  selector: 'app-create-book',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './create-book.component.html',
  styleUrl: './create-book.component.scss'
})
export class CreateBookComponent {
  error: string = '';
  name: string = '';
  genre: string = '';
  authorName: string = '';

  constructor(private router: Router, private _userService: UserService, private _bookService: BooksService) {}

  onSubmit() {
    if (!this.name || !this.authorName || !this.genre) {
      this.error = "please write all in the fields";
      return;
    }
    this._bookService.createBook(this.name, this.genre, this.authorName)
      .subscribe(
        (response) => {
          this.error = 'Book added successfully!'
          this.name = '';
          this.genre = '';
          this.authorName = '';
        }, 
        (error) => {
          this.error = error.error;
        }
      )
  }
}
