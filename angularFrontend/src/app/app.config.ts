import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { UserService } from './user.service';
import { HttpClientModule } from '@angular/common/http';
import { BooksService } from './books.service';

export const appConfig: ApplicationConfig = {
  providers: 
  [
    provideRouter(routes), 
    UserService, 
    BooksService,
    importProvidersFrom(HttpClientModule)
  ]
};
