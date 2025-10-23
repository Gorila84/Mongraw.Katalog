import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import { NavComponent } from "./core/nav/nav.component";
import { CardComponent } from "./shared/fields/components/card/card.component";
import { SvgIconService } from './shared/helpers/icon.provider';
import { HeaderBanerComponent } from "./core/header-baner/header-baner.component";
import { FooterComponent } from "./core/footer/footer.component";
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule, MatFormFieldModule, MatInputModule,MatIconModule, MatButtonModule, NavComponent, HeaderBanerComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Mongraw.SPA';

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      'facebook',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/facebook-logo.svg')
    );
  }

  redirectToFacebook(){
    window.open('https://www.facebook.com/MONGRAW', '_blank');
  }
}
