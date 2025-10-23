import { Routes } from '@angular/router';
import { MainPageComponent } from './components/main-page/main-page.component';
import { ContactComponent } from './components/main-page/contact/contact.component';

export const routes: Routes = [
    {path:'', component: MainPageComponent},
    {path: 'kontakt', component: ContactComponent}
];
