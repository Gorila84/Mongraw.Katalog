import { Component, Input } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import { StorkedButtonComponent } from '../storked-button/storked-button.component';
import { SlicePipe } from '@angular/common';
@Component({
  selector: 'app-product-card',
  imports: [MatCardModule, StorkedButtonComponent, SlicePipe],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent {
  @Input() title!: string;
  @Input() subtitle!: string;
  @Input() imageUrl!: string;
}
