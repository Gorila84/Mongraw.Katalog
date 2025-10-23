import { Injectable } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable({ providedIn: 'root' })
export class SvgIconService {
  constructor(private iconRegistry: MatIconRegistry, private sanitizer: DomSanitizer) {
    const icons = ['shopping_bag', 'receipt_long'];
    icons.forEach(icon => {
      console.log(`Registering icon: ${icon}`);
      this.iconRegistry.addSvgIcon(
        icon,
        this.sanitizer.bypassSecurityTrustResourceUrl(`assets/icons/${icon}.svg`)
      );
    });
  }
}
