import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponentComponent } from './page-not-found-component/page-not-found-component.component';
import { BooksComponent } from './books/books.component';
import { ProfileComponent } from './profile/profile.component';
import { YourBooksComponent } from './your-books/your-books.component';
import { CreateBookComponent } from './create-book/create-book.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: SignupComponent },
    { 
        path: '', 
        component: HomeComponent,
        children: [
            { path: 'books', component: BooksComponent},
            { path: 'profile', component: ProfileComponent},
            { path: 'yourBooks', component: YourBooksComponent},
            { path: 'createBook', component: CreateBookComponent}
        ]
    },
    { path: '**', component: PageNotFoundComponentComponent}
  ];
